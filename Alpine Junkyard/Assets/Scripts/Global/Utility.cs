using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Global
{
    public static class Utility
    {
        public static void Shuffle<T>(this IList<T> list, System.Random randomGen)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = randomGen.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
