using System.Threading;
using System.Threading.Tasks;

namespace YuKu.EggPrinter.Printing
{
    public sealed class PenUpInstruction : IPrintInstruction
    {
        public Task Execute(IPrinter printer, CancellationToken cancellationToken = default(CancellationToken))
        {
            return printer.PenUp();
        }
    }
}
