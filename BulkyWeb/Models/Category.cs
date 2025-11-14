using System.ComponentModel.DataAnnotations;

namespace BulkyWeb.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Display(Name= "Category Name")]
        public string Name { get; set; }

        [Display(Name= "Display Order")]
        [Range(1,100,ErrorMessage = "Display Order must be between 1 and 100")]
        public int DisplayOrder { get; set; }
    }
}
