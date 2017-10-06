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
    internal static class XimeaAPI
    {
        [DllImport( "m3api.dll" )]
        public static extern unsafe int xiGetNumberDevices( out int numDevices );

        [DllImport( "m3api.dll" )]
        public static extern unsafe int xiOpenDevice( int deviceID, [Out] out IntPtr hDevice );
        [DllImport( "m3api.dll" )]
        public static extern int xiCloseDevice( IntPtr deviceHandle );

        [DllImport( "m3api.dll" )]
        public static extern int xiStartAcquisition( IntPtr deviceHandle );
        [DllImport( "m3api.dll" )]
        public static extern int xiStopAcquisition( IntPtr deviceHandle );

        [DllImport( "m3api.dll" )]
        public static extern int xiGetImage( IntPtr deviceHandle, int timeout, [In, MarshalAs( UnmanagedType.Struct )] ref XimeaImage image );

        [DllImport( "m3api.dll" )]
        public static extern unsafe int xiGetParam( IntPtr deviceHandle, string parameter, byte* value, out int size, ref ParameterType type );
        [DllImport( "m3api.dll" )]
        public static extern int xiGetParam( IntPtr deviceHandle, string parameter, out int value, out int size, ref ParameterType type );
        [DllImport( "m3api.dll" )]
        public static extern int xiGetParam( IntPtr deviceHandle, string parameter, out float value, out int size, ref ParameterType type );

        [DllImport( "m3api.dll" )]
        public static extern int xiSetParam( IntPtr deviceHandle, string parameter, ref float value, int size, ParameterType type );
        [DllImport( "m3api.dll" )]
        public static extern int xiSetParam( IntPtr deviceHandle, string parameter, ref int value, int size, ParameterType type );

    }
}
