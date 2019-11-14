using System;
using System.Collections.Generic;
using System.Text;

namespace PolarisAICore.Response {
    class ShowNews {
        static readonly Random _random = new Random();

        static readonly String[] _noEntityResponses =
        {
            "Of course! Here's today's news:",
            "Sure! This is what is happening today:",
            "Alright! Here's today's news:",
            "You don't have to ask again! This is what is happening today:"
            "Got it! Here it is:",
            "Right! Here are the news:",
            "These are the news:",
            "Sure thing! This is the latest news:",
            "I will play the news:",
            "Here is an update:",
            "Let's see:"
        };

        public static String SetResponse(Utterance u) {
            
            return _noEntityResponses[_random.Next(_noEntityResponses.Length)];
        }
    }
}
