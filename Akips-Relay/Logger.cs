using Pastel;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akips_Relay
{
    internal class Logger
    {
        public enum Header
        {
            Startup = 0,
            Http = 1,
            Websocket = 2
        }

        private string _time => DateTime.Now.ToLongTimeString();
        private string _timeHader => $"[{_time}]".Pastel(Color.Gray);

        public void Info(string message)
        {
            string output = $"{_timeHader} {message}";
            Console.WriteLine(output);
        }

        public void Info(string message, Header type)
        {
            string typeHeader = GetHeader(type);
            string output = $"{typeHeader} {message}";
            Info(output);
        }

        public void Warning(string message)
        {
            string output = $"{_timeHader} {message}".Pastel(Color.Yellow);
            Console.WriteLine(output);
        }

        public void Error(string message)
        {
            string output = $"{_timeHader} {message}".Pastel(Color.Red);
            Console.WriteLine(output);
        }

        private string GetHeader(Header type)
        {
            if (type == Header.Http)
                return "[Http]".Pastel(Color.PaleTurquoise);
            else if (type == Header.Websocket)
                return "[Websocket]".Pastel(Color.PaleGreen);
            else if (type == Header.Startup)
                return "[Startup]".Pastel(Color.Gold);
            return string.Empty;
        }
    }
}
