﻿using Ninject.Activation;
using SharedKernel.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IRequest = SharedKernel.Service.IRequest;

namespace BusinessService.ServiceModels
{
    public class SaveCustomerLeadRequest : IRequest
    {
        public string LeadSource { get; set; }
        public string  Leadmessage { get; set; }
        public long IdNumber { get; set; } 
        public DateTime DateTimeReceived { get; set; }
        public DateTime DateTimeSent { get; set; }
    } 
}