using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TWSHelper;
using System.Threading;
using System.Runtime.InteropServices;

namespace TwsProxyExcel
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>

        public IB_Client client = new IB_Client();
        [STAThread]

        public bool Connect(string host, int port=7496)
        {
            bool ret = client.ConnectToTWS(host, Port:port);
            return ret;
        }
        public bool Connect(string host, string port="7496")
        {
            Int32 _port;
            if (Int32.TryParse(port, out _port))
                return Connect(host, _port);
            return false;
        }
        public bool IsConnected()
        {
            return client.IsConnected();
        }
        public int BuyTest()
        {
            client.add("AAPL");
            return client.Buy("AAPL", 1000);
            //return true;
        }
        public string GetBuyStatus()
        {
            return client.orderStatus(0);
        }
        static void Main()
        {
            IB_Client client = new IB_Client();
            bool ret = client.ConnectToTWS("127.0.0.1", 7496);
            if (ret)
            {
                Console.WriteLine("connect successful.");
            }

            //client = new IB_Client();
            //client.ConnectToTWS("127.0.0.1", 7496);
            //client.DefaultAccout = "DU229332";
            string acc_id = client.DefaultAccout;
            Console.WriteLine(acc_id);
            client.add("AAPL");
            //client.add("EURUSD");
            //var es = client.get("ES.20180420");

            //var ask = es.Ask;
            //var bid = es.Bid;

            //var ask_size = es.Ask_Size;
            //var bid_size = es.Bid_Size;
            int a = 2;
            while (a-->1)
            {
                client.BuyLimitOrder("AAPL", 1000, 170);
                //Console.ReadKey();
                Thread.Sleep(1000);
            }
            Thread.Sleep(1000);
            //string str = client.wrapper.orderInfo;
            //Console.WriteLine(str);
            client.CancelAllOrders();
            client.wrapper.ClientSocket.reqPositions();
            Console.WriteLine(client.ReqPositions());
            Thread.Sleep(20000);
            client.Disconnect();
            Console.ReadKey();
        }
        [ComVisible(true)]
        public string TestAdd(string a, string b)
        {
            int aa = Int32.Parse(a);
            int bb = Int32.Parse(b);
            int c = aa + bb;
            return c.ToString();
        }
    }
}
