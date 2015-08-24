//  BestCS Fuzzy Library
// BestCS.NET framework
// http://www.sf.net/projects/BestCS/
//
// Copyright © BestCS.NET, 2007-2011
// dinruspro@mail.ru
//

namespace  BestCS.Fuzzy
{
    using System;

    /// <summary>
    /// Interface with the common methods of a Fuzzy Norm.
    /// </summary>
    /// 
    /// <remarks><para>All fuzzy operators that act as a Norm must implement this interface.
    /// </para></remarks>
    /// 
    public interface INorm
    {
        /// <summary>
        /// Calculates the numerical result of a Norm (AND) operation applied to
        /// two fuzzy membership values.
        /// </summary>
        /// 
        /// <param name="membershipA">A fuzzy membership value, [0..1].</param>
        /// <param name="membershipB">A fuzzy membership value, [0..1].</param>
        /// 
        /// <returns>The numerical result the operation AND applied to <paramref name="membershipA"/>
        /// and <paramref name="membershipB"/>.</returns>
        /// 
        float Evaluate( float membershipA, float membershipB );
    }
}


