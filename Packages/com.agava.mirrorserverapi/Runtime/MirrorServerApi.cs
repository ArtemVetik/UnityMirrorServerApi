using Mirror;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace Agava.MirrorServerApi
{
    public static class MirrorServerApi
    {
        private static HttpClient _client;
        private static PortTransport _transport7777;
        private static PortTransport _transport7778;
        private static string _apiIp;
        private static ushort _apiPort;

        public static void Initialize(PortTransport transport7777, PortTransport transport7778, string apiIp, ushort apiPort)
        {
            _client = new HttpClient();
            _transport7777 = transport7777;
            _transport7778 = transport7778;
            _apiIp = apiIp;
            _apiPort = apiPort;
        }

        public static async Task<string> CreateServer()
        {
            var response = await _client.PostAsync($"http://{_apiIp}:{_apiPort}/createserver", null);
            var responseString = await response.Content.ReadAsStringAsync();
            
            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException exception)
            {
                throw new HttpRequestException($"Response: {responseString}", exception);
            }

            return responseString;
        }

        public static async Task Connect(string joinCode, Action<Server> serverLoaded = null)
        {
            var response = await _client.PostAsync($"http://{_apiIp}:{_apiPort}/connect/{joinCode}", null);
            var responseString = await response.Content.ReadAsStringAsync();

            try
            {
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException exception)
            {
                throw new HttpRequestException($"Response: {responseString}", exception);
            }

            var server =  JsonUtility.FromJson<Server>(responseString);

            _transport7777.Port = Convert.ToUInt16(server.port7777);
            _transport7778.Port = Convert.ToUInt16(server.port7777);

            await Task.Delay(1000);

            serverLoaded?.Invoke(server);

            NetworkManager.singleton.StartClient();
        }
    }
}
