using MpdBaileyTechnology.Shared.WPF;
using MpdBaileyTechnology.Shared.WPF.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MpdBaileyTechnology.GenericApp.ViewModel
{
    public class LogVM : ViewModelBase
    {
        private RelayCommand _CmdStart, _CmdStop, _CmdClear;
        private StringBuilder _StringBuilder;
        public RelayCommand CmdStart
        {
            get
            {
                if (_CmdStart == null)
                {
                    _CmdStart = new RelayCommand(param => Start());
                }
                return _CmdStart;
            }
        }
        public RelayCommand CmdStop
        {
            get
            {
                if (_CmdStop == null)
                {
                    _CmdStop = new RelayCommand(param => Stop());
                }
                return _CmdStop;
            }
        }
        public RelayCommand CmdClear
        {
            get
            {
                if (_CmdClear == null)
                {
                    _CmdClear = new RelayCommand(param => Clear());
                }
                return _CmdClear;
            }
        }
        public string Log
        {
            get
            {
                return _StringBuilder.ToString();
            }
        }
        public Boolean IsLogging
        {
            get { return MainVM.Instance.MainModel.IsLogging; }
        }
        public LogVM()
        {
            this.DisplayName = "Log";
            MainVM.Instance.MainModel.Log.CollectionChanged += Log_CollectionChanged;
            _StringBuilder = new StringBuilder();
            LoadReadings();
            this.CanSaveAs = true;
        }

        private void LoadReadings()
        {
            foreach (var item in MainVM.Instance.MainModel.Log)
            {
                _StringBuilder.AppendLine(item.ToString());
            }
        }

        void Log_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == System.Collections.Specialized.NotifyCollectionChangedAction.Add)
            {
                foreach (var item in e.NewItems)
                {
                    _StringBuilder.AppendLine(item.ToString());
                }
                OnPropertyChanged("Log");
            }
        }

        private void Start()
        {
            if (!MainVM.Instance.MainModel.Connection.Connected)
            {
                MainVM.Instance.ServiceContainer.Create<IInformationDialog>().Show("Not Connected", "Please connect to an instrument" );
                return;
            }
            if (!MainVM.Instance.MainModel.IsLogging)
            {
                MainVM.Instance.MainModel.Start();
                MainVM.Instance.Status = "Logging";
                OnPropertyChanged("IsLogging");
            }
        }
        private void Stop()
        {
            MainVM.Instance.MainModel.Stop();
            MainVM.Instance.Status = "Stopped Logging";
            OnPropertyChanged("IsLogging");
        }
        private void Clear()
        {
            MainVM.Instance.MainModel.Log.Clear();
            _StringBuilder.Clear();
            OnPropertyChanged("Log");
        }
        protected override void CleanUp()
        {
            base.CleanUp();
            MainVM.Instance.MainModel.Log.CollectionChanged -= Log_CollectionChanged;
        }
    }
}
