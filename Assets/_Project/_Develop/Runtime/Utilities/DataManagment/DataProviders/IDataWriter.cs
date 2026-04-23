using Assets.Project._Develop.Runtime.Utilities.DataManagment.KeysStorage;

namespace Assets.Project._Develop.Runtime.Utilities.DataManagment.DataProviders
{
    public interface IDataWriter<TData> where TData : ISaveData
    {
        void WriteTo(TData data);
    }
}