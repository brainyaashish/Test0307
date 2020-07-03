namespace MarketCost
{
    public class Discount : IDiscount
    {
        public char SKU { get; set; }
		public int Quantity { get; set; }
		public int Value { get; set; }
    }
}
