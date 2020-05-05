using NUnit.Framework;
using SpaceTaxi_1;
using DIKUArcade;
using SpaceTaxi_1.Entities;
using SpaceTaxi_1.Enums;
using SpaceTaxi_1.LevelLoading;
using DIKUArcade.Math;
using DIKUArcade.EventBus;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using System;



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
        public void TestOfPlayerPlacementX() {
            var test = level.ReturnPlayer().Entity.Shape.Position;
            Assert.AreEqual(test.X, ((float)32/(float)levelcreator.reader.MapData[19].Length), 0.1);
        }

        [Test]
        public void TestOfPlayerPlacementY() {
            var test = level.ReturnPlayer().Entity.Shape.Position;
            Assert.AreEqual(test.Y, (-1*((float)19/-(float) levelcreator.reader.MapData.Count)), 0.1);
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

        //Reader tests
        [Test]
        public void testStartMapDataNotSkipped() {
            Assert.AreEqual("%#%#%#%#%#%#%#%#%#^^^^^^%#%#%#%#%#%#%#%#", levelcreator.reader.MapData[0]);
        }

        [Test]
        public void testEndMapDataNotSkipped() {
            Assert.AreEqual("%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#%#", levelcreator.reader.MapData[22]);
        }

        [Test]
        public void testStartMetaDataNotSkipped() {
            Assert.AreEqual("Name: SHORT -N- SWEET", levelcreator.reader.MetaData[0]);
        }

        [Test]
        public void testEndMetaDataNotSkipped() {
            Assert.AreEqual("Platforms: 1", levelcreator.reader.MetaData[levelcreator.reader.MetaData.Count-1]);
        }

        [Test]
        public void testStartLegendDataNotSkipped() {
            Assert.AreEqual("%) white-square.png", levelcreator.reader.LegendData[0]);
        }

        [Test]
        public void testEndLegendDataNotSkipped() {
            Assert.AreEqual("x) white-lower-right.png", levelcreator.reader.LegendData[levelcreator.reader.LegendData.Count-1]);
        }

        [Test]
        public void testCustomerDataNotSkipper() {
            Assert.AreEqual("Customer: Alice 10 1 ^J 10 100", levelcreator.reader.CustomerData[0]);
        }
    }
}