﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatRoomProject.PersistentLayer;
using ChatRoomProject.CommunicationLayer;

namespace ChatRoomProject.LogicLayer
{
    public class ChatRoom
    {
        private List<IUser> users;
        private List<IMessage> messages;
        private IUser currentUser;
        public const string URL = "www";
        public const int VALID_GROUPID = 23;
        const string INVALID_GROUPID_ERROR = "The group doesn't exist!";
        const string INVALID_NICKNAME_ERROR = "Already used nickname";
        const string ILLEGAL_LOGIN = "You aren't registered, please register first";
        const string ILLEGAL_LENGTH_MESSAGE = "The message souldn't be more than 150 characters";
        public ChatRoom()
        {
            this.users = new List<IUser>();
            this.messages = new List<IMessage>();
            this.currentUser = null;
        }
        public void Start()
        {
            //restoring the users list
            List<String> usersData = UserHandler.RestoreUsers();
            foreach (string data in usersData)
            {
                string[] details = data.Split(',');
                this.users.Add(new User(details[0], details[1]));
            }

            //restoring the messages list
            List<String> messagesData = MessageHandler.RestoreMessages();
            foreach (string data in messagesData)
            {
                string[] details = data.Split(',');
                this.messages.Add(new Message(details[0], details[1], details[2], details[3], details[4]));
            }
        }

        /*Check if groupID and nickname inputs are legal. If groupID doesn't exist or nickname is already been used
        by the same group, the function throws an exception*/
        public bool Registration(string groupId, string nickname)
        {
            if (!IsValidnickname(groupId, nickname))
                throw new Exception(INVALID_NICKNAME_ERROR);
            else
            {
                this.currentUser = new User(groupId, nickname); //constructor adds user to file 
                this.users.Add(currentUser);
                return true;
            }

        }
        //Check if nickname is already been used in the same group. 
        private bool IsValidnickname(string groupId, string nickname)
        {
            foreach (IUser user in this.users)
            {
                if (user.GroupID().Equals(groupId) && user.Nickname().Equals(nickname))
                    return false;
            }
            return true;
        }

        //Check if the user is registered. If isn't throw an exception
        public bool Login(string groupId, string nickname)
        {
            foreach (IUser user in this.users)
            {
                if (user.GroupID().Equals(groupId) && user.Nickname().Equals(nickname))
                {
                    this.currentUser = user;
                    return true;
                }
            }
            throw new Exception(ILLEGAL_LOGIN);
        }
        //Logout the current user
        public void Logout()
        {
            this.currentUser.Logout();
            this.currentUser = null;
        }
        public void RetrieveNMessages(int number)
        {
            List<IMessage> retrievedMessages = Communication.Instance.GetTenMessages(URL);
            foreach (IMessage msg in retrievedMessages)
            {
                bool isAlreadySaved = false;
                foreach(IMessage savedMsg in this.messages)
                {
                    if(savedMsg.Id.Equals(msg.Id))
                    {
                        isAlreadySaved = true;
                        break;
                    }
                }
                if (!isAlreadySaved)
                {
                    Message newMessage = new Message(msg);
                    this.messages.Add(newMessage); 
                }
            }

        }
        public List<IMessage> DisplayNMessages(int number)
        {
            List<IMessage> display = new List<IMessage>();
            for (int i = this.messages.Count - 1; i >= 0 & number > 0; i = i - 1)
            {
                display.Add(new Message(this.messages[i]));
                number = number - 1;
            }
            display.Reverse();
            return display;

        }
        /*Check if the message content is legal.*/
        public void Send(string messageContent)
        {
            if (this.currentUser != null)
            {
                if (Message.CheckValidity(messageContent))
                {
                    IMessage msg = this.currentUser.Send(messageContent);
                    Message message = new Message(msg);
                    this.messages.Add(message);
                }
                else
                    throw new Exception(ILLEGAL_LENGTH_MESSAGE);
            }
            else
                throw new NullReferenceException();

        }

    }
}



