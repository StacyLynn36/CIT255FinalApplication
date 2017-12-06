using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEasy
{
    public class Hotel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int RoomsAvailable { get; set; }

        public Hotel()
        {

        }

        public Hotel(int id, string Name, int RoomsAvailable)
        {
            this.ID = id;
            this.Name = Name;
            this.RoomsAvailable = RoomsAvailable;
        }
    }
}
