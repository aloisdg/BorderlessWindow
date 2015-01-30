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

        private void InitResizing(object sender, MouseButtonEventArgs e)
        {
            var senderRect = sender as Grid;
            if (senderRect == null) return;

            //Direction = DirectionHelper.Get(senderRect.Tag.ToString());
            ResizeInProcess = true;
            senderRect.CaptureMouse();
        }

        private void EndResizing(object sender, MouseButtonEventArgs e)
        {
            var senderRect = sender as Grid;
            if (senderRect == null) return;

            //Direction = ResizeDirection.None;
            ResizeInProcess = false;
            senderRect.ReleaseMouseCapture();
        }

        private void Resize(object sender, MouseEventArgs e)
        {
            if (!ResizeInProcess) return;
            var senderRect = sender as Grid;
            if (senderRect == null) return;

            senderRect.CaptureMouse();
            //if (senderRect.Cursor.Equals(Cursors.SizeWE))
            if (senderRect.Tag.ToString().Contains("Left") || senderRect.Tag.ToString().Contains("Right"))
            {
                var x = e.GetPosition(this).X + 5;
                if (senderRect.Tag.ToString().Contains("Left"))
                //if (Direction.Equals(ResizeDirection.Left))
                {
                    if (this.Width <= x)
                        return;
                    this.Width -= x;
                    this.Left += x;
                }
                else
                    if (x > 0)
                        this.Width = x;
            }
            //else if (senderRect.Cursor.Equals(Cursors.SizeNS))
            if (senderRect.Tag.ToString().Contains("Top") || senderRect.Tag.ToString().Contains("Bottom"))
            {
                var y = e.GetPosition(this).Y + 5;
                if (senderRect.Tag.ToString().Contains("Top"))
                //if (Direction.Equals(ResizeDirection.Top))
                {
                    if (this.Height <= y)
                        return;
                    this.Height -= y;
                    this.Top += y;
                }
                else
                    if (y > 0)
                        this.Height = y;
            }
        }
    }
}
