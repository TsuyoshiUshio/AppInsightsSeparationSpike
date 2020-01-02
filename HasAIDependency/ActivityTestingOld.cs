using System;
using System.Diagnostics;
using Microsoft.ApplicationInsights.W3C;

namespace HasAIDependency
{
    public class ActivityTestingOld
    {
        public void W3CExample()
        {
            // Activity.Current = null; // We can't do it.
            Activity parent = new Activity("parent");
#pragma warning disable 618
            parent.GenerateW3CContext();

            // W3C 
            parent.Start();
            Console.WriteLine("W3C Parent Old---");
            PrintActivity(parent);

            // Correlation 
            Activity child = new Activity("child");
            child.SetTraceparent(parent.GetTraceparent());
            child.SetTracestate(parent.GetTracestate());
            Console.WriteLine("W3C Child Old---");
            child.Start();
            PrintActivity(child);

            // DiagnosticListener is the same behavior
            DiagnosticListener listener = new DiagnosticListener("hello");
            DiagnosticListener.AllListeners.Subscribe(delegate(DiagnosticListener lsn) { }
            );

        }

        public void HttpCorrelationExample()
        {
            Activity parent = new Activity("parent");
            parent.Start();
            Console.WriteLine("HTTP Parent Old---");
            PrintActivity(parent);

            // Correlation 
            Activity child = new Activity("child");
            child.SetParentId(parent.ParentId);
            child.Start();
            Console.WriteLine("HTTP Child Old---");
            PrintActivity(child);
        }

        private void PrintActivity(Activity activity)
        {
            // OperationName : activity.OperationName
            Console.WriteLine("OperationName: " + activity.OperationName);
            // StartTime : activity.StartTimeUtc
            Console.WriteLine("StartTime: " + activity.StartTimeUtc.ToString("yyyy:MM:dd hh:ss"));
            // TraceParent : activity.GetTraceparent()
            Console.WriteLine("Id: " + activity.Id);
            Console.WriteLine("TraceParent: " + activity.GetTraceparent()); // probably this one. compare this with a spec. 
            // TraceState : activity.GetTracestate()
            Console.WriteLine("TraceState: " + activity.GetTracestate());
            // ParentSpanId : activity.GetParentSpanId()
            Console.WriteLine("ParentSpanId " + activity.GetParentSpanId());
            // GetTraceId()
            Console.WriteLine("TraceId: " + activity.GetTraceId());
            // GetSpanId()
            Console.WriteLine("SpanId: " + activity.GetSpanId());
        }
#pragma warning restore 618
    }
}
