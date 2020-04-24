using NUnit.Framework;
using SpaceTaxi_1;
using DIKUArcade;
using SpaceTaxi_1.Entities;
using SpaceTaxi_1.Enums;
using SpaceTaxi_1.LevelLoading;
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
        Player player;
        Level level;
        LevelCreator levelcreator;

        [SetUp]
        public void Setup()
        {
            DIKUArcade.Window.CreateOpenGLContext();
            player = new Player(new DIKUArcade.Entities.DynamicShape(new Vec2F(0.5f,0.5f),new Vec2F(0.1f,0.1f)));
            levelcreator = new LevelCreator();
            level = levelcreator.CreateLevel("short-n-sweet.txt");
            
        }

        //Level tests
        [Test]
        public void TestOfPlayerPlacement() {
            var test = level.Player.Entity.Shape.Position;
            Assert.AreEqual(test, new Vec2F(((float)32/(float)levelcreator.reader.MapData[19].Length),(-1*((float)19/-(float) levelcreator.reader.MapData.Count))));
        }

        //Player tests
        [Test]
        public void TestOfPlayerIsLeft()
        {
            player.playerIsLeftOrRight(Orientation.Left);
            Assert.AreEqual(Orientation.Left, player.orientation);
        }

        [Test]
        public void TestOfPlayerIsRight()
        {
            player.playerIsLeftOrRight(Orientation.Right);
            Assert.AreEqual(Orientation.Right, player.orientation);
        }
    }
}