using Application.Dto_s;

namespace Application.Services
{
    public class XmlProcessingService
    {
        private readonly XsdValidatorService _xsdValidator;
        private readonly SchematronValidatorService _schematronValidator;
        private readonly XsltTransformerService _xsltTransformer;

        public XmlProcessingService()
        {
            _xsdValidator = new XsdValidatorService();
            _schematronValidator = new SchematronValidatorService();
            _xsltTransformer = new XsltTransformerService();
        }

        public async Task<XmlProcessingResult> ProcessAsync(string xmlPath, string xsdPath, string schematronXsltPath, string xsltPath)
        {
            var xsdErrors = _xsdValidator.ValidateXmlWithXsd(xmlPath, xsdPath);
            var schematronResult = _schematronValidator.ValidateWithSchematron(xmlPath, schematronXsltPath);
            var transformedOutput = _xsltTransformer.TransformXmlWithXslt(xmlPath, xsltPath);

            return new XmlProcessingResult
            {
                XsdErrors = xsdErrors,
                SchematronReport = schematronResult,
                TransformedHtml = transformedOutput
            };
        }
    }

}
