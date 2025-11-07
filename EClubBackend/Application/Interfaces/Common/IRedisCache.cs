namespace E_Club.Application.Interfaces.Common
{
    public interface IRedisCache
    {
        T? GetData<T>(string key);
        void SetData<T>(string key, T data);
        bool? RemoveData<T>(string key);
    }
}
