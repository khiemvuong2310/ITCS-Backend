using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.Interfaces;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using Microsoft.AspNetCore.Http;

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
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _transactionService.CreateTransactionAsync(request, HttpContext);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// VNPay callback URL
        /// VNPay will redirect here after payment, service handles updating transaction and pushes status via SignalR
        /// </summary>
        /// <returns>Transaction status result</returns>
        [HttpGet("vnpay-callback")]
        [AllowAnonymous] // VNPay will call without JWT
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> VnPayCallback()
        {
            var query = HttpContext.Request.Query;
            var result = await _transactionService.HandleVnPayCallbackAsync(query);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}
