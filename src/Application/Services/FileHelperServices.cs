using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Services
{
    internal class FileHelperServices
    {
        private void XMLFileConvert()
        {

        }
        private void JsonFileRead()
        {

        }
    }

    public class ZipHelper
    {
        public List<FakeModel> ConvertClass(string zipFolder)
        {
            List<FakeModel> groupedInvoices = new List<FakeModel>();

            // zipFolder içindeki tüm .zip dosyalarını bir kez döngüye alıyoruz.
            // İç içe döngüye gerek yok.
            foreach (var zipFilePath in Directory.GetFiles(zipFolder, "*.zip"))
            {
                using (ZipArchive zip = ZipFile.OpenRead(zipFilePath))
                {
                    // .xml uzantılı ilk dosyayı buluyoruz.
                    var xmlEntry = zip.Entries.FirstOrDefault(e => e.Name.EndsWith(".xml", StringComparison.OrdinalIgnoreCase));
                    if (xmlEntry == null) continue; // XML dosyası yoksa bir sonraki zip'e geç

                    using (MemoryStream ms = new MemoryStream())
                    {
                        // xmlEntry.Extract(ms); metodunun yerine bu kısmı kullanıyoruz.
                        // ZipArchiveEntry.Open() metodu ile bir stream alıp, içeriği ms'e kopyalıyoruz.
                        using (var entryStream = xmlEntry.Open())
                        {
                            entryStream.CopyTo(ms);
                        }
                        ms.Position = 0;

                        XDocument xml;
                        try
                        {
                            xml = XDocument.Load(ms);
                        }
                        catch (System.Xml.XmlException)
                        {
                            // Geçersiz XML dosyalarını atlamak için hata yakalama.
                            continue;
                        }

                        XNamespace cbc = "urn:oasis:names:specification:ubl:schema:xsd:CommonBasicComponents-2";
                        string issueDateStr = xml.Descendants(cbc + "IssueDate").FirstOrDefault()?.Value;

                        if (DateTime.TryParse(issueDateStr, out DateTime issueDate))
                        {
                            var existingGroup = groupedInvoices.FirstOrDefault(g => g.DateTime.Date == issueDate.Date);

                            if (existingGroup == null)
                            {
                                groupedInvoices.Add(new FakeModel
                                {
                                    DateTime = issueDate.Date, // Sadece tarihi saklamak daha mantıklı
                                    ListingInvoice = new List<string> { Path.GetFileName(zipFilePath) }
                                });
                            }
                            else
                            {
                                existingGroup.ListingInvoice.Add(Path.GetFileName(zipFilePath));
                            }
                        }
                    }
                }
            }
            return groupedInvoices;


        }

        public void ConverZip(string outputFolder, string sourceFolder)
        {
            // Hedef klasör yoksa oluştur
            Directory.CreateDirectory(outputFolder);

            // .html dosyalarını al
            var htmlFiles = Directory.GetFiles(sourceFolder, "*.xml");

            foreach (var htmlFile in htmlFiles)
            {
                string fileName = Path.GetFileName(htmlFile); // örnek: dosya.html
                string zipFilePath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(htmlFile) + ".zip");

                using (FileStream zipToCreate = new FileStream(zipFilePath, FileMode.Create))
                using (ZipArchive archive = new ZipArchive(zipToCreate, ZipArchiveMode.Create))
                {
                    // Arşivdeki giriş
                    ZipArchiveEntry entry = archive.CreateEntry(fileName);

                    // HTML içeriğini zip'e yaz
                    using (var entryStream = entry.Open())
                    using (var fileStream = File.OpenRead(htmlFile))
                    {
                        fileStream.CopyTo(entryStream);
                    }
                }

            }

        }
    }
    public class FakeData
    {
        public static List<string> Ags { get; } = new List<string> { @"D:\Test\10Ags\T022025000000028.zip", @"D:\Test\10Ags\T022025000000029.zip" };
        public static List<string> Tem1 { get; } = new List<string>{
            @"D:\Test\14Tem\OZG2025000000082_0790878598.zip", @"D:\Test\14Tem\T012025000000449_61795323972.zip",
            @"D:\Test\14Tem\T012025000000450_3881744981.zip", @"D:\Test\14Tem\T012025000000451_6222470755.zip",
            @"D:\Test\14Tem\T012025000000452_42334973308.zip", @"D:\Test\14Tem\T012025000000453_0710816060.zip" };
        public static List<string> Tem2 { get; } = new List<string> {
             @"D:\Test\31Tem\OTF2025000000085_21260675234.zip", @"D:\Test\31Tem\OZG2025000000086_0091846757.zip",
            @"D:\Test\31Tem\OZG2025000000087_1980060995.zip", @"D:\Test\31Tem\T012025000000499_1290359495.zip",
            @"D:\Test\31Tem\T012025000000500_3881744981.zip", @"D:\Test\31Tem\T022025000000006_3881744981.zip",
            @"D:\Test\31Tem\T022025000000007_66640171876.zip", @"D:\Test\31Tem\T022025000000008_6320088425.zip",
            @"D:\Test\31Tem\T022025000000009_4580363337.zip"
        };
        public List<FakeModel> fakeModels { get; } = new List<FakeModel>
        { new FakeModel { DateTime = new DateTime(2025, 08, 10), ListingInvoice = Ags },
            new FakeModel { DateTime = new DateTime(2025, 07, 14), ListingInvoice = Tem1},
            new FakeModel {DateTime = new DateTime(2025,07,31), ListingInvoice = Tem2}
        };
    }

    public class FakeModel
    {
        public DateTime DateTime { get; set; }
        public List<string> ListingInvoice { get; set; }
    }
}
