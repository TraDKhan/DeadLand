using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemData))]
public class ItemDataEditor : Editor
{
    SerializedProperty id;
    SerializedProperty itemName;
    SerializedProperty icon;
    SerializedProperty itemType;
    SerializedProperty stackable;
    SerializedProperty worldPrefab;

    // Equipment
    SerializedProperty equipmentType;
    SerializedProperty bonusDamage;
    SerializedProperty bonusDefense;
    SerializedProperty bonusHP;
    SerializedProperty bonusMP;
    SerializedProperty bonusCritChance;
    SerializedProperty bonusCritDamage;

    void OnEnable()
    {
        id = serializedObject.FindProperty("id");
        itemName = serializedObject.FindProperty("itemName");
        icon = serializedObject.FindProperty("icon");
        itemType = serializedObject.FindProperty("itemType");
        stackable = serializedObject.FindProperty("stackable");
        worldPrefab = serializedObject.FindProperty("worldPrefab");

        equipmentType = serializedObject.FindProperty("equipmentType");
        bonusDamage = serializedObject.FindProperty("bonusDamage");
        bonusDefense = serializedObject.FindProperty("bonusDefense");
        bonusHP = serializedObject.FindProperty("bonusHP");
        bonusMP = serializedObject.FindProperty("bonusMP");
        bonusCritChance = serializedObject.FindProperty("bonusCritChance");
        bonusCritDamage = serializedObject.FindProperty("bonusCritDamage");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.PropertyField(id);
        EditorGUILayout.PropertyField(itemName);
        EditorGUILayout.PropertyField(icon);
        EditorGUILayout.PropertyField(itemType);
        EditorGUILayout.PropertyField(stackable);
        EditorGUILayout.PropertyField(worldPrefab);

        // Chỉ hiện khi là Equipment
        ItemData data = (ItemData)target;
        if (data.itemType == ItemType.Equipment)
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Equipment Stats", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(equipmentType);
            EditorGUILayout.PropertyField(bonusDamage);
            EditorGUILayout.PropertyField(bonusDefense);
            EditorGUILayout.PropertyField(bonusHP);
            EditorGUILayout.PropertyField(bonusMP);
            EditorGUILayout.PropertyField(bonusCritChance);
            EditorGUILayout.PropertyField(bonusCritDamage);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
