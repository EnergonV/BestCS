// BestCS Statistics Library
// The BestCS.NET Framework
// http://accord-framework.net
//
// Copyright © César Souza, 2009-2015
// cesarsouza at gmail.com
//
//    This library is free software; you can redistribute it and/or
//    modify it under the terms of the GNU Lesser General Public
//    License as published by the Free Software Foundation; either
//    version 2.1 of the License, or (at your option) any later version.
//
//    This library is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
//    Lesser General Public License for more details.
//
//    You should have received a copy of the GNU Lesser General Public
//    License along with this library; if not, write to the Free Software
//    Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
//

namespace BestCS.Statistics.Models
{
    using System.Runtime.CompilerServices;
    using BestCS.Statistics.Models;
    using BestCS.Statistics.Models.Fields;
    using BestCS.Statistics.Models.Markov;
    using BestCS.Statistics.Models.Markov.Hybrid;
    using BestCS.Statistics.Models.Regression;
    using BestCS.Statistics.Models.Regression.Linear;

    /// <summary>
    ///  Contains statistical models with direct applications in machine learning, such as
    ///  <see cref="HiddenMarkovModel">Hidden Markov Models</see>, <see cref="ConditionalRandomField{T}">
    ///  Conditional Random Fields</see>, <see cref="HiddenConditionalRandomField{T}">Hidden Conditional 
    ///  Random Fields</see> and <see cref="ILinearRegression">linear</see> and <see cref="LogisticRegression">
    ///  logistic regressions</see>.
    /// </summary>
    /// 
    /// <remarks>
    /// <para>
    ///   The main algorithms and techniques available on this namespaces are certainly
    ///   the <see cref="BestCS.Statistics.Models.Markov">hidden Markov models</see>.
    ///   The BestCS.NET Framework contains one of the most popular and well-tested
    ///   offerings for creating, training and validating Markov models using either
    ///   discrete observations or any arbitrary discrete, continuous or mixed <see 
    ///   cref="BestCS.Statistics.Distributions">probability distributions</see> to 
    ///   model the observations.</para>
    ///   
    /// <para>
    ///   This namespace also brings <see cref="BestCS.Statistics.Models.Fields">
    ///   Conditional Random Fields</see>, that alongside the Markov models can be
    ///   used to build <see cref="HiddenMarkovClassifier">sequence classifiers</see>,
    ///   perform gesture recognition, and can even be combined with neural networks
    ///   to create <see cref="HybridMarkovClassifier">hybrid models</see>. Other 
    ///   models include <see cref="BestCS.Statistics.Models.Regression">regression
    ///   and survival models</see>.</para>
    ///   
    ///   
    /// <para>
    ///   The namespace class diagram is shown below. </para>
    ///   <img src="..\diagrams\classes\BestCS.Statistics.Models.png" />
    ///   
    /// <para>
    ///   Please note that class diagrams for each of the inner namespaces are 
    ///   also available within their own documentation pages.</para>
    /// </remarks>
    /// 
    /// <seealso cref="BestCS.Statistics.Models.Fields"/>
    /// <seealso cref="BestCS.Statistics.Models.Markov"/>
    /// <seealso cref="BestCS.Statistics.Models.Regression"/>
    /// 
    [CompilerGenerated]
    class NamespaceDoc
    {
    }
}
