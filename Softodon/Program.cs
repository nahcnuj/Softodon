using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softodon
{
    class Program
    {
        private static Softodon Softodon = new Softodon("friends.nico");

        static void Main(string[] args)
        {
            Console.CancelKeyPress += new ConsoleCancelEventHandler(cancelEventHandler);

            Softodon.CreateAppAndAuth();
            Softodon.SpeechLocalTimeline();
            Console.WriteLine("Press any key to stop");
            Console.ReadKey();
            Softodon.Dispose();
        }

        protected static void cancelEventHandler(object sender, ConsoleCancelEventArgs args)
        {
            Softodon.Dispose();
            Environment.Exit(0);
        }
    }
}
