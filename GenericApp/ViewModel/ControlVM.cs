using MpdBaileyTechnology.Shared.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MpdBaileyTechnology.GenericApp.ViewModel
{
    public class ControlVM : ViewModelBase
    {
        private RelayCommand _CmdButton;
        private string _Setting, _Status;

        public RelayCommand CmdButton
        {
            get
            {
                if (_CmdButton == null)
                {
                    _CmdButton = new RelayCommand(p => DoCommand((String)p));
                }
                return _CmdButton;
            }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value;
            OnPropertyChanged("Status");
            }
        }

        public string Setting
        {
            get { return _Setting; }
            set { _Setting = value; OnPropertyChanged("Setting"); }
        }  

        public ControlVM()
        {
            this.DisplayName = "Control";
            Status = "Ready";
            Setting = "42";
        }
        private void DoCommand(String cmd)
        {
            switch (cmd)
            {
                case "1":
                    DoCmd1();
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    break;
            }
            Status = String.Format("Button {0} pressed, setting = {1}", cmd, Setting);
        }

        void DoCmd1()
        {
            //TO DO - enter your code for the action
        }

    }
}
