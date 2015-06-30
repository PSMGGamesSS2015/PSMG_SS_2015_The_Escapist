using UnityEngine;
using UnityEditor;
// CopyComponents - by Michael L. Croswell for Colorado Game Coders, LLC
// March 2010
//Modified by Kristian Helle Jespersen
//June 2011
//Modified by Connor Cadellin McKee for Excamedia
//April 2015
//Modified by Fernando Medina (fermmmm)
//April 2015
public class ReplaceGameObjects : ScriptableWizard {

    public GameObject Prefab;
    public GameObject[] ObjectsToReplace;
    public bool KeepOriginalNames = false;
    [MenuItem("Custom/Replace GameObjects")]

    static void CreateWizard()
    {
        var replaceGameObjects = DisplayWizard<ReplaceGameObjects>("Replace GameObjects", "Replace");
        replaceGameObjects.ObjectsToReplace = Selection.gameObjects;
    }

    void OnWizardCreate()
    {
        foreach (GameObject go in ObjectsToReplace)
        {
            GameObject newObject;
            newObject = (GameObject)PrefabUtility.InstantiatePrefab(Prefab);
            newObject.transform.SetParent(go.transform.parent, true);
            newObject.transform.localPosition = go.transform.localPosition;
            newObject.transform.localRotation = go.transform.localRotation;
            newObject.transform.localScale = go.transform.localScale;
            if (KeepOriginalNames) newObject.transform.name = go.transform.name;
            Undo.DestroyObjectImmediate(go);
        }
    }
}