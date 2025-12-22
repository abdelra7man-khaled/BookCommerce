using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductImageRepository : Repository<ProductImage>, IProductImageRepository
    {
        private readonly AppDbContext _context;
        public ProductImageRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(ProductImage productImageToUpdate)
        {
            _context.ProductImages.Update(productImageToUpdate);
        }

    }
}
