using UnityEditor;
using UnityEngine;
using PrimitivePlus;
using System.Collections;
using System.Collections.Generic;

namespace PrimitivePlusEditor
{
	[CustomEditor(typeof(PrimitivePlusMaterial)), CanEditMultipleObjects]
	public class PrimitivePlusMaterialEditor : Editor
	{

		private void OnDisable()
		{
			Resources.UnloadUnusedAssets();
		}

		public override void OnInspectorGUI ()
		{
			GameObject[] gameObjects = Selection.gameObjects;
			List<PrimitivePlusMaterial> materials = new List<PrimitivePlusMaterial>();
			Material material = gameObjects[0].GetComponent<MeshRenderer>().sharedMaterial;

			for(int i = 0; i < gameObjects.Length; i++)
			{
				if(gameObjects[i].GetComponent<PrimitivePlusMaterial>() != null)
					materials.Add(gameObjects[i].GetComponent<PrimitivePlusMaterial>());
			}

			GUILayout.Space(10);
			GUILayout.BeginHorizontal();
			GUILayout.Space(10);
			if(materials.Count > 1 && GUILayout.Button("Share Materials", EditorStyles.miniButtonLeft))
			{
				for(int i = 0; i < materials.Count; i++)
				{
					materials[i].SetSharedMaterial(material);
				}
			}
			GUIStyle buttonStyle = (materials.Count > 1) ? EditorStyles.miniButtonRight : EditorStyles.miniButton;
			if(GUILayout.Button("New Material", buttonStyle))
			{
				for(int i = 0; i < materials.Count; i++)
				{
					materials[i].SetNewMaterial();
				}
			}
			GUILayout.Space(15);
			GUILayout.EndHorizontal();
		}
	}
}