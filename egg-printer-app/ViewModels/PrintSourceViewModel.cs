using System.Collections.Generic;
using DevExpress.Mvvm;
using YuKu.EggPrinter.Printing;

namespace YuKu.EggPrinter.ViewModels
{
    internal abstract class PrintSourceViewModel : ViewModelBase
    {
        internal abstract IEnumerable<IPrintInstruction> GetPrintSource();
    }
}
