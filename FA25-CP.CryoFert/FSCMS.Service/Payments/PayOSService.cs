using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSCMS.Core.Models.Options;
using Microsoft.Extensions.Options;
using PayOS;
using PayOS.Models.V2.PaymentRequests;


namespace FSCMS.Service.Payments
{
    public class PayOSService
    {
        private readonly PayOSClient _client;
        private readonly Core.Models.Options.PayOSOptions _options;

        public PayOSService(IOptions<Core.Models.Options.PayOSOptions> options)
        {
            _options = options.Value;

            var sdkOptions = new PayOS.PayOSOptions
            {
                ClientId = _options.pos_ClientId,
                ApiKey = _options.pos_ApiKey,
                ChecksumKey = _options.pos_ChecksumKey,
                BaseUrl = "https://api-merchant.payos.vn"
            };

            _client = new PayOSClient(sdkOptions);
        }

        public async Task<string> CreatePaymentUrlAsync(long amount, string description)
        {
            var request = new CreatePaymentLinkRequest
            {
                OrderCode = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                Amount = (int)amount,
                Description = description,
                ReturnUrl = _options.pos_ReturnUrl,
                CancelUrl = _options.pos_CancelUrl
            };

            var response = await _client.PaymentRequests.CreateAsync(request);
            return response.CheckoutUrl!;
        }
    }
}
