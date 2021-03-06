﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatRoomProject.PersistentLayer;
using ChatRoomProject.CommunicationLayer;

namespace ChatRoomProject.LogicLayer
{
    public class User : IUser
    {

        private string groupID;
        private string nickname;
        public User(string groupID, string nickname)
        {
            this.groupID = groupID;
            this.nickname = nickname;
            Save();
        }
        public string GroupID()
        {
            return this.groupID;
        }
        public string Nickname()
        {
            return this.nickname;
        }
        public void Save()
        {
            UserHandler.SaveToFile(Nickname(), GroupID());
        }
        //returns message that recieved from server. The function build a Message instance that save itself in the constructor
        public IMessage Send(string messageContent)
        {
            IMessage message = Communication.Instance.Send(ChatRoom.URL, GroupID(), Nickname(), messageContent);
            return message;
        }
        public void Logout()
        {
            //its functionality will be only in the logger
        }
        public override string ToString()
        {
            return GroupID() + Nickname();
        }
    }
}
