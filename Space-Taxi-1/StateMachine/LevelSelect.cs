using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.EventBus;

namespace SpaceTaxi_1.StateMachine {
    public class LevelSelect : IGameState {
        private static LevelSelect instance = null;
        private Entity BackgroundImage;
        private Text[] Levels;
        private int ActiveLevelButton = 0;
        public static LevelSelect GetInstance()  {
            return LevelSelect.instance ?? (LevelSelect.instance = new LevelSelect());
        }

//      Part of the IGameState interface, so has to be within the class, even if it isn't used.
//      Is not used since there is no core gameloop to run while the level is being selected
        public void GameLoop() {

        }

        ///<summary>Initializes the 'Level-selector' state of the game</summary>
        ///<returns> void </returns>
        public void InitializeGameState() {
            Levels = new Text[3] {new Text("Short-n-sweet", new Vec2F(0.35f,0.75f), new Vec2F(0.5f,0.25f)), new Text("the-beach", new Vec2F(0.35f,0.5f), new Vec2F(0.5f,0.25f)), new Text("Back to Menu", new Vec2F(0.35f,0.25f),new Vec2F(0.5f,0.25f))};

            BackgroundImage = new Entity(
                new DynamicShape(new Vec2F(0f, 0f), new Vec2F(1f, 1f)),
                new Image(Path.Combine("Assets", "Images", "SpaceBackground.png")));
        }

//      Once again, part of the IGameState interface, has to be in the class, even if not used.
//      Is not used since there is no game logic to update
        public void UpdateGameLogic() {

        }

        public void HandleKeyEvent(string keyValue, string keyAction) {
            if (keyAction == "KEY_PRESS") {
                if (keyValue == "KEY_UP" && ActiveLevelButton != 0) {
                    ActiveLevelButton--;
                } else if (keyValue == "KEY_DOWN" &&    ActiveLevelButton != 2) {
                    ActiveLevelButton++;
                } else if (keyValue == "KEY_ENTER") {
                    switch (ActiveLevelButton) {
                        case 0:
                            Utilities.EventBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForSpecificProcessor(
                                GameEventType.GameStateEvent,
                                this, GameRunning.GetInstance(),
                                "CHANGE_STATE",
                                "LEVEL_START", "short-n-sweet.txt"));
                            break;
                        case 1:
                            Utilities.EventBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.GameStateEvent,
                                this,
                                "CHANGE_STATE",
                                "LEVEL_START", "the-beach.txt"));
                            break;
                        case 2:
                            Utilities.EventBus.GetBus().RegisterEvent(GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.GameStateEvent,
                                this,
                                "CHANGE_STATE",
                                "MAIN_MENU", ""));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void RenderState() {
            BackgroundImage.RenderEntity();
            switch (ActiveLevelButton) {
                case 0:
                    Levels[0].SetColor(new Vec3F(1f,0f,0f));
                    Levels[1].SetColor(new Vec3F(0.5f,0.5f,0f));
                    Levels[2].SetColor(new Vec3F(0.5f,0.5f,0f));
                    break;
                case 1:
                    Levels[0].SetColor(new Vec3F(0.5f,0.5f,0f));
                    Levels[1].SetColor(new Vec3F(1f,0f,0f));
                    Levels[2].SetColor(new Vec3F(0.5f,0.5f,0f));
                    break;
                case 2:
                    Levels[0].SetColor(new Vec3F(0.5f,0.5f,0f));
                    Levels[1].SetColor(new Vec3F(0.5f,0.5f,0f));
                    Levels[2].SetColor(new Vec3F(1f,0f,0f));
                    break;
                default:
                    Levels[0].SetColor(new Vec3F(0.5f,0.5f,0f));
                    Levels[1].SetColor(new Vec3F(0.5f,0.5f,0f));
                    Levels[2].SetColor(new Vec3F(0.5f,0.5f,0f));
                    break;
            }
            foreach (Text text in Levels) {
                text.RenderText();
            }
        }
    }
}