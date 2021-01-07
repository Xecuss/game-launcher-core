using System.Text.Json;
using System.IO;
using System.Net.NetworkInformation;
using System.Collections.Generic;

namespace gamelaunchercore
{
    class LauncherCore
    {
        private LauncherConfig conf;
        private string savePath;
        private List<LocalNetwork> localNets;
        private long selected;

        public string cmd
        {
            get
            {
                GameConfig useConf = conf.configs.Find(item => item.id == selected);
                string res = "spice64";
                if (useConf.useSpice32) res = "spice";
                if (useConf.use720p) res += " -sdvx720";
                if (useConf.window) res += " -w";
                if (useConf.usePrinter)
                {
                    res += " -printer";
                    if (useConf.printerPath.Trim() != "") res += $" -printerPath \"{useConf.printerPath}\"";
                }
                if (useConf.card.Trim() != "") res += $" -card0 {useConf.card}";
                NetworkConfig network = conf.useAbleNetWorkConf.Find(item => item.id == useConf.nowUseNetwork);
                if(network != null)
                {
                    if (network.url.Trim() != "") res += $" -url {network.url}";
                    res += network.urlSlash ? " -urlslash 1" : " -urlslash 0";
                    res += network.http11 ? " -http11 1" : " -http11 0";
                    if (network.pcbId.Trim() != "") res += $" -p {network.pcbId}";
                }
                return res;
            }
        }

        public LauncherCore(string path)
        {
            string confStr = File.ReadAllText(path);
            conf = JsonSerializer.Deserialize<LauncherConfig>(confStr);
            savePath = path;
            GetNetworkList();
            selected = conf.configs[0].id;
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
            localNets = new List<LocalNetwork>();
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