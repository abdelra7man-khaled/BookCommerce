using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;

namespace BulkyBook.DataAccess.Repository
{
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private readonly AppDbContext _context;
        public OrderHeaderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(OrderHeader orderHeaderToUpdate)
        {
            _context.Update(orderHeaderToUpdate);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var order = _context.OrderHeaders.FirstOrDefault(o => o.Id == id);
            if (order is not null)
            {
                order.OrderStatus = orderStatus;
                if (!string.IsNullOrEmpty(paymentStatus))
                {
                    order.PaymentStatus = paymentStatus;
                }

            }
        }

        public void UpdateStripePaymentID(int id, string sessionId, string paymentIntentId)
        {
            var order = _context.OrderHeaders.FirstOrDefault(o => o.Id == id);

            if (!string.IsNullOrEmpty(sessionId))
            {
                order!.SessionId = sessionId;
            }
            if (!string.IsNullOrEmpty(paymentIntentId))
            {
                order!.PaymentIntentId = paymentIntentId;
                order.PaymentDate = DateTime.Now;
            }
        }
    }
}
