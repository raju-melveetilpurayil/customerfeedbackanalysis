using System;
using System.Collections.Generic;
using System.Text;
using Azure;
using System.Globalization;
using Azure.AI.TextAnalytics;
using CustomerFeedback.Model;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace CustomerFeedback.TextAnalytics
{
    public class SentimentAnalysis : ISentimentAnalysis
    {
        //local cache
        public static SentimentResult SentimentResultSet = new SentimentResult();

        private readonly AzureKeyCredential credentials;
        private readonly Uri endpoint;
        private readonly IConfiguration configuration;

        public SentimentAnalysis(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.credentials = new AzureKeyCredential(configuration.GetSection("TextAnalytics:Credentials").Value);
            this.endpoint = new Uri(configuration.GetSection("TextAnalytics:Endpoint").Value);
        }

        public SentimentResult GetSentimentResult(List<Feedback> feedbacks)
        {
            SentimentResult sentimentResult = new SentimentResult();
            if (SentimentResultSet.FeedbackWithSentiments.Count == 0 ||
                feedbacks.Count != SentimentResultSet.FeedbackWithSentiments.Count)
            {
                sentimentResult = getSentimentData(feedbacks);
                SentimentResultSet = sentimentResult;
            }
            else
            {
                return SentimentResultSet;
            }
            return getSentimentData(feedbacks);
        }

        private TextSentiment getOverallSentiment(int result, bool isNeutral)
        {
            int sum = result;
            TextSentiment overallSentiment = TextSentiment.Neutral;

            if (sum > 0)
            {
                overallSentiment = TextSentiment.Positive;
            }
            else if (sum < 0)
            {
                overallSentiment = TextSentiment.Negative;
            }
            else
            {
                if (isNeutral)
                {
                    overallSentiment = TextSentiment.Neutral;
                }
                else
                {
                    overallSentiment = TextSentiment.Mixed;
                }
            }
            return overallSentiment;
        }
        private SentimentResult getSentimentData(List<Feedback> feedbacks)
        {
            TextSentiment overallSentiment = TextSentiment.Neutral;
            SentimentResult sentimentResult = new SentimentResult();
            List<int> sentiments = new List<int>();
            if (feedbacks != null && feedbacks.Count > 0)
            {

                var client = new TextAnalyticsClient(endpoint, credentials);
                var allFeedbacks = new List<string>();

                foreach (var _feedback in feedbacks)
                {
                    allFeedbacks.Add(_feedback.Comment);
                }

                if (allFeedbacks.Count > 0)
                {
                    AnalyzeSentimentResultCollection _sentiments = client.AnalyzeSentimentBatch(allFeedbacks);
                    if (_sentiments != null)
                    {
                        bool isNeutral = true;
                        int result = 0;
                        foreach (AnalyzeSentimentResult _sentiment in _sentiments)
                        {
                            FeedbackWithSentiment feedbackWithSentiment = new FeedbackWithSentiment();
                            feedbackWithSentiment.Feedback = _sentiment.DocumentSentiment.Sentences.FirstOrDefault().Text;
                            feedbackWithSentiment.Sentiment = _sentiment.DocumentSentiment.Sentiment.ToString();
                            sentimentResult.FeedbackWithSentiments.Add(feedbackWithSentiment);

                            switch (_sentiment.DocumentSentiment.Sentiment)
                            {
                                case TextSentiment.Mixed:
                                    isNeutral = false;
                                    break;
                                case TextSentiment.Negative:
                                    isNeutral = false;
                                    result += -1;
                                    break;
                                case TextSentiment.Positive:
                                    isNeutral = false;
                                    result += 1;
                                    break;
                            }
                        }
                        overallSentiment = getOverallSentiment(result, isNeutral);
                    }
                }
            }
            sentimentResult.OverallFeedback = overallSentiment.ToString();

            return sentimentResult;
        }
    }
}
    


