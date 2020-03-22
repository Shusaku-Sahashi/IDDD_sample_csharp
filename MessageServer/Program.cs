using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Redis;

namespace MessageServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine(Something.Age);
            Something.Age.Value++;
            
            var task = Task.Run(async () =>
            {
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
                await Task.Delay(1000);
                Something.Age.Value++;
                Something.Age.Value++;
                Console.WriteLine(Something.Age.Value);
                return Something.Age;
            });
            
            Console.WriteLine(task.Result.Value);
            Console.WriteLine(Something.Age);
            // var task = Task.Run<string>(() => "hoge");
            // Console.WriteLine(task.Result);
        }
    }

    public class Something
    {
        public static ThreadLocal<int> Age = new ThreadLocal<int>(() => 0);
    }
}