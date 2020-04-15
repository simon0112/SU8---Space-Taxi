using System;
using SpaceTaxi_1.LevelLoading;

namespace SpaceTaxi_1 {
    internal class Program {
        public static void Main(string[] args) {
            var reader = new Reader();
            reader.ReadFile("short-n-sweet.txt");
            Console.WriteLine(reader.MapData);

            var game = new Game();
            game.GameLoop();
        }
    }
}
