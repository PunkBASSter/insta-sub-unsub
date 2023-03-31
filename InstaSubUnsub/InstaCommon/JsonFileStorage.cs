using InstaInfrastructureAbstractions;
using System.Text.Json;

namespace InstaCommon
{
    public class JsonFileStorage<T> : IKeyValueObjectStorage<T>
    {
        public void Write(string filePath, T objectToWrite)
        {
            using var writer = new StreamWriter(filePath);
            var contentsToWriteToFile = JsonSerializer.Serialize(objectToWrite);
            writer.Write(contentsToWriteToFile);
        }

        public T? Read(string filePath)
        {
            if (!File.Exists(filePath))
                return default;

            using var reader = new StreamReader(filePath);
            var fileContents = reader.ReadToEnd();
            return JsonSerializer.Deserialize<T>(fileContents);
        }

        public void Delete(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            File.Delete(filePath);
        }
    }
}
