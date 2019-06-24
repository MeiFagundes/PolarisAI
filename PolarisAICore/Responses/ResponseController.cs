namespace PolarisAICore.Responses {
    public static class ResponseController {

        public static void GenerateResponse(Dialog d) {

            if (d.IsRequest)
                Responses.Request.SetResponse(d);
            else if (d.IsQuestion)
                Responses.Question.SetResponse(d);
        }
    }
}
