using MpdBaileyTechnology.GenericApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MpdBaileyTechnology.GenericApp.ViewModel
{
    public class ListVM : ViewModelBase
    {
        public IEnumerable<Reading> Log
        {
            get { return MainVM.Instance.MainModel.Log; }
        }
        
        public ListVM()
        {
            this.DisplayName = "List";
        }
    }
}
