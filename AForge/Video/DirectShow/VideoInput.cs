﻿//  BestCS Direct Show Library
// BestCS.NET framework
// http://www.sf.net/projects/BestCS/
//
// Copyright © BestCS.NET, 2009-2012
// dinruspro@mail.ru
//

namespace  BestCS.Video.DirectShow
{
    using System;

    /// <summary>
    /// Video input of a capture board.
    /// </summary>
    /// 
    /// <remarks><para>The class is used to describe video input of devices like video capture boards,
    /// which usually provide several inputs.</para>
    /// </remarks>
    /// 
    public class VideoInput
    {
        /// <summary>
        /// Index of the video input.
        /// </summary>
        public readonly int Index;

        /// <summary>
        /// Type of the video input.
        /// </summary>
        public readonly PhysicalConnectorType Type;

        internal VideoInput( int index, PhysicalConnectorType type )
        {
            Index = index;
            Type = type;
        }

        /// <summary>
        /// Default video input. Used to specify that it should not be changed.
        /// </summary>
        public static VideoInput Default
        {
            get { return new VideoInput( -1, PhysicalConnectorType.Default ); }
        }
    }
}
