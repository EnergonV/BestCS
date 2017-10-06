// Image Processing Prototyper
// BestCS.NET framework
// http://www.aforgenet.com/framework/
//
// Copyright © BestCS.NET, 2010-2011
// contacts@aforgenet.com
//

namespace  BestCS.Imaging.IPPrototyper
{
    using System;
    using System.Drawing;

    /// <summary>
    /// Interface to implement by image processing routine.
    /// </summary>
    public interface IImageProcessingRoutine
    {
        /// <summary>
        /// Image processing routine's name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Perform image processing routine.
        /// </summary>
        /// 
        /// <param name="image">Source image to perform image processing on.</param>
        /// <param name="log">Logger to use to put information about image processing steps/results.</param>
        /// 
        void Process( Bitmap image, IImageProcessingLog log );
    }
}
