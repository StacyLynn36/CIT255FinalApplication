using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelEasy.Models;
using TravelEasy.Utilities;

namespace TravelEasy
{    
    
        public static class ConsoleView
        {
            #region ENUMERABLES


            #endregion

            #region FIELDS

            //
            // window size
            //
            private const int WINDOW_WIDTH = ViewSettings.WINDOW_WIDTH;
            private const int WINDOW_HEIGHT = ViewSettings.WINDOW_HEIGHT;

            //
            // horizontal and vertical margins in console window for display
            //
            private const int DISPLAY_HORIZONTAL_MARGIN = ViewSettings.DISPLAY_HORIZONTAL_MARGIN;
            private const int DISPALY_VERITCAL_MARGIN = ViewSettings.DISPALY_VERITCAL_MARGIN;

            #endregion

            #region CONSTRUCTORS

            #endregion

            #region METHODS

            /// <summary>
            /// method to display the manager menu and get the user's choice
            /// </summary>
            /// <returns></returns>
            public static AppEnum.ManagerAction GetUserActionChoice()
            {
                AppEnum.ManagerAction userActionChoice = AppEnum.ManagerAction.None;
                //
                // set a string variable with a length equal to the horizontal margin and filled with spaces
                //
                string leftTab = ConsoleUtil.FillStringWithSpaces(DISPLAY_HORIZONTAL_MARGIN);

                //
                // set up display area
                //
                DisplayReset();

                //
                // display the menu
                //
                DisplayMessage("Hotel Menu");
                DisplayMessage("");
                Console.WriteLine(
                    leftTab + "1. List All Hotels" + Environment.NewLine +
                    leftTab + "2. Quit" + Environment.NewLine +
                    leftTab + "3. Delete a Hotel" + Environment.NewLine +
                    leftTab + "4. Add a Hotel" + Environment.NewLine +
                    leftTab + "5. Display a Hotel Detail" + Environment.NewLine +
                    leftTab + "6. Update Hotel Information" + Environment.NewLine +
                    leftTab + "7. Query Hotel By Rooms Available" + Environment.NewLine +
                    leftTab + "E. Exit" + Environment.NewLine);

                DisplayMessage("");
                DisplayPromptMessage("Enter the number/letter for the menu choice.");
                ConsoleKeyInfo userResponse = Console.ReadKey(true);

                switch (userResponse.KeyChar)
                {
                    case '1':
                        userActionChoice = AppEnum.ManagerAction.ListAllHotels;
                        break;
                    case '2':
                        userActionChoice = AppEnum.ManagerAction.Quit;
                        break;
                    case '3':
                        userActionChoice = AppEnum.ManagerAction.DeleteHotel;
                        break;
                    case '4':
                        userActionChoice = AppEnum.ManagerAction.AddHotel;
                        break;
                    case '5':
                        userActionChoice = AppEnum.ManagerAction.DisplayHotelDetail;
                        break;
                    case '6':
                        userActionChoice = AppEnum.ManagerAction.UpdateHotel;
                        break;
                    case '7':
                        userActionChoice = AppEnum.ManagerAction.QueryHotelsByRoomsAvilable;
                        break;
                    case 'E':
                    case 'e':
                        userActionChoice = AppEnum.ManagerAction.Quit;
                        break;
                    default:
                        Console.WriteLine(
                            "You have selected an incorrect answer." + Environment.NewLine +
                            "Press any key to try again or the ESC key to exit.");

                        userResponse = Console.ReadKey(true);
                        if (userResponse.Key == ConsoleKey.Escape)
                        {
                            userActionChoice = AppEnum.ManagerAction.Quit;
                        }
                        break;
                }

                return userActionChoice;
            }

        //internal static void DisplayAllHotels(Hotel hotel)
        //{
        //    throw new NotImplementedException();
        //}

        /// <summary>
        /// method to display all the hotel information
        /// </summary>
        public static void DisplayAllHotels(List<Hotel> hotels)
            {
                DisplayReset();

                DisplayMessage("All of the area hotels are displayed below;");
                DisplayMessage("");

                StringBuilder columnHeader = new StringBuilder();

                columnHeader.Append("ID".PadRight(8));
                columnHeader.Append("Hotel".PadRight(25));
                columnHeader.Append("Rooms Available".PadRight(5));

                DisplayMessage(columnHeader.ToString());

                foreach (Hotel hotel in hotels)
                {
                    StringBuilder hotelInfo = new StringBuilder();

                    hotelInfo.Append(hotel.ID.ToString().PadRight(8));
                    hotelInfo.Append(hotel.Name.PadRight(25));
                    hotelInfo.Append(hotel.RoomsAvailable.ToString().PadRight(5));

                    DisplayMessage(hotelInfo.ToString());
                }
            }
            ///<summary>delete a hotel</summary>

            public static int GetHotelID(List<Hotel> hotels)
            {
                int hotelID = -1;

                DisplayAllHotels(hotels);
                DisplayMessage("");
                DisplayPromptMessage("Please Enter the hotel ID: ");

                hotelID = ConsoleUtil.ValidateIntegerResponse("Please Enter The Hotel ID: ", Console.ReadLine());

                return hotelID;

            }

            public static Hotel AddHotel()
            {                
                string userResponse = "";
                Hotel hotel = new Hotel();
                DisplayReset();

                DisplayMessage("");
                Console.WriteLine(ConsoleUtil.Center("Add A New Hotel", WINDOW_WIDTH));
                DisplayMessage("");

                DisplayPromptMessage("Please Enter A New Hotel ID: ");
                hotel.ID = ConsoleUtil.ValidateIntegerResponse("Please Enter A New Hotel ID: ", Console.ReadLine());
                DisplayMessage("");
                Console.WriteLine(userResponse);

                DisplayPromptMessage("Please Enter The New Hotel's Name: ");
                hotel.Name = Console.ReadLine();
                DisplayMessage("");
                Console.WriteLine(userResponse);

                DisplayPromptMessage("Please Enter The New Hotel's Rooms Available: ");
                hotel.RoomsAvailable = ConsoleUtil.ValidateIntegerResponse("Please Enter the New Hotel's Rooms Available: ", Console.ReadLine());
                Console.WriteLine(userResponse);

                return hotel;
            }
            ///<summary>
            ///display hotel info
            ///</summary>

            public static void DisplayHotelDetail(Hotel hotel)
            {
                DisplayReset();

                DisplayMessage("");
                Console.WriteLine(ConsoleUtil.Center("Hotel Details", WINDOW_WIDTH));
                DisplayMessage("");

                DisplayMessage(string.Format("Hotel: {0}", hotel.Name));
                DisplayMessage("");

                DisplayMessage(string.Format("ID: {0}", hotel.ID.ToString()));
                DisplayMessage(string.Format("Rooms Available: {0}", hotel.RoomsAvailable.ToString()));

                DisplayMessage("");
            }

            ///<summary>
            ///update the hotel's information
            ///</summary>

            public static Hotel UpdateHotels(Hotel hotel)
            {
                string userResponse = "";
                //int hotelID = 0;
                //int hotelID = 1;
                DisplayReset();

                DisplayMessage("");
                Console.WriteLine(ConsoleUtil.Center("Please Edit A Hotel's Information",
                    WINDOW_WIDTH));
                DisplayMessage("");

                  
                DisplayMessage(String.Format("Current Name: {0}", hotel.Name));
                
                DisplayPromptMessage("Please Enter A New Hotel Name or Just Hit Enter To Keep The Current One: ");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    hotel.Name = userResponse;
                }

                DisplayMessage("");

                DisplayMessage(String.Format("Current Rooms Available: {0}", hotel.RoomsAvailable.ToString()));
                DisplayPromptMessage("Please Enter The New Number Of Rooms Available Or Just Hit Enter To Keep The Current Rooms Available: ");
                userResponse = Console.ReadLine();

                if (userResponse != "")
                {
                    hotel.RoomsAvailable = ConsoleUtil.ValidateIntegerResponse("Please Enter the New Number Of Rooms Available.", userResponse);
                    DisplayContinuePrompt();

                }
                return hotel;
            }

            ///<summary>
            ///get the minimum and maximum values for rooms available query
            ///</summary>
            ///<parameter name = minimumvertical>minimum rooms available</parameter>
            ///<parameter name = maximumvertical>maximum rooms available</parameter>

            public static void GetRoomsAvailableQueryMinMaxValues(out int minimumRoomsAvailable, out int maximumRoomsAvailable)
            {
                minimumRoomsAvailable = 0;
                maximumRoomsAvailable = 0;
                string userResponse = "";

                DisplayReset();

                DisplayMessage("");
                Console.WriteLine(ConsoleUtil.Center("Please Query The Hotels By Rooms Available", WINDOW_WIDTH));
                DisplayMessage("");

                DisplayPromptMessage("Please Enter The Minimum Rooms Available: ");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    minimumRoomsAvailable = ConsoleUtil.ValidateIntegerResponse("Please Enter the New Minimum Rooms Available.", userResponse);

                }
                DisplayMessage("");

                DisplayPromptMessage("Please Enter The Maximum Rooms Available: ");
                userResponse = Console.ReadLine();
                if (userResponse != "")
                {
                    maximumRoomsAvailable = ConsoleUtil.ValidateIntegerResponse("Please Enter The New Maximum RoomsAvailable.", userResponse);

                }
                DisplayMessage("");

                DisplayMessage(String.Format("You Have Entered {0} Rooms As The Minimum Value and {1} As The Maximum Value.", minimumRoomsAvailable, maximumRoomsAvailable));

                DisplayMessage("");
                DisplayContinuePrompt();

            }

            public static void DisplayQueryResults(List<Hotel> matchinghotels)
            {
                DisplayReset();

                DisplayMessage("");
                Console.WriteLine(ConsoleUtil.Center("Please Display The Hotel Query Results", WINDOW_WIDTH));
                DisplayMessage("");

                DisplayMessage("All Of The Matching Hotels Have Been Displayed: ");
                DisplayMessage("");

                StringBuilder columHeader = new StringBuilder();

                columHeader.Append("ID".PadRight(8));
                columHeader.Append("Hotel".PadRight(25));

                DisplayMessage(columHeader.ToString());

                foreach (Hotel hotel in matchinghotels)
                {
                    StringBuilder hotelInfo = new StringBuilder();

                    hotelInfo.Append(hotel.ID.ToString().PadRight(8));
                    hotelInfo.Append(hotel.Name.PadRight(25));

                    DisplayMessage(hotelInfo.ToString());
                }
            }

            /// <summary>
            /// reset display to default size and colors including the header
            /// </summary>
            public static void DisplayReset()
            {
                Console.SetWindowSize(WINDOW_WIDTH, WINDOW_HEIGHT);

                Console.Clear();
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.BackgroundColor = ConsoleColor.White;

                Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
                Console.WriteLine(ConsoleUtil.Center("Travel Easy", WINDOW_WIDTH));
                Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

                Console.ResetColor();
                Console.WriteLine();
            }

            /// <summary>
            /// display the Continue prompt
            /// </summary>
            public static void DisplayContinuePrompt()
            {
                Console.CursorVisible = false;

                Console.WriteLine();

                Console.WriteLine(ConsoleUtil.Center("Press any key to continue.", WINDOW_WIDTH));
                ConsoleKeyInfo response = Console.ReadKey();

                Console.WriteLine();

                Console.CursorVisible = true;
            }


            /// <summary>
            /// display the Exit prompt
            /// </summary>
            public static void DisplayExitPrompt()
            {
                DisplayReset();

                Console.CursorVisible = false;

                Console.WriteLine();
                DisplayMessage("Thank you for using our application. Press any key to Exit.");

                Console.ReadKey();

                System.Environment.Exit(1);
            }

            /// <summary>
            /// display the welcome screen
            /// </summary>
            public static void DisplayWelcomeScreen()
            {
                Console.Clear();
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.BackgroundColor = ConsoleColor.White;

                Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));
                Console.WriteLine(ConsoleUtil.Center("Welcome to", WINDOW_WIDTH));
                Console.WriteLine(ConsoleUtil.Center("Travel Easy", WINDOW_WIDTH));
                Console.WriteLine(ConsoleUtil.FillStringWithSpaces(WINDOW_WIDTH));

                Console.ResetColor();
                Console.WriteLine();

                DisplayContinuePrompt();
            }

            /// <summary>
            /// display a message in the message area
            /// </summary>
            /// <param name="message">string to display</param>
            public static void DisplayMessage(string message)
            {
                //
                // calculate the message area location on the console window
                //
                const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
                const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

                // message is not an empty line, display text
                if (message != "")
                {
                    //
                    // create a list of strings to hold the wrapped text message
                    //
                    List<string> messageLines;

                    //
                    // call utility method to wrap text and loop through list of strings to display
                    //
                    messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);
                    foreach (var messageLine in messageLines)
                    {
                        Console.WriteLine(messageLine);
                    }
                }
                // display an empty line
                else
                {
                    Console.WriteLine();
                }
            }

            /// <summary>
            /// display a message in the message area without a new line for the prompt
            /// </summary>
            /// <param name="message">string to display</param>
            public static void DisplayPromptMessage(string message)
            {
                //
                // calculate the message area location on the console window
                //
                const int MESSAGE_BOX_TEXT_LENGTH = WINDOW_WIDTH - (2 * DISPLAY_HORIZONTAL_MARGIN);
                const int MESSAGE_BOX_HORIZONTAL_MARGIN = DISPLAY_HORIZONTAL_MARGIN;

                //
                // create a list of strings to hold the wrapped text message
                //
                List<string> messageLines;

                //
                // call utility method to wrap text and loop through list of strings to display
                //
                messageLines = ConsoleUtil.Wrap(message, MESSAGE_BOX_TEXT_LENGTH, MESSAGE_BOX_HORIZONTAL_MARGIN);

                for (int lineNumber = 0; lineNumber < messageLines.Count() - 1; lineNumber++)
                {
                    Console.WriteLine(messageLines[lineNumber]);
                }

                Console.Write(messageLines[messageLines.Count() - 1]);
            }


            #endregion
        }
    
}
