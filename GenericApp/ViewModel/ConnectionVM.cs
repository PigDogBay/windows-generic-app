using MpdBaileyTechnology.GenericApp.Model;
using MpdBaileyTechnology.Shared.WPF;
using MpdBaileyTechnology.Shared.WPF.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MpdBaileyTechnology.GenericApp.ViewModel
{
    public class ConnectionVM : ViewModelBase
    {
        private RelayCommand _CmdConnect, _CmdDisconnect;
        public Connection Connection { get { return MainVM.Instance.MainModel.Connection; } }
        public IEnumerable<string> PortNames
        {
            get
            {
                return this.Connection.Ports;
            }
        }
        public string PortName
        {
            get { return Connection.PortName; }
            set
            {
                Connection.PortName = value;
                OnPropertyChanged("PortName");
            }
        }

        public RelayCommand CmdConnect
        {
            get
            {
                if (_CmdConnect == null)
                {
                    _CmdConnect = new RelayCommand(param => Connect());
                }
                return _CmdConnect;
            }
        }
        public RelayCommand CmdDisconnect
        {
            get
            {
                if (_CmdDisconnect == null)
                {
                    _CmdDisconnect = new RelayCommand(param => Disconnect());
                }
                return _CmdDisconnect;
            }
        }
        public string Status
        {
            get
            {
                return this.Connection.Connected ? "Connected" : "Disconnected";
            }
        }
        public ConnectionVM()
        {
            this.DisplayName = "Connection";
            this.PortName = this.PortNames.ElementAt(0);
        }

        private void Connect()
        {
            try
            {
                this.Connection.Connect();
                OnPropertyChanged("Status");
                MainVM.Instance.Status = this.Status;
            }
            catch (Exception e)
            {
                MainVM.Instance.Status = "Connection Error";
                MainVM.Instance.ServiceContainer.Create<IErrorDialog>().Show("Connection Error", e.Message);
            }
        }
        private void Disconnect()
        {
            this.Connection.Disconnect();
            OnPropertyChanged("Status");
            MainVM.Instance.Status = this.Status;
        }
    }
}
