using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WPF_draw_QrCode
{
    /// ----------------------------------------------------------------
    /// Important:
    /// http://qrcodenet.codeplex.com/discussions/444153
    /// This shows that the reference currently cannot support .Net 4.5,
    /// hence we use .Net 4 for this project.
    /// ----------------------------------------------------------------

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            /// Added clipboard notification to this window.
            ClipboardNotification.ClipboardUpdate += ClipboardNotification_ClipboardUpdate;
        }

        /// <summary>
        /// Event handler when the clipboard was updated.
        /// </summary>
        /// <param name="sender">Parameter not used.</param>
        /// <param name="e">Parameter not used.</param>
        private void ClipboardNotification_ClipboardUpdate(object sender, EventArgs e)
        {
            try {
                /// "OpenClipboard Failed (Exception from HRESULT: 0x800401D0 (CLIPBRD_E_CANT_OPEN))"
                /// The above error might occur when executing next statement.
                /// So catch this exception and handle it by showing message in textbox.
                var text = Clipboard.GetText();
                /// If the text is too long, we ignore it.
                /// Currently, URL cannot be longer than 2K,
                /// hence 3000 threshold should be OK.
                /// Changed threshold to 2060.
                /// Since I found a 2423 long message would not show the pic.
                /// And URL is limited to 2048.
                ///   * http://en.wikipedia.org/wiki/QR_code
                ///   Maximum character storage capacity (40-L)
                ///   Numeric only      Max. 7,089 characters(0, 1, 2, 3, 4, 5, 6, 7, 8, 9)
                ///   Alphanumeric      Max. 4,296 characters(0–9, A–Z[upper -case only], space, $, %, *, +, -, ., /, :)
                ///   Binary / byte     Max. 2,953 characters(8 - bit bytes)(23624 bits)
                ///   Kanji / Kana      Max. 1,817 characters
                ///  http://www.qrcode.com/en/about/version.html
                ///  has another detailed max chars.
                ///  Version 40(177x177) with H can have upto 1852 Alphanumeric
                ///  with M can have upto 2331 Bytes.
                ///  This is confirmed by trying 2331&2332 length text.
                ///  Work for 2331 but not for 2332.
                ///  So setting 2100 is OK.
                if (text.Length > 2100) return;
                /// If no text presented, ignore it.
                if (text.Length < 1) return;
                /// Change textbox if necessary.
                if (!textBoxSrc.Text.Equals(text))
                {
                    textBoxSrc.Text = text;
                }
            }
            catch(System.Runtime.InteropServices.COMException come)
            {
                textBoxSrc.Text = come.Message;
            }
        }

        /// <summary>
        /// Added event for moving the window.
        /// </summary>
        /// <param name="sender">Parameter not used.</param>
        /// <param name="e">Parameter not used.</param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
