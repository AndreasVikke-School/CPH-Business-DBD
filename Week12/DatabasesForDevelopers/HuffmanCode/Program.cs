using System;

namespace HuffmanCode
{
    class Program
    {
        static void Main(string[] args)
        {
            HCode hCode = new HCode();
            string text = "pete is here";
            hCode.BuildHuffmanTree(text);
        }
    }
}
