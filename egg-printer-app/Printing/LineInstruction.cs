using System;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace YuKu.EggPrinter.Printing
{
    public sealed class LineInstruction : IPrintInstruction
    {
        public Int16 FromX { get; set; }

        public Int16 FromY { get; set; }

        public Int16 ToX { get; set; }

        public Int16 ToY { get; set; }

        public Task Execute(IPrinter printer, CancellationToken cancellationToken = default(CancellationToken))
        {
            var from = new Point(FromX, FromY);
            var to = new Point(ToX, ToY);
            return printer.Line(from, to);
        }
    }
}
