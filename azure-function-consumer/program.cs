using System;
using System.IO;
using System.Net.Http;
using System.Text.Json;

class Program
{
    static void Main()
    {
        // Create an instance of HttpClient (you can reuse this)
        var httpClient = new HttpClient();
        var jsonOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);

        try
        {
            // Make the synchronous GET request
            using (var request = new HttpRequestMessage(HttpMethod.Get, "http://<<serverhost>>/llrserver"))
            {
                Console.WriteLine("Start time: " + DateTime.Now);

                httpClient.Timeout = TimeSpan.FromMinutes(100);
                
                var response = httpClient.Send(request);
                response.EnsureSuccessStatusCode();
                
                Console.WriteLine("End time: " + DateTime.Now);

                // Read the response content as a stream
                using var stream = response.Content.ReadAsStream();
                //convert stream to a string
                var content = new StreamReader(stream).ReadToEnd();

                Console.WriteLine(content);
                Console.ReadLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("End time: " + DateTime.Now);
            Console.WriteLine(ex.Message);
        }
    }
}

