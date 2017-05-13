using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softodon
{
    class Program
    {
        static void Main(string[] args)
        {
            var softodon = new Softodon("friends.nico");
            softodon.CreateAppAndAuth();
            softodon.SpeechLocalTimeline();
            Console.WriteLine("Press any key to stop");
            Console.ReadKey();
            softodon.Dispose();
        }
    }
}
