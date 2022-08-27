using BepInEx;
using BepInEx.Logging;
using Comfort.Common;
using EFT;
using System;
using System.Net;
using System.Net.Http;
using UnityEngine;


namespace Akips
{
    [BepInPlugin("io.github.landuu.akips", "Akips", "1.0")]
    public class Plugin : BaseUnityPlugin
    {
        private int _timeSpanInFrames = 0;
        private static HttpClient _httpClient = new HttpClient();
        private static string _url = @"http://localhost:8080/gamedata?map={0}&x={1}&y={2}&z={3}";

        private void Awake()
        {
            _httpClient.Timeout = TimeSpan.FromSeconds(1);
            Logger.LogInfo("Akips Started! Remember to start relay server!");
        }

        private void Update()
        {
            if (_timeSpanInFrames < 500)
            {
                _timeSpanInFrames++;
                return;
            }
            _timeSpanInFrames = 0;

            var gameWorld = Singleton<GameWorld>.Instance;
            if (gameWorld == null)
                return;

            var player = gameWorld.RegisteredPlayers.Find(p => p.IsYourPlayer);
            if (player == null)
                return;

            string location = player.Location;
            var position = player.Position;
            string lok = $"Lokacja to {location}, pozycja {position[0]} {position[1]} {position[2]}";
            Logger.LogInfo(lok);
            SendGamedata(location, position);
        }

        private void SendGamedata(string map, Vector3 pos)
        {
            string reqestUrl = string.Format(_url, map, pos[0], pos[1], pos[2]);
            _httpClient.GetAsync(reqestUrl);
        }
    }
}
