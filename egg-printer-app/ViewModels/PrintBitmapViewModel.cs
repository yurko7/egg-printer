using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using DevExpress.Mvvm;
using YuKu.EggPrinter.Printing;

namespace YuKu.EggPrinter.ViewModels
{
    internal sealed class PrintBitmapViewModel : PrintSourceViewModel
    {
        public PrintBitmapViewModel()
        {
            LoadBitmapCommand = new DelegateCommand(LoadBitmap);
        }

        public BitmapSource Bitmap { get; private set; }

        public ICommand LoadBitmapCommand { get; }

        private void LoadBitmap()
        {
            IOpenFileDialogService openFileDialog = GetService<IOpenFileDialogService>();
            if (openFileDialog.ShowDialog())
            {
                _bitmap = (Bitmap) Image.FromFile(openFileDialog.GetFullFileName());

                Bitmap = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    _bitmap.GetHbitmap(),
                    IntPtr.Zero,
                    System.Windows.Int32Rect.Empty,
                    BitmapSizeOptions.FromWidthAndHeight(_bitmap.Width, _bitmap.Height));
                RaisePropertyChanged(nameof(Bitmap));
            }
        }

        internal override IEnumerable<IPrintInstruction> GetPrintSource()
        {
            return new BinaryImagePrintSource(_bitmap);
        }

        private Bitmap _bitmap;
    }
}
