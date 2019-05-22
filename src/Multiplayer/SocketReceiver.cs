using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Pong_SFML.Multiplayer
{
    class SocketReceiver
    {
        private const int listenPort = 1111;
        private static readonly UdpClient _listener = new UdpClient(listenPort);
       
        public static byte[] Received;

        public static void StartListener()
        {
            _listener.BeginReceive(new AsyncCallback(OnUdpData), null);
        }

        private static void OnUdpData(IAsyncResult res)
        {
            IPEndPoint _groupEP = new IPEndPoint(IPAddress.Any, listenPort);
            Received = _listener.EndReceive(res, ref _groupEP);
            _listener.BeginReceive(new AsyncCallback(OnUdpData), null);
        }
    }
}
