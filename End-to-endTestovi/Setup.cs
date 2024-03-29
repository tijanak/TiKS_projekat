﻿
using Microsoft.Playwright;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;

namespace End_to_endTestovi
{
    [SetUpFixture]
    public class Setup
    {

        private IAPIRequestContext Request;
        //Process front=new Process(), back=new Process();
        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            if(Directory.Exists(Globals.vidDir))
            {
                Directory.Delete(Globals.vidDir,true);
            }
            if (Directory.Exists(Globals.scDir))
            {
                Directory.Delete(Globals.scDir,true);
            }
            var headers = new Dictionary<string, string>
        {
            { "Accept", "application/json" }
        };
            using var playwright = await Playwright.CreateAsync();
            Request = await playwright.APIRequest.NewContextAsync(new()
            {
                BaseURL = "http://localhost:5100",
                ExtraHTTPHeaders = headers,
                IgnoreHTTPSErrors = true
            });
            Dictionary<string, string> headers2 = new()
        {
            { "Content-Type", "application/json" }
        };
            await using var response = await Request.DeleteAsync("Korisnik/uklonikorisnika/username/admin");

            await using var response2 = await Request.PostAsync("Korisnik/dodajkorisnika", new APIRequestContextOptions() {Headers=headers2,
                DataObject = new
                {
                    Id=0,
                    Username="admin",
                    Password="admin",
                }
            });
            var k = await response2.JsonAsync();
                if(k.HasValue)
            {
                var j = k.GetValueOrDefault();
                
                   var id= j.GetProperty("id").GetInt32();
                Globals.adminId = id;
                
            }

            await using var response3 = await Request.PostAsync("Kategorija/Post", new APIRequestContextOptions()
            {
                Headers = headers2,
                DataObject = new
                {
                    Tip="test",
                    Prioritet=10000
                }
            });
            k = await response3.JsonAsync();
            if (k.HasValue)
            {
                var j = k.GetValueOrDefault();
                int id;
                try
                {
                    if (j.TryGetInt32(out id))
                        Globals.kategorijaId.Add(id);
                }
                catch (Exception e) { }
            }
            await using var response4 = await Request.PostAsync("Kategorija/Post", new APIRequestContextOptions()
            {
                Headers = headers2,
                DataObject = new
                {
                    Tip = "za lecenje",
                    Prioritet = 900
                }
            });
            k = await response4.JsonAsync();
            if (k.HasValue)
            {
                var j = k.GetValueOrDefault();
                int id;
                try
                {
                    if (j.TryGetInt32(out id))
                        Globals.kategorijaId.Add(id);
                }
                catch (Exception e) { }


            }
            await using var response5 = await Request.PostAsync("Kategorija/Post", new APIRequestContextOptions()
            {
                Headers = headers2,
                DataObject = new
                {
                    Tip = "za udomljavanje",
                    Prioritet = 800
                }
            });
            k = await response5.JsonAsync();
            if (k.HasValue)
            {
                var j = k.GetValueOrDefault();

                int id;
                try
                {
                    if (j.TryGetInt32(out id))
                        Globals.kategorijaId.Add(id);
                }
                catch (Exception e) { }

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
        public async Task RunAfterAnyTests()
        {
            var headers = new Dictionary<string, string>
        {
            { "Accept", "application/json" }
        };
            using var playwright = await Playwright.CreateAsync();
            Request = await playwright.APIRequest.NewContextAsync(new()
            {
                BaseURL = "http://localhost:5100",
                ExtraHTTPHeaders = headers,
                IgnoreHTTPSErrors = true
            });
            foreach (int id in Globals.kategorijaId)
            {
                try
                {
                    await using var response2 = await Request.DeleteAsync("Kategorija/Delete/" + id);
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                    
                }
            }

            await using var response = await Request.DeleteAsync("Korisnik/uklonikorisnika/username/admin");

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
