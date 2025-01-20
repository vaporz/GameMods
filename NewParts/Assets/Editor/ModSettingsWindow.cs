using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;

public class ModSettingsWindow : EditorWindow
{
    public const string ValueName = "ModName";
    public const string BuildPathName = "ModBuildPath";
    public const string BuildPathValue = "[UnityEngine.Application.dataPath]/../Build";
    public const string LoadPathName = "ModLoadPath";
    public const string LoadPathValue = "{KXRocket.GameManager.RootPath}/Mods/Save/{KXRocket.GameManager.SaveName}/[ModName]/Assets";
    static string ModName = "";

    [MenuItem("Window/Asset Management/Addressables/Mod Settings")]
    public static void ShowWindow()
    {
        string defaultProfileID = GetDefaultProfileID();
        if (defaultProfileID == null)
            return;
        AddressableAssetProfileSettings p = AddressableAssetSettingsDefaultObject.Settings.profileSettings;
        string v = p.GetValueByName(defaultProfileID, ValueName);
        if (v != null)
        {
            ModName = v;
        }
        GetWindow<ModSettingsWindow>("Mod Settings");
    }

    private static string GetDefaultProfileID()
    {
        AddressableAssetProfileSettings p = AddressableAssetSettingsDefaultObject.Settings.profileSettings;
        foreach (var proName in p.GetAllProfileNames())
        {
            if (proName.Equals("Default"))
            {
                return p.GetProfileId(proName);
            }
        }
        return null;
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Mod Name", EditorStyles.boldLabel);
        ModName = EditorGUILayout.TextField("", ModName);
        EditorGUILayout.EndHorizontal();
        if (GUILayout.Button("Save Settings"))
        {
            OnClickSaveSettings();
        }
        if (GUILayout.Button("Build"))
        {
            OnClickBuild();
        }
    }
    private void OnClickSaveSettings()
    {
        string defaultProfileID = GetDefaultProfileID();
        if (defaultProfileID == null)
            return;
        AddressableAssetProfileSettings p = AddressableAssetSettingsDefaultObject.Settings.profileSettings;
        string v = p.GetValueByName(defaultProfileID, ValueName);
        if (v == null)
        {
            p.CreateValue(ValueName, ModName);
        }
        else
        {
            p.SetValue(defaultProfileID, ValueName, ModName);
        }
        v = p.GetValueByName(defaultProfileID, BuildPathName);
        if (v == null)
        {
            p.CreateValue(BuildPathName, BuildPathValue);
        }
        else
        {
            p.SetValue(defaultProfileID, BuildPathName, BuildPathValue);
        }
        v = p.GetValueByName(defaultProfileID, LoadPathName);
        if (v == null)
        {
            p.CreateValue(LoadPathName, LoadPathValue);
        }
        else
        {
            p.SetValue(defaultProfileID, LoadPathName, LoadPathValue);
        }
        AddressableAssetSettingsDefaultObject.Settings.BuildRemoteCatalog = true;
        foreach (var group in AddressableAssetSettingsDefaultObject.Settings.groups)
        {
            var schema = group.GetSchema<BundledAssetGroupSchema>();
            if (group.name.Equals(AddressableAssetSettings.PlayerDataGroupName))
                continue;
            schema.BuildPath.SetVariableByName(AddressableAssetSettingsDefaultObject.Settings, BuildPathName);
            schema.LoadPath.SetVariableByName(AddressableAssetSettingsDefaultObject.Settings, LoadPathName);
        }
    }
    private void OnClickBuild()
    {
        AddressableAssetSettingsDefaultObject.Settings.OverridePlayerVersion = string.Format("{0:yyyyMMdd-HHmmssfff}", DateTime.Now);
        AddressableAssetSettings.BuildPlayerContent();
    }
}
