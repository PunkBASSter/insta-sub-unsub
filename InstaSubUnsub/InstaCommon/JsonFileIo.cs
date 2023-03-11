using System.Text.Json;

namespace InstaCommon
{
    public class JsonFileIo
    {
        public static void Write<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            using var writer = new StreamWriter(filePath, append);
            var contentsToWriteToFile = JsonSerializer.Serialize(objectToWrite);
            writer.Write(contentsToWriteToFile);
        }

        public static T? Read<T>(string filePath) where T : new()
        {
            using var reader = new StreamReader(filePath);
            var fileContents = reader.ReadToEnd();
            return JsonSerializer.Deserialize<T>(fileContents);
        }
    }
}
