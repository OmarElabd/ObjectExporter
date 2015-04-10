using ObjectExporter.Core.Globals;
using ObjectExporter.Core.Models;
using ObjectExporter.Core.Models.RuleSets;

namespace ObjectExporter.Core.Templates
{
    public class GeneratorFactory
    {
        private readonly ExportType _type;
        private readonly RuleSetValidator _ruleSetValidator;

        public GeneratorFactory(ExportType type, RuleSetValidator ruleSetValidator)
        {
            _type = type;
            _ruleSetValidator = ruleSetValidator;
        }

        public IGenerator Create()
        {
            IGenerator generator = null;

            switch (_type)
            {
                case ExportType.Xml:
                    generator = new XmlGenerator(_ruleSetValidator);
                    break;
                case ExportType.Json:
                    generator = new JsonGenerator(_ruleSetValidator);
                    break;
                case ExportType.CSharpObject:
                    generator = new CSharpGenerator(_ruleSetValidator);
                    break;
            }

            return generator;
        }

        public static IGenerator CreateGenerator(ExportType type, RuleSetValidator ruleSetValidator)
        {
            IGenerator generator = null;

            switch (type)
            {
                case ExportType.Xml:
                    generator = new XmlGenerator(ruleSetValidator);
                    break;
                case ExportType.Json:
                    generator = new JsonGenerator(ruleSetValidator);
                    break;
                case ExportType.CSharpObject:
                    generator = new CSharpGenerator(ruleSetValidator);
                    break;
            }

            return generator;
        }
    }
}
