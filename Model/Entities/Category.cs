using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetCoreReact.Context
{

    [Table("Categories")]
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public int IdUser { get; set; }
        public int IdType { get; set; }
        public string Description { get; set; }
    }
}