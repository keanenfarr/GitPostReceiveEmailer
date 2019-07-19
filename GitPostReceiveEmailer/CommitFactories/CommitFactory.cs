using System.Linq;
using System.Collections.Generic;
using LibGit2Sharp;

namespace GitPostReceiveEmailer
{
    public class CommitFactory : ICommitFactory
    {
        public Commit Create(Repository repository, string commitSha)
        {
            //Most of this code comes from https://github.com/jakubgarfield/Bonobo-Git-Server

            Commit model = null;

            var commit = repository.Commits.SingleOrDefault(c => c.Sha == commitSha);

            if (commit != null)
            {
                string tagsString = string.Empty;
                var tags = repository.Tags.Where(o => o.Target.Sha == commit.Sha).Select(o => o.FriendlyName).ToList();

                model = new Commit
                {
                    Author = commit.Author.Name,
                    AuthorEmail = commit.Author.Email,
                    Date = commit.Author.When.LocalDateTime,
                    ID = commit.Sha,
                    Message = commit.Message,
                    TreeID = commit.Tree.Sha,
                    Parents = commit.Parents.Select(i => i.Sha).ToArray(),
                    Tags = tags,
                    Branches = ListBranchesContainingCommit(repository, commitSha),
                    Notes = (from n in commit.Notes select new CommitNote(n.Message, n.Namespace)).ToList()
                };

                TreeChanges changes = !commit.Parents.Any() ? repository.Diff.Compare<TreeChanges>(null, commit.Tree) : repository.Diff.Compare<TreeChanges>(commit.Parents.First().Tree, commit.Tree);
                Patch patches = !commit.Parents.Any() ? repository.Diff.Compare<Patch>(null, commit.Tree) : repository.Diff.Compare<Patch>(commit.Parents.First().Tree, commit.Tree);

                model.Changes = changes.OrderBy(s => s.Path).Select(i =>
                {
                    var patch = patches[i.Path];
                    return new CommitChange
                    {
                        ChangeId = i.Oid.Sha,
                        Path = i.Path.Replace('\\', '/'),
                        Status = i.Status,
                        LinesAdded = patch.LinesAdded,
                        LinesDeleted = patch.LinesDeleted,
                        Patch = patch.Patch
                    };
                });
            }

            return model;
        }

        //From: https://stackoverflow.com/questions/42156799/how-to-get-list-of-remote-branches-that-contain-a-commit-in-libgit2sharp
        private IEnumerable<Branch> ListBranchesContainingCommit(Repository repo, string commitSha)
        {
            var commit = repo.Lookup<LibGit2Sharp.Commit>(commitSha);

            IEnumerable<Reference> headsContainingTheCommit = repo.Refs.ReachableFrom(repo.Refs, new[] { commit });
            return headsContainingTheCommit.Select(branchRef => repo.Branches[branchRef.CanonicalName]).ToList();
        }
    }
}
