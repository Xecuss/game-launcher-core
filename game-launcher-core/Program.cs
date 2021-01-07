using System;

namespace gamelaunchercore
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            LauncherCore core = new LauncherCore(@"./sxLauncher.json");

            Console.WriteLine(core.cmd);

            core.DeleteGameConfig(637415013148105890);

            LocalNetwork[] nets = core.GetNetworkList();

            foreach(LocalNetwork net in nets)
            {
                Console.WriteLine("{0}: ", net.name);
                Console.WriteLine("   v4: {0}", net.ipv4);
                Console.WriteLine("   v6: {0}", net.ipv6);
            }
        }
    }
}
