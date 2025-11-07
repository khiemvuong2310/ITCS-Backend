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

        public string CreateVnPayUrl(Transaction transaction, HttpContext httpContext)
        {
            var vnpay = new VnPay();
            string ipAddress = HashAndGetIP.GetIpAddress(httpContext);

            vnpay.AddRequestData("vnp_Version", VnPay.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", _options.vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", (transaction.Amount * 100).ToString("F0"));
            vnpay.AddRequestData("vnp_CurrCode", transaction.Currency ?? "VND");
            vnpay.AddRequestData("vnp_TxnRef", transaction.TransactionCode);
            vnpay.AddRequestData("vnp_OrderInfo", transaction.Description);
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_ReturnUrl", _options.vnp_Returnurl);
            vnpay.AddRequestData("vnp_IpAddr", ipAddress);
            vnpay.AddRequestData("vnp_CreateDate", transaction.TransactionDate.ToString("yyyyMMddHHmmss"));

            return vnpay.CreateRequestUrl(_options.vnp_Url, _options.vnp_HashSecret);
        }
    }
}