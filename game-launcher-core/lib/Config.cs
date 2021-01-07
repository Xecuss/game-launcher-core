using System.Collections.Generic;
using System;

namespace gamelaunchercore
{
    public class GameConfig
    {
        public long id { get; set; }
        public string name { get; set; }
        public string path { get; set; }
        public bool use720p { get; set; }
        public bool window { get; set; }
        public bool usePrinter { get; set; }
        public string printerPath { get; set; }
        public string printerFormat { get; set; }
        public bool printerClear { get; set; }
        public long nowUseNetwork { get; set; }
        public string nowLocalNetwork { get; set; }
        public string screenShotPath { get; set; }
        public bool ea { get; set; }
        public bool useSpice32 { get; set; }
        //spice api选项，打开后可以通过工具远程控制spice
        public bool api { get; set; }
        public string apiPassword { get; set; }
        public long nowUseSC { get; set; }
        public string card { get; set; }

        public GameConfig()
        {
            id = DateTime.Now.Ticks;
            name = "默认配置";
            path = "";
            use720p = false;
            window = false;
            usePrinter = false;
            printerPath = "";
            printerFormat = "jpg";
            printerClear = false;
            nowUseNetwork = -1;
            nowLocalNetwork = "";
            screenShotPath = "";
            ea = false;
            api = false;
            apiPassword = "";
            useSpice32 = false;
            nowUseSC = -1;
            card = "";
        }
    }

    public class NetworkConfig
    {
        public bool http11 { get; set; }
        public bool urlSlash { get; set; }
        public string url { get; set; }
        public string pcbId { get; set; }
        public long id { get; set; }
        public string name { get; set; }
        public string localServCommand { get; set; }
    }

    public class SpiceConfig
    {
        public string name { get; set; }
        public long id { get; set; }
        public string filename { get; set; }
    }

    public class LauncherConfig
    {
        public List<NetworkConfig> useAbleNetWorkConf { get; set; }
        public List<GameConfig> configs { get; set; }
        public long lastUseConfig { get; set; }
        //多spice配置管理(Multi Spice Config Manage, MSCM)
        public bool enableMSCM { get; set; }
        public List<SpiceConfig> useAbleSC { get; set; }
        public string SCPath { get; set; }

        public LauncherConfig()
        {
            useAbleNetWorkConf = new List<NetworkConfig>();
            enableMSCM = false;
            lastUseConfig = -1;
            configs = new List<GameConfig>();
            useAbleSC = new List<SpiceConfig>();
            SCPath = "./SxLauncher/";
        }
    }
}
