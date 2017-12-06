using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelEasy.Models
{
    public class AppEnum
    {
        public enum ManagerAction
        {
            None,
            ListAllHotels,
            DisplayHotelDetail,
            DeleteHotel,
            AddHotel,
            UpdateHotel,
            QueryHotelsByRoomsAvilable,
            Quit
        }
    }
}
