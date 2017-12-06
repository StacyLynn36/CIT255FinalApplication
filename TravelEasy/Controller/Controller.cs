using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEasy.DAL;
using TravelEasy.Models;

namespace TravelEasy
{
    public class Controller
    {
        #region FIELDS

        bool active = true;

        #endregion

        #region PROPERTIES


        #endregion

        #region CONSTRUCTORS

        public Controller()
        {
            ApplicationControl();
        }

        #endregion

        #region METHODS

        private void ApplicationControl()
        {
            HotelRepository hotelRepository = new HotelRepository();

            ConsoleView.DisplayWelcomeScreen();

            using (hotelRepository)
            {
                List<Hotel> hotels = hotelRepository.GetAllHotels();
                
                int hotelID;
                Hotel hotel;
                string message;

                while (active)
                {
                    AppEnum.ManagerAction userActionChoice;


                    userActionChoice = ConsoleView.GetUserActionChoice();

                    switch (userActionChoice)
                    {
                        case AppEnum.ManagerAction.None:
                            break;
                        case AppEnum.ManagerAction.ListAllHotels:
                            ConsoleView.DisplayAllHotels(hotels);
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.DisplayHotelDetail:
                            hotelID = ConsoleView.GetHotelID(hotels);

                            ConsoleView.DisplayHotelDetail(hotelRepository.GetHotelByID(hotelID));
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.DeleteHotel:
                            hotelID = ConsoleView.GetHotelID(hotels);

                            hotelRepository.DeleteHotel(hotelID);
                            ConsoleView.DisplayReset();
                            message = String.Format("Hotel ID: {0} has been deleted from the list.", hotelID);
                            ConsoleView.DisplayMessage(message);
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.AddHotel:
                            hotel = ConsoleView.AddHotel();
                            hotelRepository.AddHotel(hotel);
                        
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.UpdateHotel:
                            hotelID = ConsoleView.GetHotelID(hotels);
                            hotel = hotelRepository.GetHotelByID(hotelID);

                            hotel = ConsoleView.UpdateHotels(hotel);

                            hotelRepository.UpdateHotel(hotel);
                            break;
                        case AppEnum.ManagerAction.QueryHotelsByRoomsAvilable:
                            List<Hotel> matchinghotels = new List<Hotel>();

                            int minimumRoomsAvailable;
                            int maximumRoomsAvailable;
                            ConsoleView.GetRoomsAvailableQueryMinMaxValues(out minimumRoomsAvailable, out maximumRoomsAvailable);

                            matchinghotels = hotelRepository.QueryByRoomsAvailable(minimumRoomsAvailable, maximumRoomsAvailable);
                            ConsoleView.DisplayQueryResults(matchinghotels);
                            ConsoleView.DisplayContinuePrompt();
                            break;
                        case AppEnum.ManagerAction.Quit:
                            active = false;
                            break;
                        default:
                            break;
                    }
                }
            }

            ConsoleView.DisplayExitPrompt();
        }

        #endregion

    }
}
