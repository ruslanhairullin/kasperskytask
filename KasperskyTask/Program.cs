using System;
using System.Threading.Tasks;

namespace KasperskyTask
{
    class Program
    {
        static void Main(string[] args)
        {   
            /*Надо сделать очередь с операциями push(T) и T pop(). 
             * Операции должны поддерживать обращение с разных потоков. 
             * Операция push всегда вставляет и выходит.
             * Операция pop ждет пока не появится новый элемент.
             * В качестве контейнера внутри можно использовать только стандартную очередь (Queue) .*/ 

            TaskWithQueue();
           

            /*Есть коллекция чисел и отдельное число Х. Надо вывести все пары чисел, которые в сумме равны заданному Х.*/
            
            //TaskWithArray();
        }

        private static void TaskWithQueue()
        {
            ThreadSafeQueue<int> threadSafeQueue = new ThreadSafeQueue<int>();

            int tasksCount = 20;

            Task[] tasksPush = new Task[tasksCount];
            Task[] tasksPop = new Task[tasksCount];

            for (int i = 0; i < tasksCount; i++)
            {
                tasksPop[i] = Task.Factory.StartNew(() => { threadSafeQueue.Pop(); });
                tasksPush[i] = Task.Factory.StartNew(() => { threadSafeQueue.Push(1); });
            }

            Console.ReadLine();
        }

        private static void TaskWithArray()
        {
            int[] array = { 6, 2, 3, 4, 5, -1 };

            Array.Sort(array);

            int sum = 10;

            int first = 0;
            int last = array.Length - 1;
            while (first < last)
            {
                int currentSum = array[first] + array[last];
                if (currentSum == sum)
                {
                    Console.WriteLine("{0} + {1} = {2}", array[first], array[last],sum);
                    first++;
                    last--;
                }
                else
                {
                    if (currentSum < sum) first++;
                    else last--;
                }
            }

            Console.ReadLine();
        }

    }
}
