using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    class Vector3
    {
        double x, y, z;
        public Vector3 (double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
        public double dot(Vector3 vector)
        {
            return 
                x * vector.x +
                y * vector.y +
                z * vector.z;
        }
    }
    public class PerlinNoise
    {
        #region Permutation Table
        /// <summary>
        /// Permutations List
        /// </summary>
        private List<int> _Permutations;

        /// <summary>
        /// Change order of list using seed
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private List<int> Shuffle(List<int> list)
        {
            ulong s = Seed;
            for (int i = list.Count - 1; i > 0; i--)
            {
                ulong localSeed = ((s + 743407443601) % 311239832093);
                int index = (int)(localSeed % (ulong)list.Count);

                int temp = list[i];
                list[i] = list[index];
                list[index] = temp;

                s = localSeed;
            }

            return list;
        }
        /// <summary>
        /// Initialize permutation list, fill it with walues, Shufle it, copying created list additional time and assign in to _permutation param
        /// </summary>
        private void MakePermutations()
        {
            List<int> permutations = new List<int>();

            for (int i = 0; i < 256; i++)
            {
                permutations.Add(i);
            }
            permutations = Shuffle(permutations);

            for (int i = 0; i < 256; i++)
            {
                permutations.Add(permutations[i]);
            }

            _Permutations = permutations;
        }
        #endregion Permutation Table

        private readonly ulong Seed;
        /// <summary>
        /// Get Seed
        /// </summary>
        /// <returns>Seed</returns>
        public ulong getSeed()
        {
            return Seed;
        }

        #nullable disable
        public PerlinNoise(ulong seed)
        {
            Seed = seed;
            MakePermutations();
        }

        /// <summary>
        /// Calculate Octave Noise (Multiple Noise Value, all nest value is smaller than previous)
        /// </summary>
        /// <param name="x"> Position x </param>
        /// <param name="y"> Position y </param>
        /// <param name="z"> Position y </param>
        /// <param name="numOctaves"> Number of Octaves </param>
        /// <param name="persistance"> Factor of all next octaves (Smaller factor creates smoofer values, factor 1 create Noise Value)</param>
        /// <param name="frequency"> Change frequency of positions </param>
        /// <returns> Octave Noise </returns>
        public double OctaveNoise(int x, int y, int z, int numOctaves, double persistance = 0.5, double frequency = 0.01)
        {
            double result = 0;
            double amplitude = 1;
            double maxValue = 0;

            for (int octave = 0; octave< numOctaves; octave++)
            {
                double n = amplitude * Noise(x * frequency, y * frequency, z * frequency);
                result += n;
                maxValue += amplitude;

                amplitude *= persistance;
                frequency *= 2.0;
            }
            return result/maxValue;
        }
        /// <summary>
        /// Faster Math.Floor()
        /// </summary>
        /// <param name="x"> Value to rounding </param>
        /// <returns> Rounded value </returns>
        private static int Fastfloor(double x)
        {
            return x > 0 ? (int)x : (int)x - 1;
        }
        /// <summary>
        /// Calculates Noise Value in given position
        /// </summary>
        /// <param name="x"> Position x </param>
        /// <param name="y"> Position y </param>
        /// <param name="z"> Position z </param>
        /// <returns> Noise Value of given positions </returns>
        public double Noise(double x, double y, double z)
        {
            //Round value to future take value from _permutation list
            int X = Fastfloor(x);
            int Y = Fastfloor(y);
            int Z = Fastfloor(z);

            //Relative position, slice integer value from position
            x = x - X;
            y = y - Y;
            z = z - Z;
            
            //If position is greater than 255, return rest of the division by 256
            X = X & 255;
            Y = Y & 255;
            Z = Z & 255;

            //Get permutation value by given position on Corners of the Cube
            int gi000 = _Permutations[X + _Permutations[Y + _Permutations[Z]]];
            int gi001 = _Permutations[X + _Permutations[Y + _Permutations[Z + 1]]];
            int gi010 = _Permutations[X + _Permutations[Y + 1 + _Permutations[Z]]];
            int gi011 = _Permutations[X + _Permutations[Y + 1 + _Permutations[Z + 1]]];
            int gi100 = _Permutations[X + 1 + _Permutations[Y + _Permutations[Z]]];
            int gi101 = _Permutations[X + 1 + _Permutations[Y + _Permutations[Z + 1]]];
            int gi110 = _Permutations[X + 1 + _Permutations[Y + 1 + _Permutations[Z]]];
            int gi111 = _Permutations[X + 1 + _Permutations[Y + 1 + _Permutations[Z + 1]]];

            //Corner position
            Vector3 v000 = new Vector3(x, y, z);
            Vector3 v100 = new Vector3(x - 1, y, z);
            Vector3 v010 = new Vector3(x, y - 1, z);
            Vector3 v110 = new Vector3(x - 1, y - 1, z);
            Vector3 v001 = new Vector3(x, y, z - 1);
            Vector3 v101 = new Vector3(x - 1, y, z - 1);
            Vector3 v011 = new Vector3(x, y - 1, z - 1);
            Vector3 v111 = new Vector3(x - 1, y - 1, z - 1);

            //Dot product of Corner position and Gradient vector
            double n000 = v000.dot(Grad(gi000));
            double n100 = v100.dot(Grad(gi100));
            double n010 = v010.dot(Grad(gi010));
            double n110 = v110.dot(Grad(gi110));
            double n001 = v001.dot(Grad(gi001));
            double n101 = v101.dot(Grad(gi101));
            double n011 = v011.dot(Grad(gi011));
            double n111 = v111.dot(Grad(gi111));

            //Flatte value of x,y,z position
            double u = Fade(x);
            double v = Fade(y);
            double w = Fade(z);

            //Interpolate all dot products with flatted values
            return 
            Lerp(
                Lerp(
                    Lerp(
                        n000,
                        n100,
                        u),
                    Lerp(
                        n010,
                        n110,
                        u),
                    v),
                Lerp(
                    Lerp(
                        n001,
                        n101,
                        u),
                    Lerp(
                        n011,
                        n111,
                        u),
                    v),
                w);

        }
        /// <summary>
        /// Return vector of given value (permutation value)
        /// </summary>
        /// <param name="v"> Number of vector </param>
        /// <returns> Vector </returns>
        private Vector3 Grad(int v)
        {
            switch(v % 12)
            {
                case 0: return new Vector3(1, 1, 0);
                case 1: return new Vector3(-1, 1, 0);
                case 2: return new Vector3(1, -1, 0);
                case 3: return new Vector3(-1, -1, 0);
                case 4: return new Vector3(1, 0, 1);
                case 5: return new Vector3(-1, 0, 1);
                case 6: return new Vector3(1, 0, -1);
                case 7: return new Vector3(-1, 0, -1);
                case 8: return new Vector3(0, 1, 1);
                case 9: return new Vector3(0, -1, 1);
                case 10: return new Vector3(0, 1, -1);
                default: return new Vector3(0, -1, -1);
            }
        }
        /// <summary>
        /// Flatten given value
        /// </summary>
        /// <param name="v"> Value to flatte </param>
        /// <returns> Flatted value </returns>
        private double Fade(double v)
        {
            return ((6 * v - 15) * v + 10) * v * v * v;
        }
        /// <summary>
        /// Interpolate dot products
        /// </summary>
        /// <param name="a1"> Dot product </param>
        /// <param name="a2"> Dot product </param>
        /// <param name="t"> flatted value (Fade function) </param>
        /// <returns> Interpolated value </returns>
        private double Lerp(double a1, double a2,double t)
        {
            return a1*(1-t)+ a2*t;
        }
    }
}
