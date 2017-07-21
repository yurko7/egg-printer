using DevExpress.Mvvm;
using YuKu.EggPrinter.Printing;

namespace YuKu.EggPrinter.ViewModels
{
    internal abstract class InstructionViewModel : BindableBase
    {
        internal abstract IPrintInstruction GetPrintInstruction();
    }
}
