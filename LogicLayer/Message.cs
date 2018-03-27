using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatRoomProject.CommunicationLayer;
using ChatRoomProject.PersistentLayer;


namespace ChatRoomProject.LogicLayer
{
    class Message: IMessage
    {
        private Guid id;
        private string nickname;
        private string groupId;
        private DateTime date;
        private string messageContent;
        const int MAX_LENGTH = 150;

        Guid IMessage.Id => throw new NotImplementedException();

        string IMessage.UserName => throw new NotImplementedException();

        DateTime IMessage.Date => throw new NotImplementedException();

        string IMessage.MessageContent => throw new NotImplementedException();

        public string GroupID => throw new NotImplementedException();

        public Guid Id()
        {
            return this.id;
        }

        public string UserName ()
        {
            return this.nickname;
        }

        public DateTime Date()
        {
            return this.date;
        }

        public string MessageContent()
        {
            return this.messageContent;
        }

        public string GroupId()
        {
            return this.groupId;
        }
        
        public Message(IMessage message)
        {
            this.id = message.Id;
            this.nickname = message.UserName;
            this.groupId = message.GroupID;
            this.date = message.Date;
            this.messageContent = message.MessageContent;
            Save();
        }
        public Message(string id, string nickname, string groupId, string date, string messageContent)
        {
            this.id = new Guid(id);
            this.nickname = nickname;
            this.groupId = groupId;
            this.date = DateTime.Parse(date);
            this.messageContent = messageContent;
            Save();
        }
        //Save message into file in persistent layer
        public void Save()
        {
            MessageHandler.SaveToFile(this.id, this.nickname, this.groupId, this.date, this.messageContent);
        }
        
        public static bool CheckValidity(string content)
        {
            if (content.Length > MAX_LENGTH)
                return false;
            return true;
        }

        public override string ToString()
        {
            return this.messageContent + " send by " + this.groupId + ":" + this.nickname + " " + this.date;
        }
    }
}
