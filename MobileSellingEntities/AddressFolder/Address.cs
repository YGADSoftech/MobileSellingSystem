using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MobileSellingEntities.AddressFolder
{
   public class Address
    {
        //Country.Province.city.Address
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        [Column(TypeName = "varchar")]
        public string StreetAddress { get; set; }


        [Required]
        public virtual City City { get; set; }
    }
}
