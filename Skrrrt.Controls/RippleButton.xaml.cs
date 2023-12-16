using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Skrrrt.Controls
{
    /// <summary>
    /// Interaction logic for RippleButton.xaml
    /// </summary>
    public partial class RippleButton : Button
    {
        private Storyboard _storyboard;
        private Point _mousePosition;
        private Ellipse _ellipse;

        public static readonly DependencyProperty RippleBrushProperty = DependencyProperty.Register("RippleBrush", typeof(SolidColorBrush), typeof(RippleButton), new PropertyMetadata(new SolidColorBrush(Colors.Gray)));
        public SolidColorBrush RippleBrush
        {
            get { return (SolidColorBrush)GetValue(RippleBrushProperty); }
            set { SetValue(RippleBrushProperty, value); }
        }

        public RippleButton()
        {
            InitializeComponent();

            // Get storyboard from RippleButton
            _storyboard = (Storyboard)this.Resources["RippleEffect"];
        }

        private async void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Canvas _canvas = (Canvas)sender as Canvas;
            if (_canvas != null)
            {
                // Remove the previous storyboard
                _storyboard.Remove();

                _mousePosition = e.GetPosition(_canvas);

                // Get the ellipse from the canvas
                _ellipse = _canvas.Children[1] as Ellipse;

                // Move the ellipse to the point where the mouse was clicked
                _ellipse.SetValue(Canvas.LeftProperty, _mousePosition.X - _ellipse.ActualWidth / 2);
                _ellipse.SetValue(Canvas.TopProperty, _mousePosition.Y - _ellipse.ActualHeight / 2);

                await Task.Delay(100);

                // Attach storyboard to the ellipse
                Storyboard.SetTarget(_storyboard, _ellipse);
                _storyboard.Begin();
            }
        }

        private void Storyboard_Completed(object sender, EventArgs e)
        {
            _storyboard.Remove();
        }
    }
}
