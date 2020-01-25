using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TenderComp.InfraStructure.Enums
{
    public static class Utility
    {
        public static int GetCurrency (Enums.Currencyprice CurrencyType)
        {
            int Cur = 0;
            switch (CurrencyType)
            {
                case Currencyprice.riali:
                    Cur = 0;
                    break;
                case Currencyprice.Currency:
                    Cur = 1;
                    break;
                case Currencyprice.CurrencyRiali:
                    Cur = 2;
                    break;
            }
            return Cur;
        }
    }
}