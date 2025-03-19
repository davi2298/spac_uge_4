using System.Net;
using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;

class Downloader
{
    // 100000000 = 10 seconts
    private const int TimeoutTicks = 100000000;
    private readonly PDFContext dbContext;

    public Downloader(PDFContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<HttpResponseMessage> DownloadSinglePDF(Uri url)
    {
        using var client = new HttpClient();
        client.BaseAddress = url;
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/pdf"));
        client.Timeout = new TimeSpan(TimeoutTicks);
        return await client.GetAsync(url);

    }
    public async Task DownloadPdfsAsync(string path, int limit = 10, int offset = 0)
    {
        var pdfsToGet = (await dbContext.PDF.Where(e => e.pdf_downloaded != "yes").ToListAsync()).GetRange(0 + offset, limit);
        // var pdfsToGet = await dbContext.PDF.Where(e => e.BRnum == "BR10013").ToListAsync();

        var responseTasks = getPdfs(pdfsToGet);

        var needRetrys = await handleResponsesAsync(responseTasks, path);

        var retryResponseTasks = getPdfs(needRetrys);

        await handleResponsesAsync(retryResponseTasks, path);
    }

    private List<(PDF, Task<HttpResponseMessage>)> getPdfs(IEnumerable<PDF> pdfsToGet, bool isRetry = false)
    {
        var responseTasks = new List<(PDF, Task<HttpResponseMessage>)>();
        foreach (var pdf in pdfsToGet)
        {
            if (!isRetry && pdf.Pdf_URL != null)
            {
                responseTasks.Add((pdf, DownloadSinglePDF(pdf.Pdf_URL)));
            }
            else if (isRetry && pdf.GRIContentIndexURL != null)
            {
                responseTasks.Add((pdf, DownloadSinglePDF(new Uri(pdf.GRIContentIndexURL))));
            }
        }

        return responseTasks;
    }

    private async Task<IEnumerable<PDF>> handleResponsesAsync(List<(PDF, Task<HttpResponseMessage>)> responses, string path, bool retry = false)
    {
        List<PDF> needRetry = new();
        await Parallel.ForEachAsync(responses, async (grups, _) =>
        {
            
            var pdf = grups.Item1;
            try
            {
                var response = await grups.Item2;
                if (response.StatusCode == HttpStatusCode.Forbidden)
                {
                    if (retry)
                        pdf.pdf_downloaded = "no";
                    else
                        needRetry.Add(pdf);
                    return;
                }
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (response.Content.Headers.ContentType!.MediaType!.Equals("application/pdf"))
                    {

                        var filePath = path + $"\\{pdf.BRnum}.pdf";
                        using var file = File.Create(filePath);
                        var stream = await response.Content.ReadAsStreamAsync();
                        stream.CopyToAsync(file);
                        pdf.pdf_downloaded = "yes";

                    }
                    else
                    {
                        if (retry)
                            pdf.pdf_downloaded = "no";
                        else
                            needRetry.Add(pdf);
                        return;
                    }
                }
            }
            catch (Exception)
            {
                // Console.WriteLine(e);
                if (retry)
                    pdf.pdf_downloaded = "no";
                else
                    needRetry.Add(pdf);
                return;
            }

        });

        return needRetry;
    }
}