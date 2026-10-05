using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataEntity
{
    [Table("Materials")]
    public class Material
    {
        [Key]
        public int MaterialId { get; set; }
        [Required (ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")] //Jméno je omezeno na 100 znaků.
        public string? Name { get; set; }; //Nebo string a na konec = "";
    }
}
