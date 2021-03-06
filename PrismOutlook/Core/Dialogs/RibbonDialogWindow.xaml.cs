using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
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

namespace PrismOutlook.Core.Dialogs
{
    /// <summary>
    /// Interaction logic for RibbonDialogWindow.xaml
    /// </summary>
    public partial class RibbonDialogWindow: IDialogWindow
    {
        public RibbonDialogWindow()
        {
            InitializeComponent();
        }

        public IDialogResult Result { get; set ; }
    }
}
