
using Microsoft.Playwright;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;

namespace End_to_endTestovi
{
    [SetUpFixture]
    public class Setup
    {
        //Process front=new Process(), back=new Process();
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            if(Directory.Exists(Globals.vidDir))
            {
                Directory.Delete(Globals.vidDir,true);
            }
            if (Directory.Exists(Globals.scDir))
            {
                Directory.Delete(Globals.scDir,true);
            }
            /*string workingDirectory = Environment.CurrentDirectory;
            // or: Directory.GetCurrentDirectory() gives the same result

           
            // This will get the current PROJECT directory
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            front.StartInfo.UseShellExecute = true;
            front.StartInfo.FileName = "startup.bat";
            front.StartInfo.WorkingDirectory = projectDirectory;
            //front.StartInfo.CreateNoWindow = true;
            front.Start();

            back.StartInfo.UseShellExecute = true;
            back.StartInfo.FileName = "backstartup.bat";

            back.StartInfo.WorkingDirectory = projectDirectory;
            //back.StartInfo.CreateNoWindow = true;
            back.Start();
            bool started = false;
            while (!started)
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create("http://localhost:4000");
                request.AllowAutoRedirect = false; // find out if this site is up and don't follow a redirector
                request.Method = "HEAD";
                try
                {
                    WebResponse response = request.GetResponse();
                    started = true;
                    // do something with response.Headers to find out information about the request
                }
                catch (WebException wex)
                {
                    //set flag if there was a timeout or some other issues
                }
            }*/

        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            /*try
            {
                front.Kill();
                front.WaitForExit(); // possibly with a timeout
                back.Kill();
                back.WaitForExit();
            }
            catch (Win32Exception winException)
            {
                // process was terminating or can't be terminated - deal with it
            }
            catch (InvalidOperationException invalidException)
            {
                // process has already exited - might be able to let this one go
            }*/
        }
    }
}
