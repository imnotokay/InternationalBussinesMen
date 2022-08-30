using InternationalBusinessMen.ApplicationCore.Interfaces;
using InternationalBusinessMen.ApplicationCore.Providers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InternationalBusinessMen.Extensions
{
    public static class DinamycRegistrationExtension
    {
        public static IServiceCollection AddDinamycsServices<T>(this IServiceCollection services) where T: class
        {
            services.AddSingleton<IDownloadJsonService<T>, DownloadJsonProvider<T>>();
            services.AddSingleton<IStorageJsonLocal<T>, StorageJsonLocalProvider<T>>();

            return services;
        }
    }
}
