using System.IO;

namespace TimeTracker.BusinessLogic
{
    public static class TemplateLogic
    {
        public static string GetTemplate(string templateName)
        {
            var rootPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            var filePath = Path.Combine(rootPath, "BusinessLogic", "Templates", templateName + ".cshtml");

            var template = File.ReadAllText(filePath);

            return template;
        }
    }
}
