using CustomerFeedback.Domain;
using CustomerFeedback.App.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CustomerFeedback.Model;
using CustomerFeedback.TextAnalytics;

namespace CustomerFeedback.Tests.ControllerTests
{
    [TestClass]
    public class HomeControllerTests
    {
        private readonly Feedback _feedback;
        private readonly Mock<ICustomerFeedbackService> _customerFeedbackService;
        private readonly Mock<ISentimentAnalysis> _sentimentAnalysis;
        private readonly HomeController _homeController;


        public HomeControllerTests()
        {
            _feedback = new Feedback
            {
                Firstname = "Alan",
                Lastname = "Smith",
                Comment = "This is not good"
            };
            _customerFeedbackService = new Mock<ICustomerFeedbackService>();
            _sentimentAnalysis = new Mock<ISentimentAnalysis>();
            _homeController = new HomeController(_customerFeedbackService.Object,_sentimentAnalysis.Object);
        }

        [TestMethod]
        public void Should_Call_SaveFeedback_Method_In_CusomerFeedbackService_Class()
        {
            _homeController.SaveFeedback(_feedback);

            _customerFeedbackService.Verify(x =>
                x.SaveFeedback(_feedback.Firstname, _feedback.Lastname, _feedback.Comment));
        }

        [TestMethod]
        public void Should_Redirect_To_Thanks_Page_After_Saving_Feedback()
        {
            var actionName = _homeController.SaveFeedback(_feedback) is RedirectToActionResult result
                ? result.ActionName
                : string.Empty;

            Assert.AreEqual("Thanks", actionName);
        }
    }
}
