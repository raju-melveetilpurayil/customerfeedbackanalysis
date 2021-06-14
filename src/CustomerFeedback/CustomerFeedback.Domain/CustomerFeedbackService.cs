using CustomerFeedback.Data;
using CustomerFeedback.Model;
using System.Collections.Generic;

namespace CustomerFeedback.Domain
{
    public class CustomerFeedbackService : ICustomerFeedbackService
    {
        private readonly ICustomerFeedbackRepository _customerFeedbackRepository;

        public CustomerFeedbackService(ICustomerFeedbackRepository customerFeedbackRepository)
        {
            _customerFeedbackRepository = customerFeedbackRepository;
        }

        public List<Feedback> GetAll() => _customerFeedbackRepository.GetAll();

        public void SaveFeedback(string firstname, string lastname, string comment)
        {
            _customerFeedbackRepository.Save(firstname, lastname, comment);
        }
    }
}