using Microsoft.AspNetCore.SignalR;

namespace FSCMS.Service.SignalR
{
    public class TransactionHub : Hub
    {
        // Hub để frontend subscribe theo UserId
        // Client có thể listen event "TransactionUpdated"
    }
}
