using CustomerFeedback.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CustomerFeedback.Model;
using CustomerFeedback.TextAnalytics;
using System;
using Azure.AI.TextAnalytics;
using System.Collections.Generic;
using System.Linq;

namespace CustomerFeedback.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICustomerFeedbackService _customerFeedbackService;
        private readonly ISentimentAnalysis _sentimentAnalysis;

        public HomeController(ICustomerFeedbackService customerFeedbackService, ISentimentAnalysis sentimentAnalysis)
        {
            _customerFeedbackService = customerFeedbackService;
            _sentimentAnalysis = sentimentAnalysis;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SaveFeedback(Feedback feedback)
        {
            _customerFeedbackService.SaveFeedback(feedback.Firstname, feedback.Lastname, feedback.Comment);
            return RedirectToAction("Thanks");
        }


        public IActionResult Feedbacks()
        {
            var comments = _customerFeedbackService.GetAll();
            var sentiment = _sentimentAnalysis.GetSentimentResult(comments);
            return View(sentiment);
        }

        public IActionResult Thanks()
        {
            return View();
        }
    }
}
