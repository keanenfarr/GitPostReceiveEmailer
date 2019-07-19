using LibGit2Sharp;

namespace GitPostReceiveEmailer
{
    public class CommitChange
    {
        public string ChangeId { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public ChangeKind Status { get; set; }
        public int LinesAdded { get; set; }
        public int LinesDeleted { get; set; }
        public int LinesChanged { get { return LinesAdded + LinesDeleted; } }
        public string Patch { get; set; }
    }
}
