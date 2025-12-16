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
        /// Tạo một transaction mới và trả về URL thanh toán VNPay nếu là thanh toán online
        /// </summary>
        /// <param name="request">Thông tin transaction cần tạo</param>
        /// <param name="httpContext">HttpContext để lấy IP và build URL</param>
        /// <returns>BaseResponse với TransactionResponseModel</returns>
        Task<BaseResponse<TransactionResponseModel>> CreateTransactionAsync(CreateTransactionRequest request, Guid patientId);

        /// <summary>
        /// Xử lý callback từ VNPay sau khi người dùng thanh toán
        /// </summary>
        /// <param name="query">Query string từ callback VNPay</param>
        /// <returns>BaseResponse với TransactionResponseModel đã cập nhật trạng thái</returns>
        Task<BaseResponse<TransactionResponseModel>> HandleVnPayCallbackAsync(IQueryCollection query);
        Task<DynamicResponse<TransactionResponseModel>> GetAllTransactionsAsync(GetTransactionsRequest request);
        Task<BaseResponse<TransactionResponseModel>> CreateUrlPaymentAsync(CreateUrlPaymentRequest request);
        Task<BaseResponse> CancellTransactionAsync(CancelltransactionRequest request);
        Task<BaseResponse<TransactionResponseModel>> HandlePayOSWebhookAsync(HttpRequest request);
        Task<BaseResponse<TransactionResponseModel>> CashPaymentAsync(CashPaymentRequest request);
    }
}
