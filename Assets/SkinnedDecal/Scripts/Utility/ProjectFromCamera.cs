using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SkinnedDecals {

	public class ProjectFromCamera : MonoBehaviour {

		private new Camera camera;
		public SkinnedDecal decal;
		public SkinnedDecalSystem[] skinnedDecalSystems;

		void Start() {
			camera = GetComponent<Camera>();
		}

		void Update () {
			if(Input.GetMouseButtonDown(0) || (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(0))) {
				Ray ray = camera.ScreenPointToRay(Input.mousePosition);
				for(int i = 0; i < skinnedDecalSystems.Length; i++) {
					if(skinnedDecalSystems[i] == null)
						continue;
					skinnedDecalSystems[i].CreateDecal(decal, ray.origin, ray.direction, camera.transform.up);
                }
			}
		}
	}

}