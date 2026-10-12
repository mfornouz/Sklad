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
        [Required (ErrorMessage = "Jméno je povinné.")]
        [StringLength(100, ErrorMessage = "Jméno je omezeno na 100 znaků.")] 
        public string? Name { get; set; } //Nebo string a na konec = "";

        [Range(0, int.MaxValue, ErrorMessage = "Pojistná zásoba musí být nezáporné celé číslo.")] 
        public int SafetyStock { get; set; } = 0; //Pojistná zásoba

        [Range(0, int.MaxValue, ErrorMessage = "Množství na paletu musí být nezáporné celé číslo.")]
        public int PalletCapacity { get; set; } = 0; //Množství na paletu

        [Column(TypeName = "date")]
        public DateTime ReceivedOn { get; set; } = DateTime.Today; //Datum přijetí

        public int? UnitOfMeasureId { get; set; } //Cizí klíč pro jednotku měření
        [ForeignKey(nameof(UnitOfMeasureId))]
        [Required(ErrorMessage = "Jednotka měření je povinná.")]
        public virtual UnitOfMeasure UnitOfMeasure { get; set; } = new UnitOfMeasure(); //Navigační vlastnost pro jednotku měření


    }
}
