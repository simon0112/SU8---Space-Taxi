using DIKUArcade.EventBus;
using DIKUArcade.State;
using System;

namespace SpaceTaxi_1.StateMachine {
    public class StateMachine : IGameEventProcessor<object> {
        public IGameState ActiveState { get; private set; }
        public StateMachine() {
            Utilities.EventBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            Utilities.EventBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            ActiveState = MainMenu.GetInstance();
        }
        public void SwitchState(GameStateType stateType) {
             switch (stateType) {
                 case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    break;
                case GameStateType.LevelSelect:
                    ActiveState = LevelSelect.GetInstance();
                    break;
             }
        } 
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent)
        {
            if (eventType == GameEventType.GameStateEvent) {
                if (gameEvent.Message == "CHANGE_STATE") {
                    switch (gameEvent.Parameter1) {
                        case "GAME_RUNNING":
                            SwitchState(GameStateType.GameRunning);
                            break;
                        case "GAME_PAUSED":
                            SwitchState(GameStateType.GamePaused);
                            break;
                        case "MAIN_MENU":
                            SwitchState(GameStateType.MainMenu);
                            break;
                        case "LEVEL_START":
                            GameRunning.GetInstance().CreateLevel(gameEvent.Parameter2);
                            SwitchState(GameStateType.GameRunning);
                            break;
                        case "LEVEL_SELECT":
                            SwitchState(GameStateType.LevelSelect);
                            break;
                    }
                }
            } else if (eventType == GameEventType.InputEvent) {
                this.ActiveState.HandleKeyEvent(gameEvent.Message, gameEvent.Parameter1);
            }
        }
        
    }
}
