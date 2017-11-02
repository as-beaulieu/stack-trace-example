using System;
using System.Diagnostics;

namespace StackTraceTest
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            var sample = new Program();
            try
            {
                sample.MyPublicMethod();
            }
            catch (Exception)
            {
                // Create a StackTrace that captures
                // filename, line number, and column
                // information for the current thread.
                StackTrace st = new StackTrace(true);
                for (int i = 0; i < st.FrameCount; i++)
                {
                    // Note that high up the call stack, there is only
                    // one stack frame.
                    StackFrame sf = st.GetFrame(i);
                    Console.WriteLine();
                    Console.WriteLine("High up the call stack, Method: {0}",
                        sf.GetMethod());

                    Console.WriteLine("High up the call stack, Line Number: {0}",
                        sf.GetFileLineNumber());
                }
            }
        }

        public void MyPublicMethod()
        {
            MyProtectedMethod();
        }

        protected void MyProtectedMethod()
        {
            MyInternalClass mic = new MyInternalClass();
            mic.ThrowsException();
        }

        class MyInternalClass
        {
            public void ThrowsException()
            {
                try
                {
                    throw new Exception("A problem was encountered.");
                }
                catch (Exception ex)
                {
                    // Create a StackTrace that captures filename,
                    // line number and column information.
                    StackTrace st = new StackTrace(true);
                    string stackIndent = "";
                    for (int i = 0; i < st.FrameCount; i++)
                    {
                        // Note that at this level, there are four
                        // stack frames, one for each method invocation.
                        StackFrame sf = st.GetFrame(i);
                        ShowSf(sf);
                        Console.WriteLine();
                        Console.WriteLine(stackIndent + " Method: {0}",
                            sf.GetMethod());
                        Console.WriteLine(stackIndent + " File: {0}",
                            sf.GetFileName());
                        Console.WriteLine(stackIndent + " Line Number: {0}",
                            sf.GetFileLineNumber());
                        stackIndent += "  ";
                    }
                    throw ex;
                }
            }

            public static void ShowSf(StackFrame stackFrame)
            {
                Console.WriteLine("STACK TRACE INFO *******");
                Console.WriteLine("Get File Name: " + stackFrame.GetFileName());
                Console.WriteLine("File Column Number: " + stackFrame.GetFileColumnNumber());
                Console.WriteLine("File Line Number: " + stackFrame.GetFileLineNumber());
                Console.WriteLine("File Name: " + stackFrame.GetFileName());
                Console.WriteLine("Hash Code: " + stackFrame.GetHashCode());
                Console.WriteLine("IL Offset: " + stackFrame.GetILOffset());
                Console.WriteLine("Method: " + stackFrame.GetMethod());
                Console.WriteLine("*********");
            }
        }
    }
}
