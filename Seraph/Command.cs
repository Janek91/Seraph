using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Seraph
{
    public class Command
    {
        public static string ExecuteCmd(string arguments)
        {
            string output;
            // Start the child process.
            using (Process p = new Process())
            {
                // Redirect the output stream of the child process.
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.RedirectStandardOutput = true;
                //p.StartInfo.RedirectStandardError = true;
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/c " + arguments;
                p.Start();
                // Do not wait for the child process to exit before
                // reading to the end of its redirected stream.
                // p.WaitForExit();
                // Read the output stream first and then wait.
                output = p.StandardOutput.ReadToEnd();
                p.WaitForExit();
            }
            return output;
        }

        public static string Consume(ref string sLine, char separator)
        {
            char[] separators = { separator };
            string[] slice = sLine.Split(separators, 2);
            sLine = slice[1].Trim();
            return slice[0].Trim();
        }

        public static IEnumerable<Handler> Handle(string sFile)
        {
            string output = ExecuteCmd("handle " + sFile);
            using (StringReader reader = new StringReader(output))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    line = line.Trim();

                    // Skip header
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    if (line.EndsWith("Handle viewer")) // Nthandle v4.1 - Handle viewer
                    {
                        continue;
                    }

                    if (line.EndsWith("Mark Russinovich")) // Copyright (C) 1997-2016 Mark Russinovich
                    {
                        continue;
                    }

                    if (line.EndsWith("www.sysinternals.com")) // Sysinternals - www.sysinternals.com
                    {
                        continue;
                    }

                    if (line.StartsWith("No matching handles found.")) // Sysinternals - www.sysinternals.com
                    {
                        break;
                    }

                    Handler handler = new Handler();

                    // Exemple of an output:
                    // explorer.exe       pid: 6088   type: File           6D4: C:\Seraph
                    handler.Process = Consume(ref line, ' ');
                    Consume(ref line, ':'); // pid:
                    handler.Pid = Consume(ref line, ' ');
                    Consume(ref line, ':'); // type:
                    handler.Type = Consume(ref line, ' ');
                    handler.Handle = Consume(ref line, ':');
                    handler.Path = line;

                    yield return handler;
                }
            }
        }

        public static void Close(Handler handler)
        {
            string output = ExecuteCmd($"handle -c {handler.Handle} -y -p {handler.Pid}");
        }
    }
}
