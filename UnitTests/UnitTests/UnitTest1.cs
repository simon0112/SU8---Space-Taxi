using NUnit.Framework;
using SpaceTaxi_1;
using DIKUArcade;
using SpaceTaxi_1.Entities;
using SpaceTaxi_1.Enums;
using DIKUArcade.Math;

namespace UnitTests
{
    [TestFixture]
    public class Tests
    {
      //  Orientation hey;
    //    Orientation orientation;
        [SetUp]
        public void Setup()
        {
            Player player = new Player(new DIKUArcade.Entities.DynamicShape(0f,0f,0f,0f));
        }

        [Test]
        public void TestOfPlayerIsLeftOrRight()
        {
         //   player.playerIsLeftOrRight(hey);
            System.Console.WriteLine("hi");
            Assert.AreEqual(1,1);
        }
    }
}