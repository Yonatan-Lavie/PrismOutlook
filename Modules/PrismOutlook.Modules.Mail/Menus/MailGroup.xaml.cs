using Infragistics.Windows.OutlookBar;
using PrismOutlook.Core;

namespace PrismOutlook.Modules.Mail.Menus
{
    /// <summary>
    /// Interaction logic for MailGroup.xaml
    /// </summary>
    public partial class MailGroup : OutlookBarGroup, IOutlookBarGroup
    {
        public MailGroup()
        {
            InitializeComponent();
        }

        // TODO refactor from code behaind to MailGroupViewModel this is implemintation of IOutlookBarGroup
        public string DefaultNavigationPath => "MailList";
    }
}
