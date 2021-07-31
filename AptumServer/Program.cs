﻿using System;
using System.Threading;
using System.Diagnostics;

namespace AptumServer
{
    class Program
    {
        public const double TPS = 20;
        public const long TimePerTick = (long)(10000000 / TPS);
        //public const long TimePerTick = 500000; // 20 TPS

        static void Main(string[] args)
        {
            //Console.WriteLine("[Core] Time per tick: " + TimePerTick);
            Console.WriteLine("[Core] Hello World!");
            Console.WriteLine("[Core] Aptum Server v0.1");
            Console.WriteLine("[Core] Starting server...");

            AptumServer aptumServer = new AptumServer();

            var lastTick = new Stopwatch();
            lastTick.Start();
            long timerTicks = 0;
            long nextTickId = 0;

            Console.WriteLine("[Core] Started polling for incoming data");

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
                    //Console.WriteLine("[Core] Tick, Id: " + nextTickId);
                    nextTickId++;
                }
                Thread.Sleep(1);
            }
        }
    }
}
