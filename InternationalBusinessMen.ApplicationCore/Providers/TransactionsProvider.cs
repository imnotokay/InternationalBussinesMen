using System;
using System.Linq;
using System.Collections.Generic;
using InternationalBusinessMen.ApplicationCore.Interfaces;
using InternationalBusinessMen.Domain.Domains;
using InternationalBusinessMen.Domain.Exceptions;
using InternationalBusinessMen.ApplicationCore.Extensions;

namespace InternationalBusinessMen.ApplicationCore.Providers
{

    public class TransactionsProvider : ITransactionsService
    {
        private readonly ILoggerService _logger;
        private readonly IDownloadJsonService<Rates> _downloadServiceRates;
        private readonly IStorageJsonLocal<Rates> _storageJsonLocalRates;
        private readonly IDownloadJsonService<Transactions> _downloadServiceTransactions;
        private readonly IStorageJsonLocal<Transactions> _storageJsonLocalTransactions;
        public TransactionsProvider(ILoggerService logger, IDownloadJsonService<Rates> downloadServiceRates, IStorageJsonLocal<Rates> storageJsonLocalRates, IDownloadJsonService<Transactions> downloadServiceTransactions, IStorageJsonLocal<Transactions> storageJsonLocalTransactions)
        {
            this._logger = logger;
            this._downloadServiceRates = downloadServiceRates;
            this._storageJsonLocalRates = storageJsonLocalRates;
            this._downloadServiceTransactions = downloadServiceTransactions;
            this._storageJsonLocalTransactions = storageJsonLocalTransactions;
        }


        /// <summary>
        /// Obtiene las transacciones desde el servidor, en caso de no encotrar conexión con el servidor obtendrá la información del archivo local
        /// </summary>
        /// <returns>Colección de transacciones</returns>
        public ICollection<Transactions> getAllTransactions(Boolean needStorage = true)
        {
            try
            {
                ICollection<Transactions> transactions = this.getTransactions(needStorage);

                if (transactions == null)
                {
                    throw new WarningException("No se encontró información relacionada con transacciones");
                }

                return transactions;
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
        /// Obtiene las calificaciones desde el servidor, en caso de no encotrar conexión con el servidor obtendrá la información del archivo local
        /// </summary>
        /// <returns>Colección de calificaciones</returns>
        public ICollection<Rates> getAllRates(Boolean needStorage = true)
        {
            try
            {
                ICollection<Rates> rates = this.getRates(needStorage);

                if (rates == null)
                {
                    throw new WarningException("No se encontró información relacionada con calificaciones");
                }

                return rates;
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
        /// Obtiene las transacciones y su total filtrandolas por el SKU del producto
        /// </summary>
        /// <param name="sku">Código único de identificación del producto</param>
        /// <returns>Objeto con las transacciones y el total de las mismas en euros</returns>
        public TransactionsBySku getTransactionsBySku(string sku)
        {
            try
            {
                ICollection<Transactions> transactions = this.getAllTransactions(false).Where(x => x.sku == sku).ToList();
                ICollection<Rates> rates = this.getRates(false);

                TransactionsBySku output = new TransactionsBySku();

                Rates currentRate = default(Rates);
                decimal tmpAmount;
                foreach(Transactions transaction in transactions)
                {
                    if(transaction.currency.ToUpper().Trim() != "EUR")
                    {
                        currentRate = rates.Where(x => x.from.ToUpper().Trim() == transaction.currency.ToUpper().Trim() && x.to.ToUpper().Trim() == "EUR").FirstOrDefault();
                        if(currentRate == default(Rates))
                        {
                            tmpAmount = transaction.amount;
                            calculateRateValue(rates, "EUR", transaction.currency, ref tmpAmount);
                            transaction.amount = tmpAmount.BankersRounding();
                        }
                        else
                        {
                            transaction.amount = (transaction.amount * currentRate.rate).BankersRounding();
                        }
                    }
                    else
                    {
                        transaction.amount = transaction.amount.BankersRounding();
                    }
                }

                output = new TransactionsBySku
                {
                    Transactions = transactions,
                    total = transactions.Sum(x => x.amount).BankersRounding()
                };

                return output;
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

        private void calculateRateValue(ICollection<Rates> rates, string currencyTo, string currencyFrom, ref decimal value)
        {
            List<Rates> filteredRates = rates.Where(x => x.from == currencyFrom).ToList();
            Rates finalRate = default(Rates);
            if (filteredRates.Count > 0)
            {
                foreach(Rates rate in filteredRates)
                {
                    finalRate = rates.Where(x => x.from == rate.to && x.to == currencyTo).FirstOrDefault();
                    if(finalRate != default(Rates))
                    {
                        value = value * rate.rate;
                        value = value * finalRate.rate;
                    }
                }
            }
            else
            {
                throw new WarningException("No se encontró información para realizar la conversión de moneda");
            }
        }

        private ICollection<Transactions> getTransactions(Boolean needStorage = true)
        {
            ICollection<Transactions> transactions;
            try
            {
                if (needStorage)
                {
                    transactions = this._downloadServiceTransactions.getJsonData().Result;
                    this._storageJsonLocalTransactions.saveLocalInformation(transactions);
                }
                else
                {
                    transactions = this._storageJsonLocalTransactions.readLocalInformation();
                }
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
                this._logger.LogError(exception);
                transactions = this._storageJsonLocalTransactions.readLocalInformation();
            }


            return transactions;
        }

        private ICollection<Rates> getRates(Boolean needStorage = true)
        {
            ICollection<Rates> rates;
            try
            {
                if (needStorage)
                {
                    rates = this._downloadServiceRates.getJsonData().Result;
                    this._storageJsonLocalRates.saveLocalInformation(rates);
                }
                else
                {
                    rates = this._storageJsonLocalRates.readLocalInformation();
                }
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
                this._logger.LogError(exception);

                rates = this._storageJsonLocalRates.readLocalInformation();
            }

            return rates;
        }

        public ICollection<Transactions> getAllTransactions()
        {
            throw new NotImplementedException();
        }

        public ICollection<Rates> getAllRates()
        {
            throw new NotImplementedException();
        }
    }
}
