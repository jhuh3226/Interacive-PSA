using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SkinnedDecals
{
	[CustomEditor(typeof(SkinnedDecalMesh))]
	public class SkinnedDecalMeshEditor : Editor
	{
		private Texture2D headerTexture;

		private void OnEnable()
		{
			SkinnedDecalMesh decal = (SkinnedDecalMesh)target;

			// header
			headerTexture = (Texture2D)EditorGUIUtility.Load("SkinnedDecalsHeader256.jpg");
		}

		public override void OnInspectorGUI()
		{
			SkinnedDecalMesh decal = (SkinnedDecalMesh)target;

			serializedObject.Update();

			// header
			if(headerTexture != null) {
				GUILayout.Label(headerTexture);
			}

			EditorGUILayout.HelpBox("Vertices: " + decal.decalVertices.Count + '\n' + "Triangles: " + (decal.decalTriangles.Count / 3), MessageType.Info);

			EditorGUILayout.HelpBox("You can use this component to save meshes to assets in editor.", MessageType.Info);

			// apply
			serializedObject.ApplyModifiedProperties();

			if(GUILayout.Button("Save to Assets...")) {
				string path = EditorUtility.SaveFilePanel("Save skinned mesh", "Assets", "NewDecalMesh", "");
				if(path.Length != 0) {
					int assetsStart = path.IndexOf("Assets");
					path = path.Substring(assetsStart, path.Length - assetsStart);
					Mesh mesh = new Mesh();
					mesh = decal.decalMesh;
					AssetDatabase.CreateAsset(mesh, path + ".asset");

					/*GameObject newGameObject = new GameObject(decal.gameObject.name);
					SkinnedMeshRenderer smr = newGameObject.AddComponent<SkinnedMeshRenderer>();
					smr.sharedMesh = mesh;
					GameObject root = Instantiate(decal.decalSmr.rootBone.gameObject, newGameObject.transform);
					PrefabUtility.CreatePrefab(path + ".prefab", newGameObject);*/

					AssetDatabase.Refresh();

					decal.decalSystem.Clear();
				}
			}
		}
	}
}