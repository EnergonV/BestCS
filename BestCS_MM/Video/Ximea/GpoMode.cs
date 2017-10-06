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
    /// XIMEA camera's GPO port modes.
    /// </summary>
    public enum GpoMode : int
    {
        /// <summary>
        /// Output off.
        /// </summary>
        Off = 0,

        /// <summary>
        /// Logical level.
        /// </summary>
        On = 1,

        /// <summary>
        /// High during exposure (integration) time + readout time + data transfer time.
        /// </summary>
        FrameActive = 2,

        /// <summary>
        /// Low during exposure (integration) time + readout time + data trasnfer time. 
        /// </summary>
        FrameActiveNew = 3,

        /// <summary>
        /// High during exposure(integration) time.
        /// </summary>
        ExposureActive = 4,

        /// <summary>
        /// Low during exposure(integration) time.
        /// </summary>
        ExposureActiveNeg = 5
    }
}
