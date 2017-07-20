using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ArduinoUploader.Hardware;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;

namespace YuKu.EggPrinter.ViewModels
{
    internal sealed class ArduinoViewModel : ViewModelBase
    {
        public ArduinoViewModel()
        {
            Models = new ReadOnlyCollection<ArduinoModel>((ArduinoModel[]) Enum.GetValues(typeof(ArduinoModel)));
            Ports = new ReadOnlyCollection<String>(SerialPort.GetPortNames());
            SelectedModel = ArduinoModel.UnoR3;
            SelectedPort = Ports.FirstOrDefault();
            ConnectCommand = new AsyncCommand(Connect, CanConnect);
            DisconnectCommand = new DelegateCommand(Disconnect, CanDisconnect);
        }

        public IReadOnlyCollection<ArduinoModel> Models { get; }

        public IReadOnlyCollection<String> Ports { get; }

        public ArduinoModel SelectedModel { get; set; }

        public String SelectedPort { get; set; }

        public Boolean IsConnected => _driver != null;

        public IAsyncCommand ConnectCommand { get; }

        public ICommand DisconnectCommand { get; }

        internal ArduinoDriver.ArduinoDriver Driver => _driver;

        private async Task Connect()
        {
            ArduinoModel model = SelectedModel;
            String port = SelectedPort;
            _driver = await ArduinoDriver.ArduinoDriver.CreateAsync(model, port);
        }

        private Boolean CanConnect()
        {
            return !IsConnected && !String.IsNullOrEmpty(SelectedPort);
        }

        private void Disconnect()
        {
            _driver.Dispose();
            _driver = null;
        }

        private Boolean CanDisconnect()
        {
            return IsConnected;
        }

        private ArduinoDriver.ArduinoDriver _driver;
    }
}
