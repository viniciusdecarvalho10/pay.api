using System;
namespace Pay.Api.Core.Entities
{
    /// <summary>
    /// A estrutura de dados usada para identificar
    /// por completo um usuário dentro do sistema.
    /// </summary>
    public class PayIdentity : IPayIdentity
    {
        public Guid SubscriptionId { get; set; }
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
    }
}
