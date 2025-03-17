using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("metadata")]
public class PDF
{
    [Key, Required]
    public string BRnum { get; set; }
    public string? AdherenceLevel { get; set; }
    public string? ApplicationLevel { get; set; }
    public string? AssuranceProvider { get; set; }
    public string? AssuranceScope { get; set; }
    public bool? AssuranceStanadardISAE3000 { get; set; }
    public bool? AssurancetandardAA1000AS { get; set; }
    public bool? AssuranceStandardnationalGeneral { get; set; }
    public bool? AssuranceStandardnationalSustainability { get; set; }
    public bool? CDP { get; set; }
    public string? Country { get; set; }
    public string? CountryStatus { get; set; }
    public string? Databaselink { get; set; }
    [Required]
    public DateTime DateAdded { get; set; }
    public bool? ExternalAssurance { get; set; }
    public bool? FeaturedReport { get; set; }
    public bool? GOLDCommunity { get; set; }
    public string? GRIContentIndexURL { get; set; }
    public string? GRIService { get; set; }
    public bool? IFC { get; set; }
    public bool? ISO { get; set; }
    [Column("PDFIntegrated")]
    public bool? Integrated { get; set; }
    public string? LevelOfAssurance { get; set; }
    public string? Listed { get; set; }
    public bool? MaterialityMattersCheck { get; set; }
    [Column("PDFName"), Required]
    public string Name { get; set; }
    public bool? OECD { get; set; }
    public bool? OS { get; set; }
    public string? OrganizationType { get; set; }
    [Required]
    public DateTime Pub_Year { get; set; }
    [Required]
    public string Region { get; set; }
    public string? ReportDrtAddress { get; set; }
    public string? ReportHtmlAddress { get; set; }
    public Uri? Pdf_URL { get; set; }
    public bool? SDGs { get; set; }
    public string? Sector { get; set; }
    public string? SectorSupplementsFinal { get; set; }
    [Column("PDFSize")]
    public string? Size { get; set; }
    public bool? StakeholderPanel { get; set; }
    [Column("PDFStatus")]
    public string? Status { get; set; }
    public string? Title { get; set; }
    public string? PDFType { get; set; }
    public string? TypeOfAssuranceProvider { get; set; }
    public bool? UNGC { get; set; }
    public string? DIHR_ID { get; set; }
    public string? pdf_downloaded { get; set; }
    public int? English_dummy { get; set; }
    public string? English { get; set; }

    // public PDF() { }

    public PDF(string bRnum, string? v, DateTime dateAdded, string name, DateTime pub_Year, string region, Uri? pdf_URL, string dIHR_ID)
    {
        BRnum = bRnum;
        Name = name;
        Pub_Year = pub_Year;
        DateAdded = dateAdded;
        Region = region;
        Pdf_URL = pdf_URL;
        DIHR_ID = dIHR_ID;
    }

    public PDF(string bRnum, string? adherenceLevel, string? applicationLevel, string? assuranceProvider, string? assuranceScope, bool? assuranceStanadardISAE3000, bool? assurancetandardAA1000AS, bool? assuranceStandardnationalGeneral, bool? assuranceStandardnationalSustainability, bool? cDP, string? country, string? countryStatus, string? databaselink, DateTime dateAdded, bool? externalAssurance, bool? featuredReport, bool? gOLDCommunity, string? gRIContentIndexURL, string? gRIService, bool? iFC, bool? iSO, bool? integrated, string? levelOfAssurance, string? listed, bool? materialityMattersCheck, string name, bool? oECD, bool? oS, string? organizationType, DateTime pub_Year, string region, string? reportDrtAddress, string? reportHtmlAddress, Uri? pdf_URL, bool? sDGs, string? sector, string? sectorSupplementsFinal, string? size, bool? stakeholderPanel, string? status, string? title, string? pDFType, string? typeOfAssuranceProvider, bool? uNGC, string dIHR_ID, string? pdf_downloaded, int? english_dummy, string? english)
    {
        BRnum = bRnum;
        AdherenceLevel = adherenceLevel;
        ApplicationLevel = applicationLevel;
        AssuranceProvider = assuranceProvider;
        AssuranceScope = assuranceScope;
        AssuranceStanadardISAE3000 = assuranceStanadardISAE3000;
        AssurancetandardAA1000AS = assurancetandardAA1000AS;
        AssuranceStandardnationalGeneral = assuranceStandardnationalGeneral;
        AssuranceStandardnationalSustainability = assuranceStandardnationalSustainability;
        CDP = cDP;
        Country = country;
        CountryStatus = countryStatus;
        Databaselink = databaselink;
        DateAdded = dateAdded;
        ExternalAssurance = externalAssurance;
        FeaturedReport = featuredReport;
        GOLDCommunity = gOLDCommunity;
        GRIContentIndexURL = gRIContentIndexURL;
        GRIService = gRIService;
        IFC = iFC;
        ISO = iSO;
        Integrated = integrated;
        LevelOfAssurance = levelOfAssurance;
        Listed = listed;
        MaterialityMattersCheck = materialityMattersCheck;
        Name = name;
        OECD = oECD;
        OS = oS;
        OrganizationType = organizationType;
        Pub_Year = pub_Year;
        Region = region;
        ReportDrtAddress = reportDrtAddress;
        ReportHtmlAddress = reportHtmlAddress;
        Pdf_URL = pdf_URL;
        SDGs = sDGs;
        Sector = sector;
        SectorSupplementsFinal = sectorSupplementsFinal;
        Size = size;
        StakeholderPanel = stakeholderPanel;
        Status = status;
        Title = title;
        PDFType = pDFType;
        TypeOfAssuranceProvider = typeOfAssuranceProvider;
        UNGC = uNGC;
        DIHR_ID = dIHR_ID;
        this.pdf_downloaded = pdf_downloaded;
        English_dummy = english_dummy;
        English = english;
    }

    public void UpdateWidth(PDF pDF)
    {
        this.BRnum = pDF.BRnum;
        this.AdherenceLevel = pDF.AdherenceLevel;
        this.ApplicationLevel = pDF.ApplicationLevel;
        this.AssuranceProvider = pDF.AssuranceProvider;
        this.AssuranceScope = pDF.AssuranceScope;
        this.AssuranceStanadardISAE3000 = pDF.AssuranceStanadardISAE3000;
        this.AssurancetandardAA1000AS = pDF.AssurancetandardAA1000AS;
        this.AssuranceStandardnationalGeneral = pDF.AssuranceStandardnationalGeneral;
        this.AssuranceStandardnationalSustainability = pDF.AssuranceStandardnationalSustainability;
        this.CDP = pDF.CDP;
        this.Country = pDF.Country;
        this.CountryStatus = pDF.CountryStatus;
        this.Databaselink = pDF.Databaselink;
        this.DateAdded = pDF.DateAdded;
        this.ExternalAssurance = pDF.ExternalAssurance;
        this.FeaturedReport = pDF.FeaturedReport;
        this.GOLDCommunity = pDF.GOLDCommunity;
        this.GRIContentIndexURL = pDF.GRIContentIndexURL;
        this.GRIService = pDF.GRIService;
        this.IFC = pDF.IFC;
        this.ISO = pDF.ISO;
        this.Integrated = pDF.Integrated;
        this.LevelOfAssurance = pDF.LevelOfAssurance;
        this.Listed = pDF.Listed;
        this.MaterialityMattersCheck = pDF.MaterialityMattersCheck;
        this.Name = pDF.Name;
        this.OECD = pDF.OECD;
        this.OS = pDF.OS;
        this.OrganizationType = pDF.OrganizationType;
        this.Pub_Year = pDF.Pub_Year;
        this.Region = pDF.Region;
        this.ReportDrtAddress = pDF.ReportDrtAddress;
        this.ReportHtmlAddress = pDF.ReportHtmlAddress;
        this.Pdf_URL = pDF.Pdf_URL;
        this.SDGs = pDF.SDGs;
        this.Sector = pDF.Sector;
        this.SectorSupplementsFinal = pDF.SectorSupplementsFinal;
        this.Size = pDF.Size;
        this.StakeholderPanel = pDF.StakeholderPanel;
        this.Status = pDF.Status;
        this.Title = pDF.Title;
        this.PDFType = pDF.PDFType;
        this.TypeOfAssuranceProvider = pDF.TypeOfAssuranceProvider;
        this.UNGC = pDF.UNGC;
        this.DIHR_ID = pDF.DIHR_ID;
        this.pdf_downloaded = pDF.pdf_downloaded;
        this.English_dummy = pDF.English_dummy;
        this.English = pDF.English;
    }

}