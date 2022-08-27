using Akips_Relay.Config;
using Newtonsoft.Json;
using System.Text;
using WatsonWebserver;
using WatsonWebsocket;
using HttpMethod = WatsonWebserver.HttpMethod;

namespace Akips_Relay
{
    class Program
    {
        private static readonly Logger _logger;
        private static readonly ConfigManager _configManager;
        private static WatsonWsServer _socket;
        private static Server _http;

        static Program()
        {
            _logger = new Logger();
            _configManager = new ConfigManager(_logger);
        }

        static void Main()
        {
            _logger.Info("Start...", Logger.Header.Startup);

            var config = _configManager.GetConfig();
            if(config == null)
            {
                _logger.Error("Nieprawidłowy plik konfiguracyjny, zatrzymuję aplikację");
                return;
            } else
            {
                _logger.Info("Pomyślnie wczytano plik konfiguracyjny", Logger.Header.Startup);
            }

            _http = new Server(config.HttpIp, config.HttpPort, false, DefaultRoute);
            _http.Start();
            _logger.Info($"Uruchomiono serwer HTTP pod adresem http://{config.HttpIp}:{config.HttpPort}", Logger.Header.Startup);

            _socket = new WatsonWsServer(config.SocketIp, config.SocketPort, false);
            _socket.ClientConnected += ClientConnected;
            _socket.ClientDisconnected += ClientDisconnected;
            _socket.MessageReceived += MessageReceived;
            _socket.Start();
            _logger.Info($"Uruchomiono serwer Websocket pod adresem ws://{config.SocketIp}:{config.SocketPort}", Logger.Header.Startup);

            Console.ReadLine();
        }


        // Http server
        static async Task DefaultRoute(HttpContext ctx)
        {
            await ctx.Response.Send("All set!");
        }

        [StaticRoute(HttpMethod.GET, "/gamedata")]
        public static async Task PostGamedata(HttpContext ctx)
        {
            var queryParams = ctx.Request.Query.Elements;
            var queryJson = JsonConvert.SerializeObject(queryParams);
            var gamedata = Gamedata.FromJson(queryJson);
            if(gamedata == null || gamedata.IsAnyNullOrEmpty())
            {
                ctx.Response.StatusCode = 400;
                await ctx.Response.Send();
                _logger.Info($"BadRequest: gamedata == null", Logger.Header.Http);
                return;
            }

            var json = gamedata.ToJson();
            await _socket.SendToAll(json);
            await ctx.Response.Send("Ok");
            _logger.Info($"Rozesłano {json}", Logger.Header.Http);
        }


        // Websocket
        static void ClientConnected(object? sender, ClientConnectedEventArgs args)
        {
            _logger.Info($"Klient połączony: {args.IpPort}", Logger.Header.Websocket);
        }

        static void ClientDisconnected(object? sender, ClientDisconnectedEventArgs args)
        {
            _logger.Info($"Klient rozłączony: {args.IpPort}", Logger.Header.Websocket);
        }

        static void MessageReceived(object? sender, MessageReceivedEventArgs args)
        {
            _logger.Info($"Otrzymano wiadomość ({args.IpPort}): {Encoding.UTF8.GetString(args.Data)}", Logger.Header.Websocket);
        }
    }
}

