using NUnit.Framework;
using SpaceTaxi_1;
using DIKUArcade;
using SpaceTaxi_1.Entities;
using SpaceTaxi_1.Enums;
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
        public Player player;
        [SetUp]
        public void Setup()
        {
            DIKUArcade.Window.CreateOpenGLContext();
            var player = new Player(new DIKUArcade.Entities.DynamicShape(new Vec2F(0.5f,0.5f),new Vec2F(0.1f,0.1f)));
            
            
        }

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