using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using YuKu.EggPrinter.Printing;

namespace YuKu.EggPrinter.ViewModels
{
    internal sealed class PrintFunctionsViewModel : PrintSourceViewModel
    {
        public PrintFunctionsViewModel()
        {
            Functions = new ObservableCollection<FunctionViewModel>();
            AddFunctionCommand = new DelegateCommand(AddFunction);
            RemoveFunctionCommand = new DelegateCommand<FunctionViewModel>(RemoveFunction);
        }

        public ObservableCollection<FunctionViewModel> Functions { get; }

        public ICommand AddFunctionCommand { get; }

        public ICommand RemoveFunctionCommand { get; }

        private void AddFunction()
        {
            var function = new FunctionViewModel();
            Functions.Add(function);
        }

        private void RemoveFunction(FunctionViewModel function)
        {
            Functions.Remove(function);
        }

        internal override IEnumerable<IPrintInstruction> GetPrintSource()
        {
            return Functions
                .Select(functionViewModel => new FunctionPrintSource
                {
                    FunctionX = functionViewModel.CompiledFunctionX,
                    FunctionY = functionViewModel.CompiledFunctionY,
                    ParameterFrom = functionViewModel.GetParameterFrom(),
                    ParameterTo = functionViewModel.GetParameterTo(),
                    ParameterStep = functionViewModel.ParameterStep
                })
                .SelectMany(printSource => printSource);
        }
    }
}
