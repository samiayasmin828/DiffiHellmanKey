using System;
using System.Collections.Generic;
using System.Numerics;

namespace ExchangeKey
{
    class Program
    {
        static int size = 100;
        static List<bool> parr = new List<bool>(new bool[size + 1]);
        static void sieve()
        {
            for (int i = 4; i <= size; i += 2)
            {
                parr[i] = true;
            }
            int sqrtN = (int)Math.Sqrt(size);
            for (int i = 3; i <= sqrtN; i++)
            {
                if (parr[i] == false)
                {
                    for (int j = i * i; j <= size; j += i)
                    {
                        parr[j] = true;
                    }
                }
            }
        }
        static void initPrimes(List<int> primes)
        {

            sieve();
            for (int i = 2; i <= size; i++)
            {
                if (parr[i] == false)
                {
                    primes.Add(i);
                }
            }
        }
        static int getPrimitiveRoot(int p)
        {
            int z = p - 1;
            BigInteger[,] r = new BigInteger[z, z];

            for (int i = 0; i < z; i++)
            {
                for (int j = 0; j < z; j++)
                {
                    r[i, j] = (BigInteger)Math.Pow(i + 1, j + 1);
                    r[i, j] %= p;
                }
            }

            HashSet<BigInteger> st = new HashSet<BigInteger>();
            int root = -1;

            for (int i = 0; i < z; i++)
            {
                for (int k = 0; k < z; k++)
                {
                    st.Add(r[i, k]);
                }

                if (st.Count == z)
                {

                    root = i + 1;
                    break;
                }
            }
            return root;
        }

        static int getRandomPrime(List<int> primes)
        {
            int ind = new Random().Next() % primes.Count;
            return primes[ind];
        }

        static int getPrivateKey(int p)
        {
            return new Random().Next() % p;
        }


        static void ExchangeKey(int p, int g, int xa, int xb)
        {

            BigInteger k1, k2, ya, yb;

            ya = BigInteger.Pow(g, xa);
            ya %= p;

            yb = BigInteger.Pow(g, xb);
            yb %= p;

            k1 = BigInteger.Pow(yb, xa);
            k1 %= p;

            k2 = BigInteger.Pow(ya, xb);
            k2 %= p;

            if (k1 == k2)
            {
                Console.WriteLine("Prime number is= " + p);
                Console.WriteLine("Primitive root number is= " + g);
                Console.WriteLine("Private keys are Xa= " + xa + " Xb= " + xb);
                Console.WriteLine("Public keys are Ya= " + ya + " Yb= " + yb);
                Console.WriteLine("New key is " + k1);
            }
        }
        static void Main(string[] args)
        {

            List<int> primes = new List<int>();
            initPrimes(primes);

            int p, g, xa, xb;

            p = getRandomPrime(primes);

            g = getPrimitiveRoot(p);

            xa = getPrivateKey(p); xb = getPrivateKey(p);

            ExchangeKey(p, g, xa, xb);

            Console.ReadLine();
        }
    }
}
