using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.Cms;

class Program
{
    private static readonly string SLNPATH = DotEnv.TryGetSolutionDirectoryInfo().FullName;

    public static async Task Main(string[] args)
    {
        DotEnv.Load(Path.Combine(SLNPATH, ".env"));

        if (!PDFContext.GetInstanse.PDF.Any())
        {
            Parser.GetInstanse.LoadFromCSV(Path.Combine(SLNPATH, "Data\\Data\\Metadata2006_2016.csv"));
        }
        var downloader = new Downloader(PDFContext.GetInstanse);
        for (int i = 240; i < 1000; i+= 10){
            await downloader.DownloadPdfsAsync(SLNPATH + $"\\Data\\pdfs", 10, i);
            Console.WriteLine(i);
            Thread.Sleep(20000);
        }

    }
}