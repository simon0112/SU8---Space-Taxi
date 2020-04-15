using System.IO;

namespace SpaceTaxi_1.LevelLoading {
    public class LevelCreator {
        // add fields as you see fit
        private Reader reader;

        public LevelCreator() {
            reader = new Reader();
        }
        
        public Level CreateLevel(string levelname) {
            // Create the Level here
            Level level = new Level();

            // Gather all the information/objects the levels needs here.
            reader.ReadFile(levelname);

            // ***IMPORTANT*** Kig på det her noget mere, er størrelsen på levelet konstant? Det vides ikke endnu, hvis det er, bliver alting meget nemmere ***IMPORTANT**

            for (int y = reader.MapData.Count; y >= 0; y--) {
                for (int x = 0; x < reader.MapData[y].Length; x++) {
                    var ImageIndex = reader.LegendData.IndexOf(reader.MapData[y].Substring(x,x));
                    var ImageName = reader.LegendData[ImageIndex].Substring(3);
                    level.AddObstacle((new DIKUArcade.Entities.StationaryShape((x/reader.MapData[y].Length),(y/reader.MapData.Count),(1/reader.MapData[y].Length),(1/reader.MapData.Count))), Path.Combine("Assets", ImageName));
                }
            }

            return level;
        }
    }
}
