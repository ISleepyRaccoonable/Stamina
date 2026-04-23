using System;
using System.Collections;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.DataRepository;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.KeysStorage;
using Assets.Project._Develop.Runtime.Utilities.DataManagment.Serializers;

namespace Assets.Project._Develop.Runtime.Utilities.DataManagment
{
    public class SaveLoadService : ISaveLoadService
    {
        private readonly IDataSerializer _serializer;
        private readonly IDataKeysStorage _keysStorage;
        private readonly IDataRepository _repository;

        public SaveLoadService(
            IDataSerializer dataSerializer,
            IDataKeysStorage dataKeysStorage,
            IDataRepository dataRepository)
        {
            _serializer = dataSerializer;
            _keysStorage = dataKeysStorage;
            _repository = dataRepository;
        }

        public IEnumerator Exists<TData>(Action<bool> onExistsResult) where TData : ISaveData
        {
            string key = _keysStorage.GetKeyFor<TData>();

            yield return _repository.Exists(key, result => onExistsResult?.Invoke(result));
        }

        public IEnumerator Load<TData>(Action<TData> onLoad) where TData : ISaveData
        {
            string key = _keysStorage.GetKeyFor<TData>();

            string serializedData = "";

            yield return _repository.Read(key, result => serializedData = result);

            TData deserializedData = _serializer.Desirialize<TData>(serializedData);

            onLoad?.Invoke(deserializedData);
        }

        public IEnumerator Remove<TData>() where TData : ISaveData
        {
            string key = _keysStorage.GetKeyFor<TData>();

            yield return _repository.Remove(key);
        }

        public IEnumerator Save<TData>(TData data) where TData : ISaveData
        {
            string serializedData = _serializer.Serialize(data);
            string key = _keysStorage.GetKeyFor<TData>();
            yield return _repository.Write(key, serializedData);
        }
    }
}