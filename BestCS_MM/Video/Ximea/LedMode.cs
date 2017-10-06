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
    /// XIMEA camera's LED state options.
    /// </summary>
    public enum LedMode : int
    {
        /// <summary>
        /// Blink if link is ok (led 1), heartbeat mode (led 2).
        /// </summary>
        Heartbeat = 0,

        /// <summary>
        /// Blink led if trigger detected.
        /// </summary>
        TriggerActive = 1,

        /// <summary>
        /// Blink led if external signal detected.
        /// </summary>
        ExternalEventActive = 2,

        /// <summary>
        /// Blink led during data streaming.
        /// </summary>
        Acquisition = 3,

        /// <summary>
        /// Blink led during sensor integration time.
        /// </summary>
        ExposureActive = 4,

        /// <summary>
        /// Blink if device busy/not busy.
        /// </summary>
        FrameActive = 5,

        /// <summary>
        /// Blink led if link is OK.
        /// </summary>
        Link = 6,

        /// <summary>
        /// Turn off LED.
        /// </summary>
        Off = 7,

        /// <summary>
        /// Turn on LED.
        /// </summary>
        On = 8
    }
}
