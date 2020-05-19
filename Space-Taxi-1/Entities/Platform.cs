using DIKUArcade.EventBus;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using SpaceTaxi_1.Enums;
using System.Collections.Generic;
using SpaceTaxi_1.Utilities;


namespace SpaceTaxi_1.Entities {
    public class Platform : IGameEventProcessor<object> {
        private string Name;
        
        public Entity entity { get; private set; }

        public Platform(string name, StationaryShape shape, Image img) {
            Name = name;
            entity = new Entity(shape, img);
        }





        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.TimedEvent) {
                switch (gameEvent.Message) {

                }
            } else if (eventType == GameEventType.MovementEvent) {
                switch (gameEvent.Message) {

                }
            } else if (eventType == GameEventType.PlayerEvent) {

            }
        }
    }
}
