using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CommunityCalender.Controls
{
    /// <summary>
    /// A wrapping panel for Silverlight to allow the easy layout
    /// of the days in a month. Credit for the code here goes to 
    /// http://www.codeproject.com/KB/silverlight/WrapPanelSilverlight.aspx
    /// </summary>
    public class WrapPanel : Panel
    {

        public Orientation Orientation
        {
            get { return (Orientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        public static readonly DependencyProperty OrientationProperty =
          DependencyProperty.Register("Orientation",
          typeof(Orientation), typeof(WrapPanel), null);

        public WrapPanel()
        {
            // default orientation
            Orientation = Orientation.Horizontal;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            foreach (UIElement child in Children)
            {
                child.Measure(new Size(availableSize.Width, availableSize.Height));
            }

            return base.MeasureOverride(availableSize);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Point point = new Point(0, 0);
            int i = 0;

            if (Orientation == Orientation.Horizontal)
            {
                double largestHeight = 0.0;

                foreach (UIElement child in Children)
                {
                    child.Arrange(new Rect(point, new Point(point.X +
                      child.DesiredSize.Width, point.Y + child.DesiredSize.Height)));

                    if (child.DesiredSize.Height > largestHeight)
                        largestHeight = child.DesiredSize.Height;

                    point.X = point.X + child.DesiredSize.Width;

                    if ((i + 1) < Children.Count)
                    {
                        int controlCount = i + 1;

                        if (ControlPassesPageEdge(i, finalSize.Width, point.X) || SevenControlsInRow(controlCount))
                        {
                            point.X = 0;
                            point.Y = point.Y + largestHeight;
                            largestHeight = 0.0;
                        }
                    }
                    i++;
                }
            }
            else
            {
                double largestWidth = 0.0;

                foreach (UIElement child in Children)
                {
                    child.Arrange(new Rect(point, new Point(point.X +
                      child.DesiredSize.Width, point.Y + child.DesiredSize.Height)));

                    if (child.DesiredSize.Width > largestWidth)
                        largestWidth = child.DesiredSize.Width;

                    point.Y = point.Y + child.DesiredSize.Height;

                    if ((i + 1) < Children.Count)
                    {
                        if ((point.Y + Children[i + 1].DesiredSize.Height) > finalSize.Height)
                        {
                            point.Y = 0;
                            point.X = point.X + largestWidth;
                            largestWidth = 0.0;
                        }
                    }

                    i++;
                }
            }

            return base.ArrangeOverride(finalSize);
        }

        private bool SevenControlsInRow(int controlCount)
        {
            return ((controlCount > 0) && ((controlCount % 7) == 0));                    
        }

        private bool ControlPassesPageEdge(int controlCount, double maxWidth, double xCoord)
        {
            if (controlCount + 1 > Children.Count)
            {
                return false;
            }

            return (xCoord + Children[controlCount + 1].DesiredSize.Width > maxWidth);
        }
    }
}
