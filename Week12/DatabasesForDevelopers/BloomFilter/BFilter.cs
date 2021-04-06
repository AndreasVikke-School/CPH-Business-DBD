using System;

namespace BloomFilter
{
    class BFilter
    {
        private byte[] BloomFilter;

        public BFilter() {
            BloomFilter = new byte[1000];
        }

        public void AddItem(string item)
        {
            int hash = Hash(item) & 0x7FFFFFFF; // strips signed bit
            byte bit = (byte)(1 << (hash & 7)); // you have 8 bits
            BloomFilter[hash % BloomFilter.Length] |= bit;
        }

        public bool PossiblyExists(string item)
        {
            int hash = Hash(item) & 0x7FFFFFFF;
            byte bit = (byte)(1 << (hash & 7)); // you have 8 bits;
            return (BloomFilter[hash % BloomFilter.Length] & bit) != 0;
        }

        private static int Hash(string item)
        {
            int result = 17;
            for (int i = 0; i < item.Length; i++)
            {
                unchecked
                {
                    result *= item[i];
                }
            }
            return result;
        }
    }
}