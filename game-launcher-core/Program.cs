using System;
using System.Net;

namespace gamelaunchercore
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            LauncherCore core = new LauncherCore(@"./sxLauncher.json");
            GameConfig conf = core.GetGameConfig(1603371542739);
            Console.WriteLine(conf.name);

            core.DeleteGameConfig(637415013148105890);

            core.GetNetworkList();
        }
    }
}
