using System;
using System.Collections.Generic;
using System.Linq;

namespace MarketCost
{
    public class PaymentRegister : IPaymentRegister
    {
        private readonly IEnumerable<IProduct> catalog;
        private readonly IEnumerable<IDiscount> discounts;

        private char[] scannedProducts;
        public char[] ScannedProducts { get { return scannedProducts; } }

        public PaymentRegister(IEnumerable<IProduct> products, IEnumerable<IDiscount> discounts)
        {
            this.catalog = products;
            this.discounts = discounts;
            scannedProducts = new char[] { };
        }

        public IPaymentRegister Scan(String scan)
        {
            if (!String.IsNullOrEmpty(scan))
            {
                scannedProducts = scan
                    .ToCharArray()
                    .Where(scannedSKU => catalog.Any(product => product.SKU == scannedSKU))
                    .ToArray();
            }
            return this;
        }

        public int Total()
        {
            int total = 0;
            int totalDiscount = 0;
            total = scannedProducts.Sum(item => PriceFor(item));
            totalDiscount = discounts.Sum(discount => CalculateDiscount(discount, scannedProducts));
            return total - totalDiscount;
        }

        private int PriceFor(char sku)
        {
            return catalog.Single(p => p.SKU == sku).Price;
        }

        private int CalculateDiscount(IDiscount discount, char[] cart)
        {
            int itemCount = cart.Count(item => item == discount.SKU);
            return (itemCount / discount.Quantity) * discount.Value;
        }
    }
}
