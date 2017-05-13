﻿using System;
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
            Subscriber = client.GetObservableCustomTimeline("public/local")
                         .OfType<Status>()
                         .Subscribe(x => Console.WriteLine($"{x.Account.FullUserName} tooted \"{x.Content}\""));
        }

        public void Dispose()
        {
            Subscriber.Dispose();
        }
    }
}
