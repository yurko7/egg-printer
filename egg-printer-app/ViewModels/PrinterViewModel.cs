using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
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

        private async Task Print(PrintSourceViewModel printSourceViewModel)
        {
            IEnumerable<IPrintInstruction> printSource = printSourceViewModel.GetPrintSource();
            var arduinoPrinter = new ArduinoPrinter(Arduino.Driver);
            arduinoPrinter.ChangeColorRequested += ArduinoPrinterOnChangeColorRequested;
            await arduinoPrinter.Print(printSource);
            arduinoPrinter.ChangeColorRequested -= ArduinoPrinterOnChangeColorRequested;
        }

        private void ArduinoPrinterOnChangeColorRequested(object sender, ChangeColorEventArgs changeColorEventArgs)
        {
            IMessageBoxService messageBox = GetService<IMessageBoxService>();
            messageBox.Show($"Put the {changeColorEventArgs.Color} marker.", "Change Color", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private Boolean CanPrint(PrintSourceViewModel printSourceViewModel)
        {
            return Arduino.IsConnected && printSourceViewModel != null;
        }
    }
}
