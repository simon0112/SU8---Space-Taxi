using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using System.IO;
using SpaceTaxi_1.Entities;
using System.Collections.Generic;

namespace SpaceTaxi_1.LevelLoading {
    public class Level {
        // Add fields as needed
        public EntityContainer obstacles {private set; get;}
        public List<Platform> platforms {private set; get;}
        public EntityContainer portal {private set; get;}
        public List<Customer> Customers {private set; get;}
        private Player Player;

        // constructor of level class.
        public Level() {
            obstacles = new EntityContainer();
            platforms = new List<Platform>();
            portal = new EntityContainer();
            Customers = new List<Customer>();
        }

        ///<summary>Deletes all data related to the level, such that a new one can be created without having to close and open the whole program</summary>
        ///<returns>void</returns>
        public void EmptyData() {
            obstacles.ClearContainer();
            platforms.RemoveRange(0, platforms.Count);
            portal.ClearContainer();
            foreach (Customer cust in Customers) {
                cust.visible = false;
            }
            Customers.RemoveRange(0,Customers.Count);
        }

        ///<summary> render the logic in the level objects <summary/>
        ///<returns> void </returns> 
        public void RenderLevelObjects() {
            // all rendering here
            obstacles.RenderEntities();
            foreach (Platform plat in platforms) {
                plat.entity.RenderEntity();
            }
            portal.RenderEntities();
            Player.entity.RenderEntity();
            foreach (Customer cust in Customers) {
                if (cust.visible == true) {
                    cust.entity.RenderEntity();
                }
            }
        }

        ///<summary>  adds an obstacle to the collective data of the level <summary/>
        ///<variable name="obs"> StationaryShape object </variable>
        ///<variable name="img"> image object</variable>
        ///<returns> void </returns> 
        public void AddObstacle(StationaryShape obs, Image img) {
            obstacles.AddStationaryEntity(obs, img);
        }

        ///<summary> adds a platform to the collective data of the level</summary>
        ///<var name="shape"> The shape that is to be added </var>
        ///<var name="name"> The name of the platform that is being added, this is used for collision detection and customer placement</var>
        ///<var name="img"> The image that the platform is rendered with</var>
        ///<returns> void </returns>
        public void AddPlatform(StationaryShape shape, string name, Image img) {
            platforms.Add(new Platform(name, shape, img));
        }

        ///<summary> adds a portal to the collective data of the level</summary>
        ///<var name="shape"> The shape that is to be added </var>
        ///<var name="img"> The image that the portal is rendered with</var>
        ///<returns> void </returns>
        public void AddPortal(StationaryShape obs, Image img) {
            portal.AddStationaryEntity(obs, img);
        }

        ///<summary> adds a customer to the collective data of the level</summary>
        ///<var name="cust"> the customer object that is to be added </var>
        ///<var name="platName"> the name of the platform that the customer is being put on top of,
        ///used to find out if the customer is being placed at the right platform</var>
        ///<returns> void </returns>
        public void AddCustomer(Customer cust, string platName) {
            if (cust.startPlatform.Contains(platName)) {
                Customers.Add(cust);
            }
        }

        ///<summary> adds the player if needed <summary/>
        ///<variable name="play">new player instance </variable>
        ///<variable name="cust">The customer in the taxi, if there is one</variable>
        ///<returns> void </returns> 
        public void AddPlayer(DynamicShape play, Customer cust) {
            Player = new Player(play, cust);
        }
        
        ///<summary> returns the player <summary/>
        ///<returns> Player </returns> 
        public Player ReturnPlayer() {
            return this.Player;
        }
    }
}
