using SangoNetProtol;
using SangoUtils_Common.Messages;

public class OperationKeyCoreSystem : BaseSystem<OperationKeyCoreSystem>
{
    private OperationKeyBroadcast _operationKeyBroadcast;

    public OperationKeyCoreSystem()
    {
        _operationKeyBroadcast = WebSocketService.Instance.GetNetBroadcast<OperationKeyBroadcast>(NetOperationCode.OperationKey);
    }

    public void SetAndSendOperationKey(OperationKeyType operationKeyType, string operationString)
    {
        _operationKeyBroadcast.SetAndSendOperationKeyReqMessage(CacheService.Instance.EntityCache.PlayerEntity_This.EntityID, operationKeyType, operationString, CacheService.Instance.RoomID);
    }
}
