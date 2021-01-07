using System.Text.Json;
using System.IO;
using System;
using System.Net.NetworkInformation;
using System.Collections.Generic;

namespace gamelaunchercore
{
    class LauncherCore
    {
        private LauncherConfig conf;
        private string savePath;
        private List<LocalNetwork> localNets;

        public LauncherCore(string path)
        {
            string confStr = File.ReadAllText(path);
            conf = JsonSerializer.Deserialize<LauncherConfig>(confStr);
            savePath = path;
            localNets = new List<LocalNetwork>();
        }

        public GameConfig GetGameConfig(long id)
        {
            return conf.configs.Find( x => x.id == id);
        }

        public void SaveConfig()
        {
            string configStr = JsonSerializer.Serialize(conf, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(savePath, configStr);
        }

        public GameConfig AddGameConfig()
        {
            var tempConf = new GameConfig();
            conf.configs.Add(tempConf);
            return tempConf;
        }

        public bool DeleteGameConfig(long id)
        {
            return conf.configs.Remove(GetGameConfig(id));
        }

        public LocalNetwork[] GetNetworkList()
        {
            NetworkInterface[] netInfos = NetworkInterface.GetAllNetworkInterfaces();
            foreach(var netInfo in netInfos)
            {
                LocalNetwork tempLocalNet = new LocalNetwork(netInfo.Name);
                var unis = netInfo.GetIPProperties().UnicastAddresses;

                foreach(var uni in unis)
                {
                    if(uni.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        tempLocalNet.ipv4 = uni.Address.ToString();
                    }
                    else if(uni.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        tempLocalNet.ipv6 = uni.Address.ToString();
                    }
                }

                localNets.Add(tempLocalNet);
            }
            return localNets.ToArray();
        }
    }
}