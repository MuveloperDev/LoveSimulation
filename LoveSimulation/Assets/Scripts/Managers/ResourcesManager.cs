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
    public ResourcesManager() { }
    ~ResourcesManager() { Dispose(); }

    Dictionary<ResourceScope, List<object>> handles = new();

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
        T asset = null;
        Addressables.LoadAssetAsync<T>(path).Completed += handle =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                // �ּ� �ε� ����
                asset = handle.Result;
                handles[scope].Add(handle.Result);
            }
            else
            {
                // �ּ� �ε� ����
                Debug.LogError("Failed to load asset: ");
            }
        };

        await UniTask.WaitUntil(() => null != asset);
        return asset;
    }

    public async UniTask<UnityEngine.GameObject> GameObjectInstantiate(string path, Transform parent, ResourceScope scope = ResourceScope.Global, Action<GameObject> onComplete = null)
    {
        GameObject assetInstance = null;
        bool isProcessing = true;

        if (true == string.IsNullOrEmpty(path))
            return null;

        Addressables.InstantiateAsync(path, parent).Completed += (AsyncOperationHandle<GameObject> handle) =>
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                // �ּ� �ν��Ͻ� ���� ����
                assetInstance = handle.Result;
                // ���⿡�� assetInstance�� ����Ͽ� �ʿ��� ó���� ����
                handles[scope].Add(handle.Result);
            }
            else
            {
                // �ּ� �ν��Ͻ� ���� ����
                Debug.LogError("Failed to instantiate asset: " + handle.OperationException);
            }
            isProcessing = false;
        };

        await UniTask.WaitUntil(() => true != isProcessing);

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
                // �ּ� �ν��Ͻ� ���� ����
                assetInstance = handle.Result;
            }
            else
            {
                // �ּ� �ν��Ͻ� ���� ����
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
