using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
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
            PrintPreviewCommand = new AsyncCommand<PrintSourceViewModel>(PrintPreview, CanPrintPreview);
            PrintCommand = new AsyncCommand<PrintSourceViewModel>(Print, CanPrint);
        }

        public ArduinoViewModel Arduino { get; }

        public Geometry Preview { get; private set; }

        public IAsyncCommand PrintPreviewCommand { get; }

        public IAsyncCommand PrintCommand { get; }

        private async Task PrintPreview(PrintSourceViewModel printSourceViewModel)
        {
            IEnumerable<IPrintInstruction> printSource = printSourceViewModel.GetPrintSource();
            var geometryPrinter = new GeometryPrinter();
            await geometryPrinter.Print(printSource);
            Preview = geometryPrinter.Geometry;
            RaisePropertyChanged(nameof(Preview));
        }

        private Boolean CanPrintPreview(PrintSourceViewModel printSourceViewModel)
        {
            return printSourceViewModel != null;
        }

        private async Task Print(PrintSourceViewModel printSourceViewModel)
        {
            IEnumerable<IPrintInstruction> printSource = printSourceViewModel.GetPrintSource();
            var arduinoPrinter = new ArduinoPrinter(Arduino.Driver);
            arduinoPrinter.ChangeColorRequested += ArduinoPrinterOnChangeColorRequested;
            await arduinoPrinter.Print(printSource);
            arduinoPrinter.ChangeColorRequested -= ArduinoPrinterOnChangeColorRequested;
        }

        private Boolean CanPrint(PrintSourceViewModel printSourceViewModel)
        {
            return Arduino.IsConnected && printSourceViewModel != null;
        }

        private void ArduinoPrinterOnChangeColorRequested(object sender, ChangeColorEventArgs changeColorEventArgs)
        {
            IMessageBoxService messageBox = GetService<IMessageBoxService>();
            messageBox.Show($"Put the {changeColorEventArgs.Color} marker.", "Change Color", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
