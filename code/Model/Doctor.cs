using System;

namespace Model
{
   public class Doctor
   {
      private String id;
      private String name;
      private String surname;
      private DateTime doB;
      private DoctorType type;
      
      public Examination[] examination;
   
   }
}