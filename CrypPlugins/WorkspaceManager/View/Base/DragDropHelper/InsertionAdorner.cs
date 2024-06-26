﻿using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace DragDropListBox
{
    public class InsertionAdorner : Adorner
    {
        private readonly bool isSeparatorHorizontal;
        public bool IsInFirstHalf { get; set; }
        private readonly AdornerLayer adornerLayer;
        private static readonly Pen pen;
        private static readonly PathGeometry triangle;

        // Create the pen and triangle in a static constructor and freeze them to improve performance.
        static InsertionAdorner()
        {
            pen = new Pen { Brush = Brushes.Gray, Thickness = 2 };
            pen.Freeze();

            LineSegment firstLine = new LineSegment(new Point(0, -5), false);
            firstLine.Freeze();
            LineSegment secondLine = new LineSegment(new Point(0, 5), false);
            secondLine.Freeze();

            PathFigure figure = new PathFigure { StartPoint = new Point(5, 0) };
            figure.Segments.Add(firstLine);
            figure.Segments.Add(secondLine);
            figure.Freeze();

            triangle = new PathGeometry();
            triangle.Figures.Add(figure);
            triangle.Freeze();
        }

        public InsertionAdorner(bool isSeparatorHorizontal, bool isInFirstHalf, UIElement adornedElement, AdornerLayer adornerLayer)
            : base(adornedElement)
        {
            this.isSeparatorHorizontal = isSeparatorHorizontal;
            IsInFirstHalf = isInFirstHalf;
            this.adornerLayer = adornerLayer;
            IsHitTestVisible = false;

            this.adornerLayer.Add(this);
        }

        // This draws one line and two triangles at each end of the line.
        protected override void OnRender(DrawingContext drawingContext)
        {

            CalculateStartAndEndPoint(out Point startPoint, out Point endPoint);
            drawingContext.DrawLine(pen, startPoint, endPoint);

            if (isSeparatorHorizontal)
            {
                DrawTriangle(drawingContext, startPoint, 0);
                DrawTriangle(drawingContext, endPoint, 180);
            }
            else
            {
                DrawTriangle(drawingContext, startPoint, 90);
                DrawTriangle(drawingContext, endPoint, -90);
            }
        }

        private void DrawTriangle(DrawingContext drawingContext, Point origin, double angle)
        {
            drawingContext.PushTransform(new TranslateTransform(origin.X, origin.Y));
            drawingContext.PushTransform(new RotateTransform(angle));

            drawingContext.DrawGeometry(pen.Brush, null, triangle);

            drawingContext.Pop();
            drawingContext.Pop();
        }

        private void CalculateStartAndEndPoint(out Point startPoint, out Point endPoint)
        {
            startPoint = new Point();
            endPoint = new Point();

            double width = AdornedElement.RenderSize.Width;
            double height = AdornedElement.RenderSize.Height;

            if (isSeparatorHorizontal)
            {
                endPoint.X = width;
                if (!IsInFirstHalf)
                {
                    startPoint.Y = height;
                    endPoint.Y = height;
                }
            }
            else
            {
                endPoint.Y = height;
                if (!IsInFirstHalf)
                {
                    startPoint.X = width;
                    endPoint.X = width;
                }
            }
        }

        public void Detach()
        {
            adornerLayer.Remove(this);
        }

    }
}
