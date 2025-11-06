using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FSCMS.Service.ReponseModel;
using FSCMS.Service.RequestModel;
using Microsoft.AspNetCore.Http;

namespace FSCMS.Service.Interfaces
{
    public interface ITransactionService
    {
        /// <summary>
        /// Get transaction by ID
        /// </summary>
        Task<BaseResponse<TransactionResponseModel>> GetTransactionByIdAsync(Guid transactionId);

        /// <summary>
        /// Create a new transaction
        /// </summary>
        Task<BaseResponse<TransactionResponseModel>> CreateTransactionAsync(CreateTransactionRequest request, HttpContext httpContext);

        /// <summary>
        /// Get transactions with paging and filters
        /// </summary>
        Task<DynamicResponse<TransactionResponseModel>> GetTransactionsAsync(GetTransactionsRequest request);

        /// <summary>
        /// Update existing transaction
        /// </summary>
        Task<BaseResponse<TransactionResponseModel>> UpdateTransactionAsync(Guid transactionId, UpdateTransactionRequest request);

        /// <summary>
        /// Delete transaction (soft delete)
        /// </summary>
        Task<BaseResponse> DeleteTransactionAsync(Guid transactionId);
    }
}
