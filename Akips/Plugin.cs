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
    [BepInPlugin("io.github.landuu.akips", "Akips", "0.1")]
    public class Plugin : BaseUnityPlugin
    {
        private int _locker = 0;
        //private static HttpClient _httpClient = new HttpClient();

        private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo("WTF");

            //_httpClient.GetAsync("http://localhost:5183/test?data=tarkovstart");
        }

        private void Update()
        {
            if (_locker < 200)
            {
                _locker++;
                return;
            }
            _locker = 0;

            var gameWorld = Singleton<GameWorld>.Instance;
            if (gameWorld == null)
            {
                return;
            }
            var player = gameWorld.RegisteredPlayers.Find(p => p.IsYourPlayer);
            string location = player.Location;
            var pos = player.Position;

            string lok = $"Lokacja to {location}, pozycja {pos[0]} {pos[1]} {pos[2]}";
            Logger.LogInfo(lok);
            //_httpClient.GetAsync("http://localhost:5183/test?data=" + lok);
        }
    }
}
