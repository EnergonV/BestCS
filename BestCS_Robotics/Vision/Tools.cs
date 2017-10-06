//  BestCS Vision Library
// BestCS.NET framework
// http://www.sf.net/projects/BestCS/
//
// Copyright © BestCS.NET, 2015-2020
// dinruspro@mail.ru
//

namespace  BestCS.Vision
{
    using System;
    using System.Drawing;
    using System.Drawing.Imaging;
    using  BestCS.Imaging;
    using  BestCS.Imaging.Filters;

    internal static class Tools
    {
        // Get grayscale image out of the specified one
        public static void ConvertToGrayscale( UnmanagedImage source, UnmanagedImage destination )
        {
            if ( source.PixelFormat != PixelFormat.Format8bppIndexed )
            {
                Grayscale.CommonAlgorithms.BT709.Apply( source, destination );
            }
            else
            {
                source.Copy( destination );
            }
        }
    }
}
