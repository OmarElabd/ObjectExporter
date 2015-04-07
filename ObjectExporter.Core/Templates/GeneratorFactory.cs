using ObjectExporter.Core.Globals;
using ObjectExporter.Core.Models;

namespace ObjectExporter.Core.Templates
{
    public class GeneratorFactory
    {
        private readonly ExportType _type;
        private readonly PropertyAccessibilityChecker _checker;

        public GeneratorFactory(ExportType type, PropertyAccessibilityChecker checker)
        {
            _type = type;
            _checker = checker;
        }

        public IGenerator Create()
        {
            IGenerator generator = null;

            switch (_type)
            {
                case ExportType.Xml:
                    generator = new XmlGenerator(_checker);
                    break;
                case ExportType.Json:
                    generator = new JsonGenerator(_checker);
                    break;
                case ExportType.CSharpObject:
                    generator = new CSharpGenerator(_checker);
                    break;
            }

            return generator;
        }

        public static IGenerator CreateGenerator(ExportType type, PropertyAccessibilityChecker checker)
        {
            IGenerator generator = null;

            switch (type)
            {
                case ExportType.Xml:
                    generator = new XmlGenerator(checker);
                    break;
                case ExportType.Json:
                    generator = new JsonGenerator(checker);
                    break;
                case ExportType.CSharpObject:
                    generator = new CSharpGenerator(checker);
                    break;
            }

            return generator;
        }
    }
}
