using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using SpaceTaxi_1.Entities;

namespace SpaceTaxi_1.LevelLoading {
    public class Level {
        // Add fields as needed
        public EntityContainer obstacles {private set; get;}
        public EntityContainer platforms {private set; get;}
        public EntityContainer portal {private set; get;}
        private Player Player;

        // constructor of level class.
        public Level() {
            obstacles = new EntityContainer();
            platforms = new EntityContainer();
            portal = new EntityContainer();
        }

        public void EmptyData() {
            obstacles.ClearContainer();
            platforms.ClearContainer();
            portal.ClearContainer();
        }

        ///<summary> updates the level logic <summary/>
        ///<returns> void </returns> 
        public void UpdateLevelLogic() {
           // all update logic here
        }

        ///<summary> render the logic in the level objects <summary/>
        ///<returns> void </returns> 
        public void RenderLevelObjects() {
            // all rendering here
            obstacles.RenderEntities();

            Player.entity.RenderEntity();

            platforms.RenderEntities();
            portal.RenderEntities();
            Player.Entity.RenderEntity();
        }

        ///<summary> render the logic in the level objects <summary/>
        ///<variable name="obs"> StationaryShape object </variable>
        ///<variable name="img"> image object</variable>
        ///<returns> void </returns> 
        public void AddObstacle(StationaryShape obs, Image img) {
            obstacles.AddStationaryEntity(obs, img);
        }

        public void AddPlatform(StationaryShape obs, Image img) {
            platforms.AddStationaryEntity(obs, img);
        }

        public void AddPortal(StationaryShape obs, Image img) {
            portal.AddStationaryEntity(obs, img);
        }

        ///<summary> adds the player if needed <summary/>
        ///<variable name="play">new player instance </variable>
        ///<returns> void </returns> 
        public void AddPlayer(DynamicShape play) {
            Player = new Player(play);
        }
        ///<summary> returns the player <summary/>
        ///<returns> Player </returns> 
        public Player ReturnPlayer() {
            return this.Player;
        }
    }
}
