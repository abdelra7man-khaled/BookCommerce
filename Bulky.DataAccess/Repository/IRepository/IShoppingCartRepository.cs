using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        void Update(Category categoryToUpdate);
        void Save();
    }
}
