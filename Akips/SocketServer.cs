using BepInEx.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatsonWebsocket;

namespace Akips
{
    internal class SocketServer
    {
        public WatsonWsServer _server;
        private static ManualLogSource _logger;


        
        public SocketServer(ManualLogSource l)
        {
            _server = new WatsonWsServer("localhost", 25565, false);
            _server.ClientConnected += ClientConnected;
            _server.ClientDisconnected += ClientDisconnected;
            _server.MessageReceived += MessageReceived;
            _server.Start();
            _logger = l;
            _logger.LogInfo("Websocket: Start!");
        }

        public void Send(string message)
        {
            var clients = _server.ListClients();
            foreach (string client in clients)
            {
                _server.SendAsync(client, message);
            }
            
        }

        private static void ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            _logger.LogInfo("Websocket: Nowy klient połączony!");
        }

        private static void ClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            _logger.LogInfo("Websocket: Klient rozłączył się!");
        }

        private static void MessageReceived(object sender, MessageReceivedEventArgs args)
        {
            //_logger.LogInfo("Message received from " + args.IpPort + ": " + Encoding.UTF8.GetString(args.Data));
        }
    }
}
