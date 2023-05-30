using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Collection
{
    [Serializable]
    public struct DropBlock : IDropGenerator
    {
        public string name;
        [Range(0, 1f)]
        public float chance;
        public DropFactor[] drop;

        public IEnumerable<CollectableDrop> GetDrop ()
        {
            if (drop.Length == 0)
                return Empty;
            float roll = UnityEngine.Random.Range(0, 1f);
            bool dropped = roll < chance;
            if (dropped)
            {
                int index = drop.Length == 1 ? 1 : GetRollFactorIndex(drop.Select(d => d.factor));
                return new[] { drop[index].drop };
            }
            else
                return Empty;
        }

        private int GetRollFactorIndex (IEnumerable<float> factors)
        {
            float[] f = factors.ToArray();
            float sum = f.Sum();
            if (sum == 0)
                return UnityEngine.Random.Range(0, f.Length);

            float roll = UnityEngine.Random.Range(0, sum);
            float fLenght = 0;
            for (int i = 0; i < f.Length; i++)
            {
                if (roll <= fLenght + f[i])
                    return i;
                else
                    fLenght += f[i];
            }
            Debug.LogWarning("Something wrong");
            return 0;
        }

        private IEnumerable<CollectableDrop> Empty => new CollectableDrop[] { };
    }
}