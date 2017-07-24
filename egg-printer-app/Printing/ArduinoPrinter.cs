using System;
using System.Drawing;
using System.Threading.Tasks;
using ArduinoDriver.SerialProtocol;
using Point = System.Drawing.Point;
using PrinterPoint = ArduinoDriver.SerialProtocol.Point;

namespace YuKu.EggPrinter.Printing
{
    public sealed class ArduinoPrinter : Printer
    {
        public ArduinoPrinter(ArduinoDriver.ArduinoDriver driver)
        {
            _driver = driver;
        }

        public event EventHandler<ChangeColorEventArgs> ChangeColorRequested;

        public override Task PenUp()
        {
            var request = new PenRequest(PenState.Up);
            return _driver.SendAsync(request);
        }

        public override Task PenDown()
        {
            var request = new PenRequest(PenState.Down);
            return _driver.SendAsync(request);
        }

        public override Task Move(Point point)
        {
            var request = new MoveRequest(ToPrinterPoint(point));
            return _driver.SendAsync(request);
        }

        public override Task Dot(Point point)
        {
            var request = new DotRequest(ToPrinterPoint(point));
            return _driver.SendAsync(request);
        }

        public override Task Line(Point from, Point to)
        {
            var request = new LineRequest(ToPrinterPoint(from), ToPrinterPoint(to));
            return _driver.SendAsync(request);
        }

#pragma warning disable 1998
        public override async Task SetColor(KnownColor color)
#pragma warning restore 1998
        {
            var eventArgs = new ChangeColorEventArgs(color);
            OnChangeColorRequested(eventArgs);
        }

        protected override Task BeginPrint()
        {
            return _driver.SendAsync(new BeginRequest());
        }

        protected override Task EndPrint()
        {
            return _driver.SendAsync(new EndRequest());
        }

        private PrinterPoint ToPrinterPoint(Point point)
        {
            var printerPoint = new PrinterPoint((Int16) point.X, (Int16) point.Y);
            return printerPoint;
        }

        private void OnChangeColorRequested(ChangeColorEventArgs eventArgs)
        {
            ChangeColorRequested?.Invoke(this, eventArgs);
        }

        private readonly ArduinoDriver.ArduinoDriver _driver;
    }
}
