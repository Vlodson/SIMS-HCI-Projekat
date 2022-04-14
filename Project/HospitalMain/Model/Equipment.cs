using System;

namespace Model
{
   public class Equipment
   {
      public String Type { get; set; }
      public int Quantity { get; set; }
      
      // add empty constructor for serialization

      public Equipment(Equipment e)
        {
            Type = e.Type;
            Quantity = e.Quantity;
        }

      public Equipment(String type, int quantity)
        {
            this.Type = type;
            this.Quantity = quantity;
        }
   }
}