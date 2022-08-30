using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using InternationalBusinessMen.Domain.Domains;
using InternationalBusinessMen.Domain.Exceptions;
using InternationalBusinessMen.ApplicationCore.Interfaces;

namespace InternationalBusinessMen.ApplicationCore.Providers
{
    public class DownloadJsonProvider<T> : IDownloadJsonService<T>
    {
        private readonly IConfiguration _configuration;
        public DownloadJsonProvider(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        /// <summary>
        /// Obtiene la información de las calificaciones desde el servidor
        /// </summary>
        /// <returns>Collección con la información de las calificaciones</returns>
        public async Task<ICollection<T>> getJsonData()
        {
            try
            {
                ICollection<T> output = default(ICollection<T>);
                string jsonUrl = this._configuration.GetSection("JsonService").GetSection(getSectionByType(typeof(T))).Value;
                if (string.IsNullOrEmpty(jsonUrl))
                {
                    throw new ErrorException("No se encontró información relacionada a la url de descarga del json con la información");
                }

                using (HttpClient httpClient = new HttpClient())
                {
                    using (var response = await httpClient.GetAsync(jsonUrl))
                    {
                        string responseResult = await response.Content.ReadAsStringAsync();
                        output = JsonConvert.DeserializeObject<ICollection<T>>(responseResult);
                    }
                }

                return output;
            }catch(ErrorException errorException)
            {
                throw errorException;
            }
            catch(WarningException warningException)
            {
                throw warningException;
            }
            catch(Exception exception)
            {
                throw exception;
            }
        }

        public string getSectionByType(Type currentType)
        {
            string output = string.Empty;
            if(currentType == typeof(Transactions))
            {
                output = "transactionsUrl";
            }
            else if (currentType == typeof(Rates))
            {
                output = "ratesUrl";
            }
            return output;
        }
    }
}
