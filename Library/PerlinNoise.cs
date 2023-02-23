using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Vector2
    {
        double x, y;
        public Vector2 (double x, double y)
        {
            this.x = x;
            this.y = y;
        }
        /// <summary>
        /// Calculate dot value by width and height beetween position of point and corner
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public double dot(Vector2 v)
        {
            return this.x*v.x + this.y*v.y;
        }
    }
    public class PerlinNoise
    {
        private List<int> _Permutations;
        public ulong Seed { get; }

        /// <summary>
        /// Init Noise
        /// </summary>
        /// <param name="seed">seed of noise</param>
        public PerlinNoise(ulong seed)
        {
            Seed = seed;
            _Permutations = MakePermutations();
        }
        /// <summary>
        /// Make Noise value by changing amplitude and frequency of next octave.
        /// </summary>
        /// <param name="x"> x position </param>
        /// <param name="y"> y position </param>
        /// <param name="numOctaves"> numbers of repeats</param>
        /// <returns>Sum of Noises</returns>
        public double FractalBrownianMotion(int x, int y, int numOctaves)
        {
            double result = 0;
            double amplitude = 1;
            double frequency = 0.01;

            for(int octave = 0; octave< numOctaves; octave++)
            {
                double n = amplitude * Noise((double)(x) * frequency, (double)(y) * frequency);
                result += n;

                amplitude *= 0.42;
                frequency *= 2.0;
            }

            if (result > 1)
            {
                return 1;
            }
            return result;
        }
        /// <summary>
        /// Gets Noise value in (x,y) position
        /// </summary>
        /// <param name="ix"> x position </param>
        /// <param name="iy"> y position</param>
        /// <returns> Noise value</returns>
        public double Noise(double ix, double iy)
        {
            //cast value by scale
            double x = ix;
            double y = iy;

            //positions to take permutation from list
            int X = (int)Math.Floor(x) & 255;
            int Y = (int)Math.Floor(y) & 255;

            //offset position
            double xf = x - Math.Floor(x);
            double yf = y - Math.Floor(y);

            //vectors of 4 corners
            Vector2 TR = new Vector2(xf - 1, yf - 1);
            Vector2 TL = new Vector2(xf, yf - 1);
            Vector2 BR = new Vector2(xf - 1, yf);
            Vector2 BL = new Vector2(xf, yf);

            //values of vectors
            int VTR = _Permutations[_Permutations[X + 1] + Y + 1];
            int VTL = _Permutations[_Permutations[X] + Y + 1];
            int VBR = _Permutations[_Permutations[X + 1] + Y];
            int VBL = _Permutations[_Permutations[X] + Y];

            //dot products of values
            double DTR = TR.dot(GetConstantVector(VTR));
            double DTL = TL.dot(GetConstantVector(VTL));
            double DBR = BR.dot(GetConstantVector(VBR));
            double DBL = BL.dot(GetConstantVector(VBL));

            //make Fade of x and y offset positions
            double u = Fade(xf);
            double v = Fade(yf);

            //Lerp values
            return Lerp(u,
                Lerp(v, DBL, DTL),
                Lerp(v, DBR, DTR)
                );
        }
        /// <summary>
        /// Shuffle permutation table with seed
        /// </summary>
        /// <param name="list"></param>
        /// <returns> Shuffled permutation list </returns>
        private List<int> Shuffle(List<int> list)
        {
            ulong s = Seed;
            for (int i=list.Count-1; i > 0; i--)
            {
                ulong localSeed = ((s + 743407443601) % 311239832093);
                int index = (int)(localSeed%(ulong)list.Count);

                int temp = list[i];
                list[i] = list[index];
                list[index] = temp;

                s = localSeed;
            }

            return list;
        }
        /// <summary>
        /// Generate permutation list
        /// </summary>
        /// <returns> Permutation list </returns>
        private List<int> MakePermutations()
        {
            List<int> permutations = new List<int>();

            for(int i = 0; i < 256; i++)
            {
                permutations.Add(i);
            }
            permutations = Shuffle(permutations);

            for (int i = 0; i < 256; i++)
            {
                permutations.Add(permutations[i]);
            }

            return permutations;
        }
        /// <summary>
        /// Get the vector of the corner by v value
        /// </summary>
        /// <param name="v">number of vector</param>
        /// <returns> Vector </returns>
        private Vector2 GetConstantVector(int v)
        {
            int h = v & 3;
            switch(h)
            {
                case 0: return new Vector2(1, 1);
                case 1: return new Vector2(-1, 1);
                case 2: return new Vector2(-1, -1);
                default: return new Vector2(1, -1);
            }
        }
        /// <summary>
        /// Equalize the values
        /// </summary>
        /// <param name="v"> value to equalize </param>
        /// <returns> Faded Value of v</returns>
        private double Fade(double v)
        {
            return ((6 * v - 15) * v + 10) * v * v * v;
        }
        /// <summary>
        /// Sumarize 2 values with scale t
        /// </summary>
        /// <param name="t"> scale </param>
        /// <param name="a1"> value 1 to lerp </param>
        /// <param name="a2"> value 2 to lerp </param>
        /// <returns> Lerped value </returns>
        private double Lerp(double t, double a1, double a2)
        {
            return a1 + t*(a2 - a1);
        }
    }
}
