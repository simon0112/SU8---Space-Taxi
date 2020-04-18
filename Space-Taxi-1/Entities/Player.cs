using DIKUArcade.EventBus;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using SpaceTaxi_1.Enums;

namespace SpaceTaxi_1.Entities {
    public class Player : IGameEventProcessor<object> {
        public Orientation orientation {get; private set;}
        public Entity Entity {get; private set;}
        public Player(DynamicShape shape) {
            Entity = new Entity(shape, new DIKUArcade.Graphics.Image(Path.Combine("Assets","Images","Taxi_Thrust_None.png")));
            orientation = new Orientation();
        }
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            if (eventType == GameEventType.PlayerEvent) {
                switch (gameEvent.Parameter1) {
                    case "KEY_PRESS":
                        switch (gameEvent.Message) {
                            case "KEY_LEFT":
                                this.Direction(new Vec2F(-0.0060f, 0.0000f));
                                this.orientation = Orientation.Left;
                                break;
                            case "KEY_RIGHT":
                                this.Direction(new Vec2F(0.0060f, 0.0000f));
                                this.orientation = Orientation.Right;
                                break;
                        }
                        break;
                    case "KEY_RELEASE":
                        switch (gameEvent.Message) {
                            case "KEY_LEFT":
                                this.Direction(new Vec2F(0f, 0f));
                                break;
                            case "KEY_RIGHT":
                                this.Direction(new Vec2F(0f, 0f));
                                break;
                        }
                        break;
                }     
            }    
        }
        
        private void Direction(Vec2F dir) {
            Entity.Shape.AsDynamicShape().ChangeDirection(dir);
        }

        public void Move() {
            if (Entity.Shape.Position.X > 0f && Entity.Shape.Position.X < 0.9f) {
                Entity.Shape.Move();
            } else if (Entity.Shape.Position.X <= 0f && Entity.Shape.AsDynamicShape().Direction.X > 0f) {
                Entity.Shape.Move();
            } else if (Entity.Shape.Position.X >= 0.9f && Entity.Shape.AsDynamicShape().Direction.X < 0f) {
                Entity.Shape.Move();
            }
        }
    }
}
