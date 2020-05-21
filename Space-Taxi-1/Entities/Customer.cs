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
        public int SpawnTime;
        public string startPlatform;
        public string goalPlatform;
        public int TimeLimit;
        public int pointWorth;
        public bool visible = false;

        public Customer(string name, int spwnTme, string start, string end, int Limit, int worth, StationaryShape shape) {
            Name = name;
            SpawnTime = spwnTme;
            startPlatform = start;
            if (end.Contains('^') && end.Length > 1) {
                goalPlatform = end.Substring(1);
            } else if (end.Contains('^') && end.Length == 1) {
                goalPlatform = "Portal";
            } else {
                goalPlatform = end;
            }
            
        
            TimeLimit = Limit;
            pointWorth = worth;
            entity = new Entity(shape, new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", "CustomerStandRight.png")));
            Utilities.EventBus.GetBus().Subscribe(GameEventType.TimedEvent, this);
        }

        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
        }
    }
}
