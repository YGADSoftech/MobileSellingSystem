using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileSellingEntities.MobileShop
{
    public class Products : Illistables
    {
        //constructor

        public Products(){
            
            Images = new List<ProductImages>();
            colors = new List<Colors>();
            SizesOffered = new List<Sizes>();

        }



        public int Id { get; set; }

        [Required]
        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Name { get; set; }

        public float Price { get; set; }

        [Column(TypeName = "varchar")]
        [MaxLength(100)]
        public string Description { get; set; }


        public Nullable<DateTime> LaunchDate { get; set; }


        [Required]
        public virtual ICollection<ProductImages> Images { get; set; }
        public virtual ICollection<Colors> colors { get; set; }
        public virtual ICollection<Sizes> SizesOffered { get; set; }



        public virtual Condition Condition { get; set; }

        [Required]
        public virtual SubCategory SubCategory { get; set; }
    }
}
