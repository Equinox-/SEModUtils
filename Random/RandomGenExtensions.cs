using System;
using VRageMath;

namespace Equinox.Utils.Random
{
    public static class RandomGenExtensions
    {
        public static float NextFloat(this System.Random random)
        {
            return (float)random.NextDouble();
        }
        public static long NextLong(this System.Random random)
        {
            return (long)random.Next() << 32 | (long)random.Next();
        }

        public static double NextNormal(this System.Random random, double mu = 0, double sigma = 1)
        {
            // Box-Muller Transform
            double u1 = 0, u2 = 0;
            while (u1 <= double.Epsilon)
            {
                u1 = random.NextDouble();
                u2 = random.NextDouble();
            }

            // Deterministic, but still uniformly represent the two axes
            if (u1 < 0.5D)
                return mu + sigma * Math.Sqrt(-2 * Math.Log(u1)) * Math.Cos(2 * Math.PI * u2);
            else
                return mu + sigma * Math.Sqrt(-2 * Math.Log(u1)) * Math.Sin(2 * Math.PI * u2);
        }

        public static T NextUniformChoice<T>(this System.Random random, T[] array)
        {
            return array[random.Next(0, array.Length)];
        }

        public static double NextExponential(this System.Random random, double lambda = 1)
        {
            return lambda * Math.Pow(-Math.Log(random.NextDouble()), lambda);
        }

        public static Vector3D NextVector3D(this System.Random random)
        {
            return new Vector3(random.NextDouble() * 2 - 1, random.NextDouble() * 2 - 1, random.NextDouble() * 2 - 1);
        }

        public static Quaternion NextQuaternion(this System.Random random)
        {
            return Quaternion.CreateFromYawPitchRoll((float)Math.PI * 2 * random.NextFloat(), (float)Math.PI * 2 * random.NextFloat(), (float)Math.PI * 2 * random.NextFloat());
        }

        public static float NextFloat(this System.Random random, float min, float max)
        {
            return (float)(random.Next() * (max - min) + min);
        }
    }
}