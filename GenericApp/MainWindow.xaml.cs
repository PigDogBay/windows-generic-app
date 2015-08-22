using MpdBaileyTechnology.GenericApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GenericApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainVM _MainVM { get { return DataContext as MainVM; } }

        public MainWindow()
        {
            InitializeComponent();
        }
        private void ApplicationClose(object sender, ExecutedRoutedEventArgs e)
        {
            this.Close();
            e.Handled = true;
        }

        /// <summary>
        /// I can not figure out how to bind the context menu items to application presenters commands
        /// so I do it using the click event for now
        /// </summary>
        private void CloseTab_Click(object sender, RoutedEventArgs e)
        {
            this._MainVM.CmdCloseTab.Execute(null);
        }

        private void CloseAllButThis_Click(object sender, RoutedEventArgs e)
        {
            this._MainVM.CmdCloseOtherTabs.Execute(null);
        }

        private void CommandBinding_CanExecute_SaveAs(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._MainVM.CanExecuteSaveAs();
            e.Handled = true;
        }

        private void CommandBinding_CanExecute_Save(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = this._MainVM.CanExecuteSave();
            e.Handled = true;
        }

        private void CommandBinding_Executed_SaveAs(object sender, ExecutedRoutedEventArgs e)
        {
            this._MainVM.SaveWorkspaceAs();
            e.Handled = true;
        }

        private void CommandBinding_Executed_Save(object sender, ExecutedRoutedEventArgs e)
        {
            this._MainVM.SaveWorkspace();
            e.Handled = true;
        }
        private void CommandBinding_Executed_Print(object sender, ExecutedRoutedEventArgs e)
        {
            _MainVM.Print();
            e.Handled = true;
        }
        private void CommandBinding_Executed_PrintPreview(object sender, ExecutedRoutedEventArgs e)
        {
            _MainVM.PrintPreview();
            e.Handled = true;
        }
        private void CommandBinding_CanExecute_Print(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = _MainVM.CanExecutePrint();
            e.Handled = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _MainVM.Initialize();
        }


    }
}
