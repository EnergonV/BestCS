//  BestCS Image Processing Library
// BestCS.NET framework
// http://www.sf.net/projects/BestCS/
//
// Copyright © Andrew Kirillov, 2005-2009
// andrew.kirillov@aforgenet.com
//

namespace  BestCS.Imaging.Textures
{
    using System;

    /// <summary>
    /// Labirinth texture.
    /// </summary>
    /// 
    /// <remarks><para>The texture generator creates textures with effect of labyrinth.</para>
    /// 
    /// <para>The generator is based on the <see cref=" BestCS.Math.PerlinNoise">Perlin noise function</see>.</para>
    /// 
    /// <para>Sample usage:</para>
    /// <code>
    /// // create texture generator
    /// LabyrinthTexture textureGenerator = new LabyrinthTexture( );
    /// // generate new texture
    /// float[,] texture = textureGenerator.Generate( 320, 240 );
    /// // convert it to image to visualize
    /// Bitmap textureImage = TextureTools.ToBitmap( texture );
    /// </code>
    ///
    /// <para><b>Result image:</b></para>
    /// <img src="img/imaging/labyrinth_texture.jpg" width="320" height="240" />
    /// </remarks>
    /// 
    public class LabyrinthTexture : ITextureGenerator
    {
        // Perlin noise function used for texture generation
        private  BestCS.Math.PerlinNoise noise = new  BestCS.Math.PerlinNoise( 1, 0.65, 1.0 / 16, 1.0 );

        // randmom number generator
        private Random rand = new Random( );
        private int r;

        /// <summary>
        /// Initializes a new instance of the <see cref="LabyrinthTexture"/> class.
        /// </summary>
        /// 
        public LabyrinthTexture( )
        {
            Reset( );
        }

        /// <summary>
        /// Generate texture.
        /// </summary>
        /// 
        /// <param name="width">Texture's width.</param>
        /// <param name="height">Texture's height.</param>
        /// 
        /// <returns>Two dimensional array of intensities.</returns>
        /// 
        /// <remarks>Generates new texture of the specified size.</remarks>
        ///  
        public float[,] Generate( int width, int height )
        {
            float[,] texture = new float[height, width];

            for ( int y = 0; y < height; y++ )
            {
                for ( int x = 0; x < width; x++ )
                {
                    texture[y, x] =
                        Math.Min( 1.0f,
                            (float) Math.Abs( noise.Function2D( x + r, y + r ) )
                        );

                }
            }
            return texture;
        }

        /// <summary>
        /// Reset generator.
        /// </summary>
        /// 
        /// <remarks>Regenerates internal random numbers.</remarks>
        /// 
        public void Reset( )
        {
            r = rand.Next( 5000 );
        }
    }
}
