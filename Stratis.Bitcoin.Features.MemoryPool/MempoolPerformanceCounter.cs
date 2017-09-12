﻿using System;
using System.Text;
using System.Threading;

namespace Stratis.Bitcoin.Features.MemoryPool
{
    /// <summary>
    /// Memory performance counter for tracking memory pool performance.
    /// </summary>
    public class MempoolPerformanceCounter
    {
        /// <summary>Number of transactions in the memory pool.</summary>
        private long mempoolSize;

        /// <summary>Memory pool dynamic size in bytes.</summary>
        private long mempoolDynamicSize;

        /// <summary>Memory pool orphan transaction count.</summary>
        private long mempoolOrphanSize;

        /// <summary>Counter of number of memory pool hits.</summary>
        private long hitCount;

        /// <summary>
        /// Constructs a memory pool performance counter.
        /// </summary>
        public MempoolPerformanceCounter()
        {
            this.Start = DateTime.UtcNow;
        }

        /// <summary>Gets the start time of the performance counter.</summary>
        public DateTime Start { get; }

        /// <summary>Gets the amount of elapsed time between the start and now.</summary>
        public TimeSpan Elapsed => DateTime.UtcNow - this.Start;

        /// <summary>Gets the number of transactions in the memory pool.</summary>
        public long MempoolSize => this.mempoolSize;

        /// <summary> Gets the memory pool dynamic size in bytes.</summary>
        public long MempoolDynamicSize => this.mempoolDynamicSize;

        /// <summary>
        /// Gets and sets the memory pool orphan transaction count.
        /// </summary>
        /// <remarks>TODO: Should this use be using the backing mempoolOrphanSize field?</remarks>
        public long MempoolOrphanSize { get; set; }

        /// <summary>Gets the count of memory pool hits.</summary>
        public long HitCount => this.hitCount;

        /// <summary>
        /// Sets the number of transactions in the memory pool.
        /// </summary>
        /// <param name="size">Count of number of transactions in the memory pool.</param>
        public void SetMempoolSize(long size)
        {
            Interlocked.Exchange(ref this.mempoolSize, size);
        }

        /// <summary>
        /// Sets the memory pool dynamic size in bytes.
        /// </summary>
        /// <param name="size">Dynamic size of the memory pool.</param>
        public void SetMempoolDynamicSize(long size)
        {
            Interlocked.Exchange(ref this.mempoolDynamicSize, size);
        }

        /// <summary>
        /// Sets the number of memory pool orphan transactions.
        /// </summary>
        /// <param name="size">Memory pool orphan transaction count.</param>
        public void SetMempoolOrphanSize(long size)
        {
            Interlocked.Exchange(ref this.mempoolOrphanSize, size);
        }
      
        /// <summary>
        /// Increments the memory pool hit count.
        /// </summary>
        /// <param name="count">Amount to increment hit count.</param>
        public void AddHitCount(long count)
        {
            Interlocked.Add(ref this.hitCount, count);
        }

        /// <summary>
        /// Gets string representation of memory pool counter.
        /// </summary>
        /// <returns>String representation.</returns>
        public override string ToString()
        {
            var benchLogs = new StringBuilder();
            benchLogs.AppendLine(
                "MempoolSize: " + this.MempoolSize.ToString().PadRight(4) + 
                " DynamicSize: " + ((this.MempoolDynamicSize / 1000) + " kb").ToString().PadRight(6) +
                " OrphanSize: " + this.MempoolOrphanSize.ToString().PadRight(4));
            return benchLogs.ToString();
        }
    }

}