using LibGit2Sharp;

namespace GitPostReceiveEmailer
{
    public interface ICommitFactory
    {
        Commit Create(Repository repository, string commitSha);
    }
}
