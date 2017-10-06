// BestCS TeRK Robotics Library
// BestCS.NET framework
// http://www.BestCSnet.com/framework/
//
// Copyright © Andrew Kirillov, 2007-2009
// andrew.kirillov@BestCSnet.com
//

namespace BestCS.Robotics.TeRK
{
    using System;

    /// <summary>
    /// Service access failed exception.
    /// </summary>
    /// 
    /// <remarks><para>The excetion is thrown in the case if the requested service can not
    /// be accessed or does not exist on the Qwerk's board.</para>
    /// </remarks>
    /// 
    public class ServiceAccessFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotConnectedException"/> class.
        /// </summary>
        /// 
        /// <param name="message">Exception's message.</param>
        /// 
        public ServiceAccessFailedException( string message ) :
            base( message ) { }
    }
}
