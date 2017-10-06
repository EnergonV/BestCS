﻿// BestCS Statistics Library
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

namespace BestCS.Statistics.Distributions.Fitting
{
    using System;
    using BestCS.Statistics.Models.Markov;

    /// <summary>
    ///   Fitting options for hidden Markov model distributions.
    /// </summary>
    /// 
    [Serializable]
    public class HiddenMarkovOptions : IFittingOptions
    {

        /// <summary>
        ///   Gets or sets the learning function for the hidden Markov model.
        /// </summary>
        /// 
        public Action<HiddenMarkovModel, double[][], double[]> Learning { get; set; }

    }
}
