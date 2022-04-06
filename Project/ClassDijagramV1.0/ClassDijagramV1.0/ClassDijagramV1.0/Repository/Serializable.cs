using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassDijagramV1._0.Repository
{
    public interface Serializable
    {

        public string[] toCSV();

        public void fromCSV(string[] values);

    }
}
