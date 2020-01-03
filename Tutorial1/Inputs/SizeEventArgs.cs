using System;
using System.Drawing;

namespace GFX.Tutorial.Inputs
{
    public class SizeEventArgs :
        EventArgs,
        ISizeEventArgs
    {
        #region // storage
        public Size UpdatedSize { get; set; }

        #endregion

        #region // constructor

        public SizeEventArgs(Size updatedSize)
        {
            UpdatedSize = updatedSize;
        }

        #endregion

    }
}
