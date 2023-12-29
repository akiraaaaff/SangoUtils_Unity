// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: SangoNetProtocol.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace SangoNetProtol
{

    [global::ProtoBuf.ProtoContract()]
    public partial class SangoNetMessage : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public NetMessageHead NetMessageHead { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public NetMessageBody NetMessageBody { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string NetMessageTimestamp { get; set; } = "";

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class NetMessageHead : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public NetMessageCommandCode NetMessageCommandCode { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public NetOperationCode NetOperationCode { get; set; }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class NetMessageBody : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public NetReturnCode NetReturnCode { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string NetMessageStr { get; set; } = "";

    }

    [global::ProtoBuf.ProtoContract()]
    public enum NetMessageCommandCode
    {
        NetOperationRequest = 1,
        NetOperationResponse = 2,
        NetEventData = 3,
    }

    [global::ProtoBuf.ProtoContract()]
    public enum NetOperationCode
    {
        Default = 1,
        Login = 2,
        Regist = 3,
        Aoi = 4,
        SyncTransformInfo = 5,
    }

    [global::ProtoBuf.ProtoContract()]
    public enum NetReturnCode
    {
        Succeed = 1,
        Failed = 2,
    }

    [global::ProtoBuf.ProtoContract()]
    public enum NetErrorCode
    {
        [global::ProtoBuf.ProtoEnum(Name = @"UnKnown_Error")]
        UnKnownError = 1,
        [global::ProtoBuf.ProtoEnum(Name = @"Server_Data_Error")]
        ServerDataError = 2,
    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006