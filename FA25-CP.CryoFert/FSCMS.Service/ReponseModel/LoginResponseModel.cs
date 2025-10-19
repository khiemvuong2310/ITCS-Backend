using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSCMS.Service.ReponseModel
{
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public UserResponse User { get; set; }
        public bool EmailVerified { get; set; }
    }
}
