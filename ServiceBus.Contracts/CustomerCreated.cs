using System;

namespace ServiceBus.Contracts
{
    public class CustomerCreated
    {
        public int Id { get;  set; }
        public string FullName { get;  set; }
        public DateTime DateOfBirth { get;  set; }
    }
}
