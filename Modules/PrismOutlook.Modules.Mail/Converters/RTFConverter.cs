using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace PrismOutlook.Modules.Mail.Converters
{
    [ValueConversion(typeof(string), typeof(string))]
    public class RTFConverter : IValueConverter
    {
        RichTextBox rtBox = new RichTextBox();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string rtf = (string)value;
            convertString_RTF(rtf, rtBox);
            TextRange textRange = new TextRange(rtBox.Document.ContentStart, rtBox.Document.ContentEnd);
            return textRange.Text;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }

        public static string convertString_RTF(string text, RichTextBox rtb)
        {
            string rtfText = text;
            byte[] byteArray = Encoding.ASCII.GetBytes(rtfText);
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(byteArray))
            {
                System.Windows.Documents.TextRange tr = new System.Windows.Documents.TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
                tr.Load(ms, System.Windows.DataFormats.Rtf);
            }
            return null;
        }
    }
}
