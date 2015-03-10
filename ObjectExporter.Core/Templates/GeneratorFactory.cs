using ObjectExporter.Core.Globals;

namespace ObjectExporter.Core.Templates
{
    public class GeneratorFactory
    {
        private readonly ExportType _type;

        public GeneratorFactory(ExportType type)
        {
            _type = type;
        }

        public IGenerator Create()
        {
            IGenerator generator = null;

            switch (_type)
            {
                case ExportType.Xml:
                    generator = new XmlGenerator();
                    break;
                case ExportType.Json:
                    generator = new JsonGenerator();
                    break;
                case ExportType.CSharpObject:
                    generator = new CSharpGenerator();
                    break;
            }

            return generator;
        }

        public static IGenerator CreateGenerator(ExportType type)
        {
            IGenerator generator = null;

            switch (type)
            {
                case ExportType.Xml:
                    generator = new XmlGenerator();
                    break;
                case ExportType.Json:
                    generator = new JsonGenerator();
                    break;
                case ExportType.CSharpObject:
                    generator = new CSharpGenerator();
                    break;
            }

            return generator;
        }
    }
}
