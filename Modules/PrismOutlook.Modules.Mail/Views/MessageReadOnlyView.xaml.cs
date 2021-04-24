using PrismOutlook.Core;
using PrismOutlook.Modules.Mail.Menus;
using System.Windows.Controls;

namespace PrismOutlook.Modules.Mail.Views
{

    [DependentView(RegionNames.RibbonRegion, typeof(MessageReadOnlyTab))]
    public partial class MessageReadOnlyView : ISupportDataContext
    {
        public MessageReadOnlyView()
        {
            InitializeComponent();
        }
    }
}
