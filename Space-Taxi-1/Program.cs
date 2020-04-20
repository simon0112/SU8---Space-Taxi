using System;
using SpaceTaxi_1.LevelLoading;

namespace SpaceTaxi_1 {
    internal class Program {
        public static void Main(string[] args) {
            var name = args[0];
            if (name == "short-n-sweet.txt" || name == "the-beach.txt") {
                var game = new Game(name);
                game.GameLoop();
            } else {
                Console.WriteLine("Please input a levelname as found in Space-Taxi-1/Levels");
            }
        }
    }
}
