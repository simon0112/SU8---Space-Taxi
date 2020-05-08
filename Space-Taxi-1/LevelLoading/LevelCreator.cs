using System;
using System.IO;
using System.Collections.Generic;

namespace SpaceTaxi_1.LevelLoading {
    public class LevelCreator {
        // add fields as you see fit
        private List<string> LegendIndexFinder;
        public Reader reader {private set; get;}
        private string ImageName;
        private int ImageIndex;

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
        public void EmptyData() {
            reader.EmptyData();
            this.LegendIndexFinder.RemoveRange(0, LegendIndexFinder.Count);
            this.ImageIndex = 0;
            this.ImageName = "";
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
                        level.AddPlayer(new DIKUArcade.Entities.DynamicShape((x/reader.MapData[(int) y].Length),((y/-(float) reader.MapData.Count)+(1-((float) 1/(float) reader.MapData.Count))),((float) 1/(float) reader.MapData[(int) y].Length),((float) 1/(float) reader.MapData.Count)));
                    } else if (reader.MapData[(int) y].Substring((int) x, 1) != " ") {
                        if (reader.MapData[(int) y].Substring((int) x, 1) != "^") {
                            ImageIndex = LegendIndexFinder.IndexOf(reader.MapData[(int) y].Substring((int) x, 1));
                            ImageName = reader.LegendData[ImageIndex].Substring(3);
                        }
                        if (platforminfo.Contains(reader.MapData[(int) y].Substring((int) x, 1))) {
                            level.AddPlatform((new DIKUArcade.Entities.StationaryShape((x/reader.MapData[(int) y].Length),((y/-(float) reader.MapData.Count)+(1-((float) 1/(float) reader.MapData.Count))),((float) 1/(float) reader.MapData[(int) y].Length),((float) 1/(float) reader.MapData.Count))), new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", ImageName)));
                        } else if (reader.MapData[(int) y].Substring((int) x, 1) == "^") {
                            level.AddPortal((new DIKUArcade.Entities.StationaryShape((x/reader.MapData[(int) y].Length),((y/-(float) reader.MapData.Count)+(1-((float) 1/(float) reader.MapData.Count))),((float) 1/(float) reader.MapData[(int) y].Length),((float) 1/(float) reader.MapData.Count))), new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", "aspargus-passage1.png")));
                        } else {
                            level.AddObstacle((new DIKUArcade.Entities.StationaryShape((x/reader.MapData[(int) y].Length),((y/-(float) reader.MapData.Count)+(1-((float) 1/(float) reader.MapData.Count))),((float) 1/(float) reader.MapData[(int) y].Length),((float) 1/(float) reader.MapData.Count))), new DIKUArcade.Graphics.Image(Path.Combine("Assets", "Images", ImageName)));
                        }
                    }
                }
            }

            return level;
        }
    }
}
