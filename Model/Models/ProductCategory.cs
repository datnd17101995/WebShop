using Model.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    [Table("ProductCategory")]
    public class ProductCategory :Audiable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }
        
        [Required]
        [Column(TypeName ="Varchar")]
        [MaxLength(250)]
        public string Alias { get; set; }

        public string Descriptions { get; set; }
        public int? DisplayOrder { get; set; }
        public int? ParentId { get; set; }
        public string Image { get; set; }
        public bool? HomeFlag { get; set; }
        public IEnumerable<Product> Product { get; set; }

    }
}
