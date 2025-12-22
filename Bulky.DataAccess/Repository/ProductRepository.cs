using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(Product productToUpdate)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == productToUpdate.Id);
            if (product != null)
            {
                product.Title = productToUpdate.Title;
                product.Description = productToUpdate.Description;
                product.ISBN = productToUpdate.ISBN;
                product.Author = productToUpdate.Author;
                product.ListPrice = productToUpdate.ListPrice;
                product.Price = productToUpdate.Price;
                product.Price50 = productToUpdate.Price50;
                product.Price100 = productToUpdate.Price100;
                product.CategoryId = productToUpdate.CategoryId;
                product.ProductImages = productToUpdate.ProductImages;
                _context.SaveChanges();
            }
        }


    }
}
