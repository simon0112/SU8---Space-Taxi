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
        private ImageStride imageStride;
        private LevelCreator levelCreator;
        private Level level;
        private bool GameOverActive = false;
        private GameEventBus<object> eventBus;
        private bool UpIsActive = false;

        public static GameRunning GetInstance() {
            return GameRunning.instance ?? (GameRunning.instance = new GameRunning());
        }

        public void GameLoop() {

        }

        public void InitializeGameState() {
            levelCreator = new LevelCreator();
            
            eventBus = Utilities.EventBus.GetBus();
        }
        public void GameOver() {
            GameOverActive = true;
        }

        public void UpdateGameLogic() {
            if (!GameOverActive) {
                UpdatePhysics();
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
            }
        }

        public void CreateLevel(string lvlName) {
            level = levelCreator.CreateLevel(lvlName);
            eventBus.Subscribe(GameEventType.MovementEvent, level.ReturnPlayer());
            eventBus.Subscribe(GameEventType.TimedEvent, level.ReturnPlayer());
        }

        public LevelCreator ReturnLevelCreator() {
            return this.levelCreator;
        }

        public Level ReturnLevel() {
            return this.level;
        }

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