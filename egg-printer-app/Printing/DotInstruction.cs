using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace YuKu.EggPrinter.Printing
{
    public sealed class DotInstruction : IPrintInstruction
    {
        public Int16 X { get; set; }

        public Int16 Y { get; set; }

        public Task Execute(IPrinter printer, CancellationToken cancellationToken = default(CancellationToken))
        {
            var point = new Point(X, Y);
            return printer.Dot(point);
        }
    }
}
