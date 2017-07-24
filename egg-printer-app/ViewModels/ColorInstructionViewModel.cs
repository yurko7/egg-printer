using System.Drawing;
using YuKu.EggPrinter.Printing;

namespace YuKu.EggPrinter.ViewModels
{
    internal sealed class ColorInstructionViewModel : InstructionViewModel
    {
        public KnownColor Color { get; set; }

        internal override IPrintInstruction GetPrintInstruction()
        {
            return new ChangeColorInstruction {Color = Color};
        }
    }
}
