using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEasy
{
    class InitializeDataFile
    {
        public static void AddTestData()
        {
            List<Hotel> hotels = new List<Hotel>
            {

                // initialize the IList of hotels - note: no instantiation for an interface
                new Hotel() { ID = 1, Name = "West Bay Beach", RoomsAvailable = 350 },
                new Hotel() { ID = 2, Name = "Hotel Indigo", RoomsAvailable = 225 },
                new Hotel() { ID = 3, Name = "Park Place", RoomsAvailable = 325 },
                new Hotel() { ID = 4, Name = "Comfort Inn", RoomsAvailable = 200 },
                new Hotel() { ID = 5, Name = "Howard Johnson", RoomsAvailable = 400 },
                new Hotel() { ID = 6, Name = "Bayshore Resort", RoomsAvailable = 300 },
                new Hotel() { ID = 7, Name = "Sugar Beach Resort", RoomsAvailable = 450 },
                new Hotel() { ID = 8, Name = "Knights Inn", RoomsAvailable = 200 },
                new Hotel() { ID = 9, Name = "Hampton Inn", RoomsAvailable = 250 },
                new Hotel() { ID = 10, Name = "Travellodge", RoomsAvailable = 200 }
            };

            WriteAllHotels(hotels, DataSetting.dataFilePath);
        }

        /// <summary>
        /// method to write all hotel info to the data file
        /// </summary>
        /// <param name="hotels">list of hotel info</param>
        /// <param name="dataFilePath">path to the data file</param>
        public static void WriteAllHotels(List<Hotel> hotels, string dataFilePath)
        {
            string hotelString;

            List<string> hotelStringList = new List<string>();

            // build the list to write to the text file line by line
            foreach (var hotel in hotels)
            {
                hotelString = hotel.ID + "," + hotel.Name + "," + hotel.RoomsAvailable;
                hotelStringList.Add(hotelString);
            }

            // initialize a FileStream object for writing
            FileStream wfileStream = File.OpenWrite(DataSetting.dataFilePath);

            // wrap the FieldStream object in a using statement to ensure of the dispose
            using (wfileStream)
            {
                // wrap the FileStream object in a StreamWriter object to simplify writing strings
                StreamWriter sWriter = new StreamWriter(wfileStream);

                // write each line to the data file
                foreach (string hotel in hotelStringList)
                {
                    sWriter.WriteLine(hotel);
                }

                // be sure to close the StreamWriter object
                sWriter.Close();
            }
        }
    }
}

