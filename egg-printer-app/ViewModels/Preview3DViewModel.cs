using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using DevExpress.Mvvm;

namespace YuKu.EggPrinter.ViewModels
{
    internal abstract class Preview3DViewModel : BindableBase, ISupportInitialize
    {
        public Int32 Parallels { get; set; } = 16;

        public Int32 Meridians { get; set; } = 32;

        public Point3DCollection Positions { get; private set; }

        public Int32Collection TriangleIndices { get; private set; }

        public PointCollection TextureCoordinates { get; private set; }

        void ISupportInitialize.BeginInit()
        {
            _initializing = true;
        }

        void ISupportInitialize.EndInit()
        {
            if (_initializing)
            {
                Initialize(Parallels, Meridians);
                _initializing = false;
            }
        }

        private void Initialize(Int32 parallels, Int32 meridians)
        {
            Positions = InitializePositions(parallels, meridians);
            TriangleIndices = InitializeTriangleIndices(parallels, meridians);
            TextureCoordinates = InitializeTextureCoordinates(parallels, meridians);
        }

        protected abstract void GetYR(Double phi, out Double y, out Double r);

        protected abstract void GetXZ(Double theta, out Double x, out Double z);

        private Point3DCollection InitializePositions(Int32 parallels, Int32 meridians)
        {
            var positions = new Point3DCollection(parallels * (meridians + 1) + 2);

            Double dPhi = Math.PI / (parallels + 1);
            Double dTheta = 2d * Math.PI / meridians;

            positions.Add(new Point3D(0, 1, 0));
            for (Int32 parallel = 1; parallel <= parallels; parallel++)
            {
                Double phi = dPhi * parallel;
                Double y, r;
                GetYR(phi, out y, out r);
                for (Int32 meridian = 0; meridian <= meridians; meridian++)
                {
                    Double theta = dTheta * meridian;
                    Double x, z;
                    GetXZ(theta, out x, out z);
                    positions.Add(new Point3D(x * r, y, z * r));
                }
            }
            positions.Add(new Point3D(0, -1, 0));

            return positions;
        }

        private Int32Collection InitializeTriangleIndices(Int32 parallels, Int32 meridians)
        {
            var triangleIndices = new Int32Collection();

            for (Int32 i = 1; i <= meridians; i++)
            {
                triangleIndices.Add(0);
                triangleIndices.Add(i + 1);
                triangleIndices.Add(i);
            }
            for (Int32 parallel = 0; parallel < parallels - 1; parallel++)
            {
                for (Int32 meridian = 1; meridian <= meridians; meridian++)
                {
                    Int32 baseIndex = parallel * (meridians + 1);
                    triangleIndices.Add(baseIndex + meridian);
                    triangleIndices.Add(baseIndex + meridian + 1);
                    triangleIndices.Add(baseIndex + meridians + meridian + 1);

                    triangleIndices.Add(baseIndex + meridian + 1);
                    triangleIndices.Add(baseIndex + meridians + meridian + 2);
                    triangleIndices.Add(baseIndex + meridians + meridian + 1);
                }
            }
            Int32 lastPosition = parallels * (meridians + 1) + 1;
            for (Int32 i = 1; i <= meridians; i++)
            {
                triangleIndices.Add(lastPosition - (i + 1));
                triangleIndices.Add(lastPosition - i);
                triangleIndices.Add(lastPosition);
            }

            return triangleIndices;
        }

        private PointCollection InitializeTextureCoordinates(Int32 parallels, Int32 meridians)
        {
            var textureCoordinates = new PointCollection();

            Double dPhi = Math.PI / (parallels + 1);

            textureCoordinates.Add(new Point(0.5d, 1d));
            for (Int32 parallel = 1; parallel <= parallels; parallel++)
            {
                Double phi = dPhi * parallel;
                Double y, r;
                GetYR(phi, out y, out r);
                for (Int32 meridian = 0; meridian <= meridians; meridian++)
                {
                    textureCoordinates.Add(new Point(1d - (Double)meridian / meridians, y));
                }
            }
            textureCoordinates.Add(new Point(0.5d, -1d));

            return textureCoordinates;
        }

        private Boolean _initializing;
    }
}
