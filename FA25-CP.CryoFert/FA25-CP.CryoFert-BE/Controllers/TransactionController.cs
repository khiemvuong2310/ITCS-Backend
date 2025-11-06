using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FSCMS.Service.Services;
using FSCMS.Service.RequestModel;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FA25_CP.CryoFert_BE.Controllers
{
    /// <summary>
    /// Transaction Controller - Handles transaction CRUD, filtering, and payment processing
    /// </summary>
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
        /// Get transaction by ID
        /// </summary>
        [HttpGet("{transactionId}")]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTransactionById(Guid transactionId)
        {
            if (transactionId == Guid.Empty)
            {
                return BadRequest(new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid transaction ID"
                });
            }

            var result = await _transactionService.GetTransactionByIdAsync(transactionId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Create a new transaction
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status400BadRequest)]
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
        /// Get transactions with paging and filters
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(DynamicResponse<TransactionResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(DynamicResponse<TransactionResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTransactions([FromQuery] GetTransactionsRequest request)
        {
            var result = await _transactionService.GetTransactionsAsync(request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Update a transaction
        /// </summary>
        [HttpPut("{transactionId}")]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse<TransactionResponseModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateTransaction(Guid transactionId, [FromBody] UpdateTransactionRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new BaseResponse<TransactionResponseModel>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid input data"
                });
            }

            var result = await _transactionService.UpdateTransactionAsync(transactionId, request);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }

        /// <summary>
        /// Delete a transaction (soft delete)
        /// </summary>
        [HttpDelete("{transactionId}")]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(BaseResponse), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteTransaction(Guid transactionId)
        {
            if (transactionId == Guid.Empty)
            {
                return BadRequest(new BaseResponse
                {
                    Code = StatusCodes.Status400BadRequest,
                    Message = "Invalid transaction ID"
                });
            }

            var result = await _transactionService.DeleteTransactionAsync(transactionId);
            return StatusCode(result.Code ?? StatusCodes.Status500InternalServerError, result);
        }
    }
}
