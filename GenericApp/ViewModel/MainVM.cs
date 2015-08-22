using MpdBaileyTechnology.GenericApp.Model;
using MpdBaileyTechnology.GenericApp.Service.Services;
using MpdBaileyTechnology.Shared.WPF;
using MpdBaileyTechnology.Shared.WPF.Collections;
using MpdBaileyTechnology.Shared.WPF.Printing;
using MpdBaileyTechnology.Shared.WPF.Service;
using MpdBaileyTechnology.Shared.WPF.Service.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MpdBaileyTechnology.GenericApp.ViewModel
{
    public class MainVM : ViewModelBase
    {
        private RelayCommand _CmdShowHelp, _CmdShowAbout, _CmdShowTechnicalSupport, _CmdGotoWebsite, _CmdCloseTab, _CmdCloseAll, _CmdCloseOtherTabs;
        private RelayCommand _CmdShowWoprkspace;
        private MainModel _MainModel;
        private WorkspaceManager _Workspaces = new WorkspaceManager();
        private IServiceFactory _ServiceFactory;
        private ServiceContainer _ServiceContainer;
        private static MainVM _Instance;
        public static MainVM Instance { get { return _Instance; } }
        public ServiceContainer ServiceContainer
        {
            get
            {
                if (_ServiceContainer == null)
                {
                    _ServiceContainer = _ServiceFactory.CreateServices();
                    _ServiceFactory = null;
                }
                return _ServiceContainer;
            }
        }
        public WorkspaceManager WorkspaceManager { get { return _Workspaces; } }
        public MainModel MainModel
        {
            get
            {
                return _MainModel;
            }
        }

        public string Status
        {
            get { return this.MainModel.Status; }
            set
            {
                this.MainModel.Status = value;
                OnPropertyChanged("Status");
            }
        }

        #region Commands
        /// <summary>
        /// This commands shows workspace
        /// The commanad parameter is a string specifying the class type
        /// of the view model that represents the workspace
        /// </summary>
        public RelayCommand CmdShowWorkspace
        {
            get
            {
                if (_CmdShowWoprkspace == null)
                {
                    _CmdShowWoprkspace = new RelayCommand(param =>
                    {
                        string typeName = (string)param;
                        this.WorkspaceManager.Show(
                            () => (IDisposable)Assembly.GetExecutingAssembly().CreateInstance(typeName)
                            , Type.GetType(typeName));
                    });
                }
                return _CmdShowWoprkspace;
            }
        }
        public RelayCommand CmdShowAbout
        {
            get
            {
                if (_CmdShowAbout == null)
                {
                    _CmdShowAbout = new RelayCommand(param => ShowAbout());
                }
                return _CmdShowAbout;
            }
        }

        public RelayCommand CmdShowHelp
        {
            get
            {
                if (_CmdShowHelp == null)
                {
                    _CmdShowHelp = new RelayCommand(param => ShowHelp());
                }
                return _CmdShowHelp;
            }
        }

        public RelayCommand CmdShowTechnicalSupport
        {
            get
            {
                if (_CmdShowTechnicalSupport == null)
                {
                    _CmdShowTechnicalSupport = new RelayCommand(param => TechSupport());
                }
                return _CmdShowTechnicalSupport;
            }
        }

        public RelayCommand CmdGotoWebsite
        {
            get
            {
                if (_CmdGotoWebsite == null)
                {
                    _CmdGotoWebsite = new RelayCommand(param => WebSite());
                }
                return _CmdGotoWebsite;
            }
        }
        public RelayCommand CmdCloseTab
        {
            get
            {
                if (_CmdCloseTab == null)
                {
                    _CmdCloseTab = new RelayCommand(param => this.WorkspaceManager.CloseWorkspace());
                }
                return _CmdCloseTab;
            }
        }
        public RelayCommand CmdCloseOtherTabs
        {
            get
            {
                if (_CmdCloseOtherTabs == null)
                {
                    _CmdCloseOtherTabs = new RelayCommand(param => this.WorkspaceManager.CloseOtherWorkspaces());
                }
                return _CmdCloseOtherTabs;
            }
        }
        public RelayCommand CmdCloseAll
        {
            get
            {
                if (_CmdCloseAll == null)
                {
                    _CmdCloseAll = new RelayCommand(param => this.WorkspaceManager.CloseAllWorkspaces());
                }
                return _CmdCloseAll;
            }
        }
        #endregion //Commands
        public MainVM(IServiceFactory serviceFactory)
        {
            _ServiceFactory = serviceFactory;
            MainVM._Instance = this;
            _MainModel = new Model.MainModel();
            Status = "Disconnected";
        }

        public void Initialize()
        {
            this.CmdShowWorkspace.Execute("MpdBaileyTechnology.GenericApp.ViewModel.LogVM");
            this.CmdShowWorkspace.Execute("MpdBaileyTechnology.GenericApp.ViewModel.ConnectionVM");
        }

        protected override void CleanUp()
        {
            try { this.WorkspaceManager.CloseAllWorkspaces(); }
            catch { }
            this.MainModel.Dispose();
        }
        private void ShowHelp()
        {
            this.WorkspaceManager.Show(() => new HelpVM(), typeof(HelpVM));
        }
        private void TechSupport()
        {
            try { Process.Start("mailto:" + Properties.Resources.TechSupportEmail); }
            catch { }
        }
        private void WebSite()
        {
            try
            {
                Process.Start(Properties.Resources.WebAddress);
            }
            catch { }
        }
        private void ShowAbout()
        {
            ServiceContainer.Create<IAboutBox>().Show();
        }

        internal bool CanExecuteSaveAs()
        {
            ViewModelBase vm = this.WorkspaceManager.GetCurrentWorkspace() as ViewModelBase;
            if (vm == null)
            {
                return false;
            }
            return vm.CanSaveAs;
        }
        internal bool CanExecuteSave()
        {
            ViewModelBase vm = this.WorkspaceManager.GetCurrentWorkspace() as ViewModelBase;
            if (vm == null)
            {
                return false;
            }
            return (!String.IsNullOrEmpty(vm.Filename) && vm.CanSave);
        }

        internal void SaveWorkspace()
        {
            ViewModelBase vm = this.WorkspaceManager.GetCurrentWorkspace() as ViewModelBase;
            if (vm != null)
            {
                SaveWorkspace(vm);
            }
        }
        private void SaveWorkspace(ViewModelBase viewModel)
        {
            try
            {
                viewModel.Save();
            }
            catch (Exception e)
            {
                this.ServiceContainer.Create<IErrorDialog>().Show(
                    "Save Error", e.Message);
            }

        }
        internal void SaveWorkspaceAs()
        {
            ViewModelBase vm = this.WorkspaceManager.GetCurrentWorkspace() as ViewModelBase;
            if (vm == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(vm.Filename))
            {
                vm.Filename = CreateNumberedFileName(vm.DisplayName);
            }
            ISaveFileDialog dialog = this.ServiceContainer.Create<ISaveFileDialog>();
            string filename = dialog.Show(vm.Filename, vm.Filter);
            if (!String.IsNullOrEmpty(filename))
            {
                vm.Filename = filename;
                SaveWorkspace(vm);
            }
        }

        Dictionary<string, int> _FilenameDictionary = new Dictionary<string, int>();
        private string CreateNumberedFileName(string p)
        {
            int value = 0;
            if (_FilenameDictionary.ContainsKey(p))
            {
                _FilenameDictionary.TryGetValue(p, out value);
                value++;
                _FilenameDictionary.Remove(p);
            }
            _FilenameDictionary.Add(p, value);
            return value == 0 ?
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + p :
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + p + "_" + value.ToString();
        }
        internal void Print()
        {
            ISupportFlowDocument printable = this.WorkspaceManager.GetCurrentWorkspace() as ISupportFlowDocument;
            if (printable != null)
            {
                try
                {
                    ViewModelBase vm = printable as ViewModelBase;
                    IPrintFlowDocument printer = this.ServiceContainer.Create<IPrintFlowDocument>();
                    printer.Print(vm.DisplayName, printable.CreateFlowdocumentXaml());
                }
                catch (Exception e)
                {
                    this.ServiceContainer.Create<IErrorDialog>().Show(
                        "Print Error", e.Message);
                }
            }
        }
        internal void PrintPreview()
        {
            ISupportFlowDocument printable = this.WorkspaceManager.GetCurrentWorkspace() as ISupportFlowDocument;
            if (printable != null)
            {
                try
                {
                    ViewModelBase vm = printable as ViewModelBase;
                    IPrintFlowDocument printer = this.ServiceContainer.Create<IPrintFlowDocument>();
                    printer.PrintPreview(vm.DisplayName, printable.CreateFlowdocumentXaml());
                }
                catch (Exception e)
                {
                    this.ServiceContainer.Create<IErrorDialog>().Show(
                        "Print Preview Error", e.Message);
                }
            }

        }
        internal bool CanExecutePrint()
        {
            ISupportFlowDocument printable = this.WorkspaceManager.GetCurrentWorkspace() as ISupportFlowDocument;
            return printable != null;
        }
        public void ShowViewModel(string viewModelTypeName)
        {
            Assembly mainAssembly = Assembly.GetExecutingAssembly();
            this.WorkspaceManager.Show(
                () => (IDisposable)mainAssembly.CreateInstance(viewModelTypeName)
                , Type.GetType(viewModelTypeName));

        }
    }
}
