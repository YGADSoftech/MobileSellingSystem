using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileSellingEntities.MobileShop
{
    public class Category : Illistables
    {
        public int Id { get ; set; }
        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Name { get ; set ; }
        [Column(TypeName = "varchar")]
        [MaxLength(300)]
        public string ImageUrl { get; set; }

        [Required]
        public virtual Department Department { get; set; }
    }
}
