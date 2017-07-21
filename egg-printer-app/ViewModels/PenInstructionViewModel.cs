using System;
using YuKu.EggPrinter.Printing;

namespace YuKu.EggPrinter.ViewModels
{
    internal sealed class PenInstructionViewModel : InstructionViewModel
    {
        public Boolean PenDown { get; set; }

        internal override IPrintInstruction GetPrintInstruction()
        {
            return PenDown
                ? (IPrintInstruction) new PenDownInstruction()
                : new PenUpInstruction();
        }
    }
}
