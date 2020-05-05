using DIKUArcade.EventBus;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using System;
using SpaceTaxi_1.Enums;

namespace SpaceTaxi_1.Entities
{
    public class Player : IGameEventProcessor<object>
    {
        public Orientation orientation { get; private set; }
        public Entity Entity { get; private set; }
        private int UpdatesSinceLastMovement;
        private Vec2F Gravity = new Vec2F(0f, -0.0000005f);

        ///<summary> player constructor <summary/>
        ///<variable name="shape"> The Dynamic shape that the player is, the players placement and bounds</variable>
        ///<variable name="Entity"> The entity that the player is, makes use of the shape, and an image path to create a visible player </variable>
        ///<variable name="Orientation"> The orientation of the player, can either be left or rigtht</variable>
        ///<returns> void, but instantiates the player object </returns> 
        public Player(DynamicShape shape)
        {

            Entity = new Entity(shape, new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", "Taxi_Thrust_None_Right.png")));
            orientation = new Orientation();
        }
        ///<summary> sets the Orientation value to either left or right, depending on what image we want to be displayed. <summary/>
        ///<variable name="value"> A value using the Orientation enumeration.</variable>
        ///<return> void, but changes values of the player object </returns> 
        public void playerIsLeftOrRight(Orientation value)
        {
            if (value == Orientation.Left)
            {
                this.Entity.Image = new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", "Taxi_Thrust_None.png"));
                this.orientation = Orientation.Left;

            }
            else if (value == Orientation.Right)
            {
                this.Entity.Image = new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", "Taxi_Thrust_None_Right.png"));
                this.orientation = Orientation.Right;
            }
        }

        public void AddAcceleration(Vec2F value, int UpdateAmt)
        {
            Direction(this.Entity.Shape.AsDynamicShape().Direction + (value + Gravity) * UpdateAmt);
        }

        public void GravityEffect()
        {
            Direction(this.Entity.Shape.AsDynamicShape().Direction + (Gravity) * 1);
        }

        ///<summary> The processEvent that is related to the player, it processes playerevents based on what the message of that event is in the eventBus <summary/>
        ///<variable name="eventType"> The event type that is related to the specific game event in the eventBus</variable>
        ///<variable name="gameEvent"> The game event itself </variable>
        ///<return> void, but runs functions depending on what the event consists of </insput> 
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            Orientation value = Orientation.Left;
            if (eventType == GameEventType.TimedEvent)
            {
                switch (gameEvent.Message)
                {
                    case "UPDATE_AMT_DELIVERY":
                        UpdatesSinceLastMovement = (int.Parse(gameEvent.Parameter1)) / 60;
                        break;
                }
            }
            else if (eventType == GameEventType.MovementEvent)
            {
                switch (gameEvent.Message)
                {
                    case "BOOSTER_TO_LEFT":
                        value = Orientation.Left;
                        playerIsLeftOrRight(value);
                        this.AddAcceleration(new Vec2F(-0.00040f, 0.0000f), UpdatesSinceLastMovement);
                        break;
                    case "BOOSTER_TO_RIGHT":
                        value = Orientation.Right;
                        playerIsLeftOrRight(value);
                        this.AddAcceleration(new Vec2F(0.00040f, 0.0000f), UpdatesSinceLastMovement);
                        break;
                    case "BOOSTER_UPWARDS":
                        this.AddAcceleration(new Vec2F(0.0000f, 0.0001f), UpdatesSinceLastMovement);
                        switch (value)
                        {
                            case Orientation.Left:
                                this.Entity.Image = new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", "Taxi_Thrust_Bottom.png"));
                                break;
                            case Orientation.Right:
                                this.Entity.Image = new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", "Taxi_Thrust_Bottom_Right.png"));
                                break;
                        }
                        break;
                    case "BOOSTER_UP_LEFT":
                        this.AddAcceleration(new Vec2F(-0.0002828f, 0.0002828f), UpdatesSinceLastMovement);
                        break;
                    case "BOOSTER_UP_RIGHT":
                        this.AddAcceleration(new Vec2F(0.0002828f, 0.0002828f), UpdatesSinceLastMovement);
                        break;
                    case "STOP_ACCELERATE_LEFT":
                        this.AddAcceleration(new Vec2F(0f, 0f), UpdatesSinceLastMovement);
                        playerIsLeftOrRight(value);
                        break;
                    case "STOP_ACCELERATE_RIGHT":
                        this.AddAcceleration(new Vec2F(0f, 0f), UpdatesSinceLastMovement);
                        playerIsLeftOrRight(value);
                        break;
                    case "STOP_ACCELERATE_UP":
                        this.AddAcceleration(new Vec2F(0f, 0f), UpdatesSinceLastMovement);
                        playerIsLeftOrRight(value);
                        break;
                }
            }
        }


        //<summary> this methode is choosing what direction the player is moving in,
        // depending on the key pressed. <summary/>
        //<input> type = Vec2f, name = dir </insput>
        //<return> void </insput> 
        private void Direction(Vec2F dir)
        {
            Entity.Shape.AsDynamicShape().ChangeDirection(dir);
        }

        //<summary> this methode helps us make sure that the player is only running inside
        // the screen space. So the player stops moving if it reach one of the sides. <summary/>
        //<input> none </insput>
        //<return> void </insput> 
        public void Move()
        {
            if (Entity.Shape.Position.X > 0f && Entity.Shape.Position.X < 0.97f)
            {
                Entity.Shape.Move();
            }
            else if (Entity.Shape.Position.X <= 0f && Entity.Shape.AsDynamicShape().Direction.X > 0f)
            {
                Entity.Shape.Move();
            }
            else if (Entity.Shape.Position.X >= 0.9f && Entity.Shape.AsDynamicShape().Direction.X < 0f)
            {
                Entity.Shape.Move();
            }
            else
            {
                this.Direction(new Vec2F(0f, 0f));
            }

            if (Entity.Shape.Position.Y > -0.005f && Entity.Shape.Position.Y < 0.955f)
            {
                Entity.Shape.Move();
            }
            else
            {
                this.Direction(new Vec2F(0f, 0f));
            }
        }
    }
}