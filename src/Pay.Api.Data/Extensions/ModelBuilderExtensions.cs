using System;
using System.Collections.Generic;
using Pay.Api.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Pay.Api.Data.Extensions
{
    public static partial class ModelBuilderExtensions
    {
        private static List<ISeed> seed = new List<ISeed>();

        public static void RegisterSeed<T>() where T : ISeed
        {
            seed.Add(Activator.CreateInstance<T>());
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            seed.ForEach(s =>
            {
                s.Seed(modelBuilder);
            });
        }
    }
}
