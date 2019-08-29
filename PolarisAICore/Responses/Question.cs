namespace PolarisAICore.Responses {
    public static class Question {

        public static void SetResponse(Utterance d) {

            if (d.Contains("can") && d.Contains("you") && d.GetPositionDifference("can", "you") == -1) {

                d.Response = "Well, maybe I can. I don't know it yet...";
                return;
            }

            d.Response = "Sorry, I don't know how to interpret generic questions yet.";
        }
    }
}
