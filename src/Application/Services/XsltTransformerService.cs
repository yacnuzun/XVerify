using System.Xml;
using System.Xml.Xsl;

namespace Application.Services
{
    public class XsltTransformerService
    {
        public string TransformXmlWithXslt(string xmlPath, string xsltPath)
        {
            XslCompiledTransform xslt = new XslCompiledTransform();
            xslt.Load(xsltPath);

            using StringWriter writer = new StringWriter();
            using XmlReader reader = XmlReader.Create(xmlPath);
            using XmlWriter xmlWriter = XmlWriter.Create(writer);

            xslt.Transform(reader, xmlWriter);

            return writer.ToString(); // HTML veya başka XML dönebilir
        }
    }

}
