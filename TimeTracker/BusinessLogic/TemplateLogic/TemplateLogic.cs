using System.IO;

namespace TimeTracker.BusinessLogic
{
    public static class TemplateLogic
    {
        public static string GetTemplate(string templateName)
        {
            var rootAssembly = System.Reflection.Assembly.GetExecutingAssembly();

            var assemblyDirectory = Path.GetDirectoryName(rootAssembly.Location);

            var filePath = Path.Combine(assemblyDirectory, "BusinessLogic", "TemplateLogic", "Templates", templateName + ".cshtml");

            var template = File.ReadAllText(filePath);

            return template;
        }
    }
}
