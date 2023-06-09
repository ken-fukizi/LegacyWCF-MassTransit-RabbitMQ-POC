﻿using BusinessService.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BusinessService
{   
    [ServiceContract]
    public interface IService
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        SaveCustomerLeadResponse SaveCustomerLead(SaveCustomerLeadRequest customerLeadRequest);
    }    
    
}
