//  BestCS XIMEA Video Library
// BestCS.NET framework
// http://www.sf.net/projects/BestCS/
//
// Copyright © BestCS.NET, 2015-2020
// dinruspro@mail.ru
//

namespace  BestCS.Video.Ximea
{
    using System;

    /// <summary>
    /// XIMEA camera's GPI port modes.
    /// </summary>
    public enum GpiMode : int
    {
        /// <summary>
        /// Input is off.
        /// </summary>
        Off = 0,

        /// <summary>
        /// Trigger input.
        /// </summary>
        Trigger = 1,

        /// <summary>
        /// External signal input.
        /// </summary>
        ExternalEvent = 2
    }
}
