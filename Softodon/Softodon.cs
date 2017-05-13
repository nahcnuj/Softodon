using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reactive.Linq;
using Mastodot;
using Mastodot.Utils;
using Mastodot.Enums;
using Mastodot.Entities;

namespace Softodon
{
    class Softodon : IDisposable
    {
        private string Host;
        private string AppName;
        private TokenInfo Tokens;
        private IDisposable Subscriber;
        
        private const string SoftalkExePath = @"C:\softalk\softalk.exe";

        public Softodon(string host, string appName = "Softodon")
        {
            Host = host;
            AppName = appName;
        }
        
        public void CreateAppAndAuth()
        {
            var app = ApplicaionManager.RegistApp(Host, AppName, Scope.Read).Result;
            var url = ApplicaionManager.GetOAuthUrl(app);
            Console.WriteLine("Open " + url);
            System.Diagnostics.Process.Start(url);
            Console.WriteLine("Please accept the request to access your account on opened web browser.");
            Console.Write("And copy and paste the displayed code here: ");
            var code = Console.ReadLine();
            Tokens = ApplicaionManager.GetAccessTokenByCode(app, code).Result;
        }

        public void SpeechLocalTimeline()
        {
            var client = new MastodonClient(Host, Tokens.AccessToken);
            System.Diagnostics.Process.Start(SoftalkExePath, $"/X:1");
            Subscriber = client.GetObservableCustomTimeline("public/local")
                         .OfType<Status>()
                         .Subscribe(x => {
                             Console.WriteLine($"{x.Account.FullUserName} tooted \"{x.Content}\"");
                             System.Diagnostics.Process.Start(SoftalkExePath, $"/W:{x.Content}").WaitForExit();
                         });
        }

        public void Dispose()
        {
            System.Diagnostics.Process.Start(SoftalkExePath, "/close2_now");
            if (Subscriber != null)
            {
                Subscriber.Dispose();
            }
        }
    }
}
