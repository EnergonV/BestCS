//  BestCS XIMEA Video Library
// BestCS.NET framework
// http://www.sf.net/projects/BestCS/
//
// Copyright © BestCS.NET, 2015-2020
// dinruspro@mail.ru
//

using System;
using System.Runtime.InteropServices;

namespace  BestCS.Video.Ximea.Internal
{
    [StructLayout( LayoutKind.Sequential )]
    internal struct XimeaImage
    {
        public int StructSize;
        public IntPtr BitmapData;
        public int Size;
        public ImageFormat PixelFormat;
        public int Width;
        public int Height;
        public int FrameNumber;
    }
}
