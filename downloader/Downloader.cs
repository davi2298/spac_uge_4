using System.Net;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

class Downloader
{
    private PDFContext dbContext;

    public Downloader(PDFContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task DownloadSinglePDF(Uri url, string filename)
    {
        using var client = new HttpClient();
        try
        {
            var response = await client.GetAsync(url);
            var status = response.StatusCode;
            Console.WriteLine(status);

        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
        }
    }
    public async Task DownloadPdfsAsync(string path, int limit = 10, int offset = 0)
    {
        var pdfsToGet = (await dbContext.PDF.ToListAsync()).GetRange(0 + offset, limit + offset);
        // Parallel.ForEach(pdfsToGet, async pdf =>
        // {
        //     if (pdf.Pdf_URL != null)
        //         await DownloadSinglePDF(pdf.Pdf_URL, path + $"\\{pdf.BRnum}.pdf");
        // });
        foreach (var pdf in pdfsToGet)
        {
            await DownloadSinglePDF(pdf.Pdf_URL, path + $"\\{pdf.BRnum}.pdf");
        }
    }
}