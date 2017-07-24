using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace YuKu.EggPrinter.Printing
{
    public abstract class Printer : IPrinter
    {
        public async Task Print(IEnumerable<IPrintInstruction> source, CancellationToken cancellationToken = default(CancellationToken))
        {
            await BeginPrint();
            try
            {
                foreach (IPrintInstruction printInstruction in source)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    await printInstruction.Execute(this, cancellationToken);
                }
            }
            finally
            {
                await EndPrint();
            }
        }

        public abstract Task PenUp();

        public abstract Task PenDown();

        public abstract Task Move(Point point);

        public abstract Task Dot(Point point);

        public abstract Task Line(Point from, Point to);

        public abstract Task SetColor(KnownColor color);

        protected abstract Task BeginPrint();

        protected abstract Task EndPrint();
    }
}
