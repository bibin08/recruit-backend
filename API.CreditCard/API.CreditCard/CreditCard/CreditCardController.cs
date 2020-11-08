using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.CreditCard.CreditCard.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.CreditCard.CreditCard
{
    [Route("")]
    [ApiController]
    //TODO: IdentityJwt implementation to verify the identity of the caller.
    //TODO:AuthorizationFilter could be added based on roles.
    public class CreditCardController : ControllerBase
    {
        private ICreditCardService _creditCardService { get; set; }
        public CreditCardController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        /// <summary>
        /// Returns the list of all credit cards stored.
        /// </summary>
        /// <returns>List of Credit Cards</returns>
        [HttpGet]
        [Route("~/v1/Cards")]
        public async Task<IEnumerable<CreditCardViewModel>> GetAll()
        {
            return await _creditCardService.GetAll();
        }

        /// <summary>
        /// Get the credit card detail based on the name and card number
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cardNumber"></param>
        /// <returns>Credit Card Details</returns>
        [HttpGet]
        [Route("~/v1/Cards/{name}/{cardNumber}")]
        public async Task<CreditCardViewModel> Get(string name = null, string cardNumber = null)
        {
            //TODO Validate Name and Credit Card which came through.
            return await _creditCardService.GetByQuery(name, cardNumber);
        }

       /// <summary>
       /// Save the Credit Card Details entered.
       /// </summary>
       /// <param name="cardDetails"></param>
       /// <returns>Credit Card Id</returns>
        [HttpPost]
        [Route("~/v1/Cards")]
        public async Task<Guid> Save([FromBody] CreditCardViewModel cardDetails)
        {
            return await _creditCardService.Insert(cardDetails);
        }
    }
}
