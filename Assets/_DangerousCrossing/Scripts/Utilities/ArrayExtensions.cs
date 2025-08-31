namespace _DangerousCrossing.Scripts.Utilities
{
    public static class ArrayExtensions
    {
        private static readonly System.Random systemRandom = new System.Random();

        public static void Shuffle<T>(this T[] array)
        {
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = systemRandom.Next(n + 1);
                (array[k], array[n]) = (array[n], array[k]);
            }
        }

        public static void ShuffleByUnityRandom<T>(this T[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n; i++)
            {
                int r = i + UnityEngine.Random.Range(0, n - i);
                (array[r], array[i]) = (array[i], array[r]);
            }
        }
    }
}