using CustomerFeedback.DataContext;
using CustomerFeedback.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomerFeedback.Data
{
    public class CustomerFeedbackRepository : ICustomerFeedbackRepository
    {
        private readonly CustomerFeedbackDataContext _customerFeedbackDataContext;
        public CustomerFeedbackRepository()
        {

        }
        public CustomerFeedbackRepository(CustomerFeedbackDataContext customerFeedbackDataContext)
        {
            _customerFeedbackDataContext = customerFeedbackDataContext;
        }

        public List<Feedback> GetAll() => _customerFeedbackDataContext.Feedbacks.ToList();

        public object Save(string firstname, string lastname, string comment)
        {
            object result = null;
            try
            {
                if (!string.IsNullOrEmpty(firstname) && !string.IsNullOrEmpty(lastname) && !string.IsNullOrEmpty(comment))
                {
                    var isExists = _customerFeedbackDataContext.Feedbacks.Where(x => x.Comment == comment && x.Lastname == lastname && x.Comment == comment);
                    if (isExists == null || isExists.Count() == 0)
                    {
                        var feedback = new Feedback()
                        {
                            Comment = comment,
                            Firstname = firstname,
                            Lastname = lastname
                        };
                        _customerFeedbackDataContext.Feedbacks.Add(feedback);
                        _customerFeedbackDataContext.SaveChanges();
                        result = feedback.Id;
                    }
                }
            }
            catch
            {
                throw;
            }
            return result;
        }
    }
}