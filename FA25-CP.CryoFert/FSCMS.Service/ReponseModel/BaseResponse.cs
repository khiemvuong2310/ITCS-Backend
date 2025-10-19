using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.ReponseModel
{
    public class BaseResponse<T>
    {
        public int? Code { get; set; }
        public string? SystemCode { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; }
    }

    public class BaseResponseForLogin<T> : BaseResponse<T>
    {
        public bool IsBanned { get; set; }
        public bool RequiresVerification { get; set; }
        public int BannedAccountId { get; set; }
    }

    public class BaseResponse
    {
        public int? Code { get; set; }
        public string? SystemCode { get; set; }
        public string? Message { get; set; }
    }

    public class DynamicResponse<T>
    {
        public int? Code { get; set; }
        public string? SystemCode { get; set; }
        public string? Message { get; set; }
        public PagingMetaData MetaData { get; set; }
        public List<T> Data { get; set; }
    }

    public class PagingMetaData
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 50;
        public int Total { get; set; } = 0;
    }

    public class PagingModel
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 50;
        public string? Sort { get; set; }
        public string? Order { get; set; }
    }
}
