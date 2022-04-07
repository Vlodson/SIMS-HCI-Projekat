using System;

namespace Model
{
   public class Equipment
   {
      private String type { get; set; }
      private int quantity { get; set; }

        public Equipment(string type, int quantity)
        {
            this.type = type;
            this.quantity = quantity;
        }
    }
}