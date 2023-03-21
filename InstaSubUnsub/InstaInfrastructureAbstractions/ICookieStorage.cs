namespace InstaInfrastructureAbstractions
{
    public interface IKeyValueObjectStorage<T>
    {
        void Write(string key, T objectToWrite);
        T? Read(string key);
    }
}
