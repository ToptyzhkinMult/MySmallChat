using System;
using System.Collections.Generic;
using System.Linq;
using Contract;
using System.ServiceModel;
using Server.Models;


namespace Server
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MyContractServer : IMyContract
    {
        private List<UserServer> users = new List<UserServer>();
        private int nextId = 1;
        
        public int Connect(string name)
        {
            UserServer _user = new UserServer
            {
                Id = nextId,
                Name = name,
                operationContext = OperationContext.Current
            };

            nextId++;
            SendMsg(0, $"{_user.Name} подключился к чату!"); //шлём id = 0, потому что не говорим юзеру подключился он или нет
            //SendMsg(_user.Id, $"{_user.Name} подключился к чату!");
            users.Add(_user);
            


            return _user.Id;
        }

        public void Disconnect(int id)
        {
            var _user = users.FirstOrDefault(u => u.Id == id);
            if (users != null)
            {
                users.Remove(_user);
                SendMsg(0, $"{_user.Name} покинул чат"); //шлём id = 0, потому что не говорим юзеру подключился он или нет
                //SendMsg(_user.Id,$"{_user.Name} покинул чат");
            }
        }

        public void SendMsg(int id, string message)
        {
            /*string _message = DateTime.Now.ToShortTimeString()
                              + $" {users.FirstOrDefault(u => u.Id == id).Name}: "
                              + message;
            users.ForEach(u => u.operationContext.GetCallbackChannel<IMyContractCallback>().MsgCallback(_message));*/

            foreach (var item in users) {
                string _message = DateTime.Now.ToShortTimeString();

                var user = users.FirstOrDefault(u => u.Id == id);
                if (user != null) {
                    _message += $": {user.Name} ";
                }

                _message += message;
                item.operationContext.GetCallbackChannel<IMyContractCallback>().MsgCallback(_message);
            }
            
        }
    }
}
