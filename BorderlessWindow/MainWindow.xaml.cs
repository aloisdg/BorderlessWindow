using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BorderlessWindow
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool ResizeInProcess { get; set; }
        //private ResizeDirection Direction { get; set; }

        // https://msdn.microsoft.com/en-us/library/system.windows.forms.anchorstyles.aspx

        public MainWindow()
        {
            InitializeComponent();

            ResizeInProcess = false;
        }

        #region move

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                return;
            base.OnMouseLeftButtonDown(e);
            this.Cursor = Cursors.SizeAll;
            this.DragMove();
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Cursor = null;
        }

        #endregion

        #region resize

        private void InitResizing(object sender, MouseButtonEventArgs e)
        {
            var senderRect = sender as Grid;
            if (senderRect == null) return;

            ResizeInProcess = true;
            senderRect.CaptureMouse();
        }

        private void EndResizing(object sender, MouseButtonEventArgs e)
        {
            var senderRect = sender as Grid;
            if (senderRect == null) return;

            ResizeInProcess = false;
            senderRect.ReleaseMouseCapture();
        }

        private void Resize(object sender, MouseEventArgs e)
        {
            if (!ResizeInProcess) return;
            var senderRect = sender as Grid;
            if (senderRect == null) return;

            senderRect.CaptureMouse();
            if (senderRect.Tag.ToString().Contains("Left") || senderRect.Tag.ToString().Contains("Right"))
            {
                var x = e.GetPosition(this).X + 5;
                if (senderRect.Tag.ToString().Contains("Left"))
                {
                    if (this.Width <= x) return;
                    this.Width -= x;
                    this.Left += x;
                }
                else
                    if (x > 0)
                        this.Width = x;
            }
            if (senderRect.Tag.ToString().Contains("Top") || senderRect.Tag.ToString().Contains("Bottom"))
            {
                var y = e.GetPosition(this).Y + 5;
                if (senderRect.Tag.ToString().Contains("Top"))
                {
                    if (this.Height <= y) return;
                    this.Height -= y;
                    this.Top += y;
                }
                else
                    if (y > 0)
                        this.Height = y;
            }
        }

        #endregion
    }


    //// http://stackoverflow.com/questions/4628882/wpf-window-setbounds
    //private IntPtr _handle;
    //private void SetBounds(int left, int top, int width, int height)
    //{
    //    if (_handle == IntPtr.Zero)
    //        _handle = new WindowInteropHelper(this).Handle;

    //    SetWindowPos(_handle, IntPtr.Zero, left, top, width, height, 0);
    //}

    //[DllImport("user32")]
    //static extern bool SetWindowPos(
    //    IntPtr hWnd,
    //    IntPtr hWndInsertAfter,
    //    int x,
    //    int y,
    //    int cx,
    //    int cy,
    //    uint uFlags);

    // source
    // https://social.msdn.microsoft.com/Forums/vstudio/en-US/f3d6b2f9-4843-4025-b8ed-b07bcfc75a6b/wpf-borderless-form-resizing-issues?forum=wpf
    // http://blog.creativeitp.com/posts-and-articles/c-sharp/simple-methods-to-drag-and-resize-your-c-transparent-wpf-application-with-the-windowstyle-property-set-to-none/

    // doc
    // https://msdn.microsoft.com/en-us/library/system.windows.input.cursors(v=vs.110).aspx

    // other
    // https://wpfborderless.codeplex.com/
}
