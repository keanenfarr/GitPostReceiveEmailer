using System;
using System.Linq;
using System.Text;

namespace GitPostReceiveEmailer
{
    public class HtmlEmailGenerator : IEmailGenerator
    {
        public string CreateEmailText(Commit model)
        {
            var sb = new StringBuilder();

            sb.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">");
            sb.Append("<html xmlns=\"http://www.w3.org/1999/xhtml\">");
            sb.Append("<head>");
            sb.Append("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=UTF-8\" />");
            sb.Append("<title>" + HtmlEncode(model.ID) + "</title>");
            sb.Append("<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\"/>");
            sb.Append("<style type=\"text/css\">");
            sb.Append("#outlook a{padding:0;}.ReadMsgBody{width:100%;} .ExternalClass{width:100%;}.ExternalClass, .ExternalClass p, .ExternalClass span, .ExternalClass font, .ExternalClass td, .ExternalClass div {line-height: 100%;}body, table, td, p, a, li, blockquote{-webkit-text-size-adjust:100%; -ms-text-size-adjust:100%;}table, td{mso-table-lspace:0pt; mso-table-rspace:0pt;}img{-ms-interpolation-mode:bicubic;}body{margin:0; padding:0;}img{border:0; height:auto; line-height:100%; outline:none; text-decoration:none;}table{border-collapse:collapse !important;}body, #bodyTable, #bodyCell{height:100% !important; margin:0; padding:0; width:100% !important;}#bodyCell{padding:20px;}body, #bodyTable{background-color:#fff;}#bodyCell{}body,html,td,table{font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji,Segoe UI Symbol;}h1{color:#202020 !important;display:block;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji,Segoe UI Symbol;font-size:26px;font-style:normal;font-weight:bold;line-height:100%;letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;text-align:left;}h2{color:#404040 !important;display:block;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji,Segoe UI Symbol;font-size:20px;font-style:normal;font-weight:bold;line-height:100%;letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;text-align:left;}h3{color:#606060 !important;display:block;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji,Segoe UI Symbol;font-size:16px;font-style:italic;font-weight:normal;line-height:100%;letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;text-align:left;}h4{color:#808080 !important;display:block;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji,Segoe UI Symbol;font-size:14px;font-style:italic;font-weight:normal;line-height:100%;letter-spacing:normal;margin-top:0;margin-right:0;margin-bottom:10px;margin-left:0;text-align:left;}#templatePreheader{background-color:#fff;}.preheaderContent{color:#808080;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji,Segoe UI Symbol;font-size:10px;line-height:125%;text-align:left;}.preheaderContent a:link, .preheaderContent a:visited, .preheaderContent a .yshortcuts {color:#606060;font-weight:normal;text-decoration:underline;}#templateHeader{background-color:#fff;}.headerContent{color:#505050;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji,Segoe UI Symbol;font-size:20px;font-weight:bold;line-height:100%;padding-top:0;padding-right:0;padding-bottom:0;padding-left:0;text-align:left;vertical-align:middle;}.headerContent a:link, .headerContent a:visited, .headerContent a .yshortcuts {color:#EB4102;font-weight:normal;text-decoration:underline;}#headerImage{height:auto;max-width:600px;}.bodyContent{color:#505050;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji,Segoe UI Symbol;font-size:14px;line-height:150%;padding-top:20px;padding-right:20px;padding-bottom:20px;padding-left:20px;text-align:left;}.bodyContent a:link, .bodyContent a:visited, .bodyContent a .yshortcuts {color:#EB4102;font-weight:normal;text-decoration:underline;}.bodyContent img{display:inline;height:auto;max-width:560px;}#templateFooter{background-color:#fff;}.footerContent{color:#808080;font-family:-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica,Arial,sans-serif,Apple Color Emoji,Segoe UI Emoji,Segoe UI Symbol;font-size:10px;line-height:150%;padding-top:20px;padding-right:20px;padding-bottom:20px;padding-left:20px;text-align:left;}.footerContent a:link, .footerContent a:visited, .footerContent a .yshortcuts, .footerContent a span {color:#606060;font-weight:normal;text-decoration:underline;}@media only screen and (max-width: 480px){body, table, td, p, a, li, blockquote{-webkit-text-size-adjust:none !important;}body{width:100% !important; min-width:100% !important;}#bodyCell{padding:10px !important;}#templateContainer{max-width:600px !important;width:100% !important;}h1{font-size:24px !important;line-height:100% !important;}h2{font-size:20px !important;line-height:100% !important;}h3{font-size:18px !important;line-height:100% !important;}h4{font-size:16px !important;line-height:100% !important;}#templatePreheader{display:none !important;}#headerImage{height:auto !important;max-width:600px !important;width:100% !important;}.headerContent{font-size:20px !important;line-height:125% !important;}.bodyContent{font-size:18px !important;line-height:125% !important;}.footerContent{font-size:14px !important;line-height:115% !important;}.footerContent a{display:block !important;}}");
            sb.Append("</style>");
            sb.Append("</head>");
            sb.Append("<body leftmargin=\"0\" marginwidth=\"0\" topmargin=\"0\" marginheight=\"0\" offset=\"0\">");
            sb.Append("<table align=\"left\" border=\"0\" cellpadding=\"0\" cellspacing=\"0\" height=\"100%\" width=\"100%\" id=\"bodyTable\">");
            sb.Append("<tr>");
            sb.Append("<td align=\"left\" valign=\"top\" id=\"bodyCell\">");

            sb.Append("<div style=\"font-size: 16px; color: #31383f; padding-top: 10px; padding-bottom: 10px; line-height: 15px;\">");
            sb.Append(HtmlEncode(model.Message));
            sb.Append("</div>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td height=\"15\">");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("<div style=\"font-size: 14px; font-weight: narrow; color: #979797;\">");
            sb.Append(HtmlEncode(model.Author) + " (" + HtmlEncode(model.AuthorEmail) + ")<br />");
            sb.Append(model.Date.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz"));
            sb.Append("</div>");

            sb.Append("<br /><br />");
            sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"background-color: #fafbfc;\">");
            sb.Append("<tr>");
            sb.Append("<td height=\"5\">");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td valign=\"top\">");
            sb.Append("&nbsp;&nbsp;<b>Summary</b>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td height=\"5\">");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

            foreach (var item in model.Changes)
            {
                sb.Append("<tr>");
                sb.Append("<td height=\"5\">");
                sb.Append("</td>");
                sb.Append("<tr>");
                sb.Append("<td valign=\"top\" style=\"font-size: 14px; color: #31383f;\">&nbsp;&nbsp;");
                sb.Append(item.Status.ToString());
                sb.Append(" ");
                sb.Append(HtmlEncode(item.Path));
                sb.Append(" ");
                sb.Append("<span style=\"color: #979797;\">");
                sb.Append(item.LinesChanged);
                sb.Append(" (+");
                sb.Append(item.LinesAdded);
                sb.Append(" -");
                sb.Append(item.LinesDeleted);
                sb.Append(")</span>");
                sb.Append("</td>");
                sb.Append("</tr>");
            }

            sb.Append("</table>");

            sb.Append("<br /><br />");
            sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"background-color: #fafbfc;\">");
            sb.Append("<tr>");
            sb.Append("<td height=\"5\">");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td valign=\"top\">");
            sb.Append("&nbsp;&nbsp;<b>Branch</b>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td height=\"5\">");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\">");

            foreach (var item in model.Branches)
            {
                sb.Append("<tr>");
                sb.Append("<td height=\"5\">");
                sb.Append("</td>");
                sb.Append("<tr>");
                sb.Append("<td valign=\"top\" style=\"font-size: 14px; color: #31383f;\">&nbsp;&nbsp;");
                sb.Append(HtmlEncode(item.FriendlyName));
                sb.Append("</td>");
                sb.Append("</tr>");
            }

            sb.Append("</table>");

            sb.Append("<br /><br />");
            sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\" style=\"background-color: #fafbfc;\">");
            sb.Append("<tr>");
            sb.Append("<td height=\"5\">");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td valign=\"top\">");
            sb.Append("&nbsp;&nbsp;<b>Details</b>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("<tr>");
            sb.Append("<td height=\"5\">");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("<table border=\"0\" cellpadding=\"0\" cellspacing=\"0\" width=\"100%\">");

            foreach (var item in model.Changes.Where(i => i.Status == LibGit2Sharp.ChangeKind.Modified || i.Status == LibGit2Sharp.ChangeKind.Added))
            {
                sb.Append("<tr>");
                sb.Append("<td height=\"15\">");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td height=\"5\" style=\"border-top: solid 1px #efefef;\">");
                sb.Append("</td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td valign=\"top\" style=\"font-size: 14px; color: #31383f;\">&nbsp;&nbsp;<b>");
                sb.Append(HtmlEncode(item.Path));
                sb.Append("</b></td>");
                sb.Append("</tr>");
                sb.Append("<tr>");
                sb.Append("<td height=\"15\">");
                sb.Append("</td>");
                sb.Append("</tr>");

                var patchArray = item.Patch.Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in patchArray)
                {
                    var val = line.Trim();

                    sb.Append("<tr>");
                    if (line.StartsWith("+"))
                    {
                        sb.Append("<td valign=\"top\" style=\"font-size: 12px; font-family: Courier, monospace; color: #31383f; background-color: #d9fed2;\">&nbsp;&nbsp;");
                        sb.Append(HtmlEncode(line));
                        sb.Append("</td>");
                    }
                    else if (line.StartsWith("-"))
                    {
                        sb.Append("<td valign=\"top\" style=\"font-size: 12px; font-family: Courier, monospace; color: #31383f; background-color: #fed2d2;\">&nbsp;&nbsp;");
                        sb.Append(HtmlEncode(line));
                        sb.Append("</td>");
                    }
                    else if (line.StartsWith("@@"))
                    {
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td height=\"15\">");
                        sb.Append("</td>");
                        sb.Append("</tr>");
                        sb.Append("<tr>");
                        sb.Append("<td valign=\"top\" style=\"font-size: 12px; font-family: Courier, monospace; color: blue;\">&nbsp;&nbsp;");
                        sb.Append(HtmlEncode(line));
                        sb.Append("</td>");
                    }
                    else if (line.StartsWith("diff --git"))
                    {
                        sb.Append("<td valign=\"top\" style=\"font-size: 12px; font-family: Courier, monospace; color: #909090;\">&nbsp;&nbsp;");
                        sb.Append(HtmlEncode(line));
                        sb.Append("</td>");
                    }
                    else
                    {
                        sb.Append("<td valign=\"top\" style=\"font-size: 12px; font-family: Courier, monospace; color: #31383f;\">&nbsp;&nbsp;");
                        sb.Append("&nbsp;" + HtmlEncode(line));
                        sb.Append("</td>");
                    }
                    sb.Append("</tr>");
                }
            }

            sb.Append("</table>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            sb.Append("</body>");
            sb.Append("</html>");

            return sb.ToString();
        }

        string HtmlEncode(string s)
        {
            return s.Replace("<", "&lt;").Replace(">", "&gt;").Replace("\r\n", "<br />").Replace("\n", "<br />");
        }
    }
}
