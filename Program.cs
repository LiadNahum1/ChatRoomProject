using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChatRoomProject.LogicLayer;
using ChatRoomProject.PresentationLayer;
using System.IO;
namespace ChatRoomProject
{
    class Program
    {
        static void Main(string[] args)
        {
            
                ChatRoom cr = new ChatRoom();
                cr.Start();
                Gui gui = new Gui(cr);
                gui.Start();
            
        }
    }
}
