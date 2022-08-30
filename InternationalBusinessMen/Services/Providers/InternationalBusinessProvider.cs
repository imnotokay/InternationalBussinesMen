using System;
using System.Collections.Generic;
using InternationalBusinessMen.ApplicationCore;
using InternationalBusinessMen.Domain.Domains;
using InternationalBusinessMen.Domain.Exceptions;
using InternationalBusinessMen.Domain.Interfaces;
using InternationalBusinessMen.Services.Interfaces;

namespace InternationalBusinessMen.Services.Providers
{
    public class InternationalBusinessProvider : IInternationalBusinessService
    {
        private readonly ITransactionsService _transactionsService;
        public InternationalBusinessProvider(ITransactionsService transactionsService)
        {
            this._transactionsService = transactionsService;
        }

        /// <summary>
        /// Obtiene las calificaciones desde el servidor, en caso de no encotrar conexión con el servidor obtendrá la información del archivo local
        /// </summary>
        /// <returns>Colección de calificaciones</returns>
        public ICollection<Rates> getAllRates()
        {
            try
            {
                return this._transactionsService.getAllRates();
            }
            catch (ErrorException errorException)
            {
                throw errorException;
            }
            catch (WarningException warningException)
            {
                throw warningException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// Obtiene las transacciones desde el servidor, en caso de no encotrar conexión con el servidor obtendrá la información del archivo local
        /// </summary>
        /// <returns>Colección de transacciones</returns>
        public ICollection<Transactions> getAllTransactions()
        {
            try
            {
                return this._transactionsService.getAllTransactions();
            }
            catch (ErrorException errorException)
            {
                throw errorException;
            }
            catch (WarningException warningException)
            {
                throw warningException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public TransactionsBySku getTransactionsBySku(string sku)
        {
            try
            {
                return this._transactionsService.getTransactionsBySku(sku);
            }
            catch (ErrorException errorException)
            {
                throw errorException;
            }
            catch (WarningException warningException)
            {
                throw warningException;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
