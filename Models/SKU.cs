using System;

namespace Supermarket.Models
{
    public class SKU
    {

        public SKU(char ID, int unit_price)
        {
            this.ID = ID;
            this.unit_price = unit_price;
        }

        public SKU(char ID, int unit_price, byte offer_quantity, int offer_price)
        {
            this.ID = ID;
            this.unit_price = unit_price;
            this.offer_quantity = offer_quantity;
            this.offer_price = offer_price;
        }

        public char ID { get; set; }
        public int unit_price { get; set; }
        public byte offer_quantity { get; set; }
        public int offer_price { get; set; }

    }
}
