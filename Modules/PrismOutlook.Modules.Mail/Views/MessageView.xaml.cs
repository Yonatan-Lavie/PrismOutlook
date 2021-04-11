using Infragistics.Controls.Editors;
using PrismOutlook.Core;
using PrismOutlook.Modules.Mail.Menus;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace PrismOutlook.Modules.Mail.Views
{
    [DependentViewAttribute(RegionNames.RibbonRegion, typeof(MessageTab))]
    public partial class MessageView : UserControl, ISupportDataContext, ISupportRichText
    {
        public MessageView()
        {
            InitializeComponent();
        }
        // todo: problem with richText need to impiment it
        public Xceed.Wpf.Toolkit.RichTextBox RichTextEditor { get => _richTextBox; set => throw new System.NotImplementedException(); }
    }
}
