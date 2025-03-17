using System.Threading.Tasks;

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
        await downloader.DownloadPdfsAsync(SLNPATH + $"\\Data\\pdfs", 10, Random.Shared.Next(1000));
    }
}