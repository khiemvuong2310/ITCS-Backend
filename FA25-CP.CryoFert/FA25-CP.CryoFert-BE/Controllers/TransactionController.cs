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
    }
}