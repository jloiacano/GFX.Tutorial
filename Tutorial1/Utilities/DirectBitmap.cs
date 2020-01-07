using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GFX.Tutorial.Utilities
{
    public class DirectBitmap
    {
        #region // storage

        public Size Size { get; }

        public int Width => Size.Width;

        public int Height => Size.Height;

        public int[] Buffer { get; private set; }

        private GCHandle BufferHandle { get; set; }

        public Bitmap Bitmap { get; private set; }

        public Graphics Graphics { get; private set; }

        #endregion

        #region // constructor

        public DirectBitmap(Size size)
        {
            Size = size;
            Buffer = new int[Width * Height];
            BufferHandle = GCHandle.Alloc(Buffer, GCHandleType.Pinned);
            Bitmap = new Bitmap(Width, Height, Width * 4, PixelFormat.Format32bppPArgb, BufferHandle.AddrOfPinnedObject());
            Graphics = Graphics.FromImage(Bitmap);
        }

        public DirectBitmap(int width, int height) :
            this(new Size(width, height))
        {
        }

        public void Dispose()
        {
            DisposeGraphics();
            DisposeBitmap();
            DisposeBufferHandle();

            Buffer = default;
        }

        #region // Disposal Helpers

        private void DisposeGraphics()
        {
            if (Graphics == null)
            {
                throw new NullReferenceException("Graphics in Utilities\\DirectBitmap is NULL");
            }
            Graphics.Dispose();
            Graphics = default;
        }

        private void DisposeBitmap()
        {
            if (Bitmap == null)
            {
                throw new NullReferenceException("Bitmap in Utilities\\DirectBitmap is NULL");
            }
            Bitmap.Dispose();
            Bitmap = default;
        }

        private void DisposeBufferHandle()
        {
            if (BufferHandle == null)
            {
                throw new NullReferenceException("BufferHandle in Utilities\\DirectBitmap is NULL");
            }
            BufferHandle.Free();
            BufferHandle = default;
        }

        #endregion

        #endregion

        #region // routines

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetIndex(int x, int y) => x + y * Width;
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetXY(int index, out int x, out int y)
        {
            y = index / Width;
            x = index - y * Width;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetArgb(int x, int y, int argb) => Buffer[GetIndex(x, y)] = argb;

        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetArgb(int x, int y) => Buffer[GetIndex(x, y)];


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPixel(int x, int y, Color color) => SetArgb(x, y, color.ToArgb());


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Color GetPixel(int x, int y) => Color.FromArgb(GetArgb(x, y));



        #endregion
    }
}
