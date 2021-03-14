
using Infragistics.Themes;
using Infragistics.Windows.OutlookBar;
using Infragistics.Windows.Ribbon;
using Prism.Regions;
using PrismOutlook.Core;

namespace PrismOutlook.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : XamRibbonWindow
    {
        private readonly IApplicationCommands _applicationCommands;

        // TODO Refactor regionManager from code behaind to MainWindowViewModel
        public MainWindow(IApplicationCommands applicationCommands)
        {
            InitializeComponent();

            Infragistics.Themes.ThemeManager.ApplicationTheme = new Office2013Theme();
            _applicationCommands = applicationCommands;
        }

        // TODO : Refactor handeling SelectedGroupChanged Event from code behaind to MainWindowViewModel 
        private void XamOutlookBar_SelectedGroupChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            var group = ((XamOutlookBar)sender).SelectedGroup as IOutlookBarGroup;

            if(group != null)
            {

                _applicationCommands.NavigateCommand.Execute(group.DefaultNavigationPath);
            }
        }
    }
}
