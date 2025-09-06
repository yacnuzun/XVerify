using System.Xml;

namespace Application.Services
{
    public class XsdValidatorService
    {
        public List<string> ValidateXmlWithXsd(string xmlPath, string xsdPath)
        {
            List<string> errors = new List<string>();

            XmlReaderSettings settings = new XmlReaderSettings();
            settings.Schemas.Add(null, xsdPath);
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationEventHandler += (sender, args) =>
            {
                errors.Add(args.Message);
            };

            using var reader = XmlReader.Create(xmlPath, settings);
            while (reader.Read()) { } // Trigger validation

            return errors;
        }
    }

}
