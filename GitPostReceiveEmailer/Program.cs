using System;
using System.Text;
using LibGit2Sharp;

namespace GitPostReceiveEmailer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("GitPostReceiveEmailer\n");

            Commit model = null;

            //Possibly load these dynamically in the future for configurable behavior
            IConfigurationFactory configFactory = new DefaultConfigurationFactory();
            ICommitFactory commitFactory = new CommitFactory();
            IEmailGenerator htmlEmailGenerator = new HtmlEmailGenerator();
            IEmailGenerator textEmailGenerator = new TextEmailGenerator();
            IEmailer emailer = new MailKitEmailer();

            var config = configFactory.Create();
            var html = string.Empty;
            var text = string.Empty;

            try
            {
                var commitSha = string.Empty;

                if (args.Length > 2)
                {
                    commitSha = args[1];
                }

                if (string.IsNullOrEmpty(commitSha))
                {
                    Console.Write("Usage: GitPostReceiveEmailer.exe oldrev newrev ref\n");
                    Console.Write("See post-receive hook file for example.\n");
                    Environment.Exit(1);
                }

                Console.Write("Generating email.\n");

                foreach (var repoPath in config.RepositoryPaths)
                {
                    if (System.IO.Directory.Exists(repoPath))
                    {
                        using (var repository = new Repository(repoPath))
                        {
                            model = commitFactory.Create(repository, commitSha);
                        }
                    }

                    if (model != null)
                    {
                        break;
                    }
                }

                if (model != null)
                {
                    html = htmlEmailGenerator.CreateEmailText(model);
                    text = textEmailGenerator.CreateEmailText(model);

                    if (string.IsNullOrEmpty(config.From.Address))
                    {
                        config.From.Address = model.AuthorEmail.Trim();
                        config.From.Name = model.Author.Trim();
                    }

                    if (!string.IsNullOrEmpty(config.Subject))
                    {
                        config.Subject = config.Subject
                            .Replace("{id}", model.ID)
                            .Replace("{authorname}", model.Author.Trim())
                            .Replace("{authoremail}", model.AuthorEmail.Trim())
                            .Replace("{date}", model.Date.ToString())
                            .Replace("{treeid}", model.TreeID)
                            .Replace("{name}", model.Name);
                    }
                    else
                    {
                        config.Subject = "Commit notification.";
                    }
                }
            }
            catch (Exception ex)
            {
                var sb = new StringBuilder();

                sb.AppendLine(ex.Message);
                sb.AppendLine();
                sb.AppendLine(ex.StackTrace);

                html = sb.ToString();
                text = sb.ToString();

                Console.WriteLine("Exception: " + ex.Message);
            }

            if (!string.IsNullOrEmpty(html) || !string.IsNullOrEmpty(text))
            {
                Console.Write("Sending email.\n");
                emailer.SendEmail(config, html, text);
                Console.Write("Done.\n");
            }
        }
    }
}
