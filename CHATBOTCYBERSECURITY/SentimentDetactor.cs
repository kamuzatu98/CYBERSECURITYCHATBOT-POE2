using System.Collections.Generic;
using System.Linq;

namespace CybersecurityBotWPF
{
    public static class SentimentDetector
    {
        private static readonly List<string> WorriedKeywords = new()
        {
            "worried", "scared", "afraid", "nervous", "anxious", "fearful", "stressed"
        };

        private static readonly List<string> FrustratedKeywords = new()
        {
            "frustrated", "annoyed", "angry", "confused", "lost",
            "dont understand", "don't understand"
        };

        private static readonly List<string> CuriousKeywords = new()
        {
            "curious", "interested", "want to know", "tell me more",
            "explain", "how does", "what is"
        };

        public enum Sentiment { Neutral, Worried, Frustrated, Curious }

        public static Sentiment Detect(string input)
        {
            string lower = input.ToLower();

            if (WorriedKeywords.Any(k => lower.Contains(k)))
                return Sentiment.Worried;

            if (FrustratedKeywords.Any(k => lower.Contains(k)))
                return Sentiment.Frustrated;

            if (CuriousKeywords.Any(k => lower.Contains(k)))
                return Sentiment.Curious;

            return Sentiment.Neutral;
        }

        public static string GetSentimentPrefix(Sentiment sentiment) => sentiment switch
        {
            Sentiment.Worried =>
                "It's completely understandable to feel that way. You're not alone! 💙",
            Sentiment.Frustrated =>
                "I hear you, let me try to make this clearer for you. 😊",
            Sentiment.Curious =>
                "Great curiosity! That's the first step to staying safe online. 🌟",
            _ => string.Empty
        };
    }
}