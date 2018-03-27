using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatRoomProject.LogicLayer;

namespace ChatRoomProject.PresentationLayer
{
    class Gui
    {
        private ChatRoom chatroom;
        const string INVALID_GROUPID_ERROR = "The group doesn't exist!";
        const string INVALID_NICKNAME_ERROR = "Already used nickname";

        public Gui(ChatRoom chat)
        {
            chatroom = chat;
        }
        public void Start()
        {
            Console.WriteLine("Welcome to our chat room");
            while (true)
            {
                Console.WriteLine("Press 1 - to registrate \n" +
                      "Press 2- to login +\n" +
                      "Press 0 -to exit");
                int request = int.Parse(Console.ReadLine());
                switch (request)
                {
                    case 0:
                        {
                            return; // exit the function and close the cmd
                        }
                    case 1:
                        {
                            Register();
                            break;
                        }
                    case 2:
                        {
                            Login();
                            break;
                        }

                    default:
                        {
                            Console.WriteLine("you enter illegall value, please init a correct value");
                            break;
                        }
                }
            }

        }
        public void Register()
        {
            string groupId;
            string nickname;
            Console.WriteLine("Insert GroupId:");
            groupId = Console.ReadLine();
            Console.WriteLine("Insert nickname: ");
            nickname = Console.ReadLine();
            try
            {
                chatroom.Registration(groupId, nickname);

            }
            catch (Exception e)
            {
                string exception = e.Message;
                Console.WriteLine(exception);
            }

        }
        public void Login()
        {
            Console.WriteLine("please enter your groupId");
            string groupId = Console.ReadLine();
            Console.WriteLine("please enter your nickname");
            string nickname = Console.ReadLine();
            try
            {
                chatroom.Login(groupId, nickname);
                AfterLogin();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("We will repeat the login process,please insert the correct values");
                Login();

            }
        }
        public void AfterLogin()
        {

            List<Message> Last20Messages = chatroom.DisplayNMessages(20); // display 20 last messages
            for (int i = 0; i < Last20Messages.Capacity; i++)
            {
                Console.WriteLine(Last20Messages.ElementAt(i));
            }
            while (true)
            {
                Console.WriteLine("if you want to send a message press 1" + "\n" +
                    "if you want to retrieve 10 messages press 2" + "\n" +
                    "if you want to Display 20 last messages press 3" + "\n" +
                    "if you want to Logout press 4");
                int choose = int.Parse(Console.ReadLine());
                switch (choose)
                {
                    case 1:
                        Send();
                        break;
                    case 2:

                        chatroom.RetrieveNMessages(10); // TODO - decide if to make it generic
                        break;
                    case 3:
                        Display(20);
                        break;
                    case 4:
                        Logout();
                        break;
                    default:
                        Console.WriteLine("you press illegal number, we will repeat our questions again");
                        break;
                }
            }

        }
        public void Display(int number)
        {
            List<Message> messages = chatroom.DisplayNMessages(number);
            foreach (Message msg in messages)
            {
                Console.WriteLine(msg.ToString());
            }
        }
        public void Logout()
        {
            chatroom.Logout();
        }
        public void Send()
        {
            Console.WriteLine("please enter your message, it can be only under 150 words");
            string messagetosend = Console.ReadLine();
            try
            {
                chatroom.Send(messagetosend);
                AfterLogin();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("we will repeat our questions, please enter legal message");
                Send();

            }

        }
        /*public static void DisplayNMessages()
          {
              Console.WriteLine("enter how many messages do you want to display");
              int nMessagestoDisplay = int.Parse(Console.ReadLine());
              List<Message> messages=chatroom.DisplayNMessages(nMessagestoDisplay); // TODO - decide if to make it generic
          }
      */
    }
}
