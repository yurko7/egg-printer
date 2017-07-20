using System.Threading;
using System.Threading.Tasks;

namespace YuKu.EggPrinter.Printing
{
    public interface IPrintInstruction
    {
        Task Execute(IPrinter printer, CancellationToken cancellationToken = default(CancellationToken));
    }
}
