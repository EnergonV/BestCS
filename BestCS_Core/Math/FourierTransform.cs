// BestCS Math Library
// BestCS.NET framework
// http://www.aforgenet.com/framework/
//
// Copyright � Andrew Kirillov, 2005-2009
// andrew.kirillov@aforgenet.com
//
// FFT idea from Exocortex.DSP library
// http://www.exocortex.org/dsp/
//

namespace BestCS.Math
{
    using System;
    using System.Numerics;

    /// <summary>
    /// Fourier transformation.
    /// </summary>
    /// 
    /// <remarks>The class implements one dimensional and two dimensional
    /// Discrete and Fast Fourier Transformation.</remarks>
    /// 
    public static class FourierTransform
    {
        /// <summary>
        /// Fourier transformation direction.
        /// </summary>
        public enum Direction
        {
            /// <summary>
            ///   Forward direction of Fourier transformation.
            /// </summary>
            /// 
            Forward = 1,

            /// <summary>
            ///   Backward direction of Fourier transformation.
            /// </summary>
            /// 
            Backward = -1
        };

        /// <summary>
        /// One dimensional Discrete Fourier Transform.
        /// </summary>
        /// 
        /// <param name="data">Data to transform.</param>
        /// <param name="direction">Transformation direction.</param>
        /// 
        public static void DFT(System.Numerics.Complex[] data, Direction direction)
        {
            int n = data.Length;
            double arg, cos, sin;
            var dst = new System.Numerics.Complex[n];

            // for each destination element
            for (int i = 0; i < dst.Length; i++)
            {
                dst[i] = System.Numerics.Complex.Zero;

                arg = -(int)direction * 2.0 * System.Math.PI * (double)i / (double)n;

                // sum source elements
                for (int j = 0; j < data.Length; j++)
                {
                    cos = System.Math.Cos(j * arg);
                    sin = System.Math.Sin(j * arg);

                    double re = data[j].Real * cos - data[j].Imaginary * sin;
                    double im = data[j].Real * sin + data[j].Imaginary * cos;

                    dst[i] += new System.Numerics.Complex(re, im);
                }
            }

            // copy elements
            if (direction == Direction.Forward)
            {
                // devide also for forward transform
                for (int i = 0; i < data.Length; i++)
                    data[i] /= n;
            }
            else
            {
                for (int i = 0; i < data.Length; i++)
                    data[i] = dst[i];
            }
        }

        /// <summary>
        /// Two dimensional Discrete Fourier Transform.
        /// </summary>
        /// 
        /// <param name="data">Data to transform.</param>
        /// <param name="direction">Transformation direction.</param>
        /// 
        public static void DFT2(System.Numerics.Complex[,] data, Direction direction)
        {
            int n = data.GetLength(0);	// rows
            int m = data.GetLength(1);	// columns
            double arg, cos, sin;
            var dst = new System.Numerics.Complex[System.Math.Max(n, m)];

            // process rows
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < dst.Length; j++)
                {
                    dst[j] = System.Numerics.Complex.Zero;

                    arg = -(int)direction * 2.0 * System.Math.PI * (double)j / (double)m;

                    // sum source elements
                    for (int k = 0; k < m; k++)
                    {
                        cos = System.Math.Cos(k * arg);
                        sin = System.Math.Sin(k * arg);

                        double re = data[i, k].Real * cos - data[i, k].Imaginary * sin;
                        double im = data[i, k].Real * sin + data[i, k].Imaginary * cos;

                        dst[j] += new System.Numerics.Complex(re, im);
                    }
                }

                // copy elements
                if (direction == Direction.Forward)
                {
                    // devide also for forward transform
                    for (int j = 0; j < dst.Length; j++)
                        data[i, j] = dst[j] / m;
                }
                else
                {
                    for (int j = 0; j < dst.Length; j++)
                        data[i, j] = dst[j];
                }
            }

            // process columns
            for (int j = 0; j < m; j++)
            {
                for (int i = 0; i < n; i++)
                {
                    dst[i] = System.Numerics.Complex.Zero;

                    arg = -(int)direction * 2.0 * System.Math.PI * (double)i / (double)n;

                    // sum source elements
                    for (int k = 0; k < n; k++)
                    {
                        cos = System.Math.Cos(k * arg);
                        sin = System.Math.Sin(k * arg);

                        double re = data[k, j].Real * cos - data[k, j].Imaginary * sin;
                        double im = data[k, j].Real * sin + data[k, j].Imaginary * cos;

                        dst[i] += new System.Numerics.Complex(re, im);
                    }
                }

                // copy elements
                if (direction == Direction.Forward)
                {
                    // devide also for forward transform
                    for (int i = 0; i < dst.Length; i++)
                        data[i, j] = dst[i] / n;
                }
                else
                {
                    for (int i = 0; i < dst.Length; i++)
                        data[i, j] = dst[i];
                }
            }
        }


        /// <summary>
        /// One dimensional Fast Fourier Transform.
        /// </summary>
        /// 
        /// <param name="data">Data to transform.</param>
        /// <param name="direction">Transformation direction.</param>
        /// 
        /// <remarks><para><note>The method accepts <paramref name="data"/> array of 2<sup>n</sup> size
        /// only, where <b>n</b> may vary in the [1, 14] range.</note></para></remarks>
        /// 
        /// <exception cref="ArgumentException">Incorrect data length.</exception>
        /// 
        public static void FFT(System.Numerics.Complex[] data, Direction direction)
        {
            int n = data.Length;
            int m = Tools.Log2(n);

            // reorder data first
            ReorderData(data);

            // compute FFT
            int tn = 1, tm;

            for (int k = 1; k <= m; k++)
            {
                System.Numerics.Complex[] rotation = FourierTransform.GetComplexRotation(k, direction);

                tm = tn;
                tn <<= 1;

                for (int i = 0; i < tm; i++)
                {
                    System.Numerics.Complex t = rotation[i];

                    for (int even = i; even < n; even += tn)
                    {
                        int odd = even + tm;
                        System.Numerics.Complex ce = data[even];
                        System.Numerics.Complex co = data[odd];

                        double tr = co.Real * t.Real - co.Imaginary * t.Imaginary;
                        double ti = co.Real * t.Imaginary + co.Imaginary * t.Real;

                        data[even] += new System.Numerics.Complex(tr, ti);
                        data[odd] = new System.Numerics.Complex(ce.Real - tr, ce.Imaginary - ti);
                    }
                }
            }

            if (direction == Direction.Forward)
            {
                for (int i = 0; i < data.Length; i++)
                    data[i] /= (double)n;
            }
        }

        /// <summary>
        /// Two dimensional Fast Fourier Transform.
        /// </summary>
        /// 
        /// <param name="data">Data to transform.</param>
        /// <param name="direction">Transformation direction.</param>
        /// 
        /// <remarks><para><note>The method accepts <paramref name="data"/> array of 2<sup>n</sup> size
        /// only in each dimension, where <b>n</b> may vary in the [1, 14] range. For example, 16x16 array
        /// is valid, but 15x15 is not.</note></para></remarks>
        /// 
        /// <exception cref="ArgumentException">Incorrect data length.</exception>
        /// 
        public static void FFT2(System.Numerics.Complex[,] data, Direction direction)
        {
            int k = data.GetLength(0);
            int n = data.GetLength(1);

            // check data size
            if (!Tools.IsPowerOf2(k) || !Tools.IsPowerOf2(n))
                throw new ArgumentException("The matrix rows and columns must be a power of 2.");

            if (k < minLength || k > maxLength || n < minLength || n > maxLength)
                throw new ArgumentException("Incorrect data length.");

            // process rows
            var row = new System.Numerics.Complex[n];

            for (int i = 0; i < k; i++)
            {
                // copy row
                for (int j = 0; j < row.Length; j++)
                    row[j] = data[i, j];

                // transform it
                FourierTransform.FFT(row, direction);

                // copy back
                for (int j = 0; j < row.Length; j++)
                    data[i, j] = row[j];
            }

            // process columns
            var col = new System.Numerics.Complex[k];

            for (int j = 0; j < n; j++)
            {
                // copy column
                for (int i = 0; i < k; i++)
                    col[i] = data[i, j];

                // transform it
                FourierTransform.FFT(col, direction);

                // copy back
                for (int i = 0; i < k; i++)
                    data[i, j] = col[i];
            }
        }

        #region Private Region

        private const int minLength = 2;
        private const int maxLength = 16384;
        private const int minBits = 1;
        private const int maxBits = 14;
        private static int[][] reversedBits = new int[maxBits][];
        private static System.Numerics.Complex[,][] complexRotation = new System.Numerics.Complex[maxBits, 2][];

        // Get array, indicating which data members should be swapped before FFT
        private static int[] GetReversedBits(int numberOfBits)
        {
            if ((numberOfBits < minBits) || (numberOfBits > maxBits))
                throw new ArgumentOutOfRangeException();

            // check if the array is already calculated
            if (reversedBits[numberOfBits - 1] == null)
            {
                int n = Tools.Pow2(numberOfBits);
                int[] rBits = new int[n];

                // calculate the array
                for (int i = 0; i < n; i++)
                {
                    int oldBits = i;
                    int newBits = 0;

                    for (int j = 0; j < numberOfBits; j++)
                    {
                        newBits = (newBits << 1) | (oldBits & 1);
                        oldBits = (oldBits >> 1);
                    }
                    rBits[i] = newBits;
                }
                reversedBits[numberOfBits - 1] = rBits;
            }
            return reversedBits[numberOfBits - 1];
        }

        // Get rotation of complex number
        private static System.Numerics.Complex[] GetComplexRotation(int numberOfBits, Direction direction)
        {
            int directionIndex = (direction == Direction.Forward) ? 0 : 1;

            // check if the array is already calculated
            if (complexRotation[numberOfBits - 1, directionIndex] == null)
            {
                int n = 1 << (numberOfBits - 1);
                double uR = 1.0;
                double uI = 0.0;
                double angle = System.Math.PI / n * (int)direction;
                double wR = System.Math.Cos(angle);
                double wI = System.Math.Sin(angle);
                double t;
                System.Numerics.Complex[] rotation = new System.Numerics.Complex[n];

                for (int i = 0; i < n; i++)
                {
                    rotation[i] = new System.Numerics.Complex(uR, uI);
                    t = uR * wI + uI * wR;
                    uR = uR * wR - uI * wI;
                    uI = t;
                }

                complexRotation[numberOfBits - 1, directionIndex] = rotation;
            }
            return complexRotation[numberOfBits - 1, directionIndex];
        }

        // Reorder data for FFT using
        private static void ReorderData(System.Numerics.Complex[] data)
        {
            int len = data.Length;

            // check data length
            if ((len < minLength) || (len > maxLength) || (!Tools.IsPowerOf2(len)))
                throw new ArgumentException("Incorrect data length.");

            int[] rBits = GetReversedBits(Tools.Log2(len));

            for (int i = 0; i < len; i++)
            {
                int s = rBits[i];

                if (s > i)
                {
                    System.Numerics.Complex t = data[i];
                    data[i] = data[s];
                    data[s] = t;
                }
            }
        }

        #endregion
    }
}
