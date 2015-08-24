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
    /// Enumeration of camera's trigger modes.
    /// </summary>
    public enum TriggerSource : int
    {
        /// <summary>
        /// Camera works in free run mode.
        /// </summary>
        Off = 0,

        /// <summary>
        /// External trigger (rising edge).
        /// </summary>
        EdgeRising = 1,

        /// <summary>
        /// External trigger (falling edge).
        /// </summary>
        EdgeFalling = 2,

        /// <summary>
        /// Software (manual) trigger.
        /// </summary>
        Software = 3
    }
}
