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

        //<summary> player constructor <summary/>
        //<input> type = DynamicShape, name = shape </insput>
        //<return>  </insput> 
        public Player(DynamicShape shape) {

            Entity = new Entity(shape, new DIKUArcade.Graphics.Image(Path.Combine("Assets","Images","Taxi_Thrust_None_Right.png")));
            orientation = new Orientation();
            // Entity.Shape.AsDynamicShape().Direction = (new Vec2F(0,(float) 0.0011));
        }
        //<summary> sets the Orientation value to either left or right,
        // depending on what image we want to be displayed. <summary/>
        //<input> type = Orientation, name = value </insput>
        //<return> void </insput> 
        public void playerIsLeftOrRight(Orientation value)
        {
            if (value == Orientation.Left)
            {
                this.Entity.Image = new DIKUArcade.Graphics.Image(Path.Combine("Assets","Images","Taxi_Thrust_None.png"));
                this.orientation = Orientation.Left;
                
            }
            else if (value == Orientation.Right)
            {
                this.Entity.Image = new DIKUArcade.Graphics.Image(Path.Combine("Assets","Images","Taxi_Thrust_None_Right.png"));
                this.orientation = Orientation.Right;
            }
        }
        
        //<summary> executeds the code that is deceiding if the left, right, down or up key is pressed.
        // and then deceiding how long the vectors is moving our player, and sets the right image
        // using the playerIsLeftOrRight() methode. <summary/>
        //<input> type = gameEventType and GameEvent<object> , name = gameEvent and eventType </insput>
        //<return> void </insput> 
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            Orientation value = Orientation.Left;
            if (eventType == GameEventType.PlayerEvent) {
                        switch(gameEvent.Message){
                            case "BOOSTER_TO_LEFT":
                                value = Orientation.Left;
                                playerIsLeftOrRight(value);
                                this.Direction(new Vec2F(-0.0040f, 0.0000f));
                                this.Entity.Image = new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", "Taxi_Thrust_Back.png"));
                                break;
                            case "BOOSTER_TO_RIGHT":
                                value = Orientation.Right;
                                playerIsLeftOrRight(value);
                                this.Direction(new Vec2F(0.0040f, 0.0000f));
                                this.Entity.Image = new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", "Taxi_Thrust_Back_Right.png"));
                                break;
                            case "BOOSTER_UPWARDS":
                                this.Direction(new Vec2F(0.0000f, 0.0040f));
                                switch (value) {
                                    case Orientation.Left:
                                        this.Entity.Image = new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", "Taxi_Thrust_Bottom.png"));
                                        break;
                                    case Orientation.Right:
                                        this.Entity.Image = new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", "Taxi_Thrust_Bottom_Right.png"));
                                        break;
                                }
                                break;
                            case "BOOSTER_DOWNWARDS":
                                this.Direction(new Vec2F(0.0000f, -0.0040f));
                                break;
                            case "STOP_ACCELERATE_LEFT":
                                this.Direction(new Vec2F(0f, 0f));
                                playerIsLeftOrRight(value);
                                break;
                            case "STOP_ACCELERATE_RIGHT":
                                this.Direction(new Vec2F(0f, 0f));
                                playerIsLeftOrRight(value);
                                break;
                            case "STOP_ACCELERATE_UP":
                                this.Direction(new Vec2F(0f, 0f));
                                playerIsLeftOrRight(value);
                                break;
                            case "STOP_ACCELERATE_DOWN":
                                this.Direction(new Vec2F(0f, 0f));
                                playerIsLeftOrRight(value);
                                break;
                   
                }     
            }    
        }  
        
        
        //<summary> this methode is choosing what direction the player is moving in,
        // depending on the key pressed. <summary/>
        //<input> type = Vec2f, name = dir </insput>
        //<return> void </insput> 
        private void Direction(Vec2F dir) {
            Entity.Shape.AsDynamicShape().ChangeDirection(dir);
        }
        
        //<summary> this methode helps us make sure that the player is only running inside
        // the screen space. So the player stops moving if it reach one of the sides. <summary/>
        //<input> none </insput>
        //<return> void </insput> 
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
