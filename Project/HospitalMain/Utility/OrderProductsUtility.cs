using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Text.Json;

namespace Utility
{

    public static class OrderProductsClipboard
    {
        public static String DBPath { get; set; }
        public static OrderProductsUtility ClipboardOrderProducts { get; set; }
        public static void LoadOrderProducts()
        {
            using FileStream fileStream = File.OpenRead(DBPath);
            ClipboardOrderProducts = JsonSerializer.Deserialize<OrderProductsUtility>(fileStream);
        }

        public static void SaveOrderProducts()
        {
            string jsonString = JsonSerializer.Serialize(ClipboardOrderProducts);
            File.WriteAllText(DBPath, jsonString);
        }
    }

    public class OrderProductsUtility
    {
        public String SelectedOrderType;
        public String SelectedProductType;
        public String Amount;
        public DateTime ArrivalDate;

        public OrderProductsUtility(String selectedOrderType, String selectedProductType, String amount, DateTime arrivalDate)
        {
            SelectedOrderType = selectedOrderType;
            SelectedProductType = selectedProductType;
            Amount = amount;
            ArrivalDate = arrivalDate;
        }
        
        public OrderProductsUtility(OrderProductsUtility orderProductsUtility)
        {
            this.SelectedOrderType = orderProductsUtility.SelectedOrderType;
            this.SelectedProductType = orderProductsUtility.SelectedProductType;
            this.Amount = orderProductsUtility.Amount;
            this.ArrivalDate = orderProductsUtility.ArrivalDate;
        }

        public OrderProductsUtility()
        {

        }
    }
}
