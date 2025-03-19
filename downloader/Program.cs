using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cms;

class Program
{
    private static readonly string SLNPATH = DotEnv.TryGetSolutionDirectoryInfo().FullName;
    private static readonly bool DOALL = false;

    public static async Task Main(string[] args)
    {
        DotEnv.Load(Path.Combine(SLNPATH, ".env"));
        var dbContext = PDFContext.GetInstanse;
        if (!PDFContext.GetInstanse.PDF.Any())
        {
            Parser.GetInstanse.LoadFromCSV(Path.Combine(SLNPATH, "Data\\Data\\Metadata2006_2016.csv"));
        }
        var downloader = new Downloader(dbContext);
        
        if (DOALL)
        {
            await downloader.DownloadPdfsAsync(SLNPATH + $"\\Data\\pdfs", PDFContext.getPdfsSize());
        }
        else
        {
            for (int i = 330; i < 1000; i += 10)
            {
                await downloader.DownloadPdfsAsync(SLNPATH + $"\\Data\\pdfs", 10, i);
                Console.WriteLine(i);
                Thread.Sleep(20000);
            }
        }
        dbContext.SaveChanges();
        var downloaded = dbContext.PDF.AsParallel().Where(p => p.pdf_downloaded == "yes").Select(p => p.BRnum).ToList();
        File.Create(SLNPATH + "downloaded.txt");
        using var downloadedTxt = new StreamWriter(SLNPATH + "downloaded.txt");
        downloaded.ForEach(e => downloadedTxt.WriteLine(e));

    }
}