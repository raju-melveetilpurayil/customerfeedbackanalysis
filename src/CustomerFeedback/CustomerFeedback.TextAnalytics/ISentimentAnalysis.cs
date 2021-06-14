using Azure.AI.TextAnalytics;
using CustomerFeedback.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerFeedback.TextAnalytics
{
    public interface ISentimentAnalysis
    {
        SentimentResult GetSentimentResult(List<Feedback> feedbacks);
    }
}
