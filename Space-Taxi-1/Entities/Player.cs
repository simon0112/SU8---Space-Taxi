using DIKUArcade.EventBus;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using SpaceTaxi_1.Enums;

namespace SpaceTaxi_1.Entities {
    public class Player : IGameEventProcessor<object> {
        public Orientation orientation {get; private set;}
        public Entity Entity {get; private set;}
        public Player(DynamicShape shape) {
            Entity = new Entity(shape, new DIKUArcade.Graphics.Image(Path.Combine("Assets","Images","Taxi_Thrust_None.png")));
            orientation = new Orientation();
            // Entity.Shape.AsDynamicShape().Direction = (new Vec2F(0,(float) 0.0011));
        }
        public void playerIsLeftOrRight(Orientation value)
        {
            if (value == Orientation.Left)
            {
                this.Entity.Image = new DIKUArcade.Graphics.Image(Path.Combine("Assets","Images","Taxi_Thrust_None.png"));
            }
            else if (value == Orientation.Right)
            {
                this.Entity.Image = new DIKUArcade.Graphics.Image(Path.Combine("Assets","Images","Taxi_Thrust_None_Right.png"));
            }
        }
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            Orientation value;
            if (eventType == GameEventType.PlayerEvent) {
                        switch(gameEvent.Message){
                            case "BOOSTER_TO_LEFT":
                                value = Orientation.Left;
                                playerIsLeftOrRight(value);
                                this.Direction(new Vec2F(-0.0040f, 0.0000f));
                                break;
                            case "BOOSTER_TO_RIGHT":
                                value = Orientation.Right;
                                playerIsLeftOrRight(value);
                                this.Direction(new Vec2F(0.0040f, 0.0000f));
                                break;
                            case "BOOSTER_UPWARDS":
                                this.Direction(new Vec2F(0.0000f, 0.0040f));
                                break;
                            case "BOOSTER_DOWNWARDS":
                                this.Direction(new Vec2F(0.0000f, -0.0040f));
                                break;
                            case "STOP_ACCELERATE_LEFT":
                                this.Direction(new Vec2F(0f, 0f));
                                break;
                            case "STOP_ACCELERATE_RIGHT":
                                this.Direction(new Vec2F(0f, 0f));
                                break;
                            case "STOP_ACCELERATE_UP":
                                this.Direction(new Vec2F(0f, 0f));
                                break;
                            case "STOP_ACCELERATE_DOWN":
                                this.Direction(new Vec2F(0f, 0f));
                                break;
                   
                }     
            }    
        }  
        
        
        private void Direction(Vec2F dir) {
            Entity.Shape.AsDynamicShape().ChangeDirection(dir);
        }

        public void Move() {
            if (Entity.Shape.Position.X > 0f && Entity.Shape.Position.X < 0.97f) {
                Entity.Shape.Move();
            } else if (Entity.Shape.Position.X <= 0f && Entity.Shape.AsDynamicShape().Direction.X > 0f) {
                Entity.Shape.Move();
            } else if (Entity.Shape.Position.X >= 0.9f && Entity.Shape.AsDynamicShape().Direction.X < 0f) {
                Entity.Shape.Move();
            } else{
                this.Direction(new Vec2F(0f, 0f));
            }

            if (Entity.Shape.Position.Y > -0.005f && Entity.Shape.Position.Y < 0.955f) {
                Entity.Shape.Move();
            }
            else{
                this.Direction(new Vec2F(0f, 0f));
            }
        }
    }
}
