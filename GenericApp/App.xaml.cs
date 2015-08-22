using MpdBaileyTechnology.GenericApp.Service;
using MpdBaileyTechnology.GenericApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace GenericApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private MainVM _MainVM;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow window = new MainWindow();
            window.Closing += new System.ComponentModel.CancelEventHandler(window_Closing);
            AppServiceFactory serviceFactory = new AppServiceFactory();
            _MainVM = new MainVM(serviceFactory);
            window.DataContext = _MainVM;
            window.Show();
        }
        void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _MainVM.Dispose();
        }

    }
}
