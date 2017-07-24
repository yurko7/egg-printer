using System.Drawing;
using System.Threading;
using System.Threading.Tasks;

namespace YuKu.EggPrinter.Printing
{
    public sealed class ChangeColorInstruction : IPrintInstruction
    {
        public KnownColor Color { get; set; }

        public Task Execute(IPrinter printer, CancellationToken cancellationToken = default(CancellationToken))
        {
            return printer.SetColor(Color);
        }
    }
}
