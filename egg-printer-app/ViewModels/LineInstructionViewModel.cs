using System;
using YuKu.EggPrinter.Printing;

namespace YuKu.EggPrinter.ViewModels
{
    internal sealed class LineInstructionViewModel : InstructionViewModel
    {
        public Int16 FromX { get; set; }

        public Int16 FromY { get; set; }

        public Int16 ToX { get; set; }

        public Int16 ToY { get; set; }

        internal override IPrintInstruction GetPrintInstruction()
        {
            return new LineInstruction {FromX = FromX, FromY = FromY, ToX = ToX, ToY = ToY};
        }
    }
}
