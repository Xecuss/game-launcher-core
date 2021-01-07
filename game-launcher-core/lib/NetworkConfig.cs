using System;
namespace gamelaunchercore
{
    public class LocalNetwork
    {
        public string name { get; set; }
        public string ipv4 { get; set; }
        public string ipv6 { get; set; }

        public LocalNetwork(string name)
        {
            this.name = name;
        }
    }
}
