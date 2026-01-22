using FSCMS.Service.ReponseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.RequestModel
{
    public class GetHospitalDataRequest :PagingModel
    {
        public decimal value {  get; set; }
    }

    public class GetHospitalDataValue
    {
        public decimal value { get; set; }
    }
}
