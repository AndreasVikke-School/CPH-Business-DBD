using System;
using System.Collections.Generic;
using System.Text;
using Priority_Queue;

namespace HuffmanCode
{
    public class HCode
    {
        // Traverse the Huffman Tree and store Huffman Codes in a map.
        public void encode(Node root, string str, Dictionary<char?, string> huffmanCode)
        {
            if (root == null) {
                return;
            }
    
            // Found a leaf node
            if (isLeaf(root)) {
                huffmanCode.Add(root.ch, str.Length > 0 ? str : "1");
            }
    
            encode(root.left, str + '0', huffmanCode);
            encode(root.right, str + '1', huffmanCode);
        }

        // Traverse the Huffman Tree and decode the encoded string
        public int decode(Node root, int index, StringBuilder sb)
        {
            if (root == null) {
                return index;
            }
    
            // Found a leaf node
            if (isLeaf(root))
            {
                Console.Write(root.ch);
                return index;
            }
    
            index++;
    
            root = (sb[index] == '0') ? root.left : root.right;
            index = decode(root, index, sb);
            return index;
        }

        // Utility function to check if Huffman Tree contains only a single node
        public bool isLeaf(Node root) {
            return root.left == null && root.right == null;
        }

        // Builds Huffman Tree and decodes the given input text
        public void BuildHuffmanTree(string text)
        {
            // Base case: empty string
            if (text == null || text.Length == 0) {
                return;
            }
    
            // Count the frequency of appearance of each character
            // and store it in a map
            Dictionary<char, int> freq = new Dictionary<char, int>();
            foreach(char c in text.ToCharArray()) {
                freq[c] = freq.ContainsKey(c) ? freq[c] : 0 + 1;
            }
    
            // create a priority queue to store live nodes of the Huffman tree.
            // Notice that the highest priority item has the lowest frequency
            SimplePriorityQueue<Node> pq;
            pq = new SimplePriorityQueue<Node>();
    
            // create a leaf node for each character and add it
            // to the priority queue.
            foreach(var entry in freq) {
                pq.Enqueue(new Node(entry.Key, entry.Value), 0);
            }
    
            // do till there is more than one node in the queue
            while (pq.Count != 1)
            {
                // Remove the two nodes of the highest priority
                // (the lowest frequency) from the queue
                Node left = pq.Dequeue();
                Node right = pq.Dequeue();
    
                // create a new internal node with these two nodes as children
                // and with a frequency equal to the sum of both nodes'
                // frequencies. Add the new node to the priority queue.
    
                int sum = left.freq + right.freq;
                pq.Enqueue(new Node(null, sum, left, right), 0);
            }
    
            // `root` stores pointer to the root of Huffman Tree
            Node root = pq.First;
    
            // Traverse the Huffman tree and store the Huffman codes in a map
            Dictionary<char?, string> huffmanCode = new Dictionary<char?, string>();
            encode(root, "", huffmanCode);
    
            // Print the Huffman codes
            Console.WriteLine("The original string is: " + text);
            Console.WriteLine("=====================================================");
            Console.WriteLine("Huffman Codes are: " + string.Join(", ", huffmanCode));
            Console.WriteLine("=====================================================");
    
            // Print encoded string
            StringBuilder sb = new StringBuilder();
            foreach(char c in text.ToCharArray()) {
                sb.Append(huffmanCode[c]);
            }
    
            Console.WriteLine("The encoded string is: " + sb);
            Console.WriteLine("=====================================================");
            Console.Write("The decoded string is: ");
    
            if (isLeaf(root))
            {
                // Special case: For input like a, aa, aaa, etc.
                while (root.freq-- > 0) {
                    Console.Write(root.ch);
                }
            }
            else {
                // Traverse the Huffman Tree again and this time,
                // decode the encoded string
                int index = -1;
                while (index < sb.Length - 1) {
                    index = decode(root, index, sb);
                }
            }
        }
    }

    public class Node {
        public char? ch;
        public int freq;
        public Node left = null, right = null;
    
        public Node(char? ch, int freq)
        {
            this.ch = ch;
            this.freq = freq;
        }
    
        public Node(char? ch, int freq, Node left, Node right)
        {
            this.ch = ch;
            this.freq = freq;
            this.left = left;
            this.right = right;
        }
    }
}
