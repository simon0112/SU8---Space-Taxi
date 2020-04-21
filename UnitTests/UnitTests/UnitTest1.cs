using NUnit.Framework;
using SpaceTaxi_1;
using DIKUArcade;
using SpaceTaxi_1.Entities;
using SpaceTaxi_1.Enums;
using DIKUArcade.Math;

namespace UnitTests
{
    public class Tests
    {
        Orientation hey;
        Orientation orientation;
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void TestOfPlayerIsLeftOrRight()
        {
            var player = new Player(new DIKUArcade.Entities.DynamicShape(0f,0f,0f,0f));
            player.playerIsLeftOrRight(hey);
            System.Console.WriteLine("hi");
            Assert.AreEqual(1,1);
        }
    }
}