using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreReact.Model
{
    [Table("Movements")]
    public class Movement
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date  { get; set; }
        public Single UnitPrice { get; set; }
        public int IdCategory { get; set; }
        public int IdUser { get; set; }
    }
}
