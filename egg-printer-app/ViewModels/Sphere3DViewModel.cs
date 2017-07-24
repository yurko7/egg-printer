using System;

namespace YuKu.EggPrinter.ViewModels
{
    internal sealed class Sphere3DViewModel : Preview3DViewModel
    {
        protected override void GetYR(Double phi, out Double y, out Double r)
        {
            y = Math.Cos(phi);
            r = Math.Sin(phi);
        }

        protected override void GetXZ(Double theta, out Double x, out Double z)
        {
            x = Math.Cos(theta);
            z = Math.Sin(theta);
        }
    }
}
