using System;
using System.Threading;

namespace Sample02
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Console.WriteLine("Start Main");
            try
            {
                Thread t = new Thread(Worker.Do);
                t.IsBackground = true;
                t.Start();
                t.Join();
                Console.WriteLine("Work.Do Error: " +
                                    ((Worker.Error != null) ? Worker.Error.Message : "No Error"));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Main: " + ex.Message);
            }
            finally
            {
                Console.WriteLine("End Main");
                Console.ReadLine();
            }
        }
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Console.WriteLine("Unhandled Exception: " +
                                ((Exception)e.ExceptionObject).Message);
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
    static public class Worker
    {
        static public Exception Error
        {
            get;
            private set;
        }
        static public void Do()
        {
            Console.WriteLine("Start Work.Do");
            try
            {
                Error = null;
                Thread.Sleep(1000);
                int a = 1;
                a = a / --a;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception Work.Do");
                Error = ex;
            }
            finally
            {
                Console.WriteLine("End Work.Do");
            }
        }
    }
}
