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
            var query = HttpContext.Request.Query;
            bool isSuccess = query["vnp_ResponseCode"] == "00";

            string title = isSuccess ? "Payment Successful" : "Payment Failed";
            string message = $"Amount: {query["vnp_Amount"]}, Order: {query["vnp_OrderInfo"]}";

            // Redirect tới app mobile hoặc FE sau 5 giây
            string redirectUrl = "cryofertmobile://payment/result?status=" + query["vnp_ResponseCode"];

            string html = GenerateHtmlResponse(title, message, isSuccess);

            return Content(html, "text/html");
        }

        private static string GenerateHtmlResponse(
    string title,
    string message,
    bool isSuccess)
        {
            var backgroundColor = isSuccess ? "#d4edda" : "#f8d7da";
            var textColor = isSuccess ? "#155724" : "#721c24";
            var borderColor = isSuccess ? "#c3e6cb" : "#f5c6cb";
            var icon = isSuccess ? "✓" : "✕";
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
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            min-height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            padding: 20px;
        }}
        .container {{
            background: white;
            border-radius: 16px;
            box-shadow: 0 20px 60px rgba(0,0,0,0.3);
            padding: 40px;
            max-width: 500px;
            width: 100%;
            text-align: center;
        }}
        .icon {{
            width: 80px;
            height: 80px;
            border-radius: 50%;
            background: {backgroundColor};
            border: 3px solid {borderColor};
            display: flex;
            align-items: center;
            justify-content: center;
            margin: 0 auto 24px;
            font-size: 40px;
            color: {textColor};
        }}
        h1 {{
            color: #333;
            margin-bottom: 16px;
            font-size: 24px;
        }}
        p {{
            color: #666;
            line-height: 1.6;
            margin-bottom: 24px;
        }}
        .alert {{
            background: {backgroundColor};
            border: 1px solid {borderColor};
            color: {textColor};
            padding: 16px;
            border-radius: 8px;
            margin-bottom: 24px;
        }}
        .logo {{
            color: #667eea;
            font-size: 28px;
            font-weight: bold;
            margin-bottom: 24px;
        }}
        .btn-home {{
            display: inline-block;
            margin-top: 16px;
            padding: 10px 20px;
            background: #007bff;
            color: #fff;
            text-decoration: none;
            border-radius: 6px;
        }}
        .footer {{
            color: #999;
            font-size: 14px;
            margin-top: 24px;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='logo'>🧬 CryoFert</div>
        <div class='icon'>{icon}</div>
        <h1>{title}</h1>
        <div class='alert'>
            <p style='margin: 0;'>{message}</p>
        </div>
        <p class='footer'>Healthcare/Fertility Management System</p>
    </div>
</body>
</html>";
        }

    }
}