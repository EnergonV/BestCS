//  BestCS Math Library
// BestCS.NET framework
// http://www.sf.net/projects/BestCS/
//
// Copyright © BestCS.NET, 2007-2010
// dinruspro@mail.ru
//

namespace  BestCS.Math.Metrics
{
    using System;

    /// <summary>
    /// Interface for similarity algorithms.
    /// </summary>
    /// 
    /// <remarks><para>The interface defines a set of methods implemented
    /// by similarity and correlation algorithms. These algorithms typically take a set of points and return a 
    /// similarity score for the two vectors.</para>
    /// 
    /// <para>Similarity and correlation algorithms are used in many machine learning and collaborative
    /// filtering algorithms.</para>
    ///
    /// <para>For additional details about similarity metrics, documentation of the
    /// particular algorithms should be studied.</para>
    /// </remarks>
    /// 
    public interface ISimilarity
    {
        /// <summary>
        /// Returns similarity score for two N-dimensional double vectors. 
        /// </summary>
        /// 
        /// <param name="p">1st point vector.</param>
        /// <param name="q">2nd point vector.</param>
        /// 
        /// <returns>Returns similarity score determined by the given algorithm.</returns>
        /// 
        double GetSimilarityScore( double[] p, double[] q );
    }
}
