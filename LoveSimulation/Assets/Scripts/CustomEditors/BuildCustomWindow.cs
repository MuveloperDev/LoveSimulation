#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Build;
using UnityEditor.AddressableAssets;
using UnityEditor;
using UnityEngine;

public class BuildCustomWindow : EditorWindow
{
    // â�� ũ�⸦ �����մϴ�.
    private static readonly Vector2 WindowSize = new Vector2(800, 600);

    // ��ư�� ũ�⸦ �����մϴ�.
    private static readonly GUILayoutOption ButtonWidth = GUILayout.Width(200);
    private static readonly GUILayoutOption ButtonHeight = GUILayout.Height(50);

    // ��ư�� ������ �����մϴ�.
    private static readonly GUILayoutOption ButtonPadding = GUILayout.ExpandWidth(false);
    // ��ư�� ũ��� ��ġ�� �����մϴ�.
    private static readonly Rect ButtonRect = new Rect(100, 100, 200, 50);

    // ��ư�� ��Ÿ���� �����մϴ�.
    private static GUIStyle buttonStyle = null;
    // ���� Ÿ�� �÷����� ������ �����Դϴ�.
    private BuildTarget currentTarget;
    [MenuItem("Custom/Build Window")]
    public static void ShowWindow()
    {
        // ���ο� ������ â�� �����ϰ� �����ݴϴ�.
        GetWindow<BuildCustomWindow>("Custom Window");


    }
    public static void AssignPrefabsToAddressables()
    {
        // AddressableAssetSettings�� �ε��մϴ�.
        AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.Settings;

        // Resources ������ �ִ� ��� �������� ã���ϴ�.
        string[] prefabGUIDs = AssetDatabase.FindAssets("t:Prefab", new[] { "Assets/Resources" });

        foreach (string guid in prefabGUIDs)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);

            // �̹� Addressable�� ��ϵ� �������� Ȯ���մϴ�.
            if (settings.FindAssetEntry(guid) == null)
            {
                // Addressable�� ��ϵ��� ���� �����̶��, ���� ����մϴ�.
                AddressableAssetEntry entry = settings.CreateOrMoveEntry(guid, settings.DefaultGroup);
                entry.address = assetPath;
            }
        }

        // ���� ������ �����մϴ�.
        settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, null, true, true);
    }
    private static void BuildAddressables()
    {
        AddressableAssetSettings.BuildPlayerContent();
    }

    private void OnEnable()
    {
        // ���� Ÿ�� �÷����� �����ɴϴ�.
        currentTarget = EditorUserBuildSettings.activeBuildTarget;
    }
    private void OnGUI()
    {
        GUILayout.Space(20);
        GUILayout.Label("", GUI.skin.horizontalSlider);
        GUILayout.Space(10);

        CreateBuildSetting();

        GUILayout.Space(10);
        GUILayout.Label("", GUI.skin.horizontalSlider);
        GUILayout.Space(10);

        // ��Ÿ���� ������ ���̺��� ����ϴ�.
        CreateAddressable();

        GUILayout.Space(10);
        GUILayout.Label("", GUI.skin.horizontalSlider);
    }

    private void CreateBuildSetting()
    {
        GUILayout.Label("Build Setting", FontStyle(), GUILayout.ExpandWidth(true));
        GUILayout.Space(10);
        // Ÿ�� �÷����� ������ �� �ִ� ��Ӵٿ� �޴��� ����ϴ�.
        currentTarget = (BuildTarget)EditorGUILayout.EnumPopup("Target Platform", currentTarget);
        GUILayout.Space(20);

        if (GUILayout.Button("Change Platform"))
        {
            // ������ �÷������� �����մϴ�.
            EditorUserBuildSettings.SwitchActiveBuildTarget(BuildPipeline.GetBuildTargetGroup(currentTarget), currentTarget);
        }
    }

    private void CreateAddressable()
    {
        GUILayout.Label("Addressable", FontStyle(), GUILayout.ExpandWidth(true));

        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Clear And Load", ButtonWidth, ButtonHeight, ButtonPadding))
        {
            AssignPrefabsToAddressables();
            Debug.Log("Button clicked!");
        }
        GUILayout.Space(200);
        if (GUILayout.Button("Build", ButtonWidth, ButtonHeight, ButtonPadding))
        {
            BuildAddressables();
            Debug.Log("Button clicked!");
        }
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
    }

    private GUIStyle FontStyle()
    {
        GUISkin skin = GUI.skin;
        GUIStyle style = new GUIStyle(skin.label);
        style.normal.textColor = Color.gray;
        style.fontSize = 20;
        style.alignment = TextAnchor.MiddleLeft;
        return style;
    }
}
#endif