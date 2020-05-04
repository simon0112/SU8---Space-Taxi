using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.EventBus;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using DIKUArcade.Timers;
using SpaceTaxi_1.Utilities;
using SpaceTaxi_1.LevelLoading;
using SpaceTaxi_1.StateMachine;

namespace SpaceTaxi_1 {
    public class Game : IGameEventProcessor<object> {
        private Entity backGroundImage;
        private GameEventBus<object> eventBus;
        private GameTimer gameTimer;
        private Window win;
        private StateMachine.StateMachine stateMachine;


        ///<summary> the game class constructor. <summary/>
        ///<variable name="LvlName"> the name of the level that is to be loaded.</variable>
        ///<variable name="win"> The window that the game will be displayed in.</variable>
        ///<variable name="eventBus"> The eventBus that will load every event in an orderly manner.</variable>
        ///<variable name="gameTimer"> The game timer, used to update various information in the window every interval of time.</variable>
        ///<variable name="BackgroundImage"> The background image of the game itself</variable>
        ///<variable name="LevelCreator"> The object that creates the level</variable>
        ///<variable name="Level"> The level itself</variable>
        ///<returns> Void, only instantiates the game class </returns>
        public Game() {
            // window
            win = new Window("Space Taxi Game v0.1", 500, AspectRatio.R1X1);

            // event bus.
            eventBus = EventBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType> {
                GameEventType.InputEvent, // key press / key release
                GameEventType.WindowEvent, // messages to the window, e.g. CloseWindow()
                GameEventType.PlayerEvent, // commands issued to the player object, e.g. move,
                GameEventType.GameStateEvent,
                GameEventType.TimedEvent,
                GameEventType.MovementEvent,
                                          // destroy, receive health, etc.
            });
            win.RegisterEventBus(eventBus);

            // game timer
            gameTimer = new GameTimer(60); // 60 UPS, no FPS limit

            // game assets
            backGroundImage = new Entity(
                new StationaryShape(new Vec2F(0.0f, 0.0f), new Vec2F(1.0f, 1.0f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));

            // State Machine
            stateMachine = new StateMachine.StateMachine();

            StateMachine.MainMenu.GetInstance().InitializeGameState();
            StateMachine.GamePaused.GetInstance().InitializeGameState();
            StateMachine.GameRunning.GetInstance().InitializeGameState();
            StateMachine.LevelSelect.GetInstance().InitializeGameState();

            // event delegation
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            eventBus.Subscribe(GameEventType.PlayerEvent, this);
            eventBus.Subscribe(GameEventType.GameStateEvent, this);
            eventBus.Subscribe(GameEventType.TimedEvent, this);
            eventBus.Subscribe(GameEventType.MovementEvent, this);

        }
        ///<summary>The main Game loop, used to render, and process events in an orderly fashion</summary>
        ///<returns> void </returns>
        public void GameLoop() {
           // StateMachine.StateMachine stateMachine = new StateMachine.StateMachine(); 
            while (win.IsRunning()) {
                gameTimer.MeasureTime();
                while (gameTimer.ShouldUpdate()) {
                    win.PollEvents();
                    Utilities.EventBus.GetBus().ProcessEvents();
                    stateMachine.ActiveState.UpdateGameLogic();
                }
                if (gameTimer.ShouldRender()) {
                    win.Clear();
                    stateMachine.ActiveState.RenderState();
                    win.SwapBuffers();
                }
                if (gameTimer.ShouldReset()) {
                    // could display something, 1 second has passed
                }
            }
        }

        private void DeliverUpdateAmount(int amt) {
            eventBus.RegisterEvent(
                GameEventFactory<object>.CreateGameEventForAllProcessors(
                        GameEventType.TimedEvent,
                        this,
                        "UPDATE_AMT_DELIVERY",
                        amt.ToString(), ""));
        }
        
        ///<summary> register the events if one of the casses is called. <summary/>
        ///<variable name="key"> The key that is pressed</variable>
        ///<returns> void, but pushes an event to the eventBus such that every other instance can process it</returns> 
        public void KeyPress(string key) {
            switch (key) {
            case "KEY_ESCAPE":
                win.CloseWindow();
                break;
            case "KEY_F12":
                Console.WriteLine("Saving screenshot");
                win.SaveScreenShot();
                break;
            }
        }
        ///<summary> register the events if one of the keys is released. <summary/>
        ///<variable name="key"> the key that is released </variable>
        ///<return> void, but like KeyPress, pushes an event to the eventBus </insput> 
        public void KeyRelease(string key) {
            
        }
        ///<summary> The first processEvent, it takes the regular keypresses given by the eventBus and uses helper methods to send the event onwards <summary/>
        ///<variable name="eventType"> The event type that is related to the specific game event in the eventBus</variable>
        ///<variable name="gameEvent"> The game event itself </variable>
        ///<return> void, but runs functions depending on what the event consists of </returns> 
        public void ProcessEvent(GameEventType eventType, GameEvent<object> gameEvent) {
            switch (eventType) {
                case GameEventType.WindowEvent:
                    switch (gameEvent.Message) {
                    case "CLOSE_WINDOW":
                        win.CloseWindow();
                        break;
                    }
                    break;
                case GameEventType.MovementEvent:
                    DeliverUpdateAmount(gameTimer.CapturedUpdates);
                    break;
            }
        }
    }
}
