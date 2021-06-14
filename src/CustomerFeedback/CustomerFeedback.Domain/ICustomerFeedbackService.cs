using CustomerFeedback.Model;
using System.Collections.Generic;

namespace CustomerFeedback.Domain
{
    public interface ICustomerFeedbackService
    {
        void SaveFeedback(string firstname, string lastname, string comment);
        public List<Feedback> GetAll();
    }
}