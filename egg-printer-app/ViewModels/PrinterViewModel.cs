using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
using YuKu.EggPrinter.Printing;

namespace YuKu.EggPrinter.ViewModels
{
    internal sealed class PrinterViewModel : ViewModelBase
    {
        public PrinterViewModel()
        {
            Arduino = new ArduinoViewModel();
            PrintCommand = new AsyncCommand<PrintSourceViewModel>(Print, CanPrint);
        }

        public ArduinoViewModel Arduino { get; }

        public IAsyncCommand PrintCommand { get; }

        private Task Print(PrintSourceViewModel printSourceViewModel)
        {
            IEnumerable<IPrintInstruction> printSource = printSourceViewModel.GetPrintSource();
            var arduinoPrinter = new ArduinoPrinter(Arduino.Driver);
            return arduinoPrinter.Print(printSource);
        }

        private Boolean CanPrint(PrintSourceViewModel printSourceViewModel)
        {
            return Arduino.IsConnected && printSourceViewModel != null;
        }
    }
}
