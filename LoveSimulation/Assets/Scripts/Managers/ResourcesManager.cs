using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Enum;
public class ResourcesManager : Singleton<ResourcesManager>
{
    public ResourcesManager() { Initilized(); }
    ~ResourcesManager() { Dispose(); }

    Dictionary<ResourceScope, List<object>> handles = new();

    private void Initilized()
    {
        foreach (var scope in System.Enum.GetValues(typeof(ResourceScope)))
        {
            handles[(ResourceScope)scope] = new();
        }
    }

    private void Dispose()
    {
        ReleaseAll();
        handles.Clear();
        handles = null;
    }

    private void ReleaseAll()
    {
        foreach (var list in handles.Values)
        {
            foreach (var handle in list)
            {
                Addressables.Release(handle);
            }
        }
    }

    public void ReleaseScope(ResourceScope scope)
    {
        foreach (var handle in handles[scope])
        {
            Addressables.Release(handle);
        }
    }

    public async UniTask<T> LoadAsset<T>(string path, ResourceScope scope = ResourceScope.Global) where T : class
    {
        GameObject asset = null;
        bool isProcessed = false;
        Addressables.LoadAssetAsync<GameObject>(path).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                // 애셋 로드 성공
                asset = handle.Result;
                handles[scope].Add(handle.Result);
            }
            else
            {
                // 애셋 로드 실패
                Debug.LogError("Failed to load asset: ");
            }
            isProcessed = true;
        };


        await UniTask.WaitUntil(() => false != isProcessed);
        if (null == asset)
            return default;

        T type = null;
        if (true == asset.TryGetComponent<T>(out type))
        {
            return type;
        }
        return default;
    }

    public async UniTask<UnityEngine.GameObject> GameObjectInstantiate(string path, Transform parent, ResourceScope scope = ResourceScope.Global, Action<GameObject> onComplete = null)
    {
        GameObject assetInstance = null;
        bool isProcessed = false;

        if (true == string.IsNullOrEmpty(path))
            return null;

        Addressables.InstantiateAsync(path, parent).Completed += (AsyncOperationHandle<GameObject> handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                // 애셋 인스턴스 생성 성공
                assetInstance = handle.Result;
                // 여기에서 assetInstance를 사용하여 필요한 처리를 수행
                handles[scope].Add(handle.Result);
            }
            else
            {
                // 애셋 인스턴스 생성 실패
                Debug.LogError("Failed to instantiate asset: " + handle.OperationException);
            }
            isProcessed = true;
        };

        await UniTask.WaitUntil(() => false != isProcessed);

        if (onComplete != null)
        {
            onComplete(assetInstance);
        }
        if (assetInstance != null)
        {
            return assetInstance;
        }
        return null;
    }

    public async UniTask<T> Instantiate<T>(string path, Action<T> onComplete = null) where T : class
    {
        GameObject assetInstance = null;
        bool isProcessing = true;

        Addressables.InstantiateAsync(path).Completed += (AsyncOperationHandle<GameObject> handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                // 애셋 인스턴스 생성 성공
                assetInstance = handle.Result;
            }
            else
            {
                // 애셋 인스턴스 생성 실패
                Debug.LogError("Failed to instantiate asset: " + handle.OperationException);
            }
            isProcessing = false;
        };

        await UniTask.WaitUntil(() => true != isProcessing);

        if (typeof(GameObject) == typeof(T))
        {
            if (onComplete != null)
            {
                onComplete(assetInstance as T);
            }
            return assetInstance as T;
        }

        if (assetInstance.TryGetComponent<T>(out T component))
        {
            if (onComplete != null)
            {
                onComplete(component);
            }
            return component;
        }
        return null;
    }


}
