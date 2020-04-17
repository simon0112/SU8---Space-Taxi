using System;
using SpaceTaxi_1.LevelLoading;

namespace SpaceTaxi_1 {
    internal class Program {
        public static void Main(string[] args) {
            var Creator = new LevelCreator();
            Creator.CreateLevel("short-n-sweet.txt");

            var game = new Game();
            game.GameLoop();
        }
    }
}
