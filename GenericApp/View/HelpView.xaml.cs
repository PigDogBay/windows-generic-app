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
using System.Windows.Resources;
using System.Windows.Shapes;

namespace MpdBaileyTechnology.GenericApp.View
{
    /// <summary>
    /// Interaction logic for HelpView.xaml
    /// </summary>
    public partial class HelpView : UserControl
    {
        public HelpView()
        {
            InitializeComponent();
            LoadInstructions();
        }
        /// <summary>
        /// There is no easy way to build to an rtf resource
        /// </summary>
        private void LoadInstructions()
        {
            Uri uri = new Uri("pack://application:,,,/Resources/Documents/Help.rtf");
            StreamResourceInfo sri = Application.GetResourceStream(uri);
            if (sri != null)
            {
                TextRange range = new TextRange(
                    richTextBox.Document.ContentStart,
                    richTextBox.Document.ContentEnd);
                range.Load(sri.Stream, DataFormats.Rtf);
            }
        }
    }
}
