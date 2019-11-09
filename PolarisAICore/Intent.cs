using System;
using System.Collections.Generic;
using System.Text;

namespace PolarisAICore {
    public class Intent {

        public String Name { get; set; }
        public float Score { get; set; }

        public Intent() { }
        public Intent(string name) {

            Name = name;
        }
        public Intent(string name, float score) {

            Name = name;
            Score = score;
        }
    }
}
