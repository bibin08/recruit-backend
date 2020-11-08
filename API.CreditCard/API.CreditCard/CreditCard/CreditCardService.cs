using API.CreditCard.CreditCard.Models;
using API.CreditCard.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.CreditCard.CreditCard
{
    public interface ICreditCardService
    {
        public Task<IEnumerable<CreditCardViewModel>> GetAll();
        public Task<CreditCardViewModel> GetByQuery(string name, string creditCardNumber);
        public Task<Guid> Insert(CreditCardViewModel creditCardDto);
    }

    public class CreditCardService : ICreditCardService
    {
        private ICreditCardRepository _creditCardRepository { get; set; }
        private ITokenisationService _tokenisationService { get; set; }
        public CreditCardService(ICreditCardRepository creditCardRepository,
            ITokenisationService tokenisationService)
        {
            _creditCardRepository = creditCardRepository;
            _tokenisationService = tokenisationService;
        }

        public async Task<IEnumerable<CreditCardViewModel>> GetAll()
        {
            var creditCardDetails = (await _creditCardRepository.GetAll()).ToList().Select(cc => ConvertToCreditCardViewModel(cc));
            return creditCardDetails;
        }

        public async Task<CreditCardViewModel> GetByQuery(string name, string creditCardNumber)
        {
            var creditCard = (await _creditCardRepository.GetByQuery(_tokenisationService.Encrypt(name), _tokenisationService.Encrypt(creditCardNumber)));
            return ConvertToCreditCardViewModel(creditCard);
        }

        public async Task<Guid> Insert(CreditCardViewModel creditCardViewModel)
        {
            if (creditCardViewModel == null)
                return Guid.Empty;

            var creditCardDto = new CreditCardDto()
            {
                Name = _tokenisationService.Encrypt(creditCardViewModel.Name),
                CardNumber = _tokenisationService.Encrypt(creditCardViewModel.CardNumber),
                CVC = _tokenisationService.Encrypt(creditCardViewModel.CVC.ToString()),
                ExpiryDate = creditCardViewModel.ExpiryDate
            };

            return await _creditCardRepository.Insert(creditCardDto);
        }

        /// <summary>
        /// Mapper from Dto to ViewModel with decryption.
        /// </summary>
        private CreditCardViewModel ConvertToCreditCardViewModel(CreditCardDto creditCardDto)
        {
            if (creditCardDto == null)
                return null;

            var creditCardViewModel = new CreditCardViewModel()
            {
                Name = _tokenisationService.Decrypt(creditCardDto.Name),
                CardNumber = _tokenisationService.Decrypt(creditCardDto.CardNumber),
                CVC = Int32.Parse(_tokenisationService.Decrypt(creditCardDto.CVC)),
                ExpiryDate = creditCardDto.ExpiryDate
            };

            return creditCardViewModel;
        }
    }
}
