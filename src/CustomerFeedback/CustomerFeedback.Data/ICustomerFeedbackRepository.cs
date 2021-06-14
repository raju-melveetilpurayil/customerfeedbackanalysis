using CustomerFeedback.Model;
using System;
using System.Collections.Generic;

namespace CustomerFeedback.Data
{
    public interface ICustomerFeedbackRepository
    {
        object Save(string firstname, string lastname, string comment);
        public List<Feedback> GetAll();
    }
}