using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEasy.DAL
{
    /// <summary>
    /// method to write all of the hotel information to the date file
    /// </summary>
    public class HotelRepository : IDisposable
    {
        private List<Hotel> _hotels;
        

        public HotelRepository()
        {
            _hotels = ReadHotelData(DataSetting.dataFilePath);
        }

        /// <summary>
        /// method to read all of the hotel information from the data file and return it as a list of hotel objects
        /// </summary>
        /// <param name="dataFilePath">path the data file</param>
        /// <returns>list of hotel objects</returns>
        public static List<Hotel> ReadHotelData(string dataFilePath)
        {
            const char delineator = ',';

            // create lists to hold the hotel strings and objects
            List<string> hotelStringList = new List<string>();
            List<Hotel> hotelClassList = new List<Hotel>();

            // initialize a StreamReader object for reading
            StreamReader sReader = new StreamReader(DataSetting.dataFilePath);

            using (sReader)
            {
                // keep reading lines of text until the end of the file is reached
                while (!sReader.EndOfStream)
                {
                    hotelStringList.Add(sReader.ReadLine());
                }
            }

            foreach (string hotel in hotelStringList)
            {
                
                // use the Split method and the delineator on the array to separate each property into an array of properties
                string[] properties = hotel.Split(delineator);

                // populate the hotel list with hotel objects
                hotelClassList.Add(new Hotel() { ID = Convert.ToInt32(properties[0]), Name = properties[1], RoomsAvailable = Convert.ToInt32(properties[2]) });
            }

            return hotelClassList;
        }

        /// <summary>
        /// method to write all of the list of hotels to the text file
        /// </summary>
        public void WriteHotelData()
        {
            string hotelString;

            // wrap the FileStream object in a StreamWriter object to simplify writing strings
            StreamWriter sWriter = new StreamWriter(DataSetting.dataFilePath, false);

            using (sWriter)
            {
                foreach (Hotel hotel in _hotels)
                {
                    hotelString = hotel.ID + "," + hotel.Name + "," + hotel.RoomsAvailable;

                    sWriter.WriteLine(hotelString);
                }
            }
        }

        /// <summary>
        /// method to add a new hotel
        /// </summary>
        /// <param name="hotel"></param>
        public void AddHotel(Hotel hotel)
        {
            _hotels.Add(hotel);
        }

        /// <summary>
        /// method to delete a hotel by the hotel ID
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteHotel(int ID)
        {
            for (int index = 0; index < _hotels.Count(); index++)
            {
                if (_hotels[index].ID == ID)
                {
                    _hotels.RemoveAt(index);
                }
            }

            WriteHotelData();
        }

        /// <summary>
        /// method to update an existing hotel's information
        /// </summary>
        /// <param name="hotel">hotel object</param>
        public void UpdateHotel(Hotel hotel)
        {
            DeleteHotel(hotel.ID);
            AddHotel(hotel);

            WriteHotelData();
        }

        /// <summary>
        /// method to return a hotel object given the ID
        /// </summary>
        /// <param name="ID">int ID</param>
        /// <returns>hotel object</returns>
        public Hotel GetHotelByID(int ID)
        {
            Hotel hotel = null;

            hotel = _hotels.FirstOrDefault(hl => hl.ID == ID);

            return hotel;
        }

        /// <summary>
        /// method to return a list of hotel objects
        /// </summary>
        /// <returns>list of hotel objects</returns>
        public List<Hotel> GetAllHotels()
        {
            return _hotels;
        }

        /// <summary>
        /// method to query the hotel by the rooms available
        /// </summary>
        /// <param name="minimumRoomsAvailable">int minimum rooms available</param>
        /// <param name="maximumRoomsAvailable">int maximum rooms available</param>
        /// <returns></returns>
        public List<Hotel> QueryByRoomsAvailable(int minimumRoomsAvailable, int maximumRoomsAvailable)
        {
            List<Hotel> matchinghotels = new List<Hotel>();

            //
            // use a lambda expression with the Where method to query
            //
            matchinghotels = _hotels.Where(hl => hl.RoomsAvailable >= minimumRoomsAvailable && hl.RoomsAvailable <= maximumRoomsAvailable).ToList();

            return matchinghotels;

            //List<Hotel> matchinghotels = new List<Hotel>();

            //return matchinghotels;
        }

        /// <summary>
        /// method to handle the IDisposable interface contract
        /// </summary>
        public void Dispose()
        {
            _hotels = null;
        }
    }
}
