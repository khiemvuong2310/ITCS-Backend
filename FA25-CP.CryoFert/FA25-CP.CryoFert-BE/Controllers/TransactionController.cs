using System.Security.Claims;
using FA25_CP.CryoFert_BE.AppStarts;
using FA25_CP.CryoFert_BE.Common.Attributes;
using FSCMS.Service.Interfaces;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using FSCMS.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FA25_CP.CryoFert_BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>
        /// Create a payment transaction and get VNPay redirect URL
        /// </summary>
        /// <param name="request">Transaction request</param>
        /// <returns>Transaction data with VNPay URL</returns>
        // [HttpPost]
        // [ApiDefaultResponse(typeof(TransactionResponseModel), UseDynamicWrapper = false)]
        // public async Task<IActionResult> CreateTransaction([FromQuery] CreateTransactionRequest request)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(new BaseResponse<TransactionResponseModel>
        //         {
        //             Code = StatusCodes.Status400BadRequest,
        //             Message = "Invalid input data"
        //         });
        //     }

        //     var accountId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //     if (accountId == null)
        //     {
        //         return Unauthorized(new { message = "Cannot detect user identity" });
        //     }

        //     var result = await _transactionService.CreateTransactionAsync(request, Guid.Parse(accountId));
        //     return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        // }

        /// <summary>
        /// VNPay callback URL
        /// VNPay will redirect here after payment, service handles updating transaction and pushes status via SignalR
        /// </summary>
        /// <returns>Transaction status result</returns>
        [HttpGet("vnpay-ipn")]
        [AllowAnonymous] // VNPay will call without JWT
        [ApiDefaultResponse(typeof(TransactionResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> VnPayCallback()
        {
            var query = HttpContext.Request.Query;
            var result = await _transactionService.HandleVnPayCallbackAsync(query);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Get all transactions with filters, sorting, and pagination
        /// </summary>
        /// <param name="request">Transaction filter and pagination parameters</param>
        /// <returns>List of transactions</returns>
        [HttpGet]
        [ApiDefaultResponse(typeof(TransactionResponseModel))]
        public async Task<IActionResult> GetAllTransactions([FromQuery] GetTransactionsRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new DynamicResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data",
                    MetaData = new PagingMetaData(),
                    Data = new List<TransactionResponseModel>()
                });
            }

            var result = await _transactionService.GetAllTransactionsAsync(request);

            // Trả về code từ DynamicResponse
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpPost]
        [Authorize(Roles = "Patient, Receptionist, Admin")]
        [ApiDefaultResponse(typeof(TransactionResponseModel), UseDynamicWrapper = false)]
        public async Task<IActionResult> CreateUrl([FromQuery] CreateUrlPaymentRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _transactionService.CreateUrlPaymentAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        [HttpGet("payment/result")]
        [AllowAnonymous]
        public IActionResult PaymentResult()
        {
            var q = HttpContext.Request.Query;

            bool isSuccess = q["vnp_ResponseCode"] == "00";

            string html = GenerateHtmlResponse(q, isSuccess);

            return Content(html, "text/html");
        }


        private static string GenerateHtmlResponse(
            IQueryCollection q,
            bool isSuccess)
        {
            string bg = isSuccess ? "#d4edda" : "#f8d7da";
            string color = isSuccess ? "#155724" : "#721c24";
            string border = isSuccess ? "#c3e6cb" : "#f5c6cb";
            string icon = isSuccess ? "✓" : "✕";
            string title = isSuccess ? "Payment Successful" : "Payment Failed";

            return $@"
        <!DOCTYPE html>
        <html lang='en'>
        <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>{title} - CryoFert</title>

        <style>
            * {{ margin: 0; padding: 0; box-sizing: border-box; }}
            body {{
                font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, sans-serif;
                background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
                min-height: 100vh;
                display: flex;
                align-items: center;
                justify-content: center;
                padding: 20px;
            }}
            .container {{
                background: #ffffff;
                border-radius: 18px;
                box-shadow: 0 20px 60px rgba(0,0,0,0.25);
                padding: 40px;
                width: 100%;
                max-width: 520px;
                animation: floatIn 0.7s ease-out;
                text-align: center;
            }}
            @keyframes floatIn {{
                from {{ opacity: 0; transform: translateY(30px); }}
                to {{ opacity: 1; transform: translateY(0); }}
            }}
            .icon {{
                width: 90px; height: 90px;
                border-radius: 50%;
                display: flex; justify-content: center; 
                align-items: center;
                margin: 0 auto 25px;
                background: {bg};
                border: 3px solid {border};
                font-size: 48px;
                color: {color};
            }}
            .logo {{
                font-size: 30px;
                color: #667eea;
                font-weight: 700;
                margin-bottom: 25px;
            }}
            .alert {{
                background: {bg};
                border: 1px solid {border};
                color: {color};
                padding: 18px;
                border-radius: 10px;
                margin: 20px 0;
                text-align: left;
                font-size: 15px;
                line-height: 1.6;
            }}
            .btn {{
                padding: 12px 20px;
                background: #007bff;
                color: white;
                border-radius: 8px;
                text-decoration: none;
                display: inline-block;
                margin-top: 25px;
            }}
        </style>
        </head>

        <body>
        <div class='container'>
            <div class='logo'>🧬 CryoFert</div>
            <div class='icon'>{icon}</div>
            <h1>{title}</h1>

            <div class='alert'>
                <b>Amount:</b> @(string.Format(""{{0:N0}} VND"", Convert.ToInt64(q[""vnp_Amount""]) / 100))<br>
                <b>Bank Code:</b> {q["vnp_BankCode"]}<br>
                <b>Bank Tran No:</b> {q["vnp_BankTranNo"]}<br>
                <b>Card Type:</b> {q["vnp_CardType"]}<br>
                <b>Order Info:</b> {q["vnp_OrderInfo"]}<br>
                <b>Pay Date:</b> {q["vnp_PayDate"]}<br>
                <b>Transaction No:</b> {q["vnp_TransactionNo"]}<br>
                <b>Status:</b> {(isSuccess ? "Success" : "Failed")}
            </div>

            <a class='btn' href='https://cryo.devnguyen.xyz'>Quay về trang chủ</a>
        </div>
        </body>
        </html>";
        }

    }
}