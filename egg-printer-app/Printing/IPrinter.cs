using System.Drawing;
using System.Threading.Tasks;

namespace YuKu.EggPrinter.Printing
{
    public interface IPrinter
    {
        Task PenUp();

        Task PenDown();

        Task Move(Point point);

        Task Dot(Point point);

        Task Line(Point from, Point to);

        Task SetColor(KnownColor color);
    }
}