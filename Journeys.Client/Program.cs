using System;

namespace Journeys.Client
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
