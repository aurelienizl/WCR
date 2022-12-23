using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace WCRC_Service.Reporting
{
    public class Win32_Network
    {
        public string LocalEndPoint { get; set; }
        public string LocalEndPointPort { get; set; }
        public string RemoteEndPoint { get; set; }
        public string RemoteEndPointPort { get; set; }

        public struct TcpConnection
        {
            public string LocalEndPoint;
            public string RemoteEndPoint;
            public string LocalEndPointPort;
            public string RemoteEndPointPort;
        };

        public struct Address
        {
            public string ip;
            public string port;
        }

        public Win32_Network(string localEndPoint, string remoteEndPoint)
        {
            TcpConnection tcpConnection =
                ParseConnection(localEndPoint, remoteEndPoint);

            LocalEndPoint = tcpConnection.LocalEndPoint;
            RemoteEndPoint = tcpConnection.RemoteEndPoint;
            LocalEndPointPort = tcpConnection.LocalEndPointPort;
            RemoteEndPointPort = tcpConnection.RemoteEndPointPort;
        }

        public static List<Win32_Network> GetWin32_Networks()
        {
            List<Win32_Network> list = new List<Win32_Network>();
            try
            {
                IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
                TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();
                foreach (TcpConnectionInformation connection in connections)
                {
                    list.Add(
                        new Win32_Network
                        (
                            connection.LocalEndPoint.ToString(),
                            connection.RemoteEndPoint.ToString()
                        ));
                }
                return list;
            }
            catch (Exception)
            {
                return list;
            }

        }
        private static Address GetConnectionDetails(string EndPoint)
        {
            string port = "";
            string ip = "";
            bool getPort = false;
            for (int i = EndPoint.Length - 1; i >= 0; i--)
            {
                if (getPort)
                {
                    ip = EndPoint[i] + ip;
                }
                else
                {
                    if (EndPoint[i] == ':')
                    {
                        getPort = true;
                    }
                    else
                    {
                        port = EndPoint[i] + port;
                    }
                }
            }
            Address address =
                new Address()
                {
                    ip = ip,
                    port = port
                };
            return address;
        }
        private static TcpConnection ParseConnection(string localEndPoint, string remoteEndPoint)
        {
            Address localEndPointAddr
                = GetConnectionDetails(localEndPoint);
            Address remoteEndPointAddr
                = GetConnectionDetails(remoteEndPoint);

            TcpConnection tcpConnection = new TcpConnection()
            {
                LocalEndPoint = localEndPointAddr.ip,
                RemoteEndPoint = remoteEndPointAddr.ip,
                LocalEndPointPort = localEndPointAddr.port,
                RemoteEndPointPort = remoteEndPointAddr.port
            };
            return tcpConnection;
        }

    }
}
