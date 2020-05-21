using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.EventBus;
using System;
using DIKUArcade;
using DIKUArcade.Timers;
using System.Collections.Generic;
using DIKUArcade.Physics;
using SpaceTaxi_1.Entities;
using SpaceTaxi_1.LevelLoading;

namespace SpaceTaxi_1.StateMachine {
    public class GameRunning : IGameState {
        private static GameRunning instance = null;
        private Random rand = new Random();
        private LevelCreator levelCreator;
        private Level level;
        private bool GameOverActive = false;
        private GameEventBus<object> eventBus;
        private bool UpIsActive = false;
        private List<Customer> customers;
        private List<Customer> CustomerHasBeenPickedUp;
        public int customerStartTimer;
        public int customerTimer;
        private Score pointText;

        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public void GameLoop() {

        }

        public void InitializeGameState() {
            levelCreator = new LevelCreator();
            
            eventBus = Utilities.EventBus.GetBus();

            customers = new List<Customer>();
            CustomerHasBeenPickedUp = new List<Customer>();

            customerStartTimer = 0;

            customerTimer = 0;
            pointText = new Score(new Vec2F(0.8f, -0.15f), new Vec2F(0.3f, 0.3f));
        }
        ///<summary>Used to find out if the game has ended</summary>
        ///<var name="GameOverActive">The variable later checked to determine if the game is over, boolean.</var>
        ///<returns>void</returns>
        public void GameOver() {
            GameOverActive = true;
        }
        ///<summary>Resets the flag for if the game is over, such that a new game can be started without closing and opening the program</summary>
        ///<var name="GameOverActive">The variable later checked to determine if the game is over, boolean.</var>
        ///<returns>void</returns>
        public void resetGameOver() {
            GameOverActive = false;
        }
        ///<summary>Updates all logic (collision and physics) when called</summary>
        ///<var name="GameOverActive">Checks if the game is over, if it isn't, continues into the if statement</var>
        ///<returns>void</returns>
        public void UpdateGameLogic() {
            if (!GameOverActive) {
                DetectCollision();
                UpdatePhysics();
                customerStartTimer++;
                if (level.ReturnPlayer().customerOnBoard != null) {
                    customerTimer++;
                    customerPatienceLeft();
                }
                CustomerAppear();
            }
        }
        ///<summary>Detects collision between entities and the player</summary>
        ///<var name="playerShape">the dynamic shape that is the player</var>
        ///<var name="ent">the specific shape in the given list of entities</var>
        ///<var name="platformColl">a boolean value denoting whether the the player has collided with a platform</var>
        ///<var name="obstacleColl">a boolean value denoting whether the the player has collided with an obstacle</var>
        ///<returns>void</returns>
        private void DetectCollision() {
            var playerShape = level.ReturnPlayer().entity.Shape.AsDynamicShape();
            foreach (Entity ent in level.portal) {
                if (DIKUArcade.Physics.CollisionDetection.Aabb(playerShape, ent.Shape).Collision) {
                    NextLevelCall();
                } else if (DIKUArcade.Physics.CollisionDetection.Aabb(playerShape, ent.Shape).Collision && level.ReturnPlayer().customerOnBoard.goalPlatform == "Portal") {
                    pointText.AddPoint(level.ReturnPlayer().customerOnBoard.pointWorth);
                    level.ReturnPlayer().customerOnBoard = null;
                }
            }
            foreach (Platform plat in level.platforms) {
                var PlayerSpeedCheck = playerShape.Direction.Y >= -0.001f && (playerShape.Direction.X <= 0.001f && playerShape.Direction.X >= -0.001f);
                if ((plat.entity.Shape.Position.X-playerShape.Position.X > -0.1f && plat.entity.Shape.Position.X-playerShape.Position.X < 0.1f) && (plat.entity.Shape.Position.Y-playerShape.Position.Y > -0.1f && plat.entity.Shape.Position.Y-playerShape.Position.Y < 0.1f)) {
                    if (DIKUArcade.Physics.CollisionDetection.Aabb(playerShape, plat.entity.Shape).Collision && PlayerSpeedCheck) {
                        if (level.ReturnPlayer().customerOnBoard != null && level.ReturnPlayer().customerOnBoard.TimeLimit > (customerTimer/60)) {
                            if (plat.Name == level.ReturnPlayer().customerOnBoard.goalPlatform) {
                                pointText.AddPoint(level.ReturnPlayer().customerOnBoard.pointWorth);
                                level.ReturnPlayer().customerOnBoard = null;
                                customerTimer = 0;
                            }
                        } else {
                            PlayerLanded();
                        }
                    } else if (DIKUArcade.Physics.CollisionDetection.Aabb(playerShape, plat.entity.Shape).Collision && !PlayerSpeedCheck) {
                        GameOver();
                    }
                }
            }
            foreach (Entity ent in level.obstacles) {
                if ((ent.Shape.Position.X-playerShape.Position.X > -0.1f && ent.Shape.Position.X-playerShape.Position.X < 0.1f) && (ent.Shape.Position.Y-playerShape.Position.Y > -0.1f && ent.Shape.Position.Y-playerShape.Position.Y < 0.1f)) {
                    bool obstacleColl = DIKUArcade.Physics.CollisionDetection.Aabb(playerShape, ent.Shape).Collision;
                    if (obstacleColl) {
                        GameOver();
                    }
                }
            }
            foreach (Customer cust in level.Customers) {
                if (DIKUArcade.Physics.CollisionDetection.Aabb(playerShape, cust.entity.Shape).Collision && cust.visible == true) {
                    cust.visible = false;
                    level.ReturnPlayer().AddCustomer(cust);
                    CustomerHasBeenPickedUp.Add(cust);
                }
            } 
        }
        
        ///<summary>Used to make the game continue to the next level</summary>
        ///<returns>void</returns>
        private void NextLevelCall() {
            if (levelCreator.reader.MetaData[0].Contains("SHORT -N- SWEET")) {
                Utilities.EventBus.GetBus().RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.GameStateEvent,
                        this,
                        "CHANGE_STATE",
                        "LEVEL_START", "the-beach.txt"));
            }
        }
        ///<summary>Used to send a message when the player has landed on platform</summary>
        ///<returns>void</returns>
        private void PlayerLanded() {
            Utilities.EventBus.GetBus().RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                    GameEventType.PlayerEvent,
                    this,
                    "PLAYER_LANDED",
                    "", ""));
        }

        public void CustomerAppear() {
            foreach (Customer cust in level.Customers) {
                if ((customerStartTimer/60) >= cust.SpawnTime && cust.visible == false && !CustomerHasBeenPickedUp.Contains(cust)) {
                    cust.visible = true;
                }
            }
        }

        public void customerPatienceLeft() {
            if ((customerTimer/60) >= level.ReturnPlayer().customerOnBoard.TimeLimit) {
                level.ReturnPlayer().customerOnBoard = null;
                customerTimer = 0;
                GameOver();
            } 
        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            if (keyAction == "KEY_PRESS") {
                if (keyValue == "KEY_ESCAPE") {
                    Utilities.EventBus.GetBus().RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.GameStateEvent,
                            this,
                            "CHANGE_STATE",
                            "GAME_PAUSED", ""));
                } else {
                    KeyPress(keyValue);
                }
            } else if (keyAction == "KEY_RELEASE") {
                KeyRelease(keyValue);
            }
        }

        private void KeyPress(string key) {
            switch(key) {
                case "KEY_UP":
                UpIsActive = true;
                eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.MovementEvent, this, "BOOSTER_UPWARDS", "", ""));
                break;
            case "KEY_LEFT":
                if (UpIsActive) {
                    eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.MovementEvent, this, "BOOSTER_UP_LEFT", "", ""));
                } else {
                    eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.MovementEvent, this, "BOOSTER_TO_LEFT", "", ""));
                }
                break;
            case "KEY_RIGHT":
                if (UpIsActive) {
                    eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.MovementEvent, this, "BOOSTER_UP_RIGHT", "", ""));
                } else {
                    eventBus.RegisterEvent(
                        GameEventFactory<object>.CreateGameEventForAllProcessors(
                            GameEventType.MovementEvent, this, "BOOSTER_TO_RIGHT", "", ""));
                }
                break;
            }
        }
        public void KeyRelease(string key) {
            switch (key) {
            case "KEY_LEFT":
                eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.MovementEvent, this, "STOP_ACCELERATE_LEFT", "", ""));
                break;
            case "KEY_RIGHT":
                eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.MovementEvent, this, "STOP_ACCELERATE_RIGHT", "", ""));
                break;
            case "KEY_UP":
                UpIsActive = false;
                eventBus.RegisterEvent(
                    GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.MovementEvent, this, "STOP_ACCELERATE_UP", "", ""));
                break;
            }
        }
        public void RenderState() {
            if (!GameOverActive) {
                level.RenderLevelObjects();
            } else {
                level.obstacles.RenderEntities();
            }
            pointText.RenderScore();
        }
        ///<summary>Used to create the level when the level is supposed to start, also subscribes the palyer to the eventbus</summary>
        ///<var name="level">the level that is created by the levelcreator</var>
        ///<returns>void</returns>
        public void CreateLevel(string lvlName) {
            level = levelCreator.CreateLevel(lvlName);

            

            eventBus.Subscribe(GameEventType.MovementEvent, level.ReturnPlayer());
            eventBus.Subscribe(GameEventType.TimedEvent, level.ReturnPlayer());
            eventBus.Subscribe(GameEventType.PlayerEvent, level.ReturnPlayer());
        }
        ///<summary>Returns the active levelcreator</summary>
        ///<returns>void</returns>
        public LevelCreator ReturnLevelCreator() {
            return this.levelCreator;
        }
        ///<summary>Returns the active level</summary>
        ///<returns>void</returns>
        public Level ReturnLevel() {
            return this.level;
        }
        ///<summary>Used to make a call to the eventbus that the physics should be updated</summary>
        ///<returns>void</returns>
        private void UpdatePhysics() {
            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.TimedEvent,
                        this,
                        "UPDATE_PHYSICS",
                        "", ""));
        }
    }
}