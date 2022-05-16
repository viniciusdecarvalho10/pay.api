using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Pay.Api.Core.Extensions;
using Pay.Api.Domain.Constants;
using Pay.Api.Domain.Dto;
using Pay.Api.Domain.Entities;
using Pay.Api.Domain.Interface.Services;
using Pay.Api.Domain.Models.Oauth.Request;
using Pay.Api.Domain.Models.Oauth.Response;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Pay.Api.Service.OauthService
{
    public class OauthService : IOauthService
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IUserSubscriptionService _userSubscriptionService;
        private readonly IUserService _userService;
        private readonly ICreateJwtService _createJwtService;
        private readonly IDataCacheService _cache;

        public OauthService(
            IUserService userService, 
            ISubscriptionService subscriptionService, 
            IUserSubscriptionService userSubscriptionService,
            ICreateJwtService createJwtService,
            IDataCacheService cache
        )
        {
            _userService = userService;
            _subscriptionService = subscriptionService;
            _userSubscriptionService = userSubscriptionService;
            _createJwtService = createJwtService;
            _cache = cache;
        }
        public async Task<PostOauthSignUpResponse> SignUp(PostOauthSignUpRequest model)
        {
            try
            {
                await this.Validate(model);

                var user = (await _userService.FindByAsync(u => u.Email == model.Email)).FirstOrDefault();

                if (user != null)
                    throw new ValidationException("Este usuário já existe");

                user = await _userService.AddAsync(new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Phone = model.Phone
                });

                var subscription = await _subscriptionService.AddAsync(new Subscription
                {
                    Name = user.Name,
                    Email = user.Email
                });

                var userSubscription = await _userSubscriptionService.AddAsync(new UserSubscription
                {
                    SubscriptionId = subscription.Id,
                    UserId = user.Id
                });

                return new PostOauthSignUpResponse{ Name = user.Name, Email = user.Email, Phone = user.Phone };
            }
            catch (Exception ex)
            {
                throw new ValidationException(ex.Message);
            }
        }

        public async Task<OauthTokenResponse> SignIn(PostOauthSignInRequest model)
        {
            try
            {
                var user = (await _userService.FindByAsync(u => u.Email == model.User && u.Password == model.Password)).FirstOrDefault();

                await SignInValidate(model, user);

                var userSubscription = await _userSubscriptionService.GetUserSubscriptionByUserId(user.Id);
                
                var handler = new JwtSecurityTokenHandler();

                var now = DateTime.UtcNow;
                var accessToken = await GenerateAccessToken(user, now);
                var refreshToken = await GenerateRefreshToken(user, now);
                var jwtRefreshToken = handler.WriteToken(refreshToken);

                var expirationDate = refreshToken.ValidTo;
                var cacheExpires = expirationDate.Subtract(DateTime.UtcNow);

                await _cache.SetAsync<Guid>(jwtRefreshToken, user.Id, cacheExpires);

                var oauthTokenResponse = new OauthTokenResponse
                {
                    AccessToken = handler.WriteToken(accessToken),
                    ExpiresIn = Convert.ToInt64(accessToken.ValidTo.TimeOfDay.TotalSeconds),
                    IsEmailVerified = user.EmailVerified,
                    RefreshToken = jwtRefreshToken,
                    RefreshTokenExpiresIn = Convert.ToInt64(refreshToken.ValidTo.TimeOfDay.TotalSeconds),
                    TokenType = "Bearer"
                };

                return oauthTokenResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<OauthTokenResponse> RefreshToken(PostOauthRefreshTokenRequest refreshTokenRequest)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var refreshToken = handler.ReadJwtToken(refreshTokenRequest.RefreshToken);

                if (refreshToken.ValidTo <= DateTime.UtcNow)
                    throw new HttpStatusException(HttpStatusCode.Unauthorized, "Refresh Token expired.");

                var userId = await _cache.GetAsync<Guid?>(refreshTokenRequest.RefreshToken);
                if (userId == null)
                    throw new ValidationException("Refresh token invalid.");

                var user = await _userService.GetByIdAsync(userId.Value);

                var now = DateTime.UtcNow;
                var accessToken = await GenerateAccessToken(user, now);


                var oauthTokenResponse = new OauthTokenResponse
                {
                    AccessToken = handler.WriteToken(accessToken),
                    ExpiresIn = Convert.ToInt64(accessToken.ValidTo.TimeOfDay.TotalSeconds),
                    IsEmailVerified = user.EmailVerified,
                    RefreshToken = refreshTokenRequest.RefreshToken,
                    RefreshTokenExpiresIn = Convert.ToInt64(refreshToken.ValidTo.TimeOfDay.TotalSeconds),
                    TokenType = "Bearer"
                };

                return oauthTokenResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<SecurityToken> GenerateAccessToken(User user, DateTime now)
        {
            var tokenExpirationDate = now.AddMinutes(Convert.ToInt32(Environment.GetEnvironmentVariable("JWT_EXPIRATION_IN_MINUTES")));
            var tokenParams = new Dictionary<string, string>();
            tokenParams.Add("UserId", user.Id.ToString());
            tokenParams.Add("UserEmail", user.Email);

            var userSubscription = await _userSubscriptionService.GetUserSubscriptionByUserId(user.Id);
            if (userSubscription != null)
            {
                tokenParams.Add("SubscriptionId", $"{userSubscription.SubscriptionId}");
                var subscription = await _subscriptionService.GetByIdAsync(userSubscription.SubscriptionId);

                var jwthandler = new JwtSecurityTokenHandler();
            }

            var tokenDto = new CreateJwtRequestDto
            {
                UserId = user.Id,
                Username = user.Email,
                ExpirationDate = tokenExpirationDate,
                CreatedDate = now,
                CustomClaims = tokenParams
            };

            return await _createJwtService.Execute(tokenDto);
        }

        private Task<SecurityToken> GenerateRefreshToken(User user, DateTime now)
        {

            var refreshTokenExpirationDate = now.AddMinutes(Convert.ToInt32(Environment.GetEnvironmentVariable("REFRESH_TOKEN_EXPIRES_IN_MINUTES")));
            var refreshTokenParams = new Dictionary<string, string>();
            refreshTokenParams.Add("IsRefreshToken", "Yes");

            var tokenDto = new CreateJwtRequestDto
            {
                UserId = user.Id,
                Username = user.Email,
                ExpirationDate = refreshTokenExpirationDate,
                CreatedDate = now,
                CustomClaims = refreshTokenParams
            };

            return _createJwtService.Execute(tokenDto);
        }

        private async Task SignInValidate(PostOauthSignInRequest signInRequest, User user)
        {
            if (user == null)
                throw new HttpStatusException(HttpStatusCode.Unauthorized, "User or password are invalid.");
        }

        private async Task<bool> Validate(PostOauthSignUpRequest model)
        {
            if (string.IsNullOrEmpty(model.Password))
                throw new ValidationException("Password is require.");

            if (string.IsNullOrEmpty(model.ConfirmePassword))
                throw new ValidationException("Confirmação de senha is require.");

            if (model.Password != model.ConfirmePassword)
                throw new ValidationException("Password and confirmation are different.");

            if (string.IsNullOrEmpty(model.Email))
                throw new ValidationException("E-mail is require.");

            return await Task.FromResult(true);
        }
    }
}