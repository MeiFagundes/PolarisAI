using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace PolarisAICore.Response {
    class AddAlarm {

        static readonly Random _random = new Random();

        static readonly String[] _responses =
        {
            "Of course, I've set an alarm for",
            "Sure, I've set an alarm for",
            "Got it! I've set an alarm for",
            "You don't have to ask again! I've set an alarm for",
        };

        static readonly String[] _noEntityResponses =
        {
            "Of course, what time do you want your alarm to be set to?",
            "Sure, do you want me to set your alarm for what time?",
            "Alright, what time do you want your alarm to be set to?",
            "You don't have to ask again! Do you want me to set your alarm to what time?"
        };

        public static String SetResponse(Utterance u) {

            if (u.Entity["time"].Type != JTokenType.Null)
                if (u.Entity["entity"].Type != JTokenType.Null)
                    return $"{_responses[_random.Next(_responses.Length)]} {u.Entity["entity"]}, {u.Entity["time"]}.";
                else
                    return $"{_responses[_random.Next(_responses.Length)]} {u.Entity["time"]}.";
            else
                return _noEntityResponses[_random.Next(_noEntityResponses.Length)];
        }
    }
}
