using InstaInfrastructureAbstractions;
using InstaInfrastructureAbstractions.PersistenceInterfaces;
using InstaPersistence.Utils;
using System.Text.Json;

namespace InstaCommon
{
    public class JsonDbStorage<T> : IKeyValueObjectStorage<T>
    {
        private readonly IRepository _repository;

        public JsonDbStorage(IRepository repo)
        {
            _repository= repo;
        }

        public T? Read(string key)
        {
            var qRes = _repository.Query<KeyValueJson>().FirstOrDefault(o => o.Key == key);
            if (qRes == null || qRes.Value == null)
                return default;

            var obj = JsonSerializer.Deserialize<T>(qRes.Value);
            return obj;
        }

        public void Write(string key, T objectToWrite)
        {
            var json = JsonSerializer.Serialize(objectToWrite);

            var dbRecord = new KeyValueJson { Key = key, Value = json };
            _repository.InsertOrUpdate(dbRecord, i => i.Key == dbRecord.Key);
            _repository.SaveChanges();
        }

        public void Delete(string key)
        {
            var obj = _repository.TrackedQuery<KeyValueJson>().FirstOrDefault(o => o.Key == key);
            if (obj == null)
                return;
            _repository.Delete(obj);
            _repository.SaveChanges();
        }
    }
}
