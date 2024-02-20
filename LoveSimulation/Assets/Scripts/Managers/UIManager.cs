using Enum;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public bool isExistance { get; private set; }

    public UIManager() { Initialize(); }
    ~UIManager() { Dispose(); }

    private Dictionary<ResourceScope, Dictionary<Type, GameObject>> _resourcesUI = new();
    private Dictionary<UILayer, RectTransform> _inCanvases = new(); 
    private Dictionary<UILayer, RectTransform> _outCanvases = new(); 
    
    private void Initialize()
    { 
        
    }

    private void Dispose()
    { 
    
    }

    private void CreateCanvas()
    {
        var root = new GameObject("Canvases");
        foreach (var layer in System.Enum.GetValues(typeof(UILayer)))
        {
            var canvas = new GameObject($"{nameof(layer)}Canvas", typeof(RectTransform)).GetComponent<RectTransform>();
            var cv = canvas.AddComponent<Canvas>();
            cv.renderMode = RenderMode.ScreenSpaceOverlay;
            
            var cs = canvas.AddComponent<CanvasScaler>();
            cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            cs.referenceResolution = new Vector2(2560, 1440);
            
            var gr = canvas.AddComponent<GraphicRaycaster>();
            gr.ignoreReversedGraphics = true;
            gr.blockingObjects = GraphicRaycaster.BlockingObjects.None;
        }
    }

    public async Task<T> CreateUI<T>(string path, UILayer layer, ResourceScope scope = ResourceScope.Global) where T : Component
    {
        var asset = await ResourcesManager.Instance.Instantiate<T>(path);
        if (asset == null) 
            return null;

        var go = asset.gameObject;

        _resourcesUI[scope][typeof(T)] = go;
        return asset;
    }

    public T GetUI<T>()  where T : class
    {
        foreach (var dictionary in _resourcesUI.Values)
        {
            if (false == dictionary.ContainsKey(typeof(T)))
                continue;

            return dictionary[typeof(T)] as T;
        }

        Debug.LogError("Invaild type. does not exist in _UIsDictionary.");
        return default(T);
    }
}
