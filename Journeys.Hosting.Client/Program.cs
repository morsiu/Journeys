using System;

namespace Journeys.Hosting.Client
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Bootstrap();
        }
    }
}
