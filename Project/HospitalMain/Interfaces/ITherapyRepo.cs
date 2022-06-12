using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    internal interface ITherapyRepo
    {
        public ObservableCollection<Therapy> findById(string examId);
        public void NewTherapy(Therapy therapy);
        public bool LoadTherapy();
        public bool SaveTherapy();
    }
}
