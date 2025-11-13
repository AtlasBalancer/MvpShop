using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Object = UnityEngine.Object;

namespace com.ab.mvpshop.core.playerdata
{
    public class SceneLoaderService : IInitializable
    {
        readonly Settings _settings;
        readonly RectTransform _root;
        readonly PreloaderView _preloader;
        readonly ZenjectSceneLoader _zenjectLoader;

        public SceneLoaderService(
            Settings settings,
            ZenjectSceneLoader zenjectSceneLoader)
        {
            _settings = settings;
            _zenjectLoader = zenjectSceneLoader;

            _root = Object.Instantiate(_settings.PreloaderRootPrefab);
            Object.DontDestroyOnLoad(_root);
            
            _preloader = Object.Instantiate(_settings.PreloaderPrefab, _root.transform);
            _preloader.Active(false);
        }

        public void Initialize() => 
            LoadAsync(_settings.BootSceneKey);

        public async UniTask LoadAsync(string sceneName,
            LoadSceneMode mode = LoadSceneMode.Single,
            Action<DiContainer> extraBindings = null,
            IProgress<float> progress = null,
            CancellationToken ct = default)
        {
            _preloader.Active(true);
            float showTime = Time.realtimeSinceStartup;

            var operation = _zenjectLoader.LoadSceneAsync(sceneName, mode, extraBindings: extraBindings);
            while (!operation.isDone)
            {
                ct.ThrowIfCancellationRequested();
                progress?.Report(operation.progress);
                await UniTask.Yield();
            }

            float elapsed = Time.realtimeSinceStartup - showTime;
            float wait = _settings.MinShowPreloaderDelay - elapsed;
            if (wait > 0)
                await UniTask.Delay(TimeSpan.FromSeconds(wait), cancellationToken: ct);

            _preloader.Active(false);

            progress?.Report(1f);
            ct.ThrowIfCancellationRequested();
        }

        [Serializable]
        public class Settings
        {
            public string BootSceneKey;
            public float MinShowPreloaderDelay;

            public PreloaderView PreloaderPrefab;
            public RectTransform PreloaderRootPrefab;
        }
    }
}