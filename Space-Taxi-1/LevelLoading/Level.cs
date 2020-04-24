using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using SpaceTaxi_1.Entities;

namespace SpaceTaxi_1.LevelLoading {
    public class Level {
        // Add fields as needed
        private EntityContainer obstacles;
        private Player Player;

        // constructor of level class.
        public Level() {
            obstacles = new EntityContainer();
        }

        public void UpdateLevelLogic() {
           // all update logic here
        }

        public void RenderLevelObjects() {
            // all rendering here
            obstacles.RenderEntities();
            Player.Entity.RenderEntity();
        }

        public void AddObstacle(StationaryShape obs, Image img) {
            obstacles.AddStationaryEntity(obs, img);
        }

        // adds the player if needed
        public void AddPlayer(DynamicShape play) {
            Player = new Player(play);
        }
        // returns the player
        public Player ReturnPlayer() {
            return this.Player;
        }
    }
}
