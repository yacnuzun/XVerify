using System.Xml;
using System.Xml.Xsl;

namespace Application.Services
{
    public class SchematronValidatorService
    {
        private void TransformWithXslt(string inputPath, string xsltPath, string outputPath)
        {
            var xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);
            using var reader = XmlReader.Create(inputPath);
            using var writer = XmlWriter.Create(outputPath, xslt.OutputSettings);
            xslt.Transform(reader, writer);
        }

        private string CompileSchematron(string schematronPath)
        {
            string step1 = Path.GetTempFileName();
            string step2 = Path.GetTempFileName();
            string step3 = Path.GetTempFileName();

            // 1. include işlemleri
            TransformWithXslt(schematronPath, "Schemas/iso_dsdl_include.xsl", step1);

            // 2. abstract expand
            TransformWithXslt(step1, "Schemas/iso_abstract_expand.xsl", step2);

            // 3. SVRL compiler
            TransformWithXslt(step2, "Schemas/iso_svrl_for_xslt2.xsl", step3);

            return step3; // Artık validasyon için kullanılabilir XSLT
        }


        public string ValidateWithSchematron(string xmlPath, string schematronXsltPath)
        {
            try
            {
                // Artık önceden derlenmiş XSLT olduğu için CompileSchematron çağrısı yok
                var xslt = new XslCompiledTransform();
                xslt.Load(schematronXsltPath); // hazır XSLT dosyası

                using var reader = XmlReader.Create(xmlPath);
                using var sw = new StringWriter();
                using var writer = XmlWriter.Create(sw, xslt.OutputSettings);

                xslt.Transform(reader, writer);

                return sw.ToString(); // SVRL raporu döner
            }
            catch (Exception ex)
            {
                // Hata yakalama
                throw new InvalidOperationException("Schematron validasyonu sırasında hata oluştu.", ex);
            }

        }
    }
}
