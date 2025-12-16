using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Nodes;
using FSCMS.Core.Entities;
using FSCMS.Core.Models.Options;
using FSCMS.Service.Payments;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using PayOS;
using PayOS.Models.V2.PaymentRequests;

namespace FSCMS.Service.Services
{
    public class PaymentGatewayService
    {
        #region Dependencies

        private readonly VnPayOptions _vnpOptions;
        private readonly PayOSClient _client;
        private readonly Core.Models.Options.PayOSOptions _posOptions;

        #endregion

        #region Properties

        public string HashSecret => _vnpOptions.vnp_HashSecret;

        #endregion

        #region Constructor

        public PaymentGatewayService(IOptions<VnPayOptions> vnpOptions, IOptions<Core.Models.Options.PayOSOptions> posOptions)
        {
            _vnpOptions = vnpOptions.Value;
            _posOptions = posOptions.Value;

            var sdkOptions = new PayOS.PayOSOptions
            {
                ClientId = _posOptions.pos_ClientId,
                ApiKey = _posOptions.pos_ApiKey,
                ChecksumKey = _posOptions.pos_ChecksumKey,
                BaseUrl = "https://api-merchant.payos.vn"
            };

            _client = new PayOSClient(sdkOptions);
        }

        #endregion

        #region Public Methods

        public async Task<string> CreatePayOSUrlAsync(Transaction transaction)
        {
            var request = new CreatePaymentLinkRequest
            {
                OrderCode = transaction.PayOSOrderCode,
                Amount = (int)Math.Round(transaction.Amount),
                Description = transaction.TransactionCode,
                ReturnUrl = _posOptions.pos_ReturnUrl,
                CancelUrl = _posOptions.pos_CancelUrl,
            };

            var response = await _client.PaymentRequests.CreateAsync(request);
            return response.CheckoutUrl!;
        }

        public string CreateVnPayUrl(Transaction transaction)
        {
            var vnpay = new VnPay();

            vnpay.AddRequestData("vnp_Version", VnPay.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", _vnpOptions.vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", ((long)(transaction.Amount * 100)).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.UtcNow.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", transaction.Currency ?? "VND");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", transaction.Description);
            vnpay.AddRequestData("vnp_OrderType", "other");
            var vnTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var vnTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, vnTimeZone);

            vnpay.AddRequestData(
                "vnp_ExpireDate",
                vnTime.AddMinutes(15).ToString("yyyyMMddHHmmss")
            );

            vnpay.AddRequestData("vnp_ReturnUrl", _vnpOptions.vnp_Returnurl);
            //vnpay.AddRequestData("vnp_IpnUrl", _options.vnp_IpnUrl);
            vnpay.AddRequestData("vnp_IpAddr", "127.0.0.1");
            vnpay.AddRequestData("vnp_TxnRef", transaction.TransactionCode);

            return vnpay.CreateRequestUrl(_vnpOptions.vnp_Url, _vnpOptions.vnp_HashSecret);
        }

        public string ComputeSignature(string payload)
        {
            var keyBytes = Encoding.UTF8.GetBytes(_posOptions.pos_ChecksumKey);
            var payloadBytes = Encoding.UTF8.GetBytes(payload);

            using var hmac = new HMACSHA256(keyBytes);
            var hashBytes = hmac.ComputeHash(payloadBytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }

        public bool IsValidData(string transactionJson, string transactionSignature)
        {
            try
            {
                // 1️⃣ Parse JSON
                var jsonObject = JsonNode.Parse(transactionJson).AsObject();

                // 2️⃣ Sort keys alphabetically
                var sortedKeys = jsonObject.Select(kvp => kvp.Key).OrderBy(k => k).ToList();

                // 3️⃣ Build string key=value&key=value...
                var sb = new StringBuilder();
                for (int i = 0; i < sortedKeys.Count; i++)
                {
                    string key = sortedKeys[i];
                    string value = jsonObject[key]?.ToString() ?? "";
                    sb.Append($"{key}={value}");
                    if (i < sortedKeys.Count - 1)
                        sb.Append("&");
                }

                var dataToSign = sb.ToString();

                // 4️⃣ Compute HMAC-SHA256
                using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_posOptions.pos_ChecksumKey));
                byte[] hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(dataToSign));
                string computedSignature = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                // 5️⃣ Compare signature
                return string.Equals(computedSignature, transactionSignature, StringComparison.Ordinal);
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}