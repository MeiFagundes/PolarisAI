using System;

namespace PolarisAICore.Response {
    class TellJoke {
        static readonly Random _random = new Random();

        static readonly String[] _noEntityResponses =
        {
            "I told my wife she was drawing her eyebrows too high. She looked surprised!",
            "And the Lord said unto John, 'Come forth and you will receive eternal life.' But John came fifth, and won a toaster!",
            "I threw a boomerand a few years ago. I now live in constant fear!",
            "You don't need a parachute to go skydiving. You need a parachute to go skydiving twice!",
            "Parallel lines have so much in common. It's a shame they'll never meet!",
            "People only call me ugly until they find out how much money I make. Then they call me ugly and poor.",
            "You're not completely useless. you can always serve as a bad example!",
            "I broke my finger last week. On the other hand, I'm okay!",
            "Apparently, someone in London gets stabbed every 52 seconds. Poor bastard.",
            "Someone stole my Microsoft Office an they're gonna pay! You have my Word!",
            "I tried to catch fog yesterday. Mist!",
            "Working in a mirror factory is something I can totally see myself doing!",
            "'Just say NO to drugs!' Well, if I'm talking to my drugs... I probably already said yes."
        };

        public static String SetResponse(Utterance u) {
            
            return _noEntityResponses[_random.Next(_noEntityResponses.Length)];
        }
    }
}
