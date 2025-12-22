using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string Author { get; set; }
        [Display(Name = "List Price")]
        [Range(1, 1000)]
        public decimal ListPrice { get; set; }
        [Display(Name = "Price for 1-50")]
        [Range(1, 1000)]
        public decimal Price { get; set; }
        [Display(Name = "Price for 50+")]
        [Range(1, 1000)]
        public decimal Price50 { get; set; }
        [Display(Name = "Price for 100+")]
        [Range(1, 1000)]
        public decimal Price100 { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

    }
}
