using System;
using System.Collections.Generic;
using LibGit2Sharp;

namespace GitPostReceiveEmailer
{
    public class Commit
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public string TreeID { get; set; }
        public string[] Parents { get; set; }
        public string Author { get; set; }
        public string AuthorEmail { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public string MessageShort { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<CommitChange> Changes { get; set; }
        public IEnumerable<CommitNote> Notes { get; set; }
        public IEnumerable<string> Links { get; set; }
        public IEnumerable<Branch> Branches { get; set; }
    }
}
