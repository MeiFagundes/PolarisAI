using System;
using System.Collections.Generic;
using System.Text;

namespace PolarisAICore.Response {
    class AddReminder {

        static readonly Random _random = new Random();

        static readonly String[] _responses = 
        {
            "Of course, I will remind you to",
            "Sure, i will remind you to",
            "Got it! I will remind you to",
            "You don't have to ask again! I will remind you to",
        };

        static readonly String[] _noEntityResponses =
        {
            "Of course, what do you want me to remind you about?",
            "Sure, what do you want to be reminded about?",
            "Alright, what do you want to be reminded about?",
            "You don't have to ask again! what do you want me to remind you about?"
        };

        public static String SetResponse(Utterance u) {

            if (u.Entity["entity"] != null)
                return $"{_responses[_random.Next(_responses.Length)]} {u.Entity["entity"]}.";
            else
                return _noEntityResponses[_random.Next(_noEntityResponses.Length)];
        }
    }
}
