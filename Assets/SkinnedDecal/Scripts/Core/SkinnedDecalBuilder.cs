using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkinnedDecals {

    public class SkinnedDecalBuilder : MonoBehaviour {

		// original mesh data
		public SkinnedMeshRenderer originalSmr;
		private List<Vector3> originalVertices;
		private List<Vector3> originalNormals;
		private List<Vector4> originalTangents;
		private List<int> originalTriangles;
		private List<BoneWeight> originalBoneWeights;
		private Transform[] originalBones;
		private Vector3[] bonePositions;

		// dictionaries
		private Dictionary<int, List<int>> vertexToTriangles;
		private Dictionary<int, List<int>> verticesByBones;
		private Dictionary<int, Bounds> boneBounds;

		// snapshot mesh
		private Mesh snapshotMesh;
		private List<Vector3> snapshotVertices, snapshotNormals;

		// new mesh data
		private List<int> newVerticesIndices;
		private Dictionary<int, int> alreadyAddedTriangles;
		private Dictionary<int, int> alreadyAddedVertices;

		// decal system
		private SkinnedDecalSystem decalSystem;

        #region Initialization

        public void Initialize(SkinnedDecalSystem ds) {
            decalSystem = ds;

			// original skinned mesh
			originalSmr = GetComponent<SkinnedMeshRenderer>();

			// original mesh data
			Mesh originalMesh = originalSmr.sharedMesh;

			originalVertices = new List<Vector3>();
			originalMesh.GetVertices(originalVertices);

			originalNormals = new List<Vector3>();
			originalMesh.GetNormals(originalNormals);

			originalTangents = new List<Vector4>();
			originalMesh.GetTangents(originalTangents);

			originalTriangles = new List<int>();
			originalMesh.GetTriangles(originalTriangles, 0);

			originalBoneWeights = new List<BoneWeight>();
			originalMesh.GetBoneWeights(originalBoneWeights);

			originalBones = originalSmr.bones;
			bonePositions = new Vector3[originalBones.Length];

			// build dictionary of which triangles use each vertice
			vertexToTriangles = new Dictionary<int, List<int>>();
			verticesByBones = new Dictionary<int, List<int>>();
			boneBounds = new Dictionary<int, Bounds>();
			for(int i = 0; i < originalMesh.vertexCount; i++)
			{
				vertexToTriangles.Add(i, new List<int>());

				// build dictionary of which vertices follow which bones
				List<int> boneVertices;
				if(!verticesByBones.TryGetValue(originalBoneWeights[i].boneIndex0, out boneVertices))
				{
					boneVertices = new List<int>();
					verticesByBones.Add(originalBoneWeights[i].boneIndex0, boneVertices);
				}
				boneVertices.Add(i);
			}
			for(int t = 0; t < originalTriangles.Count; t += 3)
			{
				vertexToTriangles[originalTriangles[t]].Add(t);
				vertexToTriangles[originalTriangles[t + 1]].Add(t);
				vertexToTriangles[originalTriangles[t + 2]].Add(t);
			}
			foreach(KeyValuePair<int, List<int>> kvp in verticesByBones)
			{
				Vector3 center = Vector3.zero;
				float minX = Mathf.Infinity, maxX = Mathf.NegativeInfinity,
						minY = Mathf.Infinity, maxY = Mathf.NegativeInfinity, 
						minZ = Mathf.Infinity, maxZ = Mathf.NegativeInfinity;
				List<int> boneVertices = kvp.Value;
				for(int i = 0; i < boneVertices.Count; i++)
				{
					Vector3 vertex = originalVertices[boneVertices[i]];
					if(vertex.x < minX) minX = vertex.x;
					if(vertex.x > maxX) maxX = vertex.x;
					if(vertex.y < minY) minY = vertex.y;
					if(vertex.y > maxY) maxY = vertex.y;
					if(vertex.z < minZ) minZ = vertex.z;
					if(vertex.z > maxZ) maxZ = vertex.z;

					center += originalSmr.transform.TransformPoint(vertex) - originalBones[kvp.Key].transform.position;
				}
				center /= boneVertices.Count;

				float sizeX = maxX - minX;
				float sizeY = maxY - minY;
				float sizeZ = maxZ - minZ;
				Vector3 size = new Vector3(sizeX, sizeY, sizeZ);
				//float maxSize = Mathf.Max(sizeX, Mathf.Max(sizeY, sizeZ));
				//Vector3 size = new Vector3(maxSize, maxSize, maxSize);
				boneBounds.Add(kvp.Key, new Bounds(center, Vector3.Scale(size, originalSmr.transform.localScale)));
			}

			// new mesh
			newVerticesIndices = new List<int>();
			alreadyAddedTriangles = new Dictionary<int, int>();
			alreadyAddedVertices = new Dictionary<int, int>();

			// mesh for snapshots
			snapshotMesh = new Mesh();
			snapshotVertices = new List<Vector3>();
			snapshotNormals = new List<Vector3>();

			// preload meshes decal types
			//for(int p = 0; p < decalSystem.preloadDecalTypes.Length; p++) {
			//	decalSystem.AddOrGetDecalMesh(this, decalSystem.preloadDecalTypes[p]);
			//}
		}

	    void OnDestroy() {
			Destroy(snapshotMesh);
		}

		private void OnDrawGizmos() {
			if(boneBounds == null)
				return;
			//foreach(KeyValuePair<int, Bounds> kvp in boneBounds) {
			//	Gizmos.DrawWireCube(originalBones[kvp.Key].transform.position + kvp.Value.center, kvp.Value.size);
			//}
			/*for(int i = 0; i < originalBones.Length; i++) {
				if(originalBones[i] != originalSmr.rootBone)
					Gizmos.DrawLine(originalBones[i].position, originalBones[i].parent.position);
			}*/
		}

		#endregion

		#region Creation

		public SkinnedDecalMesh skinnedDecalMesh;
		public SkinnedDecal currentDecalType;

		private Matrix4x4 originalSmrMatrix;
		private Vector3 originalSmrPosition;
		private Quaternion originalSmrRotation;
		private Bounds decalBounds;
		private Matrix4x4 inverseMatrix;
		
		private int startVerticeCount;
		private int startTriangleCount;
		private int newTriangleIndices = 0;
		private bool newGeometry = false;

		public bool Intersects(SkinnedDecal decalType, Vector3 origin, Vector3 direction, Vector3 up, float decalSize, byte atlasIndex = 0) {
			// get decal mesh
			skinnedDecalMesh = decalSystem.AddOrGetDecalMesh(this, decalType);
			currentDecalType = decalType;

			// original smr matrix
			originalSmrMatrix = new Matrix4x4();
			originalSmrPosition = originalSmr.transform.position;
			originalSmrRotation = originalSmr.transform.rotation;
			originalSmrMatrix.SetTRS(originalSmrPosition, originalSmrRotation, Vector3.one);

			// hit point
			float distanceToPlane = Vector3.Distance(origin, originalSmr.bounds.center);
			Vector3 worldSpaceHitPoint = origin + direction * distanceToPlane;

			// matrix
			Quaternion rotation = up == Vector3.zero ? Quaternion.LookRotation(direction) : Quaternion.LookRotation(direction, up);
			Matrix4x4 matrix = new Matrix4x4();
			matrix.SetTRS(origin, rotation, Vector3.one);
			inverseMatrix = matrix.inverse;
			Vector3 decalSpaceHitPoint = inverseMatrix.MultiplyPoint3x4(worldSpaceHitPoint);

			// bounds
			decalBounds = new Bounds(decalSpaceHitPoint, Vector3.one * decalSize + Vector3.forward * distanceToPlane * 2f);

			// test decal and skinned mesh overlap
			float maxSize = originalSmr.bounds.size.x;
			if(originalSmr.bounds.size.y > maxSize)
				maxSize = originalSmr.bounds.size.y;
			if(originalSmr.bounds.size.z > maxSize)
				maxSize = originalSmr.bounds.size.z;
			Bounds smrBounds = new Bounds(inverseMatrix.MultiplyPoint3x4(originalSmr.bounds.center), new Vector3(maxSize, maxSize, maxSize));

			// early exit if no overlap
			return smrBounds.Intersects(decalBounds);
		}

		public void TakeSnapshot(SkinnedDecal decalType, Vector3 origin, Vector3 direction, Vector3 up, float decalSize, byte atlasIndex = 0) {

			// take snapshot of mesh in current pose
			originalSmr.BakeMesh(snapshotMesh);
			snapshotMesh.GetVertices(snapshotVertices);
			snapshotMesh.GetNormals(snapshotNormals);

			for(int i = 0; i < bonePositions.Length; i++) {
				bonePositions[i] = originalBones[i].position;
			}
		}

		public void CreateDecal(SkinnedDecal decalType, Vector3 origin, Vector3 direction, Vector3 up, float decalSize, byte atlasIndex = 0) {

			// find triangles
			newVerticesIndices.Clear();
			alreadyAddedTriangles.Clear();
			alreadyAddedVertices.Clear();

			startVerticeCount = skinnedDecalMesh.decalVertices.Count;
			startTriangleCount = skinnedDecalMesh.decalTriangles.Count;
			newTriangleIndices = 0;

			for(int i = 0; i < snapshotVertices.Count; i++) {
				Vector3 vertexWorldPos = originalSmrPosition + originalSmrRotation * snapshotVertices[i];
				Vector3 vertexDecalPos = inverseMatrix.MultiplyPoint3x4(vertexWorldPos);
				if(decalBounds.Contains(vertexDecalPos) && Vector3.Dot(-direction, originalSmrMatrix.MultiplyVector(originalNormals[i])) > decalType.normalClip) {
					List<int> triangleStarts = vertexToTriangles[i];
					for(int t = 0; t < triangleStarts.Count; t++) {
						if(alreadyAddedTriangles.ContainsKey(triangleStarts[t]))
							continue;
						alreadyAddedTriangles.Add(triangleStarts[t], 0);

						int originalIndex1 = originalTriangles[triangleStarts[t]];
						int newIndex1;
						if(!alreadyAddedVertices.TryGetValue(originalIndex1, out newIndex1)) {
							newIndex1 = startVerticeCount + newVerticesIndices.Count;
							alreadyAddedVertices.Add(originalIndex1, newIndex1);
							newVerticesIndices.Add(originalIndex1);
						}
						int originalIndex2 = originalTriangles[triangleStarts[t] + 1];
						int newIndex2;
						if(!alreadyAddedVertices.TryGetValue(originalIndex2, out newIndex2)) {
							newIndex2 = startVerticeCount + newVerticesIndices.Count;
							alreadyAddedVertices.Add(originalIndex2, newIndex2);
							newVerticesIndices.Add(originalIndex2);
						}
						int originalIndex3 = originalTriangles[triangleStarts[t] + 2];
						int newIndex3;
						if(!alreadyAddedVertices.TryGetValue(originalIndex3, out newIndex3)) {
							newIndex3 = startVerticeCount + newVerticesIndices.Count;
							alreadyAddedVertices.Add(originalIndex3, newIndex3);
							newVerticesIndices.Add(originalIndex3);
						}

						// add triangle
						skinnedDecalMesh.decalTriangles.Add(newIndex1);
						skinnedDecalMesh.decalTriangles.Add(newIndex2);
						skinnedDecalMesh.decalTriangles.Add(newIndex3);
						newTriangleIndices += 3;
					}
				}
			}

			// exit if no new vertices
			if(newVerticesIndices.Count == 0) {
				if(newTriangleIndices > 0)
					skinnedDecalMesh.decalTriangles.RemoveRange(skinnedDecalMesh.decalTriangles.Count - newTriangleIndices, newTriangleIndices);

				newGeometry = false;
				return;
			}
			newGeometry = true;

			// build mesh
			Dictionary<int, int> indexToCombinedIndex = null;
			decalSystem.smrBoneweightToIndex.TryGetValue(originalSmr, out indexToCombinedIndex);
			for(int i = 0; i < newVerticesIndices.Count; i++) {
				// vertex
				Vector3 vertex = originalVertices[newVerticesIndices[i]];
				skinnedDecalMesh.decalVertices.Add(vertex);

				// normal
				Vector3 normal = originalNormals[newVerticesIndices[i]];
				skinnedDecalMesh.decalNormals.Add(normal);

				Vector4 tangent = originalTangents[newVerticesIndices[i]];
				skinnedDecalMesh.decalTangents.Add(tangent);

				// vertex color
				// r = unused
				// g = unused
				// b = unused
				// a = atlas sheet index
				Color32 color = new Color32(0, 0, 0, atlasIndex);
				skinnedDecalMesh.decalColors.Add(color);

				// boneweight
				BoneWeight boneWeight = originalBoneWeights[newVerticesIndices[i]];
				if(decalSystem.combineMeshesOfSameDecalType) {
					boneWeight.boneIndex0 = indexToCombinedIndex[boneWeight.boneIndex0];
					boneWeight.boneIndex1 = indexToCombinedIndex[boneWeight.boneIndex1];
					boneWeight.boneIndex2 = indexToCombinedIndex[boneWeight.boneIndex2];
					boneWeight.boneIndex3 = indexToCombinedIndex[boneWeight.boneIndex3];
				}
				skinnedDecalMesh.decalBoneWeights.Add(boneWeight);

				// positions from snapshot
				Vector3 vertexWorldPos = originalSmrPosition + originalSmrRotation * snapshotVertices[newVerticesIndices[i]];
				Vector3 vertexDecalPos = inverseMatrix.MultiplyPoint3x4(vertexWorldPos);

				// calculate uvs
				Vector2 uv = new Vector2(vertexDecalPos.x, vertexDecalPos.y) * (1f / decalSize);
				uv.x += 0.5f;
				uv.y += 0.5f;

				if(vertexDecalPos.x < decalBounds.min.x)
					uv.x = 0f;
				else if(vertexDecalPos.x > decalBounds.max.x)
					uv.x = 1f;
				if(vertexDecalPos.y < decalBounds.min.y)
					uv.y = 0f;
				else if(vertexDecalPos.y > decalBounds.max.y)
					uv.y = 1f;

				skinnedDecalMesh.decalUvs.Add(uv);
			}
		}

		public void UpdateMesh() {
			if(!newGeometry)
				return;

			// test if vertex counts exceeds limits
			bool splitLimit = startVerticeCount + newVerticesIndices.Count >= decalSystem.splitVerticeLimit;
			bool vertexLimit = startVerticeCount + newVerticesIndices.Count > 65000 && !decalSystem.allowOver65kVertices;
			if(vertexLimit)
				Debug.LogWarning("Skinned mesh decals: vertices count exceeding 65k. Check allowOver65kVertices if you can support more vertices.");

			if(splitLimit || vertexLimit) {
				// add already added triangles
				List<int> addedTriangles = new List<int>();
				for(int i = 0; i < newTriangleIndices; i++) {
					int triangleIndex = skinnedDecalMesh.decalTriangles[startTriangleCount + i] - startVerticeCount;
					addedTriangles.Add(triangleIndex);
				}

				// already added data
				List<Vector2> decalUvs = skinnedDecalMesh.decalUvs.GetRange(startVerticeCount, newVerticesIndices.Count);
				List<Vector3> decalVertices = skinnedDecalMesh.decalVertices.GetRange(startVerticeCount, newVerticesIndices.Count);
				List<Vector3> decalNormals = skinnedDecalMesh.decalNormals.GetRange(startVerticeCount, newVerticesIndices.Count);
				List<Vector4> decalTangents = skinnedDecalMesh.decalTangents.GetRange(startVerticeCount, newVerticesIndices.Count);
				List<BoneWeight> decalBoneWeights = skinnedDecalMesh.decalBoneWeights.GetRange(startVerticeCount, newVerticesIndices.Count);
				List<Color32> decalColors = skinnedDecalMesh.decalColors.GetRange(startVerticeCount, newVerticesIndices.Count);

				// replace decal mesh
				skinnedDecalMesh = decalSystem.ReplaceDecalMesh(this, currentDecalType);
				skinnedDecalMesh.decalTriangles.AddRange(addedTriangles);
				skinnedDecalMesh.decalUvs.AddRange(decalUvs);
				skinnedDecalMesh.decalVertices.AddRange(decalVertices);
				skinnedDecalMesh.decalNormals.AddRange(decalNormals);
				skinnedDecalMesh.decalTangents.AddRange(decalTangents);
				skinnedDecalMesh.decalBoneWeights.AddRange(decalBoneWeights);
				skinnedDecalMesh.decalColors.AddRange(decalColors);
			}

			// update mesh
			skinnedDecalMesh.UpdateMesh();
		}

		#endregion
	}
}
