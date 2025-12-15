namespace BulkyBook.Models.ViewModels
{
    public class OrderVM
    {
        OrderHeader OrderHeader { get; set; }
        IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
