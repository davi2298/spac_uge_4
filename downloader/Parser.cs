using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class Parser
{
    private static Parser? instanse;
    private PDFContext dbContext;

    public static Parser GetInstanse => instanse ??= new();
    private Parser()
    {
        dbContext = PDFContext.GetInstanse;
    }

    public void AddPDF(PDF PDF)
    {
        lock (dbContext)
        {
            var exsisting = dbContext.PDF.Find(PDF.BRnum);
            if (exsisting == null)
            {
                dbContext.PDF.Add(PDF);
                dbContext.SaveChanges();
            }
        }
    }
    public void LoadFromCSV(string filePath)
    {
        if (!File.Exists(filePath))
            return;
        IEnumerable<string?[]> lines = File.ReadAllLines(filePath)
            .Skip(1)
            .AsParallel()
            .Select(s => s.Replace("No", "false").Replace("Yes", "true"))
            .Select(s => s.Split(';').ToList().Select(ss => ss.Length == 0 ? null : ss).ToArray())
            ;
        List<PDF> pdfs = new();

        Parallel.ForEach(lines, async line =>
        {
            if (line[0] == null) throw new IOException("CSV is missing element");
            var pdf = await createPdf(line!);
            pdfs.Add(pdf);

        });
        // pdfs.ForEach(AddPDF);
        dbContext.PDF.AddRange(pdfs);
        dbContext.SaveChanges();

    }
    private async Task<PDF> createPdf(string[] line)
    {

        Uri.TryCreate(line[33], new UriCreationOptions(), out var pdfLink);
        return await Task.Run(() =>
        {
            return new PDF(
                line[0]!,
                line[1],
                line[2],
                line[3],
                line[4],
                line[5] == null ? null : Convert.ToBoolean(line[5]),
                line[6] == null ? null : Convert.ToBoolean(line[6]),
                line[7] == null ? null : Convert.ToBoolean(line[7]),
                line[8] == null ? null : Convert.ToBoolean(line[8]),
                line[9] == null ? null : Convert.ToBoolean(line[9]),
                line[10],
                line[11],
                line[12],
                line[13]!.Length == 4 ? new DateTime(Convert.ToInt32(line[13]), 1, 1) : DateTime.Parse(line[13]!),
                line[14] == null ? null : Convert.ToBoolean(line[14]),
                line[15] == null ? null : Convert.ToBoolean(line[15]),
                line[16] == null ? null : Convert.ToBoolean(line[16]),
                line[17],
                line[18],
                line[19] == null ? null : Convert.ToBoolean(line[19]),
                line[20] == null ? null : Convert.ToBoolean(line[20]),
                line[21] == null ? null : Convert.ToBoolean(line[21]),
                line[22],
                line[23],
                line[24] == null ? null : Convert.ToBoolean(line[24]),
                line[25]!,
                line[26] == null ? null : Convert.ToBoolean(line[26]),
                line[27] == null ? null : Convert.ToBoolean(line[27]),
                line[28],
                new DateTime(Convert.ToInt32(line[29]!), 1, 1),
                line[30]!,
                line[31],
                line[32],
                pdfLink,
                line[34] == null ? null : Convert.ToBoolean(line[34]),
                line[35],
                line[36],
                line[37],
                line[38] == null ? null : Convert.ToBoolean(line[38]),
                line[39],
                line[40],
                line[41],
                line[42],
                line[43] == null ? null : Convert.ToBoolean(line[43]),
                line[44]!,
                line[45],
                line[46] == null ? null : Convert.ToInt32(line[46]),
                line[47]
            );
        });
    }
}
