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
            var text = Clipboard.GetText();
            /// If the text is too long, we ignore it.
            /// Currently, URL cannot be longer than 2K,
            /// hence 3000 threshold should be OK.
            if (text.Length > 3000) return;
            /// If no text presented, ignore it.
            if (text.Length < 1) return;
            /// Change textbox if necessary.
            if (!textBoxSrc.Text.Equals(text))
            {
                textBoxSrc.Text = text;
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
