using System;
using System.Collections;
using System.Collections.Generic;

namespace YuKu.EggPrinter.Printing
{
    public sealed class FunctionPrintSource : IEnumerable<IPrintInstruction>
    {
        public Func<Double, Int16> FunctionX { get; set; }

        public Func<Double, Int16> FunctionY { get; set; }

        public Double ParameterFrom { get; set; } = 0d;

        public Double ParameterTo { get; set; } = Math.PI * 2d;

        public Double ParameterStep { get; set; } = 0.1d;

        public IEnumerator<IPrintInstruction> GetEnumerator()
        {
            Func<Double, Int16> functionX = FunctionX;
            Func<Double, Int16> functionY = FunctionY;
            Double parameterFrom = ParameterFrom;
            Double parameterTo = ParameterTo;
            Double parameterStep = ParameterStep;

            yield return new PenUpInstruction();

            Int16 x = functionX(parameterFrom);
            Int16 y = functionY(parameterFrom);
            yield return new MoveInstruction
            {
                X = x,
                Y = y
            };
            yield return new PenDownInstruction();

            //TODO: Validate parameter step direction.
            for (Double parameter = parameterFrom + parameterStep; parameter < parameterTo; parameter += parameterStep)
            {
                x = functionX(parameter);
                y = functionY(parameter);
                yield return new MoveInstruction
                {
                    X = x,
                    Y = y
                };
            }

            x = functionX(parameterTo);
            y = functionY(parameterTo);
            yield return new MoveInstruction
            {
                X = x,
                Y = y
            };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
