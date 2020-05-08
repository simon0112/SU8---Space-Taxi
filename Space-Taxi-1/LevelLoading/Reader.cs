using System.IO;
using System.Collections.Generic;
using SpaceTaxi_1.Utilities;
using System;

namespace SpaceTaxi_1.LevelLoading {
    public class Reader {
        // creates the list, so we can get the value if needed.
        // Or sets the value.
        public List<string> MapData {get; private set;}
        public List<string> MetaData {get; private set;}
        public List<string> LegendData {get; private set;}
        public List<string> CustomerData {get; private set;}

        // constructor of the reader class.
        // its being sets when the class is called.
        public Reader() {
            this.MapData = new List<string>();
            this.MetaData = new List<string>();
            this.LegendData = new List<string>();
            this.CustomerData = new List<string>();
        }
        ///<summary> first creates it two int objetcs. 
        /// then it get each line of file as an entry in array lines. <summary/>
        ///<var name="filename">A variable used to find out which of the two levels are to be loaded </var>
        ///<return> void. </returns> 
        public void ReadFile(string filename) {
            int CustomerDataStart = -1;
            int linePointer = 0;
            
            string[] lines = File.ReadAllLines(Utils.GetLevelFilePath(filename));
        
            // Iterate over lines and add data till the corresponding field
            // It is assumed that every level has the same dimensions, AKA, amount of lines.
            for (int i = 0; i <= 22; i++) {
                this.MapData.Add(lines[i]);
            }
            // The same is assumed for Metadata
            for (int i = 24; i <= 25; i++) {
                this.MetaData.Add(lines[i]);
            }
            // This is used to find out when the legendData ends and when the CustomerData starts.
            while (CustomerDataStart == -1) {
                if (lines[linePointer].Length > 7) {
                    if (lines[linePointer].Substring(0, 8) == "Customer") {
                        CustomerDataStart = linePointer;
                    } else { 
                        linePointer++;
                    }
                } else {
                    linePointer++;
                }
            }
            // Since it is assumed that meta data and level data line amount is constant, the start of the legend data can be assumed to be constant
            for (int i = 27; i <= CustomerDataStart-2; i++) {
                this.LegendData.Add(lines[i]);
            }
            // Customer data is assumed to be the last data found within the level text file, as such, the last lines in the array after the found point where customer data starts, is added to the customer data list.
            for (int i = CustomerDataStart; i < lines.Length; i++) {
                this.CustomerData.Add(lines[i]);
            }
        }
        ///<summary>Deletes all data related to the reader, such that a new level can be read without having to close and open the whole program</summary>
        ///<returns>void</returns>
        public void EmptyData() {
            this.CustomerData.RemoveRange(0, CustomerData.Count);
            this.LegendData.RemoveRange(0, LegendData.Count);
            this.MetaData.RemoveRange(0, MetaData.Count);
            this.MapData.RemoveRange(0, MapData.Count);
        }
    }
}