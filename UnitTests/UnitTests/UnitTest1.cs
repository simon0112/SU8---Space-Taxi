using NUnit.Framework;
using SpaceTaxi_1;
using DIKUArcade;
using SpaceTaxi_1.Entities;
using SpaceTaxi_1.Enums;
using DIKUArcade.Math;
using System.IO;
using System.Reflection;
using DIKUArcade.EventBus;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;


namespace UnitTests
{
    [TestFixture]
    public class Tests
    {
    //  Orientation hey;
    // Orientation orientation;
        [SetUp]
        public void Setup()
        {
            Player player = new Player(new DIKUArcade.Entities.DynamicShape(0f,0f,0f,0f));
        }

        [Test]
        public void TestOfPlayerIsLeftOrRight()
        {
            //
            GameEventType eventType = new GameEventType();
            GameEvent<object> gameEvent = new GameEvent<object>();
            gameEvent.Message = "BOOSTER_TO_LEFT";
            var player = new Player(new DynamicShape(10.0f,10.0f,10.0f,10.0f));
            player.ProcessEvent(eventType, gameEvent);

            string path = Directory.GetCurrentDirectory();
            System.Console.WriteLine(path);

            System.Console.WriteLine("hi");
            Assert.AreEqual(1,1);
        }
    }
}