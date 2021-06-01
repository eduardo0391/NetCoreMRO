using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreReact.Model.ViewModel
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public bool Status {get; set;}
        public UserResponse User { get; set; }
    }
}
