using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreReact.Context
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime ShippingDate { get; set; }
        public string ActivationCode { get; set; }
        public bool IsConfirmed { get; set; }
        public bool IsSuperUser { get; set; }
        public DateTime ExpirationDate { get; set; }


    }
}
