﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DwarfCorp
{
    public class AutoScaleThread
    {
        private GamePerformance.ThreadIdentifier Identifier;
        private Thread Thread;
        private Action<float> Worker;
        private ChunkManager Manager;

        public float TimeBudget = 0.1f; // Allow thread to run for 1 tenth of every second.
        public float Frequency = 0.2f;
        public float FrequencyStep = 0.1f;
        public float MaxFrequency = 2.0f;
        public float MinFrequency = 0.1f;

        private DateTime LastRan = DateTime.Now;

        private class Run
        {
            public DateTime StartTime;
            public TimeSpan Duration;
        }

        private LinkedList<Run> RecentRuns = new LinkedList<Run>();

        public AutoScaleThread(ChunkManager Manager, GamePerformance.ThreadIdentifier Identifier, Action<float> Worker)
        {
            this.Manager = Manager;
            this.Identifier = Identifier;
            this.Worker = Worker;

            Thread = new Thread(MainLoop);
            Thread.Name = Identifier.ToString();
        }

        public void Start()
        {
            Thread.Start();
        }

        public void Join()
        {
            Thread.Join();
        }

        private void MainLoop()
        {
            GamePerformance.Instance.RegisterThreadLoopTracker(Identifier.ToString(), Identifier);

#if CREATE_CRASH_LOGS
            try
#endif
            {
                while (!DwarfGame.ExitGame && !Manager.ExitThreads)
                {
                    if (Program.ShutdownEvent.WaitOne(500))
                        break;

                    
                    if (!Manager.PauseThreads)
                    {
                        var timeNow = DateTime.Now;
                        var delta = timeNow - LastRan;

                        if (delta.TotalSeconds >= Frequency)
                        {
                            LastRan = timeNow;

                            GamePerformance.Instance.PreThreadLoop(Identifier);
                            GamePerformance.Instance.EnterZone(Identifier.ToString());

                            Worker(1.0f);

                            var endTime = DateTime.Now;

                            RecentRuns.AddLast(new Run
                            {
                                StartTime = timeNow,
                                Duration = endTime - timeNow
                            });

                            while (RecentRuns.Count > 0 && (timeNow - RecentRuns.First.Value.StartTime).TotalSeconds > 1.0f)
                                RecentRuns.RemoveFirst();

                            var totalTime = RecentRuns.Sum(r => r.Duration.TotalSeconds);

                            if (totalTime > TimeBudget)
                            {
                                Frequency = Math.Min(Frequency * (1.0f +  FrequencyStep), MaxFrequency);
                            }
                            else if (totalTime < TimeBudget)
                            {
                                Frequency = Math.Max(Frequency * (1.0f - FrequencyStep), MinFrequency);
                            }

                            GamePerformance.Instance.TrackValueType("Water Update Frequency", Frequency);
                            GamePerformance.Instance.PostThreadLoop(Identifier);
                            GamePerformance.Instance.ExitZone(Identifier.ToString());
                        }
                        else
                        {
                            var sleepTime = (Frequency - delta.TotalSeconds) * 1000;
                            Thread.Sleep((int)sleepTime);
                        }
                    }

                }
            }
#if CREATE_CRASH_LOGS
            catch (Exception exception)
            {
                ProgramData.WriteExceptionLog(exception);
            }
#endif
        }
    }
}
