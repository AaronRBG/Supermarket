using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Supermarket.Models;
using System.Data;
using Microsoft.Extensions.Options;
using System.Configuration;

namespace Supermarket.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SupermarketController : ControllerBase
    {
        private readonly ILogger<SupermarketController> _logger;
        private IEnumerable<SKU> skus;

        public SupermarketController(ILogger<SupermarketController> logger)
        {
            _logger = logger;
            skus = retrieveSkus();
        }

        private IEnumerable<SKU> retrieveSkus()
        {
            DataSet ds = Broker.Instance().Run("SELECT * FROM [dbo].[SKUs]", "Read_SKUs");
            DataTable dt = ds.Tables["Read_SKUs"];

            return parseSKUs(dt);
        }

        [HttpGet]
        public IEnumerable<SKU> Get()
        {
            return skus;
        }

        [HttpGet("Checkout")]
        public int Checkout([FromQuery] string selected_skus)
        {
            // This solutions omits throwing an error for wrong inputs (characters that don't correspond to SKU IDs) and simply ignores them.

            if (selected_skus is null) return 0;

            int amount = 0;

            foreach (SKU s in skus)
            {
                int occurrences = selected_skus.Count(c => c == s.ID);

                if (occurrences > 0)    // If the sku has been selected at least once.
                {
                    if ((s.offer_quantity != 0) && (occurrences >= s.offer_quantity))
                    {
                        amount += (occurrences / s.offer_quantity) * s.offer_price;
                        amount += (occurrences % s.offer_quantity) * s.unit_price;
                    }
                    else
                    {
                        amount += occurrences * s.unit_price;
                    }
                }
            }
            
            return amount;
        }

        // Parse the SKU object from the order of the columns in the database (ID, price, offer_quantity, offer_price) as received in the DataRow.
        private IEnumerable<SKU> parseSKUs(DataTable dt)
        {
            List<SKU> result = new List<SKU>();

            foreach (DataRow dr in dt.Rows)
            {
                SKU s;
                object[] aux = dr.ItemArray;
                char ID = ((string)aux[0])[0];
                int price = (int)aux[1];


                if (!(aux[2] is DBNull))    // Check if the offer_quantity field is not a NULL in the database indicating there is no offer for that SKU.
                {
                    byte offer_quantity = (byte)aux[2];
                    int offer_price = (int)aux[3];
                    s = new SKU(ID, price, offer_quantity, offer_price);
                }
                else
                {
                    s = new SKU(ID, price);
                }

                result.Add(s);
            }

            return result.ToArray();
        }

    }
}
