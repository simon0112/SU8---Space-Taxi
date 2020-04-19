using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using SpaceTaxi_1.Entities;

namespace SpaceTaxi_1.LevelLoading {
    public class Level {
        // Add fields as needed
        private EntityContainer obstacles;
        private Player player;

        public Level() {
            obstacles = new EntityContainer();
        }

        public void UpdateLevelLogic() {
           // all update logic here
        }

        public void RenderLevelObjects() {
            // all rendering here
            obstacles.RenderEntities();
            this.player.Entity.RenderEntity();
        }

        public void AddObstacle(StationaryShape obs, Image img) {
            obstacles.AddStationaryEntity(obs, img);
        }

        public void AddPlayer(DynamicShape player) {
            this.player = new Player(player);
        }
    }
}
