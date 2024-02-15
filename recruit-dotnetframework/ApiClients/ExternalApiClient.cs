using recruit_dotnetframework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace recruit_dotnetframework.ApiClients
{
    public interface IExternalApiClient
    {
        Task<ValidationResponse> ValidateCreditCardInformation(CreditCard creditCard);
    }

    public class ExternalApiClient: IExternalApiClient
    {
        private readonly HttpClient _httpClient;
        public ExternalApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<ValidationResponse> ValidateCreditCardInformation(CreditCard creditCard)
        {
            await Task.Delay(100);

            if (creditCard.CardNumber.Length != 16 || !Regex.IsMatch(creditCard.CardNumber, @"^\d{16}$"))
            {
                throw new ArgumentException("Invalid card number");
  
            }

            if (creditCard.CVC.Length != 3 || !Regex.IsMatch(creditCard.CVC, @"^\d{3}$"))
            {
                throw new ArgumentException("Invalid CVC format");
            }


            if (creditCard.ExpiryMonth > 12 || creditCard.ExpiryMonth < 1)
            {
                throw new ArgumentException("Invalid month");
            }

            DateTime now = DateTime.Now;
            int currentMonth = now.Month;
            int currentYear = now.Year % 100;
            if (creditCard.ExpiryYear < currentYear || (creditCard.ExpiryYear == currentYear && creditCard.ExpiryMonth < currentMonth))
            {
                throw new ArgumentException("Invalid date");
            }

            return new ValidationResponse
            {
                IsValid = true,
                Message = "Card validated successfully",
            };
        }
    }
}