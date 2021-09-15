using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Gameplay.Unit.UnitDatabases;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;

public class UnitEditor : OdinMenuEditorWindow
{
    [MenuItem("Tools/Unit Data Editor")]
    private static void OpenWindow()
    {
        GetWindow<UnitEditor>().Show();
    }
    private CreateNewCombatUnitData _createNewCombatUnitData;
    private CreateNewUtilityUnitData _createNewCombatUtilityData;
    protected override void OnDestroy()
    {
        base.OnDestroy();
        
        if(_createNewCombatUnitData != null)
            DestroyImmediate(_createNewCombatUnitData.CombatUnitData);
        
        if(_createNewCombatUtilityData != null)
            DestroyImmediate(_createNewCombatUtilityData.UtilityUnitData);
    }
    
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();
        
        _createNewCombatUnitData = new CreateNewCombatUnitData();
        _createNewCombatUtilityData = new CreateNewUtilityUnitData();
        
        
        tree.Add("Create New CombatUnit",_createNewCombatUnitData);
        tree.Add("Create New UtilityUnit",_createNewCombatUtilityData);
        
        
        tree.AddAllAssetsAtPath("Combat Unit Data's", "Assets/_Project/ScriptableObjects/Units", typeof(CombatUnitDataBase));
        tree.AddAllAssetsAtPath("Utility Unit Data's", "Assets/_Project/ScriptableObjects/Units", typeof(UtilityUnitDatabase));
        
        return tree;
    }

    

    public class CreateNewCombatUnitData
    {
        public CreateNewCombatUnitData()
        {
            CombatUnitData = ScriptableObject.CreateInstance<CombatUnitDataBase>();
            CombatUnitData.unitName = "New Unit";
            
        }
        
        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public CombatUnitDataBase CombatUnitData;

        [Button("Add new Combat Unit SO")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(CombatUnitData, "Assets/_Project/ScriptableObjects/Units/"+ CombatUnitData.unitName  + ".asset");
            AssetDatabase.SaveAssets();
            
            //create new Instance of the SO
            CombatUnitData = ScriptableObject.CreateInstance<CombatUnitDataBase>();
            CombatUnitData.unitName = "New Combat Unit";
        }
    }
    
    public class CreateNewUtilityUnitData
    {
        public CreateNewUtilityUnitData()
        {
            UtilityUnitData = ScriptableObject.CreateInstance<UtilityUnitDatabase>();
            UtilityUnitData.unitName = "New Unit";
            
        }
        
        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public UtilityUnitDatabase UtilityUnitData;

        [Button("Add new Combat Unit SO")]
        private void CreateNewData()
        {
            AssetDatabase.CreateAsset(UtilityUnitData, "Assets/_Project/ScriptableObjects/Units/"+ UtilityUnitData.unitName  + ".asset");
            AssetDatabase.SaveAssets();
            
            //create new Instance of the SO
            UtilityUnitData = ScriptableObject.CreateInstance<UtilityUnitDatabase>();
            UtilityUnitData.unitName = "New Utility Unit";
        }
    }
}
