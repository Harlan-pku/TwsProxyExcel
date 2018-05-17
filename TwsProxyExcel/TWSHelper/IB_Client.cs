﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using IBApi;

namespace TWSHelper
{
    public struct AssetUpdate
    {
        public string strAssetID;
        public decimal Ask;
        public decimal Bid;
        public int AskSize;
        public int BidSize;
        public Asset theAsset;
        public DateTime TimeTip;
    }
    

    public class IB_Client
    {
        public EWrapperImpl wrapper = new EWrapperImpl();

        public const int TICK_ID_BASE = 10000000;

        private const int DESCRIPTION_INDEX = 0;

        private const int BID_PRICE_INDEX = 2;
        private const int ASK_PRICE_INDEX = 3;
        private const int CLOSE_PRICE_INDEX = 7;

        private const int BID_SIZE_INDEX = 1;
        private const int ASK_SIZE_INDEX = 4;
        private const int LAST_SIZE_INDEX = 5;
        private const int VOLUME_SIZE_INDEX = 6;

        protected int currentTicker = 1;
        private int OrderID = 5000;
        private string Host = "";
        private Int32 Port = 0;


        /// <summary>
        /// 是否已经连接了TWS
        /// </summary>
        public bool IsConnected()
        {
            return wrapper.ClientSocket.IsConnected();
        }

        public string DefaultAccout { get; set; }

        /// <summary>
        /// 已订阅的资产列表
        /// </summary>
        List<Asset> SubscribedAssets = new List<Asset>();

        /// <summary>
        /// 用于记录TickerID与资产的映射关系
        /// </summary>
        Dictionary<int, Asset> dicAssetTickerID = new Dictionary<int, Asset>();

        public BroadcastBlock<AssetUpdate> UpdateBoradCast = new BroadcastBlock<AssetUpdate>((msg) => { return msg; });

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public IB_Client()
        {
            wrapper.parent_client = this;
        }
        
        ~IB_Client()
        {
            if (IsConnected())
            {
                Console.WriteLine("disconnecting");
                Disconnect();
            }
        }

        /// <summary>
        /// 根据AssetID寻找到对应的Contract
        /// </summary>
        /// <param name="strAssetID">目前资产的AssetID</param>
        /// <returns>返回Contract，如果返回null，则说明该资产不存在或者未订阅</returns>
        private Contract GetContractByAssetID(string strAssetID)
        {
            if(SubscribedAssets.Exists((a)=>a.AssetID==strAssetID))
            {
                return SubscribedAssets.First((a) => a.AssetID == strAssetID).IB_Contract;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取Asset
        /// </summary>
        /// <param name="strAssetID">AssetID</param>
        /// <returns>返回对应的Asset，如果没有订阅过则返回null</returns>
        public Asset GetAssetByID(string strAssetID)
        {
            if(SubscribedAssets.Exists((a)=>a.AssetID==strAssetID))
            {
                return SubscribedAssets.First((a) => a.AssetID == strAssetID);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取Asset
        /// </summary>
        /// <param name="strAssetID">AssetID</param>
        /// <returns>返回对应的Asset，如果没有订阅过则返回null</returns>
        public Asset get(string strAssetID)
        {
            return GetAssetByID(strAssetID);
        }

        /// <summary>
        /// 连接到TWS
        /// </summary>
        /// <param name="IP_Address">TWS 客户端所在的IP地址</param>
        /// <param name="Port">端口号，默认为7496，模拟账号默认为7497</param>
        /// <returns>如果成功连接返回true，否则返回false</returns>
        public bool ConnectToTWS(string IP_Address, int Port)
        {
            if (IP_Address == Host && Port == this.Port && IsConnected())
            {
                return true;
            }
            else if (IsConnected())
            {
                Disconnect();
            }
            try
            {
                wrapper.ClientSocket.eConnect(IP_Address, Port, 0, false);
                EClientSocket clientSocket = wrapper.ClientSocket;
                EReaderSignal readerSignal = wrapper.Signal;
                //Create a reader to consume messages from the TWS. The EReader will consume the incoming messages and put them in a queue
                var reader = new EReader(clientSocket, readerSignal);
                reader.Start();
                //Once the messages are in the queue, an additional thread need to fetch them
                new Thread(() => { while (clientSocket.IsConnected()) { readerSignal.waitForSignal(); reader.processMsgs(); } }) { IsBackground = true }.Start();
                while (wrapper.NextOrderId <= 0) { }
                new Thread(() => { while (clientSocket.IsConnected()) { clientSocket.reqPositions(); Thread.Sleep(500); } }) { IsBackground = true }.Start();
                //wrapper.NextOrderId = 2000;
                OrderID = wrapper.NextOrderId;
                //IsConnected = true;
                this.Port = Port;
                this.Host = IP_Address;

                return true;
            }
            catch (Exception exp)
            {
                Console.WriteLine(exp.Message.ToString());
                return false;
            }
            finally { }
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void Disconnect()
        {
            wrapper.ClientSocket.eDisconnect();
            //IsConnected = false;
        }

        /// <summary>
        /// 订阅一个资产
        /// </summary>
        /// <param name="asset">资产</param>
        /// <returns>1表示成功，0表示已经订阅过了，-1表示没有连接，-2表示未知错误</returns>
        public int SubscribeMarketData(Asset asset)
        {
            try
            {
                if (IsConnected())
                {
                    //检测是否已经订阅了
                    if (!SubscribedAssets.Exists((a) => a.AssetID == asset.AssetID))
                    {
                        int ticker_id = TICK_ID_BASE + (currentTicker++);
                        wrapper.ClientSocket.reqMktData(ticker_id, asset.IB_Contract, "", false, new List<TagValue>());

                        dicAssetTickerID.Add(ticker_id, asset);
                        SubscribedAssets.Add(asset);

                        return 1;
                    }
                    else
                    {
                        //如果已经订阅了，则返回false
                        return 0;
                    }
                }
                else
                {
                    return -1;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
                return -2;
            }
            finally { }
        }

        /// <summary>
        /// 订阅一个资产
        /// </summary>
        /// <param name="strAssetID">资产的描述</param>
        /// <returns>1表示成功，0表示已经订阅过了，-1表示没有连接，-2表示未知错误</returns>
        public int SubscribeMarketData(string strAssetID)
        {
            Asset newAsset = new Asset(strAssetID);
            return SubscribeMarketData(newAsset);
        }

        public int SubscribeMarketData(string strAssetID,string Exchange)
        {
            Asset newAsset = new Asset(strAssetID);
            newAsset.IB_Contract.Exchange = Exchange;
            return SubscribeMarketData(newAsset);
        }

        public int SubscribeMarketData(string strAssetID, string Exchange,string Currency)
        {
            Asset newAsset = new Asset(strAssetID);
            newAsset.IB_Contract.Exchange = Exchange;
            newAsset.IB_Contract.Currency = Currency;
            return SubscribeMarketData(newAsset);
        }

        public string ReqPositions()
        {
            //wrapper.ClientSocket.reqPositions();
            //while (wrapper.posInfoReady is false) { }
            string tmp = wrapper.posInfo;
            //wrapper.posInfo = "";
            //wrapper.posInfoReady = false;
            return tmp;
        }

        public int add(string strAssetID)
        {
            return SubscribeMarketData(strAssetID);
        }

        public int add(string strAssetID,string Exchange)
        {
            return SubscribeMarketData(strAssetID, Exchange);
        }

        public int add(string strAssetID, string Exchange, string Currency)
        {
            return SubscribeMarketData(strAssetID, Exchange, Currency);
        }

        public string orderStatus(int orderID)
        {
            return wrapper.getOrderInfo(orderID);
        }

        public double OrderRemaining(int orderID)
        {
            if (wrapper.orderInfoDetail.ContainsKey(orderID))
                return wrapper.orderInfoDetail[orderID].remaining;
            return -1;
        }

        #region response of IB API call

        private void DoBroadCast(Asset theAsset)
        {
            AssetUpdate update = new AssetUpdate()
            {
                strAssetID = theAsset.AssetID,
                Ask = theAsset.Ask,
                Bid = theAsset.Bid,
                AskSize = theAsset.Ask_Size,
                BidSize = theAsset.Bid_Size,
                theAsset = theAsset,
                TimeTip = DateTime.Now
            };

            UpdateBoradCast.Post(update);
        }

        public void UpdateMKTAsk(int tickerId, int position, double price, int size)
        {
            Asset theAsset = dicAssetTickerID[tickerId];

            MKT_MESSAGE newMessage = new MKT_MESSAGE();

            newMessage.Type = MKT_MESSAGE.MessageType.ASK_ONLY;
            newMessage.Ask = Convert.ToDecimal(price);
            //newMessage.Ask_Size = size;

            theAsset.UpdateMarketData(newMessage);

            DoBroadCast(theAsset);
        }

        public void UpdateMKTBid(int tickerId, int position, double price, int size)
        {
            Asset theAsset = dicAssetTickerID[tickerId];

            MKT_MESSAGE newMessage = new MKT_MESSAGE();

            newMessage.Type = MKT_MESSAGE.MessageType.BID_ONLY;
            newMessage.Bid = Convert.ToDecimal(price);
            //newMessage.Bid_Size = size;

            theAsset.UpdateMarketData(newMessage);

            DoBroadCast(theAsset);
        }

        public void UpdateGreeks(int tickerId, double delta, double gamma, double vega, double theta)
        {
            Asset theAsset = dicAssetTickerID[tickerId];

            //出现-2是希腊字母计算错误导致
            if (delta == -2 || gamma == -2 || vega == -2 || theta == -2)
                return;

            theAsset.UpdateGreeks(delta, gamma, vega, theta);

            DoBroadCast(theAsset);
        }

        public void UpdateIV(int tickerId, double iv)
        {
            Asset theAsset = dicAssetTickerID[tickerId];

            theAsset.UpdateIV(iv);

            DoBroadCast(theAsset);
        }

        public void UpdateAskSize(int tickerId, int size)
        {
            Asset theAsset = dicAssetTickerID[tickerId];

            MKT_MESSAGE newMessage = new MKT_MESSAGE();

            newMessage.Type = MKT_MESSAGE.MessageType.ASK_WITH_SIZE;
            newMessage.Ask = theAsset.Ask;
            newMessage.Ask_Size = size;

            theAsset.UpdateMarketData(newMessage);

            DoBroadCast(theAsset);
        }

        public void UpdateBidSize(int tickerId, int size)
        {
            Asset theAsset = dicAssetTickerID[tickerId];

            MKT_MESSAGE newMessage = new MKT_MESSAGE();

            newMessage.Type = MKT_MESSAGE.MessageType.BID_WITH_SIZE;
            newMessage.Bid = theAsset.Bid;
            newMessage.Bid_Size = size;

            theAsset.UpdateMarketData(newMessage);

            DoBroadCast(theAsset);
        }
        #endregion

        /// <summary>
        /// 以市价买入一个合约
        /// </summary>
        /// <param name="strAssetID">资产ID</param>
        /// <param name="quantity">数量</param>
        /// <returns>返回一个OrderID</returns>
        public int Buy(string strAssetID, int quantity)
        {
            return BuyMarketOrder(strAssetID, quantity);
        }

        /// <summary>
        /// 以市价卖出一个合约
        /// </summary>
        /// <param name="strAssetID">资产ID</param>
        /// <param name="quanity">数量</param>
        /// <returns>返回一个OrderID</returns>
        public int Sell(string strAssetID, int quantity)
        {
            return SellMarketOrder(strAssetID, quantity);
        }

        public int PlaceLimitOrder(string strAssetID, string action, int quantity, double price)
        {
            Order order = OrderHelper.LimitOrder(action, quantity, price);
            return PlaceOrder(strAssetID, order);             
        }

        public int BuyLimitOrder(string strAssetID, int quantity, double limitPrice)
        {
            return PlaceLimitOrder(strAssetID, "BUY", quantity, limitPrice);
        }

        public int SellLimitOrder(string strAssetID, int quantity, double limitPrice)
        {
            return PlaceLimitOrder(strAssetID, "SELL", quantity, limitPrice);
        }

        public void CancelOrder(int OrderId)
        {
            wrapper.ClientSocket.cancelOrder(OrderId);
        }

        public void CancelAllOrders()
        {
            wrapper.ClientSocket.reqGlobalCancel();
        }

        private int PlaceOrder(string strAssetID, Order order)
        {
            if (!IsConnected())
                return -1;
            Contract contract = GetContractByAssetID(strAssetID);
            if (contract == null)
                return -1;
            if (!string.IsNullOrEmpty(DefaultAccout))
                order.Account = DefaultAccout;
            wrapper.ClientSocket.placeOrder(OrderID, contract, order);
            return OrderID++;
        }

        public int PlaceMarketOrder(string strAssetID, string action, double quantity)
        {
            Order order = OrderHelper.MarketOrder(action, quantity);
            return PlaceOrder(strAssetID, order);
        }

        public int BuyMarketOrder(string strAssetID, double quantity)
        {
            return this.PlaceMarketOrder(strAssetID, "BUY", quantity);
        }

        public int SellMarketOrder(string strAssetID, double quantity)
        {
            return this.PlaceMarketOrder(strAssetID, "SELL", quantity);
        }

        public int PlaceAtAuction(string strAssetID, string action, double quantity, double price)
        {
            Order order = OrderHelper.AtAuction(action, quantity, price);
            return PlaceOrder(strAssetID, order);
        }

        public int PlaceDiscretionary(string strAssetID, string action, double quantity, double price, double discretionaryAmount)
        {
            Order order = OrderHelper.Discretionary(action, quantity, price, discretionaryAmount);
            return PlaceOrder(strAssetID, order);
        }

        public int PlaceMarketIfTouched(string strAssetID, string action, double quantity, double price)
        {
            Order order = OrderHelper.MarketIfTouched(action, quantity, price);
            return PlaceOrder(strAssetID, order);
        }

        public int PlaceMarketOnClose(string strAssetID, string action, double quantity)
        {
            Order order = OrderHelper.MarketOnClose(action, quantity);
            return PlaceOrder(strAssetID, order);
        }
    }
}
