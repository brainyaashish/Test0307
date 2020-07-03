using System;

namespace MarketCost
{
    public interface IPaymentRegister
    {
        IPaymentRegister Scan(String scan);
        int Total();
        char[] ScannedProducts { get; }
    }
}
