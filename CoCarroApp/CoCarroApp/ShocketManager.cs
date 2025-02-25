﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CoCarroApp
{
    public class ShocketManager
    {
        #region client
        Socket client;
        public bool ConnectServer()
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(myIP), myPort);
            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                client.Connect(iep);
                return true;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region server
        Socket server;
        public void CreateServer()
        {
            
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(myIP), myPort);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(iep);
            server.Listen(10);//server đợi kết nối 10s với client,nếu quá thì bỏ
            //tạo một luồng riêng không dính dáng gì đến luồng của main
            Thread acceptClient = new Thread(() => {
                client = server.Accept();
            });
            acceptClient.IsBackground = true;//để chạy ngầm nếu không khi kết thúc thì cũng sẽ kết thúc nếu ko sẽ là foreground
            acceptClient.Start();
        }
        #endregion

        #region both
        public string myIP = "127.0.0.1";
        public int myPort = 9999;
        public bool isServer = true;
        public const int BUFFERED = 1024;

        public bool Send(Object data)
        {
            byte[] dataSended = SerializeData(data);
           
               return  SendData(client, dataSended);
            

            
        }
        public Object Receive()
        {
            byte[] dataReceiveed = new byte[BUFFERED];
            bool isOK = ReceiveData(client, dataReceiveed);


            return DeserializeData(dataReceiveed);
        }

        private bool SendData(Socket target, byte[] data)
        {
            return target.Send(data) == 1 ? true : false; //==1 là gửi thành công
        }
        private bool ReceiveData(Socket target,byte[] data)
        {
            return target.Receive(data) == 1 ? true : false;
        }
        /// <summary>
        /// Nén đối tượng thành mảng byte[]
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public byte[] SerializeData(Object o)
        {
            MemoryStream ms = new MemoryStream();   //stream để lưu trữ 
            BinaryFormatter bf1 = new BinaryFormatter(); //format của kiểu byte
            bf1.Serialize(ms, o);
            return ms.ToArray();
        }

        /// <summary>
        /// Giải nén mảng byte[] thành đối tượng object
        /// </summary>
        /// <param name="theByteArray"></param>
        /// <returns></returns>
        public object DeserializeData(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;
            return bf1.Deserialize(ms);
        }

        /// <summary>
        /// Lấy ra IP V4 của card mạng đang dùng
        /// </summary>
        /// <param name="_type"></param>
        /// <returns></returns>
        public string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }
        #endregion
    }
}
