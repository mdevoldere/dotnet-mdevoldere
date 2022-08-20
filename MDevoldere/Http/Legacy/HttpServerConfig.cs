//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net.NetworkInformation;
//using System.Net.Sockets;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace MDevoldere.Http.Legacy
//{
//    public class HttpServerConfig
//    {
//        //static readonly public string Path = (Application.StartupPath + "/www");

//        public string Host { get; private set; }

//        public int Port { get; private set; }

//        public string Prefix { get; private set; }

//        public string LocalPrefix { get; private set; }

//        public string User { get; private set; }

//        public string Password { get; private set; }


//        public HttpServerConfig() : this("127.0.0.1", 7070) { }

//        public HttpServerConfig(string host, int port)
//        {
//            SetPrefix(host, port);
//            User = "";
//            Password = "";
//        }

//        public HttpServerConfig SetPrefix(string host, int port)
//        {
//            Host = host;
//            Port = port;
//            Prefix = "http://" + Host + ":" + Port.ToString() + "/";
//            LocalPrefix = "http://localhost:" + Port.ToString() + "/";
//            return this;
//        }

//        public HttpServerConfig SetCredentials(string usr, string pass)
//        {
//            User = usr;
//            Password = pass;
//            return this;
//        }

//        public bool IsPrivate()
//        {
//            return (User.Length > 2 && Password.Length > 3);
//        }

//        public bool Login(string usr, string pass)
//        {
//            return !IsPrivate() || (usr == User && pass == Password);
//        }
//    }
//}
