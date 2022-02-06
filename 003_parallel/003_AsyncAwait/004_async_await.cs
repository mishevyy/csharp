class Program
{
    private static async Task Main()
    {
        try
        {
            string result = await EAP_TAP();
            Console.WriteLine($"File content: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.GetType());
            Console.WriteLine(ex.Message);
        }
    }

    private static Task<string> EAP_TAP()
    {
        WebClient webClient = new WebClient();

        TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();

        webClient.DownloadStringCompleted += (sender, eventData) =>
        {
            try
            {
                Console.WriteLine($"Sender - {sender}");
                tcs.TrySetResult(eventData.Result);
            }
            catch (OperationCanceledException ex)
            {
                if (eventData.Cancelled == true)
                {
                    Console.WriteLine($"ОТМЕНА");
                }

                tcs.TrySetCanceled(ex.CancellationToken);
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    tcs.TrySetException(ex.InnerException);
                }
                else
                {
                    tcs.TrySetException(ex);
                }
            }
        };

        webClient.DownloadStringAsync(new Uri("http://microsoft.com/"));

        return tcs.Task;
    }
}