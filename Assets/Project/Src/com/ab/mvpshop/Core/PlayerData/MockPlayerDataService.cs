using System;
using UnityEngine;
using System.Collections.Generic;
using com.ab.mvpshop.core.mvp;

namespace com.ab.mvpshop.core.playerdata
{
    public class MockPlayerDataService : IPlayerDataService
    {
        readonly string _keyPersistentPattern;
        const string PERSISTEN_SYFIX = "MockData";

        Dictionary<Type, IModel> _storage = new();

        public MockPlayerDataService(Settings settings) =>
            _keyPersistentPattern = $"{PERSISTEN_SYFIX}_{settings.PersistentDataVersion}_";

        public void Init<TModel>(bool rewerite = false) where TModel : IModel
        {
            var key = typeof(TModel);
            if (_storage.ContainsKey(key))
            {
                if (!rewerite)
                    return;

                _storage.Remove(key);
            }

            var data = LoadData<TModel>(key);

            if (data == null)
                data = Activator.CreateInstance<TModel>();

            _storage.Add(key, data);
        }

        TModel LoadData<TModel>(Type key) where TModel : IModel
        {
            var data = PlayerPrefs.GetString(GetPersistKey(key));
            TModel parsed = JsonUtility.FromJson<TModel>(data);
            return parsed;
        }

        public void Registry<T>(IModel model) =>
            _storage.Add(typeof(T), model);

        public TModel Get<TModel>() where TModel : IModel
        {
            var key = typeof(TModel);
            if (!_storage.ContainsKey(key))
                throw new ArgumentException(
                    $"{nameof(MockPlayerDataService)}::Get({nameof(TModel)}): Can't find type in storage");

            return (TModel)_storage[key];
        }

        public void Commit<T>(T model)
        {
            var key = GetPersistKey(typeof(T));
            PlayerPrefs.SetString(key, ParseToJSONString(model));
        }

        string ParseToJSONString<T>(T model) =>
            JsonUtility.ToJson(model);

        string GetPersistKey(Type t) =>
            $"{_keyPersistentPattern}{t.Name}";

        [Serializable]
        public class Settings
        {
            public string PersistentDataVersion;
        }
    }
}