using DIKUArcade.Entities;
using DIKUArcade.Graphics;


namespace SpaceTaxi_1.Entities {
    public class Platform {
        public string Name;
        
        public Entity entity { get; private set; }

        ///<summary> Platform constructor <summary/>
        ///<variable name="name"> name of the platform</variable>
        ///<variable name="shape."> the position of the shape </variable>
        ///<variable name="image"> the texture of the platform</variable>
        ///<returns> void, but instantiates the player object </returns> 
        public Platform(string name, StationaryShape shape, Image img) {
            Name = name;
            entity = new Entity(shape, img);
        }
    }
}
