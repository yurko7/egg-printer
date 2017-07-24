using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace YuKu.EggPrinter.Printing
{
    public sealed class BinaryImagePrintSource : IEnumerable<IPrintInstruction>
    {
        public BinaryImagePrintSource(Bitmap bitmap)
        {
            _bitmap = bitmap;
        }

        public Int16 PenWidth { get; set; } = 4;

        public IEnumerator<IPrintInstruction> GetEnumerator()
        {
            Color black = Color.FromArgb(0xff, 0x00, 0x00, 0x00);
            Int16 height = (Int16) _bitmap.Height;
            Int16 yOffset = (Int16) (height / 2);
            Int16 scale = PenWidth;
            for (Int16 x = 0; x < _bitmap.Width; x++)
            {
                for (Int16 y = 0; y < height; y++)
                {
                    if (_bitmap.GetPixel(x, height - 1 - y) == black)
                    {
                        Int16 blackCount = 1;
                        while (++y < height && _bitmap.GetPixel(x, height - 1 - y) == black)
                        {
                            blackCount++;
                        }
                        var instruction = blackCount == 1
                            ? (IPrintInstruction) new DotInstruction
                            {
                                X = (Int16) (x * scale),
                                Y = (Int16) ((y - 1 - yOffset) * scale)
                            }
                            : new LineInstruction
                            {
                                FromX = (Int16) (x * scale),
                                FromY = (Int16) ((y - blackCount - yOffset) * scale),
                                ToX = (Int16) (x * scale),
                                ToY = (Int16) ((y - 1 - yOffset) * scale)
                            };
                        yield return instruction;
                    }
                }

                if (++x == _bitmap.Width)
                {
                    break;
                }
                for (Int16 y = (Int16) (height - 1); y >= 0; y--)
                {
                    if (_bitmap.GetPixel(x, height - 1 - y) == black)
                    {
                        Int16 blackCount = 1;
                        while (--y >= 0 && _bitmap.GetPixel(x, height - 1 - y) == black)
                        {
                            blackCount++;
                        }
                        var instruction = blackCount == 1
                            ? (IPrintInstruction) new DotInstruction
                            {
                                X = (Int16) (x * scale),
                                Y = (Int16) ((y + 1 - yOffset) * scale)
                            }
                            : new LineInstruction
                            {
                                FromX = (Int16) (x * scale),
                                FromY = (Int16) ((y + blackCount - yOffset) * scale),
                                ToX = (Int16) (x * scale),
                                ToY = (Int16) ((y + 1 - yOffset) * scale)
                            };
                        yield return instruction;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private readonly Bitmap _bitmap;
    }
}
