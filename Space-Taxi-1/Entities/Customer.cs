using DIKUArcade.EventBus;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using SpaceTaxi_1.Enums;
using System.Collections.Generic;
using SpaceTaxi_1.Utilities;


namespace SpaceTaxi_1.Entities {
    public class Customer : IGameEventProcessor<object> {
        
        public Entity entity { get; private set; }
        private string Name;
        private int SpawnTime;
        private string startPlatform;
        private string goalPlatform;
        private int TimeLimit;
        private int pointWorth;

        public Customer(string name, int spwnTme, string start, string end, int Limit, int worth) {
            Name = name;
            SpawnTime = spwnTme;
            startPlatform = start;
            goalPlatform = end;
            TimeLimit = Limit;
            pointWorth = worth;
        }





        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {

        }
    }
}