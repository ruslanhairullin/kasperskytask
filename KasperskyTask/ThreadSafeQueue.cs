using System;
using System.Collections.Generic;
using System.Threading;


namespace KasperskyTask
{
    /// <summary>
    /// Thread safe queue
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ThreadSafeQueue<T>
    {
        private readonly Queue<T> _queue;
        private readonly object _locker;

        public ThreadSafeQueue()
        {
            _queue = new Queue<T>();
            _locker = new object();
        }

        public void Push(T item)
        {
            lock (_locker)
            {
                _queue.Enqueue(item);
                if(_queue.Count > 0)
                    Monitor.PulseAll(_locker);
                Console.WriteLine("In push {0}", item);
            }
        }

        public T Pop()
        {
            lock (_locker)
            {
                while (_queue.Count == 0)
                    Monitor.Wait(_locker);
                
                try
                {                    
                    T item = _queue.Dequeue();                    
                    Console.WriteLine("In pop {0}", item);
                    return item;
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine(ex);
                    //Handle of error
                }

                return default(T);

            }
        }
    }
}
