using CustomerFeedback.Data;
using CustomerFeedback.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CustomerFeedback.Model;

namespace CustomerFeedback.Tests.ServiceTests
{
    [TestClass]
    public class CustomerFeedbackServiceTests
    {  
        private readonly Feedback _feedback;
        private readonly Mock<ICustomerFeedbackRepository> _customerFeedbackRepository;
        private readonly CustomerFeedbackService _customerFeedbackService;

        public CustomerFeedbackServiceTests()
        {
             _customerFeedbackRepository = new Mock<ICustomerFeedbackRepository>();
            _customerFeedbackService = new CustomerFeedbackService(_customerFeedbackRepository.Object);


            _feedback = new Feedback
            {
                Firstname = "Alan",
                Lastname = "Smith",
                Comment = "This is not good"
            };
        }

        [TestMethod]
        public void Should_Call_Save_Method_In_Repository()
        {
            _customerFeedbackService.SaveFeedback(_feedback.Firstname, _feedback.Lastname, _feedback.Comment);
            _customerFeedbackRepository.Verify(x => x.Save(_feedback.Firstname, _feedback.Lastname, _feedback.Comment));
        }
        [TestMethod]
        public void Should_Call_GetAll_Method_In_Repository()
        {
            _customerFeedbackService.GetAll();
            _customerFeedbackRepository.Verify(x => x.GetAll());
        }
    }
}
