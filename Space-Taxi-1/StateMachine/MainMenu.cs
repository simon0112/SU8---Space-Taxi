using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;
using System.IO;
using DIKUArcade.EventBus;

namespace SpaceTaxi_1.StateMachine {
    public class MainMenu : IGameState {
        private static MainMenu instance = null;
        private Entity BackgroundImage;
        private Text[] menuButtons;
        private int activeMenuButton = 0;
        public static MainMenu GetInstance() {
            return MainMenu.instance ?? (MainMenu.instance = new MainMenu());
        }

//      Part of the IGameState interface, so has to be within the class, even if it isn't used.
//      Is not used since there is no core gameloop to run while in the main menu
        public void GameLoop() {

        }

        ///<summary>Initializes the 'Main-Menu' state of the game</summary>
        ///<returns> void </returns>
        public void InitializeGameState() {
            menuButtons = new Text[2] {new Text("- New Game", new Vec2F(0.35f,0.5f), new Vec2F(0.5f,0.4f)), new Text("- Quit", new Vec2F(0.35f,0.0f), new Vec2F(0.5f,0.4f))};

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
                if (keyValue == "KEY_UP") {
                    activeMenuButton = 0;
                } else if (keyValue == "KEY_DOWN") {
                    activeMenuButton = 1;
                } else if (keyValue == "KEY_ENTER") {
                    if (activeMenuButton == 0) {
                        Utilities.EventBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.GameStateEvent,
                                this,
                                "CHANGE_STATE",
                                "LEVEL_SELECT", ""));
                    } else if (activeMenuButton == 1) {
                        Utilities.EventBus.GetBus().RegisterEvent(
                            GameEventFactory<object>.CreateGameEventForAllProcessors(
                                GameEventType.WindowEvent, this, "CLOSE_WINDOW", "", ""));
                    }
                }
            }

        }

        public void RenderState() {
                BackgroundImage.RenderEntity();
            if (activeMenuButton == 0) {
                menuButtons[0].SetColor(new Vec3F(1f,0f,0f));
                menuButtons[1].SetColor(new Vec3F(0.5f,0.5f,0f));
            } else if (activeMenuButton == 1) {
                menuButtons[1].SetColor(new Vec3F(1,0,0));
                menuButtons[0].SetColor(new Vec3F(0.5f,0.5f,0f));
            }
            foreach (Text text in menuButtons) {
                text.RenderText();
            }
        }
    }
}