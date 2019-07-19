namespace GitPostReceiveEmailer
{
    public class CommitNote
    {
        public CommitNote(string message, string @namespace)
        {
            this.Message = message;
            this.Namespace = @namespace;
        }

        public string Message { get; set; }
        public string Namespace { get; set; }
    }
}
