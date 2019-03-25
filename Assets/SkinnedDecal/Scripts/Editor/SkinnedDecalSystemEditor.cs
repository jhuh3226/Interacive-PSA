using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace SkinnedDecals
{
	[CanEditMultipleObjects]
	[CustomEditor(typeof(SkinnedDecalSystem))]
	public class SkinnedDecalSystemEditor : Editor
	{
		private Texture2D headerTexture;

		// skinned meshes
		private ReorderableList skinnedMeshList;
		private SerializedProperty findAllChildSkinnedMeshes;

		// combining
		private SerializedProperty combineMeshesOfSameDecalType;

		// settings
		private SerializedProperty runThreaded;
		private SerializedProperty markDynamic;
		private SerializedProperty updateWhenOffscreen;
		private SerializedProperty allowOver65kVertices;

		// splitting
		private SerializedProperty splitVerticeLimit;

		// editor
		private SerializedProperty editorDecal;

		private void OnEnable() {
			SkinnedDecalSystem decalSystem = (SkinnedDecalSystem)target;

			// header
			headerTexture = (Texture2D)EditorGUIUtility.Load("SkinnedDecalsHeader256.jpg");

			// skinned meshes
			skinnedMeshList = new ReorderableList(serializedObject, serializedObject.FindProperty("skinnedMeshes"), true, true, true, true);
			skinnedMeshList.drawHeaderCallback += (Rect rect) => {
				EditorGUI.LabelField(rect, "Specific Skinned Meshes");
			};
			skinnedMeshList.drawElementCallback += (Rect rect, int index, bool isActive, bool isFocused) => {
				var element = skinnedMeshList.serializedProperty.GetArrayElementAtIndex(index);
				EditorGUI.PropertyField(rect, element);
			};

			findAllChildSkinnedMeshes = serializedObject.FindProperty("findAllChildSkinnedMeshes");
			combineMeshesOfSameDecalType = serializedObject.FindProperty("combineMeshesOfSameDecalType");

			// settings
			runThreaded = serializedObject.FindProperty("runThreaded");
			markDynamic = serializedObject.FindProperty("markDynamic");
			updateWhenOffscreen = serializedObject.FindProperty("updateWhenOffscreen");
			allowOver65kVertices = serializedObject.FindProperty("allowOver65kVertices");

			// splitting
			splitVerticeLimit = serializedObject.FindProperty("splitVerticeLimit");

			// editor
			editorDecal = serializedObject.FindProperty("editorDecal");
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			// header
			if(headerTexture != null) {
				GUILayout.Label(headerTexture);
			}

			//EditorGUILayout.HelpBox("Add a decal to this system by calling CreateDecal()", MessageType.None);
			EditorGUILayout.HelpBox("This component allows adding decals to one or more SkinnedMeshRenderers. Add a decal by calling CreateDecal().", MessageType.Info);

			// finding skinned meshes
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Skinned meshes", EditorStyles.boldLabel);
			EditorGUIUtility.labelWidth = 240;
			EditorGUIUtility.fieldWidth = 20;
			findAllChildSkinnedMeshes.boolValue = EditorGUILayout.Toggle("Find all child skinned meshes", findAllChildSkinnedMeshes.boolValue);

			EditorGUI.BeginDisabledGroup(findAllChildSkinnedMeshes.boolValue);
			EditorGUIUtility.labelWidth = 80;
			EditorGUIUtility.fieldWidth = 200;
			skinnedMeshList.DoLayoutList();
			EditorGUI.EndDisabledGroup();

			EditorGUILayout.HelpBox("Add specific skinned meshes or allow the system to find all skinned meshes under this in hierarchy.", MessageType.Info);
			EditorGUILayout.Space();

			// combining
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Combining", EditorStyles.boldLabel);
			EditorGUIUtility.labelWidth = 240;
			EditorGUIUtility.fieldWidth = 20;
			combineMeshesOfSameDecalType.boolValue = EditorGUILayout.Toggle("Combine meshes of same decal type", combineMeshesOfSameDecalType.boolValue);

			EditorGUILayout.HelpBox("If enabled, the system will combine to one decal mesh per decal type across all skinned meshes." + '\n' + '\n' + "Otherwise there will be one for each decal type on each skinned mesh.", MessageType.Info);
			EditorGUILayout.Space();

			// settings
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Settings", EditorStyles.boldLabel);
			EditorGUIUtility.labelWidth = 240;
			EditorGUIUtility.fieldWidth = 20;
			runThreaded.boolValue = EditorGUILayout.Toggle("Multithreading", runThreaded.boolValue);
			updateWhenOffscreen.boolValue = EditorGUILayout.Toggle("Update when offscreen", updateWhenOffscreen.boolValue);
			allowOver65kVertices.boolValue = EditorGUILayout.Toggle("Allow over 65k vertices", allowOver65kVertices.boolValue);
			markDynamic.boolValue = EditorGUILayout.Toggle("Mark dynamic", markDynamic.boolValue);
			EditorGUILayout.Space();

			// pre-allocation
			/*EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Preloading", EditorStyles.boldLabel);
			EditorGUIUtility.labelWidth = 120;
			EditorGUIUtility.fieldWidth = 45;
			preAllocateDataContainers.intValue = EditorGUILayout.IntSlider("Pre-allocate amount", preAllocateDataContainers.intValue, 0, 65000);
			preloadedDecalsList.DoLayoutList();

			EditorGUILayout.HelpBox("Pre-allocation can help to minimize GC allocation during runtime, but increases memory footprint on decal mesh creation." + '\n' + '\n' + "Pre-alloacation amount initializes vertex data containers (vertex, normals, uv etc) for given amount.", MessageType.Info);*/

			// splitting
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Splitting", EditorStyles.boldLabel);
			EditorGUIUtility.labelWidth = 120;
			EditorGUIUtility.fieldWidth = 45;
			splitVerticeLimit.intValue = EditorGUILayout.IntSlider("Split vertex limit", splitVerticeLimit.intValue, 0, 65000);

			EditorGUILayout.HelpBox("This controls the vertex count when the system will create a new decal mesh. Splitting results to more draw calls but helps to keep GC allocations lower.", MessageType.Info);
			EditorGUILayout.Space();

			// editor
			EditorGUILayout.Separator();
			EditorGUILayout.LabelField("Editor", EditorStyles.boldLabel);
			EditorGUIUtility.labelWidth = 80;
			EditorGUIUtility.fieldWidth = 200;
			editorDecal.objectReferenceValue = EditorGUILayout.ObjectField("Decal", editorDecal.objectReferenceValue, typeof(SkinnedDecal), false);

			EditorGUILayout.HelpBox("Add decals in the editor by ALT-clicking on the model in the Scene-view." + '\n' + '\n' + "To save mesh to file, find SkinnedDecalMesh-component and press Save-button", MessageType.Info);
			EditorGUILayout.Space();

			// apply
			serializedObject.ApplyModifiedProperties();
		}

		void OnSceneGUI()
		{
			SkinnedDecalSystem decalSystem = (SkinnedDecalSystem)target;

			Event e = Event.current;
			if(e.alt)
			{
				int controlID = GUIUtility.GetControlID(FocusType.Passive);

				switch(Event.current.GetTypeForControl(controlID))
				{
					case EventType.MouseDown:
						GUIUtility.hotControl = controlID;
						Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
						decalSystem.CreateDecal((SkinnedDecal)editorDecal.objectReferenceValue, ray.origin, ray.direction);
						Event.current.Use();
						break;

					case EventType.MouseUp:
						GUIUtility.hotControl = 0;
						Event.current.Use();
						break;
				}
			}
		}
	}
}