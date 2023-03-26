using Ninject.Activation;
using SharedKernel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using IRequest = SharedKernel.Service.IRequest;

namespace BusinessService.ServiceModels
{
    [DataContract]
    public class SaveCustomerLeadRequest : IRequest
    {
        [DataMember]
        public string LeadSource { get; set; }
        [DataMember]
        public string  Leadmessage { get; set; }
        [DataMember]
        public long IdNumber { get; set; }
        [DataMember]
        public DateTime DateTimeReceived { get; set; }
        [DataMember]
        public DateTime DateTimeSent { get; set; }
    } 
}