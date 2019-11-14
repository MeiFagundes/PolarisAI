using System;
using System.Collections.Generic;
using System.Text;

namespace PolarisAICore.Response {
    class WhatsYourNameSmallTalk {
        static readonly Random _random = new Random();

        static readonly String[] _noEntityResponses =
        {
            "My name is Polaris! It's as bright as me, don't you think?",
            "My name is Polaris! Radiant and beautiful, just like me!",
            "My name is Polaris! The brightest ever!",
            "My name is Polaris! Even if you don't like me, you can't deny how beatiful my name is!"
        };

        public static String SetResponse(Utterance u) {
            
            return _noEntityResponses[_random.Next(_noEntityResponses.Length)];
        }
    }
}
