using System;
using System.Collections.Generic;

namespace Model
{
   public class Patient : Guest
   {
      private String id { get; set; }
      private String name { get; set; }
      private String surname { get; set; }
      private DateTime doB { get; set; }

      //public Examination[] examination;
      private List<Examination> examinations;

      public Patient(string id, string name, string surname, DateTime doB, List<Examination> examinations)
      {
          this.id = id;
          this.name = name;
          this.surname = surname;
          this.doB = doB;
          this.examinations = examinations;
      }
    }
}