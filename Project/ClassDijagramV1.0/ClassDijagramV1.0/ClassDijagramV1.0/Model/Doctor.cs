using System;
using System.Collections.Generic;

namespace Model
{
   public class Doctor
   {
      private String id { get; set; }
      private String name { get; set; }
      private String surname { get; set; }
      private DateTime doB { get; set; }
      private DoctorType type { get; set; }

      //public Examination[] examination;
      private List<Examination> examination;

        public Doctor(string id, string name, string surname, DateTime doB, DoctorType type, List<Examination> examination)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.doB = doB;
            this.type = type;
            this.examination = examination;
        }
    }
}