using System;

namespace YuKu.EggPrinter.ViewModels
{
    internal sealed class Egg3DViewModel : Preview3DViewModel
    {
        protected override void GetYR(Double phi, out Double y, out Double r)
        {
            const Double a = 2d;
            const Double b = 0.6d * a;
            phi = Math.PI - phi;
            Double cosPhi = Math.Cos(phi);
            Double sinPhi = Math.Sin(phi);
            r = (a / 2d - b / 4d * (1d - cosPhi)) * sinPhi;
            y = -(a / 2d - b / 4d * (1d - cosPhi)) * (1d + cosPhi) + 1d;
        }

        protected override void GetXZ(Double theta, out Double x, out Double z)
        {
            x = Math.Cos(theta);
            z = Math.Sin(theta);
        }
    }
}
