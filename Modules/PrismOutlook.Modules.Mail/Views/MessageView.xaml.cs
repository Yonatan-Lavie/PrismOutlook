using PrismOutlook.Core;
using PrismOutlook.Modules.Mail.Menus;
using System.Windows.Controls;

namespace PrismOutlook.Modules.Mail.Views
{
    [DependentViewAttribute(RegionNames.RibbonRegion, typeof(HomeTab))]
    [DependentViewAttribute(RegionNames.RibbonRegion, typeof(MessageTab))]
    public partial class MessageView : UserControl, ISupportDataContext
    {
        public MessageView()
        {
            InitializeComponent();
        }
    }
}
