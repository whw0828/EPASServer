using Chloe.Entity;
using Chloe.Infrastructure.Interception;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chloe.Infrastructure
{
    public static class DbConfiguration
    {
        /// <summary>
        /// Fluent Mapping
        /// </summary>
        /// <param name="entityTypeBuilders"></param>
        public static void UseTypeBuilders(params IEntityTypeBuilder[] entityTypeBuilders)
        {
            EntityTypeContainer.UseBuilders(entityTypeBuilders);
        }
        /// <summary>
        /// Fluent Mapping
        /// </summary>
        /// <param name="entityTypeBuilderTypes"></param>
        public static void UseTypeBuilders(params Type[] entityTypeBuilderTypes)
        {
            EntityTypeContainer.UseBuilders(entityTypeBuilderTypes);
        }
        public static void UseEntities(params TypeDefinition[] entityTypeDefinitions)
        {
            EntityTypeContainer.Configure(entityTypeDefinitions);
        }

        public static void UseInterceptors(params IDbCommandInterceptor[] interceptors)
        {
            if (interceptors == null)
                return;

            foreach (var interceptor in interceptors)
            {
                if (interceptor == null)
                    continue;

                DbInterception.Add(interceptor);
            }
        }

        public static void UseMappingType(IMappingType mappingType)
        {
            MappingTypeSystem.Configure(mappingType);
        }
    }
}
