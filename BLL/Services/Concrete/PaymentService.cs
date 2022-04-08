using BLL.Services.Abstract;
using CIL.DTOs;
using CIL.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Concrete
{
    public class PaymentService : IPaymentService
    {
        public string publicKey;
        public string privateKey;
        public PaymentService()
        {
            publicKey = "sandbox_i74192177345";
            privateKey = "sandbox_OHhPtZO9uyU6GoPG4fXTgTZOcWHn2fPbqYp4yM5X";
        }

        public PaymentDto CreatePayment(User user)
        {
            var payModel = new PayModel()
            {
                public_key = publicKey,
                version = 3,
                action = "pay",
                amount = (decimal)user.Subscription.Price,
                currency = "UAH",
                description = "Payment for a subscription",
                order_id = Convert.ToString(Guid.NewGuid()),
                sandbox = 1,
                product_name = user.Subscription.Name,
                customer_user_id = user.Id.ToString()
            };

            var serializedModel = JsonConvert.SerializeObject(payModel);
            var convertedData = Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedModel));
            var signatureHash = HashSignuture(convertedData);
            var paymentData = new PaymentDto()
            {
                Data = convertedData,
                Signature = signatureHash,
                Result = (int)payModel.amount
            };
            return paymentData;
        }

        private string HashSignuture(string hashData)
        {
            return Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(privateKey + hashData + privateKey)));
        }
    }
}
