using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkinnedDecals {

	public class SkinnedDecalSystem : MonoBehaviour {

		// affected skinned meshes
		public SkinnedMeshRenderer[] skinnedMeshes = new SkinnedMeshRenderer[0];
		private List<SkinnedMeshRenderer> _skinnedMeshes = new List<SkinnedMeshRenderer>();
		public bool findAllChildSkinnedMeshes = true;
		public List<SkinnedDecalBuilder> builders = new List<SkinnedDecalBuilder>();

		// combined meshes
		public bool combineMeshesOfSameDecalType = true;
		private Dictionary<SkinnedDecal, SkinnedDecalMesh> combinedMeshes = new Dictionary<SkinnedDecal, SkinnedDecalMesh>();
		private Dictionary<SkinnedDecalBuilder, Dictionary<SkinnedDecal, SkinnedDecalMesh>> perBuilderMeshes = new Dictionary<SkinnedDecalBuilder, Dictionary<SkinnedDecal, SkinnedDecalMesh>>();
		private Dictionary<SkinnedDecal, int> decalMeshCounts = new Dictionary<SkinnedDecal, int>();

		// settings
		public bool runThreaded = true;
		public bool markDynamic = false;
		public bool updateWhenOffscreen = false;
		public bool allowOver65kVertices = false;

		// combined mesh data
		public Dictionary<Transform, int> boneToIndex = new Dictionary<Transform, int>();
		public Dictionary<SkinnedMeshRenderer, Dictionary<int, int>> smrBoneweightToIndex = new Dictionary<SkinnedMeshRenderer, Dictionary<int, int>>();
		private List<Transform> allBones = new List<Transform>();
		private List<Matrix4x4> allBindposes = new List<Matrix4x4>();
		[System.NonSerialized] public Transform[] combinedBones;
		[System.NonSerialized] public Matrix4x4[] combinedBindposes;

		// pre-allocation
		//public int preAllocateDataContainers = 0;
		//public SkinnedDecal[] preloadDecalTypes;

		// splitting
		public int splitVerticeLimit = 65000;
		public bool instantiateMaterial = false;

		// editor
		public SkinnedDecal editorDecal;
		private bool initialized = false;

		// texture
		public Bounds totalBounds;

		#region  Initialization

		void Awake() {
			Initialize();
		}

		public void Initialize() {
			// find skinned mesh renderers
			if(findAllChildSkinnedMeshes) {
				skinnedMeshes = GetComponentsInChildren<SkinnedMeshRenderer>();
			}
			_skinnedMeshes.Clear();
			_skinnedMeshes.AddRange(skinnedMeshes);

			// combine bones if necessary
			if(combineMeshesOfSameDecalType)
				CombineBones();

			// clean up if necessary
			Clear();

			// calculate total bounds
			totalBounds = new Bounds(transform.position, Vector3.zero);
			for(int i = 0; i < _skinnedMeshes.Count; i++) {
				totalBounds.Encapsulate(_skinnedMeshes[i].bounds);
			}

			// create builders
			for(int i = 0; i < _skinnedMeshes.Count; i++) {
				SkinnedDecalBuilder builder = _skinnedMeshes[i].gameObject.AddComponent<SkinnedDecalBuilder>();
				perBuilderMeshes.Add(builder, new Dictionary<SkinnedDecal, SkinnedDecalMesh>());
				builder.Initialize(this);
				builders.Add(builder);
			}

			initialized = true;
		}

		public void Clear() {
			if(builders.Count > 0) {
				for(int i = 0; i < builders.Count; i++)
					DestroyImmediate(builders[i]);
				builders.Clear();
			}
			if(perBuilderMeshes.Count > 0) {
				foreach(KeyValuePair<SkinnedDecalBuilder, Dictionary<SkinnedDecal, SkinnedDecalMesh>> kvp1 in perBuilderMeshes) {
					foreach(KeyValuePair<SkinnedDecal, SkinnedDecalMesh> kvp2 in kvp1.Value) {
						DestroyImmediate(kvp2.Value);
					}
				}
				perBuilderMeshes.Clear();
			}
			if(combinedMeshes.Count > 0) {
				foreach(KeyValuePair<SkinnedDecal, SkinnedDecalMesh> kvp in combinedMeshes) {
					DestroyImmediate(kvp.Value);
				}
				combinedMeshes.Clear();
			}
		}

		#endregion

		#region  Interface

		public void CreateDecal(SkinnedDecal decalType, Vector3 origin, Vector3 direction) {
			CreateDecal(decalType, origin, direction, Vector3.zero);
		}

		public void CreateDecal(SkinnedDecal decalType, Vector3 origin, Vector3 direction, Vector3 up) {
			if(decalType == null)
				return;

			if(!gameObject.activeInHierarchy)
				return;

			if(!initialized)
				Initialize();

			// size
			float decalSize = decalType.GetSize();

			// selected tile from atlas
			byte atlasIndex = decalType.GetAtlasIndex();

			// random rotation
			if(decalType.randomRotation) {
				Quaternion randomRotation = Quaternion.AngleAxis(Random.Range(0f, 360f), direction);
				up = randomRotation * up;
			}

			// create decal on each builder
			for(int i = 0; i < builders.Count; i++) {
				if(builders[i].Intersects(decalType, origin, direction, up, decalSize, atlasIndex)) {
					builders[i].TakeSnapshot(decalType, origin, direction, up, decalSize, atlasIndex);
					if(runThreaded && Application.isPlaying) {
						decalTasks.Enqueue(new DecalTask(builders[i], decalType, origin, direction, up, decalSize, atlasIndex));
					} else {
						builders[i].CreateDecal(decalType, origin, direction, up, decalSize, atlasIndex);
						builders[i].UpdateMesh();
					}
				}
			}
		}

		public void AddSkinnedMesh(SkinnedMeshRenderer smr) {
			if(_skinnedMeshes.Contains(smr))
				return;
			_skinnedMeshes.Add(smr);

			// combine if necessary
			if(combineMeshesOfSameDecalType)
				CombineBones();

			// add builder
			SkinnedDecalBuilder builder = smr.gameObject.AddComponent<SkinnedDecalBuilder>();
			perBuilderMeshes.Add(builder, new Dictionary<SkinnedDecal, SkinnedDecalMesh>());
			builder.Initialize(this);
			builders.Add(builder);

			combinedMeshes.Clear();
		}

		public void RemoveSkinnedMesh(SkinnedMeshRenderer smr) {
			if(!_skinnedMeshes.Contains(smr))
				return;
			_skinnedMeshes.Remove(smr);

			// combine if necessary
			if(combineMeshesOfSameDecalType)
				CombineBones();

			// remove builder
			List<SkinnedDecalBuilder> toBeRemoved = new List<SkinnedDecalBuilder>();
			for(int i = 0; i < builders.Count; i++) {
				if(builders[i].originalSmr == smr) {
					toBeRemoved.Add(builders[i]);
				}
			}
			for(int i = 0; i < toBeRemoved.Count; i++) {
				builders.Remove(toBeRemoved[i]);
			}

			combinedMeshes.Clear();
		}

		#endregion

		#region Multithreading

		private Queue<DecalTask> decalTasks = new Queue<DecalTask>();
		private bool workInProgress = false;
		private DecalTask currentTask;

		private class DecalTask {
			public SkinnedDecalBuilder builder;
			public SkinnedDecal decalType;
			public float decalSize;
			public byte atlasIndex;
			public Vector3 origin, direction, up;

			public DecalTask(SkinnedDecalBuilder builder, SkinnedDecal decalType, Vector3 origin, Vector3 direction, Vector3 up, float decalSize, byte atlasIndex) {
				this.builder = builder;
				this.decalType = decalType;
				this.decalSize = decalSize;
				this.atlasIndex = atlasIndex;
				this.origin = origin;
				this.direction = direction;
				this.up = up;
			}
		}

		void LateUpdate() {
			if(!workInProgress && decalTasks.Count > 0) {
				workInProgress = true;
				currentTask = decalTasks.Dequeue();
				ThreadPooler.RunOnThread(() => {
					currentTask.builder.CreateDecal(currentTask.decalType, currentTask.origin, currentTask.direction, currentTask.up, currentTask.decalSize, currentTask.atlasIndex);
					ThreadPooler.RunOnMainThread(() => {
						currentTask.builder.UpdateMesh();
						workInProgress = false;
					});
				});
			}
		}

		#endregion

		#region Meshes

		public SkinnedDecalMesh AddOrGetDecalMesh(SkinnedDecalBuilder builder, SkinnedDecal decalType)
		{
			SkinnedDecalMesh dm = null;
			if(combineMeshesOfSameDecalType) {
				if(!combinedMeshes.TryGetValue(decalType, out dm)) {
					dm = CreateNewDecalMesh(decalType, this.transform);
					dm.Initialize(this, builder.originalSmr, decalType);
					combinedMeshes.Add(decalType, dm);
				}
			}
			else {
				Dictionary<SkinnedDecal, SkinnedDecalMesh> decalMeshes = perBuilderMeshes[builder];
				if(!decalMeshes.TryGetValue(decalType, out dm)) {
					dm = CreateNewDecalMesh(decalType, builder.transform);
					dm.Initialize(this, builder.originalSmr, decalType);
					decalMeshes.Add(decalType, dm);
				}
			}

			// if destroyed somehow
			if(dm == null)
				ReplaceDecalMesh(builder, decalType);

			return dm;
		}

		public SkinnedDecalMesh ReplaceDecalMesh(SkinnedDecalBuilder builder, SkinnedDecal decalType)
		{
			SkinnedDecalMesh dm = null;
			if(combineMeshesOfSameDecalType)
			{
				// destroy previous
				if(Application.isPlaying)
					Destroy(combinedMeshes[decalType]);

				// create new
				dm = CreateNewDecalMesh(decalType, this.transform);
				dm.Initialize(this, builder.originalSmr, decalType);
				combinedMeshes[decalType] = dm;

				// replace for other builders as well
				for(int i = 0; i < builders.Count; i++) {
					if(builders[i] == builder)
						continue;
					if(builders[i].currentDecalType == decalType) {
						builders[i].skinnedDecalMesh = dm;
					}
				}
			}
			else
			{
				Dictionary<SkinnedDecal, SkinnedDecalMesh> decalMeshes = perBuilderMeshes[builder];
				dm = CreateNewDecalMesh(decalType, builder.transform);
				dm.Initialize(this, builder.originalSmr, decalType);
				decalMeshes[decalType] = dm;
			}
			return dm;
		}

		private SkinnedDecalMesh CreateNewDecalMesh(SkinnedDecal decalType, Transform parent) {
			IncrementDecalMeshCount(decalType);

			SkinnedDecalMesh dm = null;
			GameObject newGo = new GameObject("SkinnedDecalMesh (" + decalType.name + ")");
			newGo.transform.SetParent(parent, false);
			newGo.transform.localPosition = Vector3.zero;
			newGo.transform.rotation = Quaternion.identity;
			dm = newGo.AddComponent<SkinnedDecalMesh>();
			return dm;
		}

		private void IncrementDecalMeshCount(SkinnedDecal decalType) {
			int count = 0;
			if(!decalMeshCounts.TryGetValue(decalType, out count))
				decalMeshCounts.Add(decalType, count);
			else {
				decalMeshCounts[decalType] = count + 1;
			}
		}

		public int GetDecalMeshCount(SkinnedDecal decalType) {
			int count = 0;
			decalMeshCounts.TryGetValue(decalType, out count);
			return count;
		}

		private void CombineBones() {
			// find all bones and bindposes, map bone indices
			int boneCount = -1;
			boneToIndex.Clear();
			smrBoneweightToIndex.Clear();
			allBones.Clear();
			allBindposes.Clear();
			for(int i = 0; i < _skinnedMeshes.Count; i++)
			{
				Matrix4x4[] bindposes = _skinnedMeshes[i].sharedMesh.bindposes;

				Dictionary<int, int> indexToNewIndex = new Dictionary<int, int>();
				for(int b = 0; b < _skinnedMeshes[i].bones.Length; b++)
				{
					int index = 0;
					if(!boneToIndex.TryGetValue(_skinnedMeshes[i].bones[b], out index))
					{
						boneCount++;
						boneToIndex.Add(_skinnedMeshes[i].bones[b], boneCount);
						index = boneCount;

						allBones.Add(_skinnedMeshes[i].bones[b]);
						allBindposes.Add(_skinnedMeshes[i].sharedMesh.bindposes[b]);
					}
					indexToNewIndex.Add(b, index);
				}
				smrBoneweightToIndex.Add(_skinnedMeshes[i], indexToNewIndex);
			}
			combinedBones = allBones.ToArray();
			combinedBindposes = allBindposes.ToArray();
		}

		#endregion
	}
}