using Infragistics.Windows.Ribbon;
using PrismOutlook.Core;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace PrismOutlook.Modules.Mail.Menus
{
    /// <summary>
    /// Interaction logic for MessageTab.xaml
    /// </summary>
    public partial class MessageTab : ISupportDataContext, ISupportRichText
    {
        #region Properties
        private RichTextBox _richTextEditor;
        public RichTextBox RichTextEditor
        {
            get => _richTextEditor;
            set
            {
                if (_richTextEditor != null)
                {
                    _richTextEditor.Loaded -= RrichTextEditor_Loaded;
                    _richTextEditor.SelectionChanged -= RichTextEditor_SelectionChanged;
                }

                _richTextEditor = value;
                if (_richTextEditor != null)
                {
                    _richTextEditor.Loaded += RrichTextEditor_Loaded;
                    _richTextEditor.SelectionChanged += RichTextEditor_SelectionChanged;
                }
            }
        }
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
        #endregion

        public MessageTab()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(RibbonTabItem));
            _fontSizes.ItemsSource = FontSizes;
            _fontNames.ItemsSource = Fonts.SystemFontFamilies;
        }

        #region Event handlers
        private void FontSizes_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if(e.NewValue == null)
            {
                return;
            }
            var fontSize = (double)e.NewValue;
            var textSelection = RichTextEditor.Selection;
            textSelection.ApplyPropertyValue(FontSizeProperty, fontSize);
        }

        private void FontNames_SelectedItemChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<object> e)
        {
            if (e.NewValue == null)
                return;
            var fontFamily = (FontFamily)e.NewValue;
            var textSelection = RichTextEditor.Selection;
            textSelection.ApplyPropertyValue(FontFamilyProperty, fontFamily);
        }
        
        private void RrichTextEditor_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            InitVisualState();
        }

        private void RichTextEditor_SelectionChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            UpdateVisualState();
        }
        #endregion

        #region Fonctions
        void InitVisualState()
        {
            _richTextEditor.FontFamily = new FontFamily("Arial");
            _richTextEditor.FontSize = 16.0;
            _fontSizes.Value = 16.0;
            _fontNames.Value = new FontFamily("Arial");
        }

        void UpdateVisualState()
        {
            var settings = RichTextEditor.Selection;
            if (settings == null)
                return;
            // leftToRight
            var a = settings.GetPropertyValue(Inline.FlowDirectionProperty);
            var b = settings.GetPropertyValue(Paragraph.TextAlignmentProperty);
            UpdateAligment(settings);
            UpdateBold(settings);
            UpdateItalic(settings);
            UpdateUnderline(settings);
            UpdateFontSizes(settings);
            UpdateFontFamily(settings);
        }

        void UpdateAligment(TextSelection settings)
        {
            var textAlignment = settings.GetPropertyValue(Paragraph.TextAlignmentProperty);
            if(textAlignment is TextAlignment alignment)
            {
                switch (alignment)
                {
                    case TextAlignment.Left:
                        _alignLeft.IsChecked = true;
                        break;
                    case TextAlignment.Right:
                        _alignRight.IsChecked = true;
                        break;
                    case TextAlignment.Center:
                        _alignCenter.IsChecked = true;
                        break;
                    case TextAlignment.Justify:
                        _alignJustify.IsChecked = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                _alignCenter.IsChecked = true;
                _alignCenter.IsChecked = false;
            }

        }

        void UpdateItalic(TextSelection settings)
        {
            var IsItalic = settings.GetPropertyValue(FontStyleProperty).ToString() == "Italic" ? true : false;
            _italicButton.IsChecked = IsItalic;
        }
        
        void UpdateBold(TextSelection settings)
        {
            var IsBold = settings.GetPropertyValue(FontWeightProperty).ToString() == "Bold" ? true : false;
            _boldButton.IsChecked = IsBold;
        }
        
        void UpdateUnderline(TextSelection settings)
        {
            var textDecorations = settings.GetPropertyValue(Inline.TextDecorationsProperty);
            bool IsUnderline = false;
            if (textDecorations is TextDecorationCollection decorations)
            {
                IsUnderline = decorations.FirstOrDefault(x => x.Location == TextDecorationLocation.Underline) == null ? false : true;
            }
            _underlineButton.IsChecked = IsUnderline;
        }
        
        void UpdateFontSizes(TextSelection settings)
        {
            _fontSizes.Value  = settings.GetPropertyValue(FontSizeProperty);
        }
        
        void UpdateFontFamily(TextSelection settings)
        {
            _fontNames.Value = settings.GetPropertyValue(FontFamilyProperty);
        }


        #endregion
    }
}
