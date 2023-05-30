using OpenAI_API;

namespace isaaholic.ReadyWittedLibrary
{
    public static class ReadyWitted
    {
        // instance of OpenAIAPI
        private static OpenAIAPI? _openAI;

        /// <summary>
        /// Create instance for working on OpenAI API.
        /// First must run that function.
        /// </summary>
        /// <param name="apiKey"></param>
        public static void CreateInstance(string apiKey)
        {
            var authentication = new APIAuthentication(apiKey);
            _openAI = new OpenAIAPI(authentication);
        }

        /// <summary>
        /// Send Request to OpenAI and return response or Authentication Excception Message
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static async Task<string> SendRequest(string request)
        {
            if (_openAI is null)
                throw new Exception("ReadyWitted OpenAI Instance doesn't exists  example:ReadyWitted.CreateInstance(key)");

            var conversation = _openAI.Chat.CreateConversation();

            // Append system message with instructions for the chat
            conversation.AppendSystemMessage("answer it please");

            // Append user input and get response from ChatGP
            conversation.AppendUserInput(request);

            try
            {
                return await conversation.GetResponseFromChatbot();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Ask Simple Question
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        public static async Task<string> AskSimpleQuestion(string question)
            => await SendRequest(question);

        /// <summary>
        /// Get Schema of Presentation without contents
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public static async Task<string> GetPresentationSchema(string theme, int pageNumber = 10)
        {
            var request = $"Create a {pageNumber} page presentation about {theme}";
            return await SendRequest(request);
        }

        /// <summary>
        /// Get Presentation with content of pages. Return time is estimated (30 seconds * number of page)
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        public static async Task<List<string>> GetPresentation(string theme, int pageNumber = 10)
        {
            var request = $"let's give me a {pageNumber} page presentation schema about {theme} only name of page";
            var schema = await SendRequest(request);
            var result = new List<string>
            {
                schema
            };

            List<string> stringList = schema.Split('\n').ToList();
            result.Add("\n");
            foreach (var line in stringList)
            {
                var isDone = true;
                foreach (var c in line)
                {
                    if (char.IsDigit(c))
                    {
                        isDone = false;
                        break;
                    }
                }
                if (isDone)
                    continue;
                Thread.Sleep(15000);
                result.Add($"\n{line}\n");
                result.Add(await SendRequest($"what is {line}?"));
            }
            return result;
        }
    }
}
