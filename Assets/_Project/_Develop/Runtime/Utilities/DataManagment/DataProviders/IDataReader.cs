using Assets.Project._Develop.Runtime.Utilities.DataManagment.KeysStorage;

namespace Assets.Project._Develop.Runtime.Utilities.DataManagment.DataProviders
{
    public interface IDataReader<TData> where TData : ISaveData
    {
        void ReadFrom(TData data);
    }
}