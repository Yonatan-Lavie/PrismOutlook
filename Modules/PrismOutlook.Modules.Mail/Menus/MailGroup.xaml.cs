using Infragistics.Controls.Menus;
using Infragistics.Windows.OutlookBar;
using PrismOutlook.Business;
using PrismOutlook.Core;
using PrismOutlook.Modules.Mail.ViewModels;

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

            // TODO : refactor
            _dataTree.Loaded += _dataTree_Loaded;
        }

        // TODO : refactor
        private void _dataTree_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            _dataTree.Loaded -= _dataTree_Loaded;

            var perentNode = _dataTree.Nodes[0];
            var nodeToSelect = perentNode.Nodes[0];
            nodeToSelect.IsSelected = true;
        }

        // TODO refactor from code behaind to MailGroupViewModel this is implemintation of IOutlookBarGroup
        public string DefaultNavigationPath
        {
            get
            {
                var item = _dataTree.SelectionSettings.SelectedNodes[0] as XamDataTreeNode;
                if(item != null)
                {
                    return ((NavigationItem)item.Data).NavigationPath;
                }
                
                return $"MailList?{FolderParameters.FolderKey}={FolderParameters.Inbox}";
            }
        }
    }
}
