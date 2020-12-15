using System;

namespace ServiceBus.Producer.Requests
{
    public class CreateCustomerRequest
    {
        public int Id { get;  set; }
        public string FullName { get;  set; }
        public DateTime DateOfBirth { get;  set; }
    }
}