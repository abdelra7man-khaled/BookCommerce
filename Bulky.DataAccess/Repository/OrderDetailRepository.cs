using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private AppDbContext _context;
        public OrderDetailRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(OrderDetail orderDetailToUpdate)
        {
            _context.OrderDetails.Update(orderDetailToUpdate);
        }

    }
}
