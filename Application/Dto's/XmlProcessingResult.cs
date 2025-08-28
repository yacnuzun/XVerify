using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto_s
{
    public class XmlProcessingResult
    {
        public List<string> XsdErrors { get; set; }
        public string SchematronReport { get; set; }
        public string TransformedHtml { get; set; }
    }

}
