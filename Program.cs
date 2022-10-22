using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System.Linq;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

Random rng = new Random();
int black, white, grey, count;
List<PdfPage> pages = new List<PdfPage>();

string[] counts = args[0].Split(',');
black = int.Parse(counts[0]);
white = int.Parse(counts[1]);
grey = int.Parse(counts[2]);

count = 11*black + 11*white + 11*grey;

Console.WriteLine($"Farben: {black}xSchwarz {white}xWeiß {grey}xGrau");
Console.WriteLine($"Anzahl an Karten: {count}");

Console.WriteLine($"Lade Vorderseiten");
PdfDocument doc = PdfReader.Open(args[1] + "-front.pdf", PdfDocumentOpenMode.Import);
PdfDocument doc2 = PdfReader.Open(args[1] + "-red-blue.pdf", PdfDocumentOpenMode.Import);
for(int i = 0; i < white; i++)
    for(int j = 0; j < 11; j++)
        pages.Add(doc.Pages[j]);
for(int i = 0; i < black; i++)
    for(int j = 11; j < 22; j++)
        pages.Add(doc.Pages[j]);
for(int i = 0; i < grey; i++)
    for(int j = 22; j < 33; j++)
        pages.Add(doc.Pages[j]);

Console.WriteLine($"Anzahl an Seiten: {pages.Count}");

doc = new PdfDocument();
doc.AddPage(doc2.Pages[0]);
foreach(PdfPage page in pages.OrderBy(a => rng.Next()).ToList())
    doc.AddPage(page);
doc.Pages.RemoveAt(1);

doc.Save(args[1] + "-front-final.pdf");


pages.Clear();
Console.WriteLine($"Lade Rückseiten");
doc = PdfReader.Open(args[1] + "-back.pdf", PdfDocumentOpenMode.Import);

for(int i = 0; i < white; i++)
    for(int j = 0; j < 11; j++)
        pages.Add(doc.Pages[j]);
for(int i = 0; i < black; i++)
    for(int j = 11; j < 22; j++)
        pages.Add(doc.Pages[j]);
for(int i = 0; i < grey; i++)
    for(int j = 22; j < 33; j++)
        pages.Add(doc.Pages[j]);

Console.WriteLine($"Anzahl an Seiten: {pages.Count}");

doc = new PdfDocument();
doc.AddPage(doc2.Pages[1]);
foreach(PdfPage page in pages.OrderBy(a => rng.Next()).ToList())
    doc.AddPage(page);
doc.Pages.RemoveAt(1);

doc.Save(args[1] + "-back-final.pdf");