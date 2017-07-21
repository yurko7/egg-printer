using System;
using YuKu.EggPrinter.Printing;

namespace YuKu.EggPrinter.ViewModels
{
    internal sealed class MoveInstructionViewModel : InstructionViewModel
    {
        public Int16 X { get; set; }

        public Int16 Y { get; set; }

        internal override IPrintInstruction GetPrintInstruction()
        {
            return new MoveInstruction {X = X, Y = Y};
        }
    }
}
