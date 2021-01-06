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

        public LauncherCore(string path)
        {
            string confStr = File.ReadAllText(path);
            conf = JsonSerializer.Deserialize<LauncherConfig>(confStr);
            savePath = path;
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

        public string[] GetNetworkList()
        {
            List<string> result = new List<string>();
            NetworkInterface[] netInfos = NetworkInterface.GetAllNetworkInterfaces();
            foreach(var netInfo in netInfos)
            {
                Console.WriteLine("-------{0}-------", netInfo.Name);
                var unis = netInfo.GetIPProperties().UnicastAddresses;
                foreach(var uni in unis)
                {
                    if(uni.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        Console.WriteLine("ipv4: {0}", uni.Address.ToString());
                    }
                    else if(uni.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
                    {
                        Console.WriteLine("ipv6: {0}", uni.Address.ToString());
                    }
                }
                Console.WriteLine("-----------------");
            }
            return result.ToArray();
        }
    }
}