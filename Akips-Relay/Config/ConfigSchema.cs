using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akips_Relay.Config
{
    internal class ConfigSchema
    {
        public string HttpIp { get; set; } = "127.0.0.1";
        public int HttpPort { get; set; } = 8080;
        public string SocketIp { get; set; } = "127.0.0.1";
        public int SocketPort { get; set; } = 9090;
    }
}
