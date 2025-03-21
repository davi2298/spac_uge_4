using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cms;

class Program
{
    private static readonly string SLNPATH = DotEnv.TryGetSolutionDirectoryInfo().FullName;
    private static readonly bool DOALL = false;
    private static readonly int doksPrIter = 40;

    public static async Task Main(string[] _)
    {
        DotEnv.Load(Path.Combine(SLNPATH, ".env"));
        if (!PDFContext.GetInstanse.PDF.Any())
        {
            Parser.GetInstanse.LoadFromCSV(Path.Combine(SLNPATH, "Data\\Data\\Metadata2006_2016.csv"));
        }
        var downloader = new Downloader(PDFContext.GetInstanse);
        Console.WriteLine(PDFContext.getMissingPdfsSize());
        

        if (DOALL)
        {
            await downloader.DownloadPdfsAsync(SLNPATH + $"\\Data\\pdfs", PDFContext.getMissingPdfsSize());

            PDFContext.GetInstanse.SaveChanges();
        }
        else
        {
            // for (int i = 0; i < doksPrIter; i += doksPrIter)
            for (int i = 0; i < PDFContext.getMissingPdfsSize(); i += doksPrIter)
            {
                var start = DateTime.Now;
                await downloader.DownloadPdfsAsync(SLNPATH + $"\\Data\\pdfs", doksPrIter, i);
                PDFContext.GetInstanse.SaveChanges();
                Console.WriteLine(i);
                Console.WriteLine(DateTime.Now - start);

                Thread.Sleep(10000);
            }
        }
        var downloaded = PDFContext.GetInstanse.PDF.AsParallel().Where(p => p.pdf_downloaded == "yes").Select(p => p.BRnum).ToList();
        var file = File.Create(SLNPATH + "\\downloaded.txt");
        file.Close();
        using var downloadedTxt = new StreamWriter(SLNPATH + "\\downloaded.txt");
        downloaded.ForEach(e => downloadedTxt.WriteLine(e));

    }
}