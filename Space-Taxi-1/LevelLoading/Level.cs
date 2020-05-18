using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using SpaceTaxi_1.Entities;
using System.Collections.Generic;

namespace SpaceTaxi_1.LevelLoading {
    public class Level {
        // Add fields as needed
        public EntityContainer obstacles {private set; get;}
        public EntityContainer platforms {private set; get;}
        public EntityContainer portal {private set; get;}
        public List<Customer> Customers {private set; get;}
        private Player Player;

        // constructor of level class.
        public Level() {
            obstacles = new EntityContainer();
            platforms = new EntityContainer();
            portal = new EntityContainer();
            Customers = new List<Customer>();
        }
        ///<summary>Deletes all data related to the level, such that a new one can be created without having to close and open the whole program</summary>
        ///<returns>void</returns>
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
            platforms.RenderEntities();
            portal.RenderEntities();
            Player.entity.RenderEntity();
            foreach (Customer cust in Customers) {
                if (cust.visible == true) {
                    cust.entity.RenderEntity();
                }
            }
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

        public void AddCustomer(Customer cust) {
            Customers.Add(cust);
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
