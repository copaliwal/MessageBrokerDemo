using System.Text.Json;

namespace MessageBrokerDemo.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
