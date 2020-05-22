using System;
using System.IO;
using System.Collections.Generic;
using SpaceTaxi_1.Entities;

namespace SpaceTaxi_1.LevelLoading {
    public class LevelCreator {
        // add fields as you see fit
        private List<string> LegendIndexFinder;
        public Reader reader {private set; get;}
        private string ImageName;
        private int ImageIndex;
        private Customer PrevLvlCustomer;

        ///<summary> the instantiator of the LevelCreator object</summary>
        ///<variable name="reader"> the reader that reads the files that save how the levels look</variable>
        ///<variable name="LegendIndexFinder"> A list used to determine which character in the map data has what index in the legend data</variable>
        ///<returns> </returns>
        public LevelCreator() {
            reader = new Reader();
            LegendIndexFinder = new List<string>();
        }
        ///<summary>Deletes all data related to the levelcreator, such that a new one can be created without having to close and open the whole program</summary>
        ///<returns>void</returns>
        public void EmptyData(Customer cust) {
            reader.EmptyData();
            this.LegendIndexFinder.RemoveRange(0, LegendIndexFinder.Count);
            this.ImageIndex = 0;
            this.ImageName = "";
            if (cust != null) {
                PrevLvlCustomer = cust;
            }
        }
        
        ///<summary> Creates and instantiates the level object, also adds obstacles and the player to the level<summary/>
        ///<variable name="levelname"> levelname is the name of the level that is to be created, used to find the right file</variable>
        ///<variable name="level"> the level itself</variable>
        ///<returns> The level that has been filled with obstacles and the player</returns> 
        public Level CreateLevel(string levelname) {
            // Create the Level here.
            Level level = new Level();

            // Gather all the information/objects the levels needs here.
            reader.ReadFile(levelname);

            string platforminfo = reader.MetaData[1].Substring(10);
            Console.WriteLine(platforminfo);

            for (int i = 0; i < reader.LegendData.Count; i++) {
                if (reader.LegendData[i] != "") {
                    LegendIndexFinder.Add(reader.LegendData[i].Substring(0,1));
                } else {
                    LegendIndexFinder.Add("");
                }
            }

            for (float y = 0; y < reader.MapData.Count; y++) {
                for (float x = 0; x < reader.MapData[(int) y].Length; x++) {
                    if (reader.MapData[(int) y].Substring((int) x, 1) == ">") {
                        // Adds the player if the character denoting it is encountered.
                        level.AddPlayer(new DIKUArcade.Entities.DynamicShape((x/reader.MapData[(int) y].Length),
                            ((y/-(float) reader.MapData.Count)+(1-( 1f/(float) reader.MapData.Count))),
                            (1f/(float) reader.MapData[(int) y].Length),
                            (1f/(float) reader.MapData.Count)), PrevLvlCustomer);
                    } else if (reader.MapData[(int) y].Substring((int) x, 1) != " ") {
                        //if it isn't a player-character, then it checks that the character reached isn't a " "
                        //after this it checks which character is read, and does different things depending.
                        if (reader.MapData[(int) y].Substring((int) x, 1) != "^") {
                            //As long as it isn't the character denoting a portal, the character is searched for in the LegendIndexFinder
                            //and the equivalent image-name is also found
                            ImageIndex = LegendIndexFinder.IndexOf(reader.MapData[(int) y].Substring((int) x, 1));
                            ImageName = reader.LegendData[ImageIndex].Substring(3);
                        }
                        if (platforminfo.Contains(reader.MapData[(int) y].Substring((int) x, 1))) {
                            //if the character is within the list of characters denoting a portal
                            //a platform object is created and stored in the platform data
                            level.AddPlatform((new DIKUArcade.Entities.StationaryShape((x/reader.MapData[(int) y].Length),
                                ((y/-(float) reader.MapData.Count)+(1-( 1f/(float) reader.MapData.Count))),
                                (1f/(float) reader.MapData[(int) y].Length),
                                (1f/(float) reader.MapData.Count))), reader.MapData[(int) y].Substring((int) x, 1),
                                new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", ImageName)));
                            if (!(platforminfo.Contains(reader.MapData[(int) y].Substring((int) x-3, 1))) 
                            && platforminfo.Contains(reader.MapData[(int) y].Substring((int) x+8, 1))) {
                                //this if statement is used to limit the placement of customer-objects.
                                //If this were not in place, then there would be one customer-object on top of each platform object.
                                foreach (String str in reader.CustomerData) {

                                    var tempcustomer = str.Split(' ');

                                    level.AddCustomer(new Customer(tempcustomer[1], int.Parse(tempcustomer[2]), tempcustomer[3], tempcustomer[4], int.Parse(tempcustomer[5]), int.Parse(tempcustomer[6]), new DIKUArcade.Entities.StationaryShape((x/reader.MapData[(int) y].Length),(((y-1)/-(float) reader.MapData.Count)+(1-((float) 1/(float) reader.MapData.Count))),((float) 1/(float) reader.MapData[(int) y].Length),((float) 1/(float) reader.MapData.Count))), reader.MapData[(int) y].Substring((int) x, 1));
                                }
                            }
                        } else if (reader.MapData[(int) y].Substring((int) x, 1) == "^") {
                            //adds a portal to the correct list of data if the corresponding character is reached.
                            level.AddPortal((new DIKUArcade.Entities.StationaryShape((x/reader.MapData[(int) y].Length),((y/-(float) reader.MapData.Count)+(1-((float) 1/(float) reader.MapData.Count))),((float) 1/(float) reader.MapData[(int) y].Length),((float) 1/(float) reader.MapData.Count))), new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", "aspargus-passage1.png")));
                        } else {
                            //Lastly adds any object that isn't a platform, space, or portal to the list of obstacles.
                            level.AddObstacle((new DIKUArcade.Entities.StationaryShape((x/reader.MapData[(int) y].Length),((y/-(float) reader.MapData.Count)+(1-((float) 1/(float) reader.MapData.Count))),((float) 1/(float) reader.MapData[(int) y].Length),((float) 1/(float) reader.MapData.Count))), new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", ImageName)));
                        }
                    }
                }
            }

            return level;
        }
    }
}
