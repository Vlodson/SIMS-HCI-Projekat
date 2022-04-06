using System;

namespace Model
{
   public class Patient : Guest
   {
      private String id;
      private String name;
      private String surname;
      private DateTime doB;
      
      public Examination[] examination;
   
   }
}