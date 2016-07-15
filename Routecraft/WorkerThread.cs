using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Routecraft
{
    public class WorkerThread
    {
        private Thread Thread;
        private bool ShouldStop = false;
        private ManualResetEvent Event = new ManualResetEvent(false);

        private Queue<Action> JobQueue = new Queue<Action>();

        public WorkerThread()
        {
            this.Thread = new Thread(this.WorkerThreadStart);
        }

        public void Abort()
        {
            this.Thread.Abort();
        }

        public void AddJob(Action job)
        {
            lock (this.JobQueue)
            {
                this.JobQueue.Enqueue(job);
                this.Event.Set();
            }
        }

        public void Start()
        {
            if (this.Thread.IsAlive)
            {
                return;
            }
            this.ShouldStop = false;
            this.Thread.Start();
        }

        public void Stop()
        {
            if (!this.Thread.IsAlive)
            {
                return;
            }
            this.ShouldStop = true;
            this.AddJob(delegate() { });

            this.Thread.Join();
        }

        public bool Stop(int timeout)
        {
            if (!this.Thread.IsAlive)
            {
                return true;
            }
            this.ShouldStop = true;
            this.AddJob(delegate() { });

            return this.Thread.Join(timeout);
        }

        private void WorkerThreadStart()
        {
            while (!this.ShouldStop)
            {
                this.Event.WaitOne();
                while (this.JobQueue.Count != 0)
                {
                    Action job;
                    lock (this.JobQueue)
                    {
                        job = this.JobQueue.Dequeue();
                        if (this.JobQueue.Count == 0)
                        {
                            this.Event.Reset();
                        }
                    }
                    job();
                }
            }
        }
    }
}
