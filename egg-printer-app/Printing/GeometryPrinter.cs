using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Media;
#pragma warning disable 1998

namespace YuKu.EggPrinter.Printing
{
    public sealed class GeometryPrinter : Printer
    {
        public Geometry Geometry => _geometry;

        public override async Task PenUp()
        {
            _penDown = false;
        }

        public override async Task PenDown()
        {
            _penDown = true;
            _geometryContext.BeginFigure(new System.Windows.Point(_position.X, _position.Y), false, false);
        }

        public override async Task Move(Point point)
        {
            if (_penDown)
            {
                _geometryContext.LineTo(new System.Windows.Point(point.X, point.Y), true, true);
            }
            _position = point;
        }

        public override async Task Dot(Point point)
        { }

        public override async Task Line(Point from, Point to)
        {
            _geometryContext.BeginFigure(new System.Windows.Point(from.X, from.Y), false, false);
            _geometryContext.LineTo(new System.Windows.Point(to.X, to.Y), true, true);
        }

        public override async Task SetColor(KnownColor color)
        { }

        protected override async Task BeginPrint()
        {
            _geometry = new StreamGeometry();
            _geometryContext = _geometry.Open();
            _geometryContext.BeginFigure(new System.Windows.Point(0d, -400d), true, true);
            _geometryContext.PolyLineTo(new[]
            {
                new System.Windows.Point(0d, 400d),
                new System.Windows.Point(1600d, 400d),
                new System.Windows.Point(1600d, -400d) 
            }, true, true);
        }

        protected override async Task EndPrint()
        {
            _geometryContext.Close();
            _geometryContext = null;
        }

        private StreamGeometry _geometry;
        private StreamGeometryContext _geometryContext;
        private Boolean _penDown;
        private Point _position;
    }
}
