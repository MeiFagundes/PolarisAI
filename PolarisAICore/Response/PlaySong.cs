using Newtonsoft.Json.Linq;
using System;

namespace PolarisAICore.Response {
    class PlaySong {

        static readonly Random _random = new Random();

        static readonly String[] _responses = 
        {
            "Of course, I will now play",
            "Sure, I will now play",
            "Got it! Playing",
            "You don't have to ask again! Playing",
            "Sure thing, We will now listen to",
            "Okay, Listening",           
        };

        static readonly String[] _noEntityResponses =
        {
            "Of course, what song do you want me to play?",
            "What song do you want?",
            "Sure, what do you want me to play?",
            "Alright, what do you want me to play for you?",
            "You don't have to ask again! What song do you want?"
        };

        public static String SetResponse(Utterance u) {

            if (u.Entity["entity"].Type != JTokenType.Null)
                return $"{_responses[_random.Next(_responses.Length)]} '{u.Entity["entity"]}'.";
            else
                return _noEntityResponses[_random.Next(_noEntityResponses.Length)];
        }
    }
}
