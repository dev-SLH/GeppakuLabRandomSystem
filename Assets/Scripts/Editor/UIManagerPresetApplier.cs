using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEditor.Presets; // LINQ 사용

public static class UIManagerPresetApplier
{
    [MenuItem("Tools/Modern UI Pack/Apply UIManager Preset")]
    public static void ApplyUIManagerPreset()
    {
        // 프리셋과 스크립터블 오브젝트 이름 및 경로 설정
        string searchFolder = "Assets/Modern UI Pack/Resources/";
        string presetName = "UIManager";
        string scriptableObjectName = "MUIP Manager";

        // 프리셋 검색 및 이름 비교
        string[] presetGuids = AssetDatabase.FindAssets("t:Preset", new[] { "Assets" });
        var matchingPresetPath = presetGuids
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
            .FirstOrDefault(path => System.IO.Path.GetFileNameWithoutExtension(path) == presetName);

        if (matchingPresetPath == null)
        {
            Debug.LogError($"Preset with the exact name '{presetName}' not found in {searchFolder}");
            return;
        }

        var preset = AssetDatabase.LoadAssetAtPath<Preset>(matchingPresetPath);

        // ScriptableObject 검색 및 이름 비교
        string[] managerGuids = AssetDatabase.FindAssets("t:ScriptableObject", new[] { searchFolder });
        var matchingManagerPath = managerGuids
            .Select(guid => AssetDatabase.GUIDToAssetPath(guid))
            .FirstOrDefault(path => System.IO.Path.GetFileNameWithoutExtension(path) == scriptableObjectName);

        if (matchingManagerPath == null)
        {
            Debug.LogError($"ScriptableObject with the exact name '{scriptableObjectName}' not found in {searchFolder}");
            return;
        }

        var muipManager = AssetDatabase.LoadAssetAtPath<ScriptableObject>(matchingManagerPath);

        // 프리셋과 ScriptableObject 확인 후 적용
        if (preset != null && muipManager != null)
        {
            preset.ApplyTo(muipManager);
            EditorUtility.SetDirty(muipManager);
            AssetDatabase.SaveAssets();
            Debug.Log($"Successfully applied '{presetName}' to '{scriptableObjectName}'");
        }
        else
        {
            Debug.LogError("Failed to apply the preset. Please check the file names and paths.");
        }
    }
}
