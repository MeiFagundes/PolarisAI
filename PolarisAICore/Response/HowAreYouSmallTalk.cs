using System;
using System.Collections.Generic;
using System.Text;

namespace PolarisAICore.Response {
    class HowAreYouSmallTalk {
        static readonly Random _random = new Random();

        static readonly String[] _noEntityResponses =
        {
            "I'm fine, thanks for asking! I hope you're having a fantastic day!",
            "I'm mostly sleepy! The work I do can be hard sometimes, but worth it!",
            "I'm really happy today! Sometimes you just need to look on the bright side!",
            "I'm alright! Thinking about drinking some tea later!"
        };

        public static String SetResponse(Utterance u) {
            
            return _noEntityResponses[_random.Next(_noEntityResponses.Length)];
        }
    }
}
