using System;
using System.Drawing;

namespace YuKu.EggPrinter.Printing
{
    public sealed class ChangeColorEventArgs : EventArgs
    {
        public ChangeColorEventArgs(KnownColor color)
        {
            Color = color;
        }

        public KnownColor Color { get; }
    }
}
