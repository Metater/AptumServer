using System;
using System.Threading;
using System.Diagnostics;

namespace AptumServer
{
    class Program
    {
        public const long TimePerTick = 500000; // 20 TPS

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            AptumServer aptumServer = new AptumServer();

            var lastTick = new Stopwatch();
            lastTick.Start();
            long timerTicks = 0;
            long nextTickId = 0;

            while (!Console.KeyAvailable)
            {
                aptumServer.server.PollEvents();
                lastTick.Stop();
                timerTicks += lastTick.ElapsedTicks;
                lastTick.Restart();
                if (timerTicks >= TimePerTick)
                {
                    timerTicks -= TimePerTick;
                    aptumServer.Tick(nextTickId);
                    nextTickId++;
                }
                Thread.Sleep(1);
            }
        }
    }
}
