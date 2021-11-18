using System.ServiceModel;

namespace Contract
{
    [ServiceContract(CallbackContract = typeof(IMyContractCallback))]
    public interface IMyContract
    {
        [OperationContract]
        int Connect(string name);

        [OperationContract]
        void Disconnect(int id);

        [OperationContract(IsOneWay = true)]
        void SendMsg(int id, string message);
    }
    

    public interface IMyContractCallback
    {
        [OperationContract(IsOneWay = true)]
        void MsgCallback(string message);
    }
}
