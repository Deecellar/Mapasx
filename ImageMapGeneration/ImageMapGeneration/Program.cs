using System;
using MapGen;

namespace ImageMapGeneration
{
    class Program
    {
        static void Main(string[] args)
        {
            Generate generate = new Generate();
            generate.DrawMap(640, 640);
            Console.WriteLine("Hello World!");
        }
    }
}