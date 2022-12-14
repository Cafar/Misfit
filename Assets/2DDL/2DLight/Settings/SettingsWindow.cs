#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

public class SettingsWindow : EditorWindow {


	SerializedObject settingProfileAsset;
	int selectableLayerField;
	int selectableLayerMask;

	GUIStyle style1;
	GUIStyle style0;


	[MenuItem("2DDL/Settings")]
	public static void Init()
	{

		//window = (DynamicLightAboutWindow )EditorWindow.GetWindow(typeof(DynamicLightAboutWindow));
		EditorWindow.GetWindow( typeof(SettingsWindow), true, "2DDL settings" );
		//Debug.Log ("Init");asda

	}

	void OnEnable(){
		settingProfileAsset = new SerializedObject(SettingsManager.LoadMainSettings());
		selectableLayerField = AssetUtility.LoadPropertyAsInt ("layerCaster", settingProfileAsset);
		selectableLayerMask = AssetUtility.LoadPropertyAsInt("layerMask", settingProfileAsset);


	}

	void OnGUI(){

		style1 = new GUIStyle(GUI.skin.label);
		style1.fontSize = 14;
		style1.fontStyle = FontStyle.Bold;
		style1.alignment = TextAnchor.MiddleLeft;

		style0 = new GUIStyle(GUI.skin.label);
		style0.fontSize = 10;
		style0.fontStyle = FontStyle.Italic;
		style0.alignment = TextAnchor.UpperLeft;
		style0.fixedHeight = 40f;
				style0.normal.textColor = Color.cyan;
		

	
		EditorGUILayout.Separator ();

		EditorGUILayout.LabelField ("2DDL Settings", style1);

		EditorGUILayout.Separator ();
		EditorGUILayout.Separator ();

		EditorGUILayout.LabelField ("Note: This values are setting up on every new \n 2DLight or Caster creation.", style0);

		EditorGUILayout.Separator ();
		EditorGUILayout.Separator ();
		EditorGUILayout.Separator ();

		EditorGUILayout.LabelField ("CASTERS: Layer by Default");
		selectableLayerField = EditorGUILayout.LayerField ("", selectableLayerField);

		EditorGUILayout.Separator ();

		EditorGUILayout.LabelField ("2D LIGHT: LayerMask by Default");
		selectableLayerMask = LayerMaskField("", selectableLayerMask);

		AssetUtility.SaveProperty ("layerCaster",selectableLayerField, settingProfileAsset);

		EditorGUILayout.Separator ();

		AssetUtility.SaveProperty ("layerMask",selectableLayerMask, settingProfileAsset);
	}

	private LayerMask LayerMaskField( string label, LayerMask layerMask) {
		List<string> layers = new List<string>();
		List<int> layerNumbers = new List<int>();
		
		for (int i = 0; i < 32; i++) {
			string layerName = LayerMask.LayerToName(i);
			if (layerName != "") {
				layers.Add(layerName);
				layerNumbers.Add(i);
			}
		}
		int maskWithoutEmpty = 0;
		for (int i = 0; i < layerNumbers.Count; i++) {
			if (((1 << layerNumbers[i]) & layerMask.value) > 0)
				maskWithoutEmpty |= (1 << i);
		}
		maskWithoutEmpty = EditorGUILayout.MaskField( label, maskWithoutEmpty, layers.ToArray());
		int mask = 0;
		for (int i = 0; i < layerNumbers.Count; i++) {
			if ((maskWithoutEmpty & (1 << i)) > 0)
				mask |= (1 << layerNumbers[i]);
		}
		layerMask.value = mask;
		return layerMask;
	}
	
	
}

#endif
