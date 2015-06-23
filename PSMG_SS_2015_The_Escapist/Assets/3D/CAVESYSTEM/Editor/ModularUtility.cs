using UnityEngine;
using UnityEditor;
using System.Collections;
using UnityEditor.AnimatedValues;

public class ModularUtility : EditorWindow  {


	public GameObject prefab1;
	private string i_path;
	private Color colorPrefab;
	private MeshRenderer[] childGO;
	private Texture textureSet1;
	private Texture textureSet2;
	private Texture texturePbr;
	private int count1; 
	private int count2;
	[MenuItem("Window/CaveSystem Texture Utility", false, 1050)]
	public static void ShowWindow()
	{

		EditorWindow.GetWindow(typeof(ModularUtility));	
		
	}

	void OnEnable()
	{
		i_path = "Assets/CaveSystem";

	}

	void OnGUI()
	{
	
	//	EditorGUILayout.BeginHorizontal();
		GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});
		GUILayout.Label("Cave model");
		if (prefab1 != null)
		{
			colorPrefab = Color.green;
		}
		else
		{
			colorPrefab = Color.red;
		}
		GUI.backgroundColor = colorPrefab;

		prefab1 = EditorGUILayout.ObjectField(prefab1, typeof(GameObject), true) as GameObject;

		EditorGUILayout.BeginHorizontal();
		GUI.backgroundColor = Color.white;
		if (GUILayout.Button(new GUIContent("SET 1","Texture set 1"),GUILayout.Width(45),GUILayout.Height(32)))
		{

			childGO = prefab1.GetComponentsInChildren<MeshRenderer>();
			count1 = 0;
			foreach(MeshRenderer ms in childGO)
			{
			
				for (int i = 0; i < ms.sharedMaterials.Length; i++)
				{

					string textureName = "/Texture/" + ms.sharedMaterials[i].name + "Set1" + ".png";
					string pbrName = "/Texture/Maps/" + ms.sharedMaterials[i].name + "pbr" + ".png";
					textureSet1    = AssetDatabase.LoadAssetAtPath(i_path+textureName,typeof(Texture)) as Texture;
					texturePbr    = AssetDatabase.LoadAssetAtPath(i_path+pbrName,typeof(Texture)) as Texture;
					if (textureSet1 != null)
					{
					ms.sharedMaterials[i].SetTexture("_MainTex", textureSet1);
					ms.sharedMaterials[i].SetTexture("_MetallicGlossMap", texturePbr);
					ms.sharedMaterials[i].SetFloat("_Glossiness",0);
					count1++;
					}
				}


				

			}
			//EditorUtility.ClearProgressBar();
			EditorUtility.DisplayDialog("Operation completed","Set 1 textures applied", "Ok");
		}

		if (GUILayout.Button(new GUIContent("SET 2","Texture set 2"),GUILayout.Width(45),GUILayout.Height(32)))
		{
			childGO = prefab1.GetComponentsInChildren<MeshRenderer>();
			count1 = 0;
			foreach(MeshRenderer ms in childGO)
			{
				
				for (int i = 0; i < ms.sharedMaterials.Length; i++)
				{
					string textureName = "/Texture/Set2/" + ms.sharedMaterials[i].name + "Set2" + ".png";
					textureSet2    = AssetDatabase.LoadAssetAtPath(i_path+textureName,typeof(Texture)) as Texture;
					if (textureSet2 != null)
					{
						ms.sharedMaterials[i].SetTexture("_MainTex", textureSet2);
						ms.sharedMaterials[i].SetTexture("_MetallicGlossMap", null);
					    ms.sharedMaterials[i].SetFloat("_Glossiness",0.6f);
					    count1++;
					}
				}

				
				
			}

			EditorUtility.DisplayDialog("Operation completed","Set 2 textures applied", "Ok");

		}



		GUI.backgroundColor = Color.white;
		EditorGUILayout.EndHorizontal();
		GUILayout.Box("", new GUILayoutOption[]{GUILayout.ExpandWidth(true), GUILayout.Height(1)});

		
	} //FINE ON GUI

	void OnInspectorUpdate()
	{


	}
	void LoadTexture()
	{
		//button_set1 = AssetDatabase.LoadAssetAtPath(i_path+"/Utility/buttonTexture/button_tree.png",typeof(Texture)) as Texture;
		//button_fx = AssetDatabase.LoadAssetAtPath(i_path+"/Utility/buttonTexture/fx.jpg",typeof(Texture)) as Texture;

		 
	}
	


}
