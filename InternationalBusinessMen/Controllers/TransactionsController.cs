using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using InternationalBusinessMen.Domain.Domains;
using InternationalBusinessMen.Domain.Exceptions;
using InternationalBusinessMen.Services.Interfaces;

namespace InternationalBusinessMen.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IInternationalBusinessService _internationalBusinessService;
        public TransactionsController(IInternationalBusinessService internationalBusinessService)
        {
            this._internationalBusinessService = internationalBusinessService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            return Ok("Bienvenido!");
        }

        [HttpGet]
        [Route("getAllTransactions")]
        public ICollection<Transactions> getAllTransactions()
        {
            try
            {
                return this._internationalBusinessService.getAllTransactions();
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

        [HttpGet]
        [Route("getAllRates")]
        public ICollection<Rates> getAllRates()
        {
            try
            {
                return this._internationalBusinessService.getAllRates();
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

        [HttpGet]
        [Route("getTransactionsBySku/{sku}")]
        public TransactionsBySku getTransactionsBySku(string sku)
        {
            try
            {
                return this._internationalBusinessService.getTransactionsBySku(sku);
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
