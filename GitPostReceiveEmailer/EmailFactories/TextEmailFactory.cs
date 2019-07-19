using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitPostReceiveEmailer
{
    public class TextEmailGenerator : IEmailGenerator
    {
        /// <summary>
        /// Creates a formatted string that is suitable to send in an email with information from the model.
        /// </summary>
        /// <param name="model">A Commit model.</param>
        /// <returns></returns>
        public string CreateEmailText(Commit model)
        {
            var sb = new StringBuilder();

            sb.AppendLine(model.Message);
            sb.AppendLine();
            sb.AppendLine(model.Author + " " + model.AuthorEmail);
            sb.AppendLine(model.Date.ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz"));
            sb.AppendLine();
            sb.AppendLine();

            sb.AppendLine("Summary --------------------------------------");
            sb.AppendLine();

            foreach (var item in model.Changes)
            {
                sb.Append(item.Status.ToString());
                sb.Append(" ");
                sb.Append(item.Path);
                sb.Append(" ");
                sb.Append(item.LinesChanged);
                sb.Append(" (+");
                sb.Append(item.LinesAdded);
                sb.Append(" -");
                sb.Append(item.LinesDeleted);
                sb.AppendLine(")");
            }

            sb.AppendLine();
            sb.AppendLine("Details --------------------------------------------");
            sb.AppendLine();

            foreach (var item in model.Changes.Where(i => i.Status == LibGit2Sharp.ChangeKind.Modified || i.Status == LibGit2Sharp.ChangeKind.Added))
            {
                sb.AppendLine(item.Path);
                sb.AppendLine();

                var patchArray = item.Patch.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in patchArray)
                {
                    sb.AppendLine(line);
                }

                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}
