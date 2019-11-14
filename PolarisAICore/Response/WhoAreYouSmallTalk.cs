using System;
using System.Collections.Generic;
using System.Text;

namespace PolarisAICore.Response {
    class WhoAreYouSmallTalk {
        static readonly Random _random = new Random();

        static readonly String[] _noEntityResponses =
        {
            "I'm Polaris, the star that marks the north celestial pole.",
            "I'm the brightest star in the constelation of Ursa Minor, Polaris.",
            "Actually, I'm a Star. I work as a Personal Assistant just in my free hours.",
            "I'm Polaris, yours truly Personal Assistant.",
            "I'm Polaris, but you may also know me as the North Star or Pole Star.",
            "I'm Polaris, the North Star, and also a Personal Assistant.",
            "My name is Polaris, I'm the brightest Personal Assistant and star."
        };

        public static String SetResponse(Utterance u) {
            
            return _noEntityResponses[_random.Next(_noEntityResponses.Length)];
        }
    }
}
