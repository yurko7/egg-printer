using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using DevExpress.Mvvm;
using YuKu.EggPrinter.Printing;

namespace YuKu.EggPrinter.ViewModels
{
    internal sealed class PrintInstructionsViewModel : PrintSourceViewModel
    {
        public PrintInstructionsViewModel()
        {
            Instructions = new ObservableCollection<InstructionViewModel>();
            AddInstructionCommand = new DelegateCommand<Type>(AddInstruction, CanAddInstruction);
            RemoveInstructionCommand = new DelegateCommand<InstructionViewModel>(RemoveInstruction);
        }

        public ObservableCollection<InstructionViewModel> Instructions { get; }

        public ICommand AddInstructionCommand { get; }

        public ICommand RemoveInstructionCommand { get; }

        private void AddInstruction(Type instructionType)
        {
            var instruction = (InstructionViewModel) Activator.CreateInstance(instructionType);
            Instructions.Add(instruction);
        }

        private Boolean CanAddInstruction(Type instructionType)
        {
            return typeof(InstructionViewModel).IsAssignableFrom(instructionType);
        }

        private void RemoveInstruction(InstructionViewModel instruction)
        {
            Instructions.Remove(instruction);
        }

        internal override IEnumerable<IPrintInstruction> GetPrintSource()
        {
            return Instructions.Select(vm => vm.GetPrintInstruction());
        }
    }
}
