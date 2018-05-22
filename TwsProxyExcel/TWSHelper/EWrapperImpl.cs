﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IBApi;

namespace TWSHelper
{
    // public class EWrapperImpl : EWrapper
    // {
    //     EClientSocket clientSocket;
    //     private int nextOrderId;
    //     public readonly EReaderSignal Signal;
    //     //public string orderInfo = "bug";
        
    //     //{
    //     //    get { return orderInfo; }
    //     //    set { }
    //     //}
    //     public string getOrderInfo(int orderID)
    //     {
    //         if (orderErrorInfo.ContainsKey(orderID))
    //         {
    //             return orderErrorInfo[orderID];
    //         }
    //         if (orderInfo.ContainsKey(orderID))
    //         {
    //             return orderInfo[orderID];
    //         }
    //         return "unavailable";
    //     }

    //     public IB_Client parent_client;

    //     public EWrapperImpl()
    //     {
    //         Signal = new EReaderMonitorSignal();
    //         clientSocket = new EClientSocket(this, Signal);
    //         //clientSocket = new EClientSocket(this);
    //     }

    //     public EClientSocket ClientSocket
    //     {
    //         get { return clientSocket; }
    //         set { clientSocket = value; }
    //     }

    //     public int NextOrderId
    //     {
    //         get { return nextOrderId; }
    //         set { nextOrderId = value; }
    //     }

    //     public virtual void error(Exception e)
    //     {
    //         Console.WriteLine("Exception thrown: " + e);
    //         //throw e;
    //     }

    //     public virtual void error(string str)
    //     {
    //         Console.WriteLine("Error: " + str + "\n");
    //     }

    //     public virtual void error(int id, int errorCode, string errorMsg)
    //     {
    //         Console.WriteLine("Error. Id: " + id + ", Code: " + errorCode + ", Msg: " + errorMsg + "\n");
    //     }

    //     public virtual void connectionClosed()
    //     {
    //         Console.WriteLine("Connection closed.\n");
    //     }

    //     public virtual void currentTime(long time)
    //     {
    //         Console.WriteLine("Current Time: " + time + "\n");
    //     }

    //     public virtual void tickPrice(int tickerId, int field, double price, int canAutoExecute)
    //     {
    //         if (field == 1)
    //         {
    //             parent_client.UpdateMKTBid(tickerId, 0, price, 0);
    //         }
    //         else if (field == 2)
    //         {
    //             parent_client.UpdateMKTAsk(tickerId, 0, price, 0);
    //         }

    //         Console.WriteLine("Tick Price. Ticker Id:" + tickerId + ", Field: " + field + ", Price: " + price + ", CanAutoExecute: " + canAutoExecute + "\n");
    //     }

    //     public virtual void tickSize(int tickerId, int field, int size)
    //     {
    //         Console.WriteLine("Tick Size. Ticker Id:" + tickerId + ", Field: " + field + ", Size: " + size + "\n");

    //         if (field == 0)
    //         {
    //             parent_client.UpdateAskSize(tickerId, size);
    //         }
    //         else if (field == 3)
    //         {
    //             parent_client.UpdateBidSize(tickerId, size);
    //         }
    //     }

    //     public virtual void tickString(int tickerId, int tickType, string value)
    //     {
    //         //Console.WriteLine("Tick string. Ticker Id:" + tickerId + ", Type: " + tickType + ", Value: " + value+"\n");
    //     }

    //     public virtual void tickGeneric(int tickerId, int field, double value)
    //     {
    //         Console.WriteLine("Tick Generic. Ticker Id:" + tickerId + ", Field: " + field + ", Value: " + value + "\n");
    //     }

    //     public virtual void tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureExpiry, double dividendImpact, double dividendsToExpiry)
    //     {
    //         Console.WriteLine("TickEFP. " + tickerId + ", Type: " + tickType + ", BasisPoints: " + basisPoints + ", FormattedBasisPoints: " + formattedBasisPoints + ", ImpliedFuture: " + impliedFuture + ", HoldDays: " + holdDays + ", FutureExpiry: " + futureExpiry + ", DividendImpact: " + dividendImpact + ", DividendsToExpiry: " + dividendsToExpiry + "\n");
    //     }

    //     public virtual void tickSnapshotEnd(int tickerId)
    //     {
    //         Console.WriteLine("TickSnapshotEnd: " + tickerId + "\n");
    //     }

    //     public virtual void nextValidId(int orderId)
    //     {
    //         Console.WriteLine("Next Valid Id: " + orderId + "\n");
    //         NextOrderId = orderId;
    //     }

    //     public virtual void deltaNeutralValidation(int reqId, UnderComp underComp)
    //     {
    //         Console.WriteLine("DeltaNeutralValidation. " + reqId + ", ConId: " + underComp.ConId + ", Delta: " + underComp.Delta + ", Price: " + underComp.Price + "\n");
    //     }

    //     public virtual void managedAccounts(string accountsList)
    //     {
    //         Console.WriteLine("Account list: " + accountsList + "\n");
    //     }

    //     public virtual void tickOptionComputation(int tickerId, int field, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
    //     {
    //         Console.WriteLine("TickOptionComputation. TickerId: " + tickerId + ", field: " + field + ", ImpliedVolatility: " + impliedVolatility + ", Delta: " + delta
    //             + ", OptionPrice: " + optPrice + ", pvDividend: " + pvDividend + ", Gamma: " + gamma + ", Vega: " + vega + ", Theta: " + theta + ", UnderlyingPrice: " + undPrice + "\n");

    //         //InteractiveBroker.UpdateGreeks(tickerId, delta, gamma, vega, theta);
    //         //InteractiveBroker.UpdateIV(tickerId, impliedVolatility);
    //     }

    //     public virtual void accountSummary(int reqId, string account, string tag, string value, string currency)
    //     {
    //         Console.WriteLine("Acct Summary. ReqId: " + reqId + ", Acct: " + account + ", Tag: " + tag + ", Value: " + value + ", Currency: " + currency + "\n");
    //     }

    //     public virtual void accountSummaryEnd(int reqId)
    //     {
    //         Console.WriteLine("AccountSummaryEnd. Req Id: " + reqId + "\n");
    //     }

    //     public virtual void updateAccountValue(string key, string value, string currency, string accountName)
    //     {
    //         Console.WriteLine("UpdateAccountValue. Key: " + key + ", Value: " + value + ", Currency: " + currency + ", AccountName: " + accountName + "\n");
    //     }

    //     public virtual void updatePortfolio(Contract contract, double position, double marketPrice, double marketValue, double averageCost, double unrealisedPNL, double realisedPNL, string accountName)
    //     {
    //         Console.WriteLine("UpdatePortfolio. " + contract.Symbol + ", " + contract.SecType + " @ " + contract.Exchange
    //             + ": Position: " + position + ", MarketPrice: " + marketPrice + ", MarketValue: " + marketValue + ", AverageCost: " + averageCost
    //             + ", UnrealisedPNL: " + unrealisedPNL + ", RealisedPNL: " + realisedPNL + ", AccountName: " + accountName + "\n");
    //     }

    //     public virtual void updateAccountTime(string timestamp)
    //     {
    //         Console.WriteLine("UpdateAccountTime. Time: " + timestamp + "\n");
    //     }

    //     public virtual void accountDownloadEnd(string account)
    //     {
    //         Console.WriteLine("Account download finished: " + account + "\n");
    //     }

    //     public virtual void orderStatus(int orderId, string status, double filled, double remaining, double avgFillPrice, int permId, int parentId, double lastFillPrice, int clientId, string whyHeld)
    //     {
    //         string result = "OrderStatus. Id: " + orderId + ", Status: " + status + ", Filled" + filled + ", Remaining: " + remaining
    //             + ", AvgFillPrice: " + avgFillPrice + ", PermId: " + permId + ", ParentId: " + parentId + ", LastFillPrice: " + lastFillPrice + ", ClientId: " + clientId + ", WhyHeld: " + whyHeld;
    //         if (orderInfo.ContainsKey(orderId))
    //         {
    //             orderInfo[orderId] = result;
    //         }
    //         else
    //         {
    //             orderInfo.Add(orderId, result);
    //         }
            
    //         Console.WriteLine("OrderStatus. Id: " + orderId + ", Status: " + status + ", Filled" + filled + ", Remaining: " + remaining
    //             + ", AvgFillPrice: " + avgFillPrice + ", PermId: " + permId + ", ParentId: " + parentId + ", LastFillPrice: " + lastFillPrice + ", ClientId: " + clientId + ", WhyHeld: " + whyHeld + "\n");
    //     }

    //     public virtual void openOrder(int orderId, Contract contract, Order order, OrderState orderState)
    //     {
    //         Console.WriteLine("OpenOrder. ID: " + orderId + ", " + contract.Symbol + ", " + contract.SecType + " @ " + contract.Exchange + ": " + order.Action + ", " + order.OrderType + " " + order.TotalQuantity + ", " + orderState.Status + "\n");
    //         //clientSocket.reqMktData(2, contract, "", false);
    //         contract.ConId = 0;
    //         clientSocket.placeOrder(nextOrderId, contract, order);
    //     }

    //     public virtual void openOrderEnd()
    //     {
    //         Console.WriteLine("OpenOrderEnd");
    //     }

    //     public virtual void contractDetails(int reqId, ContractDetails contractDetails)
    //     {
    //         Console.WriteLine("ContractDetails. ReqId: " + reqId + " - " + contractDetails.Summary.Symbol + ", " + contractDetails.Summary.SecType + ", ConId: " + contractDetails.Summary.ConId + " @ " + contractDetails.Summary.Exchange + "\n");
    //     }

    //     public virtual void contractDetailsEnd(int reqId)
    //     {
    //         Console.WriteLine("ContractDetailsEnd. " + reqId + "\n");
    //     }

    //     public virtual void execDetails(int reqId, Contract contract, Execution execution)
    //     {
    //         Console.WriteLine("ExecDetails. " + reqId + " - " + contract.Symbol + ", " + contract.SecType + ", " + contract.Currency + " - " + execution.ExecId + ", " + execution.OrderId + ", " + execution.Shares + "\n");
    //     }

    //     public virtual void execDetailsEnd(int reqId)
    //     {
    //         Console.WriteLine("ExecDetailsEnd. " + reqId + "\n");
    //     }

    //     public virtual void commissionReport(CommissionReport commissionReport)
    //     {
    //         Console.WriteLine("CommissionReport. " + commissionReport.ExecId + " - " + commissionReport.Commission + " " + commissionReport.Currency + " RPNL " + commissionReport.RealizedPNL + "\n");
    //     }

    //     public virtual void fundamentalData(int reqId, string data)
    //     {
    //         Console.WriteLine("FundamentalData. " + reqId + "" + data + "\n");
    //     }

    //     public virtual void historicalData(int reqId, string date, double open, double high, double low, double close, int volume, int count, double WAP, bool hasGaps)
    //     {
    //         Console.WriteLine("HistoricalData. " + reqId + " - Date: " + date + ", Open: " + open + ", High: " + high + ", Low: " + low + ", Close: " + close + ", Volume: " + volume + ", Count: " + count + ", WAP: " + WAP + ", HasGaps: " + hasGaps + "\n");
    //     }

    //     public virtual void marketDataType(int reqId, int marketDataType)
    //     {
    //         Console.WriteLine("MarketDataType. " + reqId + ", Type: " + marketDataType + "\n");
    //     }

    //     public virtual void updateMktDepth(int tickerId, int position, int operation, int side, double price, int size)
    //     {
    //         Console.WriteLine("UpdateMarketDepth. " + tickerId + " - Position: " + position + ", Operation: " + operation + ", Side: " + side + ", Price: " + price + ", Size" + size + "\n");
    //         if (position == 0)
    //         {
    //             if (side == 0)
    //             {
    //                 //Global.UpdateDepthAsk(tickerId, position, price, size);
    //                 //InteractiveBroker.UpdateMarketDepthAsk(tickerId, position, price, size);
    //             }
    //             else if (side == 1)
    //             {
    //                 //Global.UpdateDepthBid(tickerId, position, price, size);
    //                 //InteractiveBroker.UpdateMarketDepthBid(tickerId, position, price, size);
    //             }
    //         }
    //     }

    //     public virtual void updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, int size)
    //     {
    //         Console.WriteLine("UpdateMarketDepthL2. " + tickerId + " - Position: " + position + ", Operation: " + operation + ", Side: " + side + ", Price: " + price + ", Size" + size + "\n");
    //     }


    //     public virtual void updateNewsBulletin(int msgId, int msgType, String message, String origExchange)
    //     {
    //         Console.WriteLine("News Bulletins. " + msgId + " - Type: " + msgType + ", Message: " + message + ", Exchange of Origin: " + origExchange + "\n");
    //     }

    //     public virtual void position(string account, Contract contract, double pos, double avgCost)
    //     {
    //         Console.WriteLine("Position. " + account + " - Symbol: " + contract.Symbol + ", SecType: " + contract.SecType + ", Currency: " + contract.Currency + ", Position: " + pos + ", Avg cost: " + avgCost + "\n");

    //         /*
    //         PositionInfo info = new PositionInfo()
    //         {
    //             strSymbol = contract.Symbol,
    //             strSecType = contract.SecType,
    //             strStrikePirce = contract.Strike.ToString(),
    //             strRight = contract.Right,
    //             strExpireDate = contract.Expiry,
    //             strPosition = pos.ToString(),
    //             strAvgCost = avgCost.ToString()
    //         };

    //         Global.Portfolio.Add(info);
    //         */
    //     }

    //     public virtual void positionEnd()
    //     {
    //         Console.WriteLine("PositionEnd \n");

    //         //Global.Boradcaster.Post("PositionEnd");
    //     }

    //     public virtual void realtimeBar(int reqId, long time, double open, double high, double low, double close, long volume, double WAP, int count)
    //     {
    //         Console.WriteLine("RealTimeBars. " + reqId + " - Time: " + time + ", Open: " + open + ", High: " + high + ", Low: " + low + ", Close: " + close + ", Volume: " + volume + ", Count: " + count + ", WAP: " + WAP + "\n");
    //     }

    //     public virtual void scannerParameters(string xml)
    //     {
    //         Console.WriteLine("ScannerParameters. " + xml + "\n");
    //     }

    //     public virtual void scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
    //     {
    //         Console.WriteLine("ScannerData. " + reqId + " - Rank: " + rank + ", Symbol: " + contractDetails.Summary.Symbol + ", SecType: " + contractDetails.Summary.SecType + ", Currency: " + contractDetails.Summary.Currency
    //             + ", Distance: " + distance + ", Benchmark: " + benchmark + ", Projection: " + projection + ", Legs String: " + legsStr + "\n");
    //     }

    //     public virtual void scannerDataEnd(int reqId)
    //     {
    //         Console.WriteLine("ScannerDataEnd. " + reqId + "\n");
    //     }

    //     public virtual void receiveFA(int faDataType, string faXmlData)
    //     {
    //         Console.WriteLine("Receing FA: " + faDataType + " - " + faXmlData + "\n");
    //     }

    //     public virtual void bondContractDetails(int requestId, ContractDetails contractDetails)
    //     {
    //         Console.WriteLine("Bond. Symbol " + contractDetails.Summary.Symbol + ", " + contractDetails.Summary);
    //     }

    //     public virtual void historicalDataEnd(int reqId, string startDate, string endDate)
    //     {
    //         Console.WriteLine("Historical data end - " + reqId + " from " + startDate + " to " + endDate);
    //     }

    //     public virtual void verifyMessageAPI(string apiData)
    //     {
    //         Console.WriteLine("verifyMessageAPI: " + apiData);
    //     }
    //     public virtual void verifyCompleted(bool isSuccessful, string errorText)
    //     {
    //         Console.WriteLine("verifyCompleted. IsSuccessfule: " + isSuccessful + " - Error: " + errorText);
    //     }
    //     public virtual void verifyAndAuthMessageAPI(string apiData, string xyzChallenge)
    //     {
    //         Console.WriteLine("verifyAndAuthMessageAPI: " + apiData + " " + xyzChallenge);
    //     }
    //     public virtual void displayGroupList(int reqId, string groups)
    //     {
    //         Console.WriteLine("DisplayGroupList. Request: " + reqId + ", Groups" + groups);
    //     }
    //     public virtual void displayGroupUpdated(int reqId, string contractInfo)
    //     {
    //         Console.WriteLine("displayGroupUpdated. Request: " + reqId + ", ContractInfo: " + contractInfo);
    //     }
    // }


    //! [ewrapperimpl]
    public class EWrapperImpl : EWrapper
    {
        //! [ewrapperimpl]
        private int nextOrderId;
        //! [socket_declare]
        EClientSocket clientSocket;
        public readonly EReaderSignal Signal;
        //! [socket_declare]

        private Dictionary<int, string> orderInfo = new Dictionary<int, string>();
        private Dictionary<int, string> orderErrorInfo = new Dictionary<int, string>();
        public Dictionary<int, OrderStatusReturnStruct> orderInfoDetail = new Dictionary<int, OrderStatusReturnStruct>();
        public Dictionary<string, Tuple<Contract, Execution, int>> executions = new Dictionary<string, Tuple<Contract, Execution, int>>();
        public string posInfo = "";
        private string tmpPosInfo = "";
        public bool posInfoReady = false;

        public string accountsList;
        //{
        //    get { return accoutsList;  }
        //    set { accoutsList = value;  }
        //}

        //! [socket_init]
        public EWrapperImpl()
        {
            Signal = new EReaderMonitorSignal();
            clientSocket = new EClientSocket(this, Signal);
        }
        //! [socket_init]

        public EClientSocket ClientSocket
        {
            get { return clientSocket; }
            set { clientSocket = value; }
        }


        public IB_Client parent_client;

        public string GetOrderInfo(int orderID)
        {
            if (orderErrorInfo.ContainsKey(orderID))
            {
                return orderErrorInfo[orderID];
            }
            if (orderInfo.ContainsKey(orderID))
            {
                return orderInfo[orderID];
            }
            return "unavailable";
        }

        public string GetExecutions()
        {
            if (this.executions.Count == 0)
                return "";
            string ans = "";
            foreach (var el in this.executions)
            {
                ans += el.Value.Item2.Time + ", " + el.Value.Item2.OrderId + ", " + el.Value.Item1.Symbol + ", " + el.Value.Item2.Side + ", " + el.Value.Item1.Currency + ", " + el.Value.Item3.ToString() + "\n";
            }
            return ans.Substring(0, ans.Length - 1);
        }

        public int NextOrderId
        {
            get { return nextOrderId; }
            set { nextOrderId = value; }
        }

        public virtual void error(Exception e)
        {
            Console.WriteLine("Exception thrown: " + e);
            throw e;
        }

        public virtual void error(string str)
        {
            Console.WriteLine("Error: " + str + "\n");
        }

        //! [error]
        public virtual void error(int id, int errorCode, string errorMsg)
        {
            Console.WriteLine("Error. Id: " + id + ", Code: " + errorCode + ", Msg: " + errorMsg + "\n");
        }
        //! [error]

        public virtual void connectionClosed()
        {
            Console.WriteLine("Connection closed.\n");
        }

        public virtual void currentTime(long time)
        {

            Console.WriteLine("Current Time: " + time + "\n");
        }

        //! [tickprice]
        public virtual void tickPrice(int tickerId, int field, double price, int canAutoExecute)
        {
            Console.WriteLine("Tick Price. Ticker Id:" + tickerId + ", Field: " + field + ", Price: " + price + ", CanAutoExecute: " + canAutoExecute);
        }
        //! [tickprice]

        //! [ticksize]
        public virtual void tickSize(int tickerId, int field, int size)
        {
            Console.WriteLine("Tick Size. Ticker Id:" + tickerId + ", Field: " + field + ", Size: " + size);
        }
        //! [ticksize]

        //! [tickstring]
        public virtual void tickString(int tickerId, int tickType, string value)
        {
            Console.WriteLine("Tick string. Ticker Id:" + tickerId + ", Type: " + tickType + ", Value: " + value);
        }
        //! [tickstring]

        //! [tickgeneric]
        public virtual void tickGeneric(int tickerId, int field, double value)
        {
            Console.WriteLine("Tick Generic. Ticker Id:" + tickerId + ", Field: " + field + ", Value: " + value);
        }
        //! [tickgeneric]

        public virtual void tickEFP(int tickerId, int tickType, double basisPoints, string formattedBasisPoints, double impliedFuture, int holdDays, string futureLastTradeDate, double dividendImpact, double dividendsToLastTradeDate)
        {
            Console.WriteLine("TickEFP. " + tickerId + ", Type: " + tickType + ", BasisPoints: " + basisPoints + ", FormattedBasisPoints: " + formattedBasisPoints + ", ImpliedFuture: " + impliedFuture + ", HoldDays: " + holdDays + ", FutureLastTradeDate: " + futureLastTradeDate + ", DividendImpact: " + dividendImpact + ", DividendsToLastTradeDate: " + dividendsToLastTradeDate);
        }

        //! [ticksnapshotend]
        public virtual void tickSnapshotEnd(int tickerId)
        {
            Console.WriteLine("TickSnapshotEnd: " + tickerId);
        }
        //! [ticksnapshotend]

        //! [nextvalidid]
        public virtual void nextValidId(int orderId)
        {
            Console.WriteLine("Next Valid Id: " + orderId);
            NextOrderId = orderId;
        }
        //! [nextvalidid]

        public virtual void deltaNeutralValidation(int reqId, UnderComp underComp)
        {
            Console.WriteLine("DeltaNeutralValidation. " + reqId + ", ConId: " + underComp.ConId + ", Delta: " + underComp.Delta + ", Price: " + underComp.Price);
        }

        //! [managedaccounts]
        public virtual void managedAccounts(string accountsList)
        {
            this.accountsList = accountsList;
            Console.WriteLine("Account list: " + accountsList);
        }
        //! [managedaccounts]

        //! [tickoptioncomputation]
        public virtual void tickOptionComputation(int tickerId, int field, double impliedVolatility, double delta, double optPrice, double pvDividend, double gamma, double vega, double theta, double undPrice)
        {
            Console.WriteLine("TickOptionComputation. TickerId: " + tickerId + ", field: " + field + ", ImpliedVolatility: " + impliedVolatility + ", Delta: " + delta
                + ", OptionPrice: " + optPrice + ", pvDividend: " + pvDividend + ", Gamma: " + gamma + ", Vega: " + vega + ", Theta: " + theta + ", UnderlyingPrice: " + undPrice);
        }
        //! [tickoptioncomputation]

        //! [accountsummary]
        public virtual void accountSummary(int reqId, string account, string tag, string value, string currency)
        {
            Console.WriteLine("Acct Summary. ReqId: " + reqId + ", Acct: " + account + ", Tag: " + tag + ", Value: " + value + ", Currency: " + currency);
        }
        //! [accountsummary]

        //! [accountsummaryend]
        public virtual void accountSummaryEnd(int reqId)
        {
            Console.WriteLine("AccountSummaryEnd. Req Id: " + reqId + "\n");
        }
        //! [accountsummaryend]

        //! [updateaccountvalue]
        public virtual void updateAccountValue(string key, string value, string currency, string accountName)
        {
            Console.WriteLine("UpdateAccountValue. Key: " + key + ", Value: " + value + ", Currency: " + currency + ", AccountName: " + accountName);
        }
        //! [updateaccountvalue]

        //! [updateportfolio]
        public virtual void updatePortfolio(Contract contract, double position, double marketPrice, double marketValue, double averageCost, double unrealisedPNL, double realisedPNL, string accountName)
        {
            Console.WriteLine("UpdatePortfolio. " + contract.Symbol + ", " + contract.SecType + " @ " + contract.Exchange
                + ": Position: " + position + ", MarketPrice: " + marketPrice + ", MarketValue: " + marketValue + ", AverageCost: " + averageCost
                + ", UnrealisedPNL: " + unrealisedPNL + ", RealisedPNL: " + realisedPNL + ", AccountName: " + accountName);
        }
        //! [updateportfolio]

        //! [updateaccounttime]
        public virtual void updateAccountTime(string timestamp)
        {
            Console.WriteLine("UpdateAccountTime. Time: " + timestamp + "\n");
        }
        //! [updateaccounttime]

        //! [accountdownloadend]
        public virtual void accountDownloadEnd(string account)
        {
            Console.WriteLine("Account download finished: " + account + "\n");
        }
        //! [accountdownloadend]

        //! [orderstatus]
        public virtual void orderStatus(int orderId, string status, double filled, double remaining, double avgFillPrice, int permId, int parentId, double lastFillPrice, int clientId, string whyHeld)
        {
            string result = "Status: " + status + ", Filled" + filled + ", Remaining: " + remaining
                + ", AvgFillPrice: " + avgFillPrice + ", PermId: " + permId + ", ParentId: " + parentId + ", LastFillPrice: " + lastFillPrice + ", ClientId: " + clientId + ", WhyHeld: " + whyHeld;
            if (orderInfo.ContainsKey(orderId))
            {
                orderInfo[orderId] = result;
            }
            else
            {
                orderInfo.Add(orderId, result);
            }
            OrderStatusReturnStruct tmp = new OrderStatusReturnStruct(orderId, status, filled, remaining, avgFillPrice, permId, parentId, lastFillPrice, clientId, whyHeld);
            orderInfoDetail[orderId] = tmp;
            Console.WriteLine("OrderStatus. Id: " + orderId + ", Status: " + status + ", Filled" + filled + ", Remaining: " + remaining
                + ", AvgFillPrice: " + avgFillPrice + ", PermId: " + permId + ", ParentId: " + parentId + ", LastFillPrice: " + lastFillPrice + ", ClientId: " + clientId + ", WhyHeld: " + whyHeld);
        }
        //! [orderstatus]

        //! [openorder]
        public virtual void openOrder(int orderId, Contract contract, Order order, OrderState orderState)
        {
            Console.WriteLine("OpenOrder. ID: " + orderId + ", " + contract.Symbol + ", " + contract.SecType + " @ " + contract.Exchange + ": " + order.Action + ", " + order.OrderType + " " + order.TotalQuantity + ", " + orderState.Status);
        }
        //! [openorder]

        //! [openorderend]
        public virtual void openOrderEnd()
        {
            Console.WriteLine("OpenOrderEnd");
        }
        //! [openorderend]

        //! [contractdetails]
        public virtual void contractDetails(int reqId, ContractDetails contractDetails)
        {
            Console.WriteLine("ContractDetails. ReqId: " + reqId + " - " + contractDetails.Summary.Symbol + ", " + contractDetails.Summary.SecType + ", ConId: " + contractDetails.Summary.ConId + " @ " + contractDetails.Summary.Exchange);
        }
        //! [contractdetails]

        //! [contractdetailsend]
        public virtual void contractDetailsEnd(int reqId)
        {
            Console.WriteLine("ContractDetailsEnd. " + reqId + "\n");
        }
        //! [contractdetailsend]

        //! [execdetails]
        public virtual void execDetails(int reqId, Contract contract, Execution execution)
        {
            var execution_tuple = new Tuple<Contract, Execution, int>(contract, execution, reqId);
            executions[execution.ExecId] = execution_tuple;
            Console.WriteLine("ExecDetails. " + reqId + " - " + contract.Symbol + ", " + contract.SecType + ", " + contract.Currency + " - " + execution.ExecId + ", " + execution.OrderId + ", " + execution.Shares);
        }
        //! [execdetails]

        //! [execdetailsend]
        public virtual void execDetailsEnd(int reqId)
        {
            Console.WriteLine("ExecDetailsEnd. " + reqId + "\n");
        }
        //! [execdetailsend]

        //! [commissionreport]
        public virtual void commissionReport(CommissionReport commissionReport)
        {
            Console.WriteLine("CommissionReport. " + commissionReport.ExecId + " - " + commissionReport.Commission + " " + commissionReport.Currency + " RPNL " + commissionReport.RealizedPNL);
        }
        //! [commissionreport]

        //! [fundamentaldata]
        public virtual void fundamentalData(int reqId, string data)
        {
            Console.WriteLine("FundamentalData. " + reqId + "" + data + "\n");
        }
        //! [fundamentaldata]

        //! [historicaldata]
        public virtual void historicalData(int reqId, string date, double open, double high, double low, double close, int volume, int count, double WAP, bool hasGaps)
        {
            Console.WriteLine("HistoricalData. " + reqId + " - Date: " + date + ", Open: " + open + ", High: " + high + ", Low: " + low + ", Close: " + close + ", Volume: " + volume + ", Count: " + count + ", WAP: " + WAP + ", HasGaps: " + hasGaps);
        }
        //! [historicaldata]

        //! [marketdatatype]
        public virtual void marketDataType(int reqId, int marketDataType)
        {
            Console.WriteLine("MarketDataType. " + reqId + ", Type: " + marketDataType + "\n");
        }
        //! [marketdatatype]

        //! [updatemktdepth]
        public virtual void updateMktDepth(int tickerId, int position, int operation, int side, double price, int size)
        {
            Console.WriteLine("UpdateMarketDepth. " + tickerId + " - Position: " + position + ", Operation: " + operation + ", Side: " + side + ", Price: " + price + ", Size" + size);
        }
        //! [updatemktdepth]

        //! [updatemktdepthl2]
        public virtual void updateMktDepthL2(int tickerId, int position, string marketMaker, int operation, int side, double price, int size)
        {
            Console.WriteLine("UpdateMarketDepthL2. " + tickerId + " - Position: " + position + ", Operation: " + operation + ", Side: " + side + ", Price: " + price + ", Size" + size);
        }
        //! [updatemktdepthl2]

        //! [updatenewsbulletin]
        public virtual void updateNewsBulletin(int msgId, int msgType, String message, String origExchange)
        {
            Console.WriteLine("News Bulletins. " + msgId + " - Type: " + msgType + ", Message: " + message + ", Exchange of Origin: " + origExchange + "\n");
        }
        //! [updatenewsbulletin]

        //! [position]
        public virtual void position(string account, Contract contract, double pos, double avgCost)
        {
            this.tmpPosInfo += "Position. " + account + " - Symbol: " + contract.Symbol + ", SecType: " + contract.SecType + ", Currency: " + contract.Currency + ", Position: " + pos + ", Avg cost: " + avgCost + '\n';
            //Console.WriteLine("Position. " + account + " - Symbol: " + contract.Symbol + ", SecType: " + contract.SecType + ", Currency: " + contract.Currency + ", Position: " + pos + ", Avg cost: " + avgCost);
        }
        //! [position]

        //! [positionend]
        public virtual void positionEnd()
        {
            this.posInfo = this.tmpPosInfo.Substring(0, tmpPosInfo.Length - 1);
            this.tmpPosInfo = "";
            this.posInfoReady = true;
            //Console.WriteLine("PositionEnd \n");
        }
        //! [positionend]

        //! [realtimebar]
        public virtual void realtimeBar(int reqId, long time, double open, double high, double low, double close, long volume, double WAP, int count)
        {
            Console.WriteLine("RealTimeBars. " + reqId + " - Time: " + time + ", Open: " + open + ", High: " + high + ", Low: " + low + ", Close: " + close + ", Volume: " + volume + ", Count: " + count + ", WAP: " + WAP);
        }
        //! [realtimebar]

        //! [scannerparameters]
        public virtual void scannerParameters(string xml)
        {
            Console.WriteLine("ScannerParameters. " + xml + "\n");
        }
        //! [scannerparameters]

        //! [scannerdata]
        public virtual void scannerData(int reqId, int rank, ContractDetails contractDetails, string distance, string benchmark, string projection, string legsStr)
        {
            Console.WriteLine("ScannerData. " + reqId + " - Rank: " + rank + ", Symbol: " + contractDetails.Summary.Symbol + ", SecType: " + contractDetails.Summary.SecType + ", Currency: " + contractDetails.Summary.Currency
                + ", Distance: " + distance + ", Benchmark: " + benchmark + ", Projection: " + projection + ", Legs String: " + legsStr);
        }
        //! [scannerdata]

        //! [scannerdataend]
        public virtual void scannerDataEnd(int reqId)
        {
            Console.WriteLine("ScannerDataEnd. " + reqId);
        }
        //! [scannerdataend]

        //! [receivefa]
        public virtual void receiveFA(int faDataType, string faXmlData)
        {
            Console.WriteLine("Receing FA: " + faDataType + " - " + faXmlData);
        }
        //! [receivefa]

        public virtual void bondContractDetails(int requestId, ContractDetails contractDetails)
        {
            Console.WriteLine("Bond. Symbol " + contractDetails.Summary.Symbol + ", " + contractDetails.Summary);
        }

        //! [historicaldataend]
        public virtual void historicalDataEnd(int reqId, string startDate, string endDate)
        {
            Console.WriteLine("Historical data end - " + reqId + " from " + startDate + " to " + endDate);
        }
        //! [historicaldataend]

        public virtual void verifyMessageAPI(string apiData)
        {
            Console.WriteLine("verifyMessageAPI: " + apiData);
        }
        public virtual void verifyCompleted(bool isSuccessful, string errorText)
        {
            Console.WriteLine("verifyCompleted. IsSuccessfule: " + isSuccessful + " - Error: " + errorText);
        }
        public virtual void verifyAndAuthMessageAPI(string apiData, string xyzChallenge)
        {
            Console.WriteLine("verifyAndAuthMessageAPI: " + apiData + " " + xyzChallenge);
        }
        public virtual void verifyAndAuthCompleted(bool isSuccessful, string errorText)
        {
            Console.WriteLine("verifyAndAuthCompleted. IsSuccessful: " + isSuccessful + " - Error: " + errorText);
        }
        //! [displaygrouplist]
        public virtual void displayGroupList(int reqId, string groups)
        {
            Console.WriteLine("DisplayGroupList. Request: " + reqId + ", Groups" + groups);
        }
        //! [displaygrouplist]

        //! [displaygroupupdated]
        public virtual void displayGroupUpdated(int reqId, string contractInfo)
        {
            Console.WriteLine("displayGroupUpdated. Request: " + reqId + ", ContractInfo: " + contractInfo);
        }
        //! [displaygroupupdated]

        //! [positionmulti]
        public virtual void positionMulti(int reqId, string account, string modelCode, Contract contract, double pos, double avgCost)
        {
            Console.WriteLine("Position Multi. Request: " + reqId + ", Account: " + account + ", ModelCode: " + modelCode + ", Symbol: " + contract.Symbol + ", SecType: " + contract.SecType + ", Currency: " + contract.Currency + ", Position: " + pos + ", Avg cost: " + avgCost + "\n");
        }
        //! [positionmulti]

        //! [positionmultiend]
        public virtual void positionMultiEnd(int reqId)
        {
            Console.WriteLine("Position Multi End. Request: " + reqId + "\n");
        }
        //! [positionmultiend]

        //! [accountupdatemulti]
        public virtual void accountUpdateMulti(int reqId, string account, string modelCode, string key, string value, string currency)
        {
            Console.WriteLine("Account Update Multi. Request: " + reqId + ", Account: " + account + ", ModelCode: " + modelCode + ", Key: " + key + ", Value: " + value + ", Currency: " + currency + "\n");
        }
        //! [accountupdatemulti]

        //! [accountupdatemultiend]
        public virtual void accountUpdateMultiEnd(int reqId)
        {
            Console.WriteLine("Account Update Multi End. Request: " + reqId + "\n");
        }
        //! [accountupdatemultiend]

        //! [securityDefinitionOptionParameter]
        public void securityDefinitionOptionParameter(int reqId, string exchange, int underlyingConId, string tradingClass, string multiplier, HashSet<string> expirations, HashSet<double> strikes)
        {
            Console.WriteLine("Security Definition Option Parameter. Reqest: {0}, Exchange: {1}, Undrelying contract id: {2}, Trading class: {3}, Multiplier: {4}, Expirations: {5}, Strikes: {6}",
                              reqId, exchange, underlyingConId, tradingClass, multiplier, string.Join(", ", expirations), string.Join(", ", strikes));
        }
        //! [securityDefinitionOptionParameter]

        //! [securityDefinitionOptionParameterEnd]
        public void securityDefinitionOptionParameterEnd(int reqId)
        {
            Console.WriteLine("Security Definition Option Parameter End. Request: " + reqId + "\n");
        }
        //! [securityDefinitionOptionParameterEnd]

        //! [connectack]
        public void connectAck()
        {
            if (ClientSocket.AsyncEConnect)
                ClientSocket.startApi();
        }
        //! [connectack]


        public void softDollarTiers(int reqId, SoftDollarTier[] tiers)
        {
            Console.WriteLine("Soft Dollar Tiers:");

            foreach (var tier in tiers)
            {
                Console.WriteLine(tier.DisplayName);
            }
        }
    }
}
