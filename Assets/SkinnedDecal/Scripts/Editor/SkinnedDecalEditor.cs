using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace SkinnedDecals
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(SkinnedDecal))]
	public class SkinnedDecalEditor : Editor
	{
		private Texture2D headerTexture;

		private MaterialEditor _materialEditor;
		private Material _material;

		private SerializedProperty material;
		private SerializedProperty size;
		private SerializedProperty randomSize;
		private SerializedProperty minSize;
		private SerializedProperty maxSize;
		private SerializedProperty normalClip;
		private SerializedProperty randomRotation;
		
		private SerializedProperty selectedAtlasItem;
		private SerializedProperty atlasItemCount;
		private SerializedProperty randomFromAtlas;

		private void OnEnable()
		{
			SkinnedDecal decal = (SkinnedDecal)target;

			// header
			headerTexture = (Texture2D)EditorGUIUtility.Load("SkinnedDecalsHeader256.jpg");

			// settings
			material = serializedObject.FindProperty("material");
			
			selectedAtlasItem = serializedObject.FindProperty("selectedAtlasItem");
			atlasItemCount = serializedObject.FindProperty("atlasItemCount");
			randomFromAtlas = serializedObject.FindProperty("randomFromAtlas");

			size = serializedObject.FindProperty("size");
			randomSize = serializedObject.FindProperty("randomSize");
			minSize = serializedObject.FindProperty("minSize");
			maxSize = serializedObject.FindProperty("maxSize");
			randomRotation = serializedObject.FindProperty("randomRotation");

			normalClip = serializedObject.FindProperty("normalClip");
		}

		public override void OnInspectorGUI()
		{
			SkinnedDecal decal = (SkinnedDecal)target;

			serializedObject.Update();

			// header
			if(headerTexture != null) {
				GUILayout.Label(headerTexture);
			}
			EditorGUILayout.HelpBox("Use this to define a decal type.", MessageType.Info);

			// material
			EditorGUILayout.LabelField("Material", EditorStyles.boldLabel);
			EditorGUIUtility.labelWidth = 160;
			EditorGUIUtility.fieldWidth = 120;
			material.objectReferenceValue = EditorGUILayout.ObjectField("Material", material.objectReferenceValue, typeof(Material), false);

			// size
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Size", EditorStyles.boldLabel);
			EditorGUI.BeginDisabledGroup(randomSize.boolValue);
			size.floatValue = EditorGUILayout.FloatField("Size", size.floatValue);
			EditorGUI.EndDisabledGroup();
			randomSize.boolValue = EditorGUILayout.Toggle("Random size", randomSize.boolValue);
			EditorGUI.BeginDisabledGroup(!randomSize.boolValue);
			minSize.floatValue = EditorGUILayout.FloatField("Min size", minSize.floatValue);
			maxSize.floatValue = EditorGUILayout.FloatField("Max size", maxSize.floatValue);
			EditorGUI.EndDisabledGroup();

			// normal clip
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Normal clip", EditorStyles.boldLabel);
			normalClip.floatValue = EditorGUILayout.FloatField("Normal clip", normalClip.floatValue);
			EditorGUILayout.HelpBox("-1 = front and back, 0 = front only", MessageType.Info);

			// atlas
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Atlas", EditorStyles.boldLabel);
			selectedAtlasItem.intValue = EditorGUILayout.IntField("Selected item", selectedAtlasItem.intValue);
			atlasItemCount.intValue = EditorGUILayout.IntField("Atlas item count", atlasItemCount.intValue);
			randomFromAtlas.boolValue = EditorGUILayout.Toggle("Select random item", randomFromAtlas.boolValue);
			EditorGUILayout.HelpBox("These apply only if you use atlas shader.", MessageType.Info);

			// rotation
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Rotation", EditorStyles.boldLabel);
			randomRotation.boolValue = EditorGUILayout.Toggle("Random rotation", randomRotation.boolValue);
			EditorGUILayout.HelpBox("Warning, rotation does not rotate normal maps correctly.", MessageType.Warning);

			// apply
			serializedObject.ApplyModifiedProperties();

			// if material has changed
			if(decal.material != _material)
			{
				if(_materialEditor != null)
					DestroyImmediate(_materialEditor);
				if(decal.material != null)
					_materialEditor = (MaterialEditor)CreateEditor(decal.material);

				_material = decal.material;
			}

			// material inspector
			if(_materialEditor != null)
			{
				_materialEditor.DrawHeader();

				bool isDefaultMaterial = !AssetDatabase.GetAssetPath(decal.material).StartsWith("Assets");
				using(new EditorGUI.DisabledGroupScope(isDefaultMaterial))
				{
					_materialEditor.OnInspectorGUI();
				}
			}
		}
	}
}