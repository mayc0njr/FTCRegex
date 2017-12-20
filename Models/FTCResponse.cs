

namespace FTCRegex.Models
{

    /* Represents the response to the client of the API.
     * Informs the code of the message, and a String describing the response.
     * 0, if is just a feedback.
     * 1, if is a warning, the execution have finished, but something happens.
     * 2, if is an error, and the execution could not finish.
     * */
    public class FTCResponse
    {        
        public const int INFO = 0;
        public const int WARNING = 1;
        public const int ERROR = 2;
        public const string INVALID_REQUEST = "Invalid Request Format.";

        public int Code { get; set; } //0=Info, 1=Warning, 2=Error.
        public string Content { get; set; }
        public int Id { get; set; }
    }
}