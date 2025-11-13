using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace com.ab.mvpshop.core.assetlaod
{
    public class AddressableService : IAddressableService
    {
        public T Load<T>(string key) => 
            LoadAsync<T>(key).WaitForCompletion();

        public AsyncOperationHandle<T> LoadAsync<T>(string key) => 
            Addressables.LoadAssetAsync<T>(key);
    }
}