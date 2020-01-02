using System;
using System.Diagnostics;


namespace NoAIDependency
{
    public class ActivityTesting
    {
        static IDisposable applicationInsightsSubscription = null;

        public void W3CExample()
        {
            
            Activity parent = new Activity("parent");
            parent.SetIdFormat(ActivityIdFormat.W3C);
            // W3C 
            parent.Start();
            Console.WriteLine("W3C Parent---");
            PrintActivity(parent);

            // Correlation 
            Activity child = new Activity("child");
            child.SetParentId(parent.ParentId);
            Console.WriteLine("W3C Child ---");
            child.Start();
            PrintActivity(child);

            // DiagnosticListener is the same behavior
            DiagnosticListener listener = new DiagnosticListener("hello");
            DiagnosticListener.AllListeners.Subscribe(delegate(DiagnosticListener lsn) { }
            );

        }

        public void HttpCorrelationExample()
        {
            Activity.Current = null;
            Activity parent = new Activity("parent");
            parent.SetIdFormat(ActivityIdFormat.Hierarchical);
            parent.Start();
            Console.WriteLine("HTTP Parent---");
            PrintActivity(parent);

            // Correlation 
            Activity child = new Activity("child");
            child.SetParentId(parent.ParentId);
            child.Start();
            Console.WriteLine("HTTP Child ---");
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
            Console.WriteLine("TraceParent: " + activity.Id); // probably this one. compare this with a spec. 
            // TraceState : activity.GetTracestate()
            Console.WriteLine("TraceState: " + activity.TraceStateString);
            // ParentSpanId : activity.GetParentSpanId()
            Console.WriteLine("ParentSpanId " + activity.ParentSpanId);
            // GetTraceId()
            Console.WriteLine("TraceId: " + activity.TraceId.ToString());
            // GetSpanId()
            Console.WriteLine("SpanId: " + activity.SpanId.ToString());
        }


    }
}
