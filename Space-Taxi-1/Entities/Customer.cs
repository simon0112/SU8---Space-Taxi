using DIKUArcade.Entities;
using System.IO;



namespace SpaceTaxi_1.Entities {
    public class Customer {
        
        public Entity entity { get; private set; }
        public string Name { get; private set; }
        public int SpawnTime { get; private set; }
        public string startPlatform { get; private set; }
        public string goalPlatform { get; private set; }
        public int TimeLimit { get; private set; }
        public int pointWorth { get; private set; }
        public bool visible;

        ///<summary> Customer constructor <summary/>
        ///<variable name="name"> name of the customer</variable>
        ///<variable name="spawnTime">amount of seconds before spawning </variable>
        ///<variable name="a letter</variable>
        //////<variable name="end"> a letter </variable>
        ///<variable name="limit"> limit amount </variable>
        ///<variable name="worth"> worth amount. </variable>
        ///<returns> customer object </returns> 
        public Customer(string name, int spwnTme, string start,
            string end, int Limit, int worth, StationaryShape shape) {
            Name = name;
            SpawnTime = spwnTme;
            startPlatform = start;
            
            if (end.Contains('^') && end.Length > 1) {
                goalPlatform = end.Substring(1);
            } else if (end.Contains('^') && end.Length == 1) {
                goalPlatform = "Portal";
            } else {
                goalPlatform = end;
            }
            
            visible = false;

            TimeLimit = Limit;
            pointWorth = worth;
            entity = new Entity(shape, new DIKUArcade.Graphics.Image
                (Path.Combine("Assets", "Images", "CustomerStandRight.png")));
        }
    }
}
