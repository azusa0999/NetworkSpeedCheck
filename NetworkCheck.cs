using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.NetworkInformation;

namespace NetworkSpeedCheck
{
    public class NetworkCheck
    {
        string _netSpeed = string.Empty;
        public string _NetSpeed
        {
            get { return _netSpeed; }
        }

        public long getRoundTime(string ip)
        {
            // Ping's the local machine.
            Ping pingSender = new Ping();
            PingReply reply = pingSender.Send(ip);
            if (reply.Status == IPStatus.Success)
            {
                return reply.RoundtripTime == 0 ? 1000L : reply.RoundtripTime;
            }

            return 0L;
        }
    }
}
