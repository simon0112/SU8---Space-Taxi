using System.IO;
using System.Collections.Generic;
using SpaceTaxi_1.Utilities;
using System;

namespace SpaceTaxi_1.LevelLoading {
    public class Reader {
        public List<string> MapData {get; private set;}
        public List<string> MetaData {get; private set;}
        public List<string> LegendData {get; private set;}
        public List<string> CustomerData {get; private set;}


        public Reader() {
            this.MapData = new List<string>();
            this.MetaData = new List<string>();
            this.LegendData = new List<string>();
            this.CustomerData = new List<string>();
        }
        public void ReadFile(string filename) {
            int CustomerDataStart = -1;
            int linePointer = 0;
            // Get each line of file as an entry in array lines.
            string[] lines = File.ReadAllLines(Utils.GetLevelFilePath(filename));
        
            // Iterate over lines and add data till the corresponding field
            // It is assumed that every level has the same dimensions, AKA, amount of lines
            for (int i = 0; i <= 22; i++) {
                this.MapData.Add(lines[i]);
            }
            // The same is assumed for Metadata
            for (int i = 24; i <= 25; i++) {
                this.MetaData.Add(lines[i]);
            }
            // This is used to find out when the legendData ends and when the CustomerData starts
            while (CustomerDataStart == -1) {
                if (lines[linePointer].Length > 7) {
                    Console.WriteLine(lines[linePointer].Substring(0, 8));
                    if (lines[linePointer].Substring(0, 8) == "Customer") {
                        CustomerDataStart = linePointer;
                    } else { 
                        linePointer++;
                    }
                }
            }
            // Since it is assumed that meta data and level data line amount is constant, the start of the legend data can be assumed to be constant
            for (int i = 27; i <= CustomerDataStart-2; i++) {
                this.LegendData.Add(lines[i]);
            }
            // Customer data is assumed to be the last data found within the level text file, as such, the last lines in the array after the found point where customer data starts, is added to the customer data list.
            for (int i = CustomerDataStart; i <= lines.Length; i++) {
                this.CustomerData.Add(lines[i]);
            }
        }
    }
}