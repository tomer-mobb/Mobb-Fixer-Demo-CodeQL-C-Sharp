using System;
using System.Net;
using System.Diagnostics;
using Microsoft.Extensions.Logging;



public class Program
{
    static void Main(string[] args)
    {
        var factory = LoggerFactory.Create(builder => {
            builder.AddConsole();
        });

        var log = factory.CreateLogger<Program>();

        using var listener = new HttpListener();

        listener.Prefixes.Add("http://localhost:8001/");
        listener.Start();

        while (true)
        {
            HttpListenerContext ctx = listener.GetContext();
            HttpListenerRequest req = ctx.Request;
            var var1 = req.QueryString["var1"];
            var var2 = req.QueryString["var2"];
            Trace.TraceInformation(var1); 
            Trace.TraceInformation("test" + var2); 
            Trace.TraceInformation("test" + var2 + "test" + var1); 
            Trace.TraceInformation("Test, {0}", var2); 

            string val = (string)req.QueryString["val"];
            try {
                int value = Int32.Parse(val);
            }
                catch (FormatException fe) {
                log.LogError("Failed to parse val= " + val); 
            }
        }
    }
}
