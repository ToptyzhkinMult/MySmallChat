using System.ServiceModel;

namespace Server.Models
{
    class UserServer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public OperationContext operationContext { get; set; }
    }
}
