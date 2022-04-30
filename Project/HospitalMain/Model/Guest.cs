using System;

namespace Model
{
   public class Guest
   {
      private String id;
    
      public String ID
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                }
            }
        }

        public Guest(string id)
        {
            this.id = id;
        }

        public Guest()
        {

        }

    }
}