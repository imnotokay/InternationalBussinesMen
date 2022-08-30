using System;
using System.IO;
using InternationalBusinessMen.ApplicationCore.Interfaces;
using System.Collections.Generic;
using InternationalBusinessMen.Domain.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using InternationalBusinessMen.Domain.Interfaces;
using InternationalBusinessMen.Domain.Domains;

namespace InternationalBusinessMen.ApplicationCore.Providers
{
    public class StorageJsonLocalProvider<T>: IStorageJsonLocal<T>
    {
        private readonly string _rootFolder;

        public StorageJsonLocalProvider(IHostingEnvironment hostingEnvironments)
        {
            this._rootFolder = hostingEnvironments.ContentRootPath;
        }

        /// <summary>
        /// Obtiene la información de un almacenada de manera local para el json de transacciones
        /// </summary>
        /// <returns>Colección de tipo personalizado con la información de las transacciones almacenadas localmente</returns>
        public ICollection<T> readLocalInformation()
        {
            try
            {
                ICollection<T> output = null;
                KeyValuePair<string,Boolean> folderPath = this.getStoragePath(typeof(T));
                if (folderPath.Value)
                {
                    using (StreamReader sr = new StreamReader(folderPath.Key))
                    {
                        output = JsonConvert.DeserializeObject<ICollection<T>>(sr.ReadToEnd());
                    }
                }

                return output;
            }
            catch(ErrorException errorException)
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

        /// <summary>
        /// Almacena la información de las transacciones en un json local
        /// </summary>
        /// <param name="data">Información a almacenar en el json local</param>
        public void saveLocalInformation(ICollection<T> data)
        {
            try
            {
                string dataToStorage = JsonConvert.SerializeObject(data);
                KeyValuePair<string, Boolean> folderPath = this.getStoragePath(typeof(T));
                using (StreamWriter sr = new StreamWriter(folderPath.Key))
                {
                    sr.Write(dataToStorage);
                }
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                throw new WarningException(fileNotFoundException.Message, fileNotFoundException.InnerException);
            }
            catch (UnauthorizedAccessException unauthorizedAccessException)
            {
                throw new ErrorException(unauthorizedAccessException.Message, unauthorizedAccessException.InnerException);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Obtiene la información del la carpeta local de almacenamiento
        /// </summary>
        /// <returns>Información de la cadena de la carpeta y si el archivo existe o no</returns>
        private KeyValuePair<string, Boolean> getStoragePath(Type currentType)
        { 
            string outputPath = Path.Combine(this._rootFolder, "JsonLocalStorage");
            KeyValuePair<string, Boolean> output;
            if (!Directory.Exists(outputPath))
            {
                Directory.CreateDirectory(outputPath);
            }
            if(currentType == typeof(Transactions))
            {
                outputPath = Path.Combine(outputPath, "JsonTransactions.json");
            }else if (currentType == typeof(Rates))
            {
                outputPath = Path.Combine(outputPath, "JsonRates.json");
            }
            output = new KeyValuePair<string, Boolean>(outputPath, File.Exists(outputPath));

            return output;
        }
    }
}
