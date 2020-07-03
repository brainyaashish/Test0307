
using System;
using MarketCost;
using Xunit;
using System.Collections.Generic;

namespace MarketCost.Tests
{

    public class PaymentRegisterTests
    {
        private IPaymentRegister register;

        public PaymentRegisterTests()
        {
            IEnumerable<Product> products = new[]
            {
                new Product{SKU = 'A', Price = 50},
                new Product{SKU = 'B', Price = 30},
                new Product{SKU = 'C', Price = 20},
                new Product{SKU = 'D', Price = 15}
            };

            IEnumerable<Discount> discounts = new[]
            {
                new Discount{SKU = 'A', Quantity = 3, Value = 20},
                new Discount{SKU = 'B', Quantity = 2, Value = 15}
            };

            register = new PaymentRegister(products, discounts);
        }

        [Theory]
        [InlineData("AA", 100)]
        public void Payment_TwoA_Is100(string item, int expected)
        {
            Assert.Equal(expected, register.Scan(item).Total());
        }
        [Theory]
        [InlineData("ABC", 100)]
        public void Payment_OneAOneBOneC_Is100(string item, int expected)
        {
            Assert.Equal(expected, register.Scan(item).Total());
        }

        [Theory]
        [InlineData("AAAAABBBBBC", 370)]
        public void Payment_FiveAFiveBOneC_Is100_Is370(string item, int expected)
        {
            Assert.Equal(expected, register.Scan(item).Total());
        }

        [Theory]
        [InlineData("ABB", 95)]
        public void Payment_OneATwoB_Is95(string item, int expected)
        {
            Assert.Equal(expected, register.Scan(item).Total());
        }

        [Theory]
        [InlineData("AAABBBBBCD", 285)]
        public void Payment_ThreeAFiveBOneCOneD_Is280(string item, int expected)
        {
            Assert.Equal(expected, register.Scan(item).Total());
        }
        [Fact]
        public void No_items_returns_zero()
        {
            Assert.Equal(0, register.Scan("").Total());
        }

        [Theory]
        [InlineData("A", 50)]
        [InlineData("B", 30)]
        [InlineData("C", 20)]
        [InlineData("D", 15)]
        public void Scan_single_item_expect_correct_price(string item, int expected)
        {
            Assert.Equal(expected, register.Scan(item).Total());
        }

       
    }
}
