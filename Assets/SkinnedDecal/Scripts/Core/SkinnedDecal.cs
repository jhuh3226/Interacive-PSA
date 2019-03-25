using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkinnedDecals {

    [CreateAssetMenu(fileName = "NewSkinnedDecal", menuName = "SkinnedDecal")]
    public class SkinnedDecal : ScriptableObject {
	    public Material material;
	    public float size = 0.1f;
		public bool randomSize = false;
		public float minSize = 0.1f, maxSize = 0.2f;

		public float normalClip = -1f;

		public bool randomRotation = true;
		
		public byte selectedAtlasItem = 0;
		public byte atlasItemCount;
		public bool randomFromAtlas = true;

		public float GetSize() {
			return randomSize ? Random.Range(minSize, maxSize) : size;
		}

		public byte GetAtlasIndex() {
			return randomFromAtlas ? (byte)Random.Range(0, atlasItemCount) : selectedAtlasItem;
		}
    }

}