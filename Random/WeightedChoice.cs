using System;
using System.Collections.Generic;
using VRageMath;

namespace Equinox.Utils.Random
{
    public class WeightedChoice<TK>
    {
        private readonly Dictionary<TK, float> m_values;

        public void Clear()
        {
            m_values.Clear();
        }

        public WeightedChoice(IEqualityComparer<TK> compare = null)
        {
            m_values = new Dictionary<TK, float>(compare ?? EqualityComparer<TK>.Default);
        }

        public void Add(TK key, float weight)
        {
            float cv = 0;
            m_values.TryGetValue(key, out cv);
            m_values[key] = cv + weight;
        }

        public enum WeightedNormalization
        {
            ClampToZero = 0,
            ShiftToZero = 1,
            Exponential = 2
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="normNoise">Noise to apply</param>
        /// <param name="quantileFifty">The quantile mapped to 0.5 noise</param>
        /// <returns></returns>
        public TK ChooseByQuantile(double normNoise, double quantileFifty)
        {
            var list = new List<KeyValuePair<TK, float>>(m_values);
            list.Sort((a, b) => a.Value.CompareTo(b.Value));
            // swap equal weight items randomly.
            var j = 1L;
            var rmask = (long)(normNoise * long.MaxValue);
            for (var i = 0; i < list.Count - 1; i++)
            {
                if (!(Math.Abs(list[i].Value - list[i + 1].Value) < float.Epsilon)) continue;
                if ((rmask & j) != 0)
                {
                    var tmp = list[i];
                    list[i] = list[i + 1];
                    list[i + 1] = tmp;
                }
                j = (j << 1) | (j >> 63);
            }

            // (0.5)^x == quantileFifty
            var exponent = Math.Log(quantileFifty) / Math.Log(0.5);
            var res = Math.Pow(normNoise, exponent);
            return list[(int)MyMath.Clamp((float)res * list.Count, 0, list.Count - 1)].Key;
        }

        public TK ChooseBest()
        {
            var best = float.MinValue;
            var bestVal = default(TK);
            foreach (var kv in m_values)
                if (kv.Value > best)
                {
                    best = kv.Value;
                    bestVal = kv.Key;
                }
            return bestVal;
        }

        public TK Choose(double normNoise, WeightedNormalization strat = WeightedNormalization.ShiftToZero)
        {
            var sum = 0.0;
            var min = double.MaxValue;
            foreach (var weight in m_values.Values)
            {
                switch (strat)
                {
                    case WeightedNormalization.ClampToZero:
                        sum += Math.Max(0, weight);
                        min = 0;
                        break;
                    case WeightedNormalization.Exponential:
                        sum += Math.Exp(weight);
                        break;
                    case WeightedNormalization.ShiftToZero:
                    default:
                        sum += weight;
                        min = Math.Min(min, weight);
                        break;
                }
            }
            if (strat == WeightedNormalization.ShiftToZero)
                sum -= min * m_values.Count;

            var evalNoise = normNoise * sum;
            var seenNoise = 0.0;

            var best = default(TK);
            var bestWeight = 0.0;
            foreach (var entry in m_values)
            {
                var weight = entry.Value;

                var weightReal = 0.0;
                switch (strat)
                {
                    case WeightedNormalization.ClampToZero:
                        weightReal = Math.Max(0, weight);
                        break;
                    case WeightedNormalization.Exponential:
                        weightReal = Math.Exp(weight);
                        break;
                    case WeightedNormalization.ShiftToZero:
                    default:
                        weightReal = weight - min;
                        break;
                }
                if (weightReal >= bestWeight)
                {
                    bestWeight = weightReal;
                    best = entry.Key;
                }
                seenNoise += weightReal;
                if (evalNoise <= seenNoise)
                    return entry.Key;
            }
            return best;
        }

        public int Count => m_values.Count;
    }
}
