using MpdBaileyTechnology.GenericApp.Properties;
using MpdBaileyTechnology.Shared.WPF;
using MpdBaileyTechnology.Shared.WPF.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MpdBaileyTechnology.GenericApp.ViewModel
{
    public class SettingsVM : ViewModelBase
    {
        private RelayCommand _CmdReset, _CmdSaveSettings;
        public RelayCommand CmdReset
        {
            get
            {
                if (_CmdReset == null)
                {
                    _CmdReset = new RelayCommand(p => Reset());
                }
                return _CmdReset;
            }
        }
        public RelayCommand CmdSaveSettings
        {
            get
            {
                if (_CmdSaveSettings == null)
                {
                    _CmdSaveSettings = new RelayCommand(p => SaveSettings());
                }
                return _CmdSaveSettings;
            }
        }

        private String _UserName;
        private int _MaxCount;
        private long _TimeOut;

        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                OnPropertyChanged("UserName");
            }
        }
        public int MaxCount
        {
            get { return _MaxCount; }
            set
            {
                _MaxCount = value;
                OnPropertyChanged("MaxCount");
            }
        }
        public long TimeOut
        {
            get { return _TimeOut; }
            set
            {
                _TimeOut = value;
                OnPropertyChanged("TimeOut");
            }
        }

        public SettingsVM()
        {
            this.DisplayName = "Settings";
            _UserName = Settings.Default.UserName;
            _MaxCount = Settings.Default.MaxCount;
            _TimeOut = Settings.Default.TimeOut;
        }

        private void Reset()
        {
            UserName = "";
            MaxCount = 100;
            TimeOut = 1000L;
        }
        private void SaveSettings()
        {
            Settings.Default.UserName = _UserName;
            Settings.Default.MaxCount = _MaxCount;
            Settings.Default.TimeOut = _TimeOut;
            Settings.Default.Save();
            MainVM.Instance.ServiceContainer.Create<IInformationDialog>()
                .Show("Save Settings",
                        "The settings have been successfully saved.");
        }


    }
}
