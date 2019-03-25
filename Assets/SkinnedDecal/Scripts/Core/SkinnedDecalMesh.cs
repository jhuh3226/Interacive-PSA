using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkinnedDecals {

	public class SkinnedDecalMesh : MonoBehaviour {

		public SkinnedDecal decalType;
		public Mesh decalMesh;
		public SkinnedMeshRenderer decalSmr;

		// decal skinned mesh data
		public List<Vector2> decalUvs;
		public List<Vector3> decalVertices;
		public List<Vector3> decalNormals;
		public List<Vector4> decalTangents;
		public List<int> decalTriangles;
		public List<BoneWeight> decalBoneWeights;
		public List<Color32> decalColors;

		public SkinnedDecalSystem decalSystem;
		private SkinnedMeshRenderer originalSmr;

		public void Initialize(SkinnedDecalSystem ds, SkinnedMeshRenderer smr, SkinnedDecal dt) {
			decalSystem = ds;
			originalSmr = smr;
			decalType = dt;

			// decal mesh
			decalMesh = new Mesh();
			if(decalSystem.markDynamic)
				decalMesh.MarkDynamic();
			decalMesh.bindposes = decalSystem.combineMeshesOfSameDecalType ? decalSystem.combinedBindposes : originalSmr.sharedMesh.bindposes;

			// preallocate lists
			decalUvs = new List<Vector2>();
			decalVertices = new List<Vector3>();
			decalNormals = new List<Vector3>();
			decalTangents = new List<Vector4>();
			decalTriangles = new List<int>();
			decalBoneWeights = new List<BoneWeight>();
			decalColors = new List<Color32>();

			// create new skinned mesh renderer
			decalSmr = gameObject.AddComponent<SkinnedMeshRenderer>();
			decalSmr.rootBone = decalSystem.combineMeshesOfSameDecalType ? decalSystem.combinedBones[decalSystem.boneToIndex[originalSmr.rootBone]] : originalSmr.rootBone;
			decalSmr.bones = decalSystem.combineMeshesOfSameDecalType ? decalSystem.combinedBones : originalSmr.bones;
			if(decalSystem.instantiateMaterial) {
				decalSmr.sharedMaterial = new Material(decalType.material);
				decalSmr.sharedMaterial.renderQueue = decalSmr.sharedMaterial.renderQueue + decalSystem.GetDecalMeshCount(decalType);
			} else {
				decalSmr.sharedMaterial = decalType.material;
			}
			decalSmr.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
			decalSmr.updateWhenOffscreen = decalSystem.updateWhenOffscreen;

		}

		public void UpdateMesh() {
			decalMesh.Clear();
			decalMesh.SetVertices(decalVertices);
			decalMesh.SetNormals(decalNormals);
			decalMesh.SetTangents(decalTangents);
			decalMesh.SetTriangles(decalTriangles, 0);
			decalMesh.SetUVs(0, decalUvs);
			decalMesh.boneWeights = decalBoneWeights.ToArray();
			decalMesh.SetColors(decalColors);

			decalMesh.RecalculateBounds();

			if(decalSmr.sharedMesh == null)
				decalSmr.sharedMesh = decalMesh;
		}
	}
}
