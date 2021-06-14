using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerFeedback.TextAnalytics
{
    public class SentimentResult
    {
        public string OverallFeedback { get; set; }
        public List<FeedbackWithSentiment> FeedbackWithSentiments { get; set; } = new List<FeedbackWithSentiment>();
    }
}
