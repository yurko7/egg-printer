using System;
using DevExpress.Mvvm;
using YuKu.MathExpression;

namespace YuKu.EggPrinter.ViewModels
{
    internal sealed class FunctionViewModel : BindableBase
    {
        public FunctionViewModel()
        {
            ParameterFrom = "0";
            ParameterTo = "2PI";
            ParameterStep = 0.1;
        }

        public String FunctionX
        {
            get { return _functionX; }
            set
            {
                if (_functionX != value)
                {
                    _functionX = value;
                    Func<Double, Double> compiled = _functionX.Compile<Func<Double, Double>>("t");
                    _compiledFunctionX = parameter => (Int16) Math.Round(compiled(parameter));
                }
            }
        }

        public String FunctionY
        {
            get { return _functionY; }
            set
            {
                if (_functionY != value)
                {
                    _functionY = value;
                    Func<Double, Double> compiled = _functionY.Compile<Func<Double, Double>>("t");
                    _compiledFunctionY = parameter => (Int16) Math.Round(compiled(parameter));
                }
            }
        }

        public String ParameterFrom
        {
            get { return _parameterFrom; }
            set
            {
                if (_parameterFrom != value)
                {
                    _parameterFrom = value;
                    _compiledParameterFrom = _parameterFrom.Compile<Func<Double>>();
                }
            }
        }

        public String ParameterTo
        {
            get { return _parameterTo; }
            set
            {
                if (_parameterTo != value)
                {
                    _parameterTo = value;
                    _compiledParameterTo = _parameterTo.Compile<Func<Double>>();
                }
            }
        }

        public Double ParameterStep { get; set; }

        internal Func<Double, Int16> CompiledFunctionX => _compiledFunctionX;

        internal Func<Double, Int16> CompiledFunctionY => _compiledFunctionY;

        internal Double GetParameterFrom()
        {
            return _compiledParameterFrom();
        }

        internal Double GetParameterTo()
        {
            return _compiledParameterTo();
        }

        private String _functionX;
        private Func<Double, Int16> _compiledFunctionX;
        private String _functionY;
        private Func<Double, Int16> _compiledFunctionY;
        private String _parameterFrom;
        private Func<Double> _compiledParameterFrom;
        private String _parameterTo;
        private Func<Double> _compiledParameterTo;
    }
}
