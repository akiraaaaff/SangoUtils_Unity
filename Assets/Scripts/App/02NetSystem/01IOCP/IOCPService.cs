using SangoNetProtol;
using SangoUtils_Bases_UnityEngine;
using SangoUtils_Bases_Universal;
using SangoUtils_IOCP;
using SangoUtils_NetOperation;
using System.Collections.Generic;

namespace SangoUtils_Unity_Scripts.Net
{
    public class IOCPService : BaseService<IOCPService>, INetClientOperation
    {
        private IOCPPeer<IOCPClientPeer> _clientPeerInstance;
        private NetClientOperationHandler _netOperationHandler = new();

        private Queue<SangoNetMessage> _netMessageProxyQueue = new();

        private NetEnvironmentConfig _currentNetEnvironmentConfig;

        public override void OnInit()
        {
            
        }

        public void SetConfig(NetEnvironmentConfig netEnvironmentConfig)
        {
            _currentNetEnvironmentConfig = netEnvironmentConfig;
        }

        protected override void OnUpdate()
        {
            if (_netMessageProxyQueue.Count > 0)
            {
                SangoNetMessage sangoNetMessage = _netMessageProxyQueue.Dequeue();
                OnMessageReceivedInMainThread(sangoNetMessage);
            }
        }

        public void SendOperationRequest(NetOperationCode operationCode, string messageStr)
        {
            _clientPeerInstance.ClientPeer.SendOperationRequest(operationCode, messageStr);
        }

        public void SendOperationBroadcast(NetOperationCode operationCode, string messageStr)
        {
            throw new System.NotImplementedException();
        }

        private void AddNetMessageProxy(SangoNetMessage sangoNetMessage)
        {
            _netMessageProxyQueue.Enqueue(sangoNetMessage);
        }

        public void OnMessageReceived(SangoNetMessage sangoNetMessage)
        {
            AddNetMessageProxy(sangoNetMessage);
        }

        private void OnMessageReceivedInMainThread(SangoNetMessage sangoNetMessage)
        {
            _netOperationHandler.NetMessageCommandBroadcast(sangoNetMessage);
        }

        public T GetNetRequest<T>(NetOperationCode netOperationCode) where T : BaseNetRequest, new()
        {
            return _netOperationHandler.GetNetRequest<T>(netOperationCode);
        }

        public T GetNetEvent<T>(NetOperationCode operationCode) where T : BaseNetEvent, new()
        {
            return _netOperationHandler.GetNetEvent<T>(operationCode);
        }

        public T GetNetBroadcast<T>(NetOperationCode operationCode) where T : BaseNetBroadcast, new()
        {
            return _netOperationHandler.GetNetBroadcast<T>(operationCode);
        }

        public override void OnDispose()
        {
            
        }

        public void OpenClient()
        {
            string ipAddress = _currentNetEnvironmentConfig.ServerAddress;
            int port = _currentNetEnvironmentConfig.ServerPort;
            _clientPeerInstance = new IOCPPeer<IOCPClientPeer>();
            _clientPeerInstance.OpenAsUnityClient(ipAddress, port);
            _netOperationHandler = new();

            DefaultIOCPRequest defaultNetRequest = _netOperationHandler.GetNetRequest<DefaultIOCPRequest>(NetOperationCode.Default);
            DefaultIOCPEvent defaultNetEvent = _netOperationHandler.GetNetEvent<DefaultIOCPEvent>(NetOperationCode.Default);
        }

        public void CloseClient()
        {
            _clientPeerInstance.CloseAsClient();
        }
    }

    public class NetEnvironmentConfig : BaseConfig
    {
        public NetEnvMode NetEnvMode { get; set; } = NetEnvMode.Offline;
        public string ServerAddress { get; set; } = "127.0.0.1";
        public int ServerPort { get; set; } = 52037;
        public string ServerAddressAndPort { get; set; } = "ws://127.0.0.1:52516";
    }

    public enum NetEnvMode
    {
        Offline,
        Online_IOCP,
        Online_Http,
        Online_WebSocket
    }
}
