using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatsonWebsocket;

namespace Akips_Relay
{
    internal static class ExtensionMethods
    {
        public static async Task SendToAll(this WatsonWsServer socket, string message)
        {
            var clients = socket.ListClients();
            foreach (var client in clients)
            {
                await socket.SendAsync(client, message);
            }
        }
    }
}
