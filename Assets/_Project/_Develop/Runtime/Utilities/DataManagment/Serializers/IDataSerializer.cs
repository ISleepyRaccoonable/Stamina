
namespace Assets.Project._Develop.Runtime.Utilities.DataManagment.Serializers
{
    public interface IDataSerializer
    {
        string Serialize<TData>(TData data);

        TData Desirialize<TData>(string serializedData);
    }
}