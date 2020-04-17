using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;

namespace SpaceTaxi_1.LevelLoading {
    public class Level {
        // Add fields as needed
        private EntityContainer obstacles;
        //private Player player;

        public Level() {
            this.obstacles = new EntityContainer();
        }

        public void UpdateLevelLogic() {
           // all update logic here
        }

        public void RenderLevelObjects() {
            // all rendering here
            obstacles.RenderEntities();
        }

        public void AddObstacle(StationaryShape obs, Image img) {
            obstacles.AddStationaryEntity(obs, img);
        }
    }
}
