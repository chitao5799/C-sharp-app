using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoCarroApp
{ 
    [Serializable]
    public class SocketData //lớp chứa dữ liệu truyền đi 
    {
        private int command;
        private Point point;
        private string message;
        public int Command { get => command; set => command = value; }
        public Point Point { get => point; set => point = value; }
        public string Message { get => message; set => message = value; }

        public SocketData(int command,string message ,Point point) //cho phép point được null khi truyền vào.
        {
            this.Command = command;
            this.Point = point;
            this.Message = message;
        }
        public enum SocketCommand
        {
            SEND_POINT,
            NOTIFY,
            NEW_GAME,
            UNDO,
            END_GAME,
            QUIT
        }
    }
}
