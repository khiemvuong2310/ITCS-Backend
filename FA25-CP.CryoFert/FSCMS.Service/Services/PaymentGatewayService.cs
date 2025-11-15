using FSCMS.Core.Entities;
using FSCMS.Core.Models.Options;
using FSCMS.Service.Payments;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FSCMS.Service.Services
{
    public class PaymentGatewayService
    {
        private readonly VnPayOptions _options;

        public string HashSecret => _options.vnp_HashSecret;

        public PaymentGatewayService(IOptions<VnPayOptions> options)
        {
            _options = options.Value;
        }

        public string CreateVnPayUrl(Transaction transaction)
        {
            var vnpay = new VnPay();

            vnpay.AddRequestData("vnp_Version", VnPay.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", _options.vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (transaction.Amount * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", transaction.Currency ?? "VND");
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", transaction.Description);
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ExpireDate", DateTime.Now.AddMinutes(15).ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_ReturnUrl", _options.vnp_Returnurl);
            vnpay.AddRequestData("vnp_IpnUrl", _options.vnp_IpnUrl);
            vnpay.AddRequestData("vnp_IpAddr", "127.0.0.1");
            vnpay.AddRequestData("vnp_TxnRef", transaction.TransactionCode);

            return vnpay.CreateRequestUrl(_options.vnp_Url, _options.vnp_HashSecret);
        }
    }
}