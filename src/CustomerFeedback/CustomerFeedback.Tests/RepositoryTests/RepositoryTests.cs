using CustomerFeedback.Data;
using CustomerFeedback.DataContext;
using CustomerFeedback.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace CustomerFeedback.Tests.RepositoryTests
{
    [TestClass]
    public class RepositoryTests
    {
        private readonly Feedback _feedback;
        private readonly CustomerFeedbackRepository _customerFeedbackRepository;

        public RepositoryTests()
        {
            var options = new DbContextOptionsBuilder<CustomerFeedbackDataContext>().UseInMemoryDatabase(databaseName: "CustomerFeedbackDB").Options;
            var _customerFeedbackDataContext = new CustomerFeedbackDataContext(options);
           _customerFeedbackRepository = new CustomerFeedbackRepository(_customerFeedbackDataContext);
            
            _feedback = new Feedback
            {
                Firstname = "Alan",
                Lastname = "Smith",
                Comment = "This is not good"
            };
        }

        [TestMethod]
        public void Saving_Feedback_Should_Return_GUID_Of_New_Feedback_Record()
        {
            var response = _customerFeedbackRepository.Save(_feedback.Firstname, _feedback.Lastname, _feedback.Comment);
            Assert.AreEqual(response.GetType(), typeof(Guid));
        }
        
        [TestMethod]
        public void GetAll_Feedback()
        {
            var response = _customerFeedbackRepository.GetAll();
            Assert.IsNotNull(response);
        }
    }
}
