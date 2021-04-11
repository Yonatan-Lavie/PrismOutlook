using Infragistics.Windows.Ribbon;
using PrismOutlook.Core;
using System.Linq;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace PrismOutlook.Modules.Mail.Menus
{
    /// <summary>
    /// Interaction logic for MessageTab.xaml
    /// </summary>
    public partial class MessageTab : ISupportDataContext, ISupportRichText
    {
        private RichTextBox richTextEditor;

        public static double[] FontSizes
        {
            get
            {
                return new double[]
                {
                    3.0, 4.0, 5.0, 6.0, 6.5, 7.0, 7.5, 8.0, 8.5, 9.0, 9.5,
                    10.0, 10.5, 11.0, 11.5, 12.0, 12.5, 13.0, 13.5, 14.0, 15.0,
                    16.0, 17.0, 18.0, 19.0, 20.0, 22.0, 24.0, 26.0, 28.0, 30.0,
                    32.0, 34.0, 36.0, 38.0, 40.0, 44.0, 48.0, 52.0, 56.0, 60.0, 64.0, 68.0, 72.0, 76.0,
                    80.0, 88.0, 96.0, 104.0, 112.0, 120.0, 128.0, 136.0, 144.0
                };
            }
        }



        public MessageTab()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(RibbonTabItem));
            _fontSizes.ItemsSource = FontSizes;
            _fontNames.ItemsSource = Fonts.SystemFontFamilies;
        }
        public RichTextBox RichTextEditor 
        { 
            get => richTextEditor; 
            set { 
                    richTextEditor = value; 
                } 
        }

        private void FontSizes_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            RichTextEditor.FontSize = (double)e.NewValue;
        }

        private void FontNames_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue == null)
                return;
            RichTextEditor.FontFamily = (FontFamily)e.NewValue;
        }
    }
}
