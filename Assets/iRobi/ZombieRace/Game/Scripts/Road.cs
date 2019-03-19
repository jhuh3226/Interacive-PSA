using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Road : MonoBehaviour {

	public Transform bornPoint;
	public Transform car;

	public GameObject zombiePrefab;
	public GameObject obstaclePrefab;

	GameObject tmp;

	public GameObject[] ZombieSpawnMap;

	public GameObject[] ObstaclesSpawnMap;

	static public int prefCount;

	bool created;




	GameObject asd;
	GameObject aaa;
	GameObject bbb;

	List<GameObject> Zombies = new List<GameObject>();



	// Use this for initialization
	void Start () {
		asd = GameObject.Instantiate(ZombieSpawnMap[Mathf.Clamp(prefCount, 0, ZombieSpawnMap.Length-1)]) as GameObject;
		asd.transform.position = transform.position;

		GameObject qqq;

		int count = asd.GetComponentsInChildren<Transform> ().Length;

		for (int i = 0; i < count-1; i++) {
			qqq = GameObject.Instantiate (zombiePrefab) as GameObject;
			qqq.transform.position = asd.transform.GetChild (i).position;
			qqq.transform.Rotate (Vector3.up*Random.Range(220,310));
		}

		if(prefCount > 1){
			aaa = GameObject.Instantiate (ObstaclesSpawnMap[1]) as GameObject;
			aaa.transform.position = transform.position;

			int[] num = new int[]{-1,1};
			aaa.transform.localScale = new Vector3(1, 1, num [Random.Range (0, num.Length)]);

			count = aaa.GetComponentsInChildren<Transform> ().Length;

			for (int i = 0; i < count-1; i++) {
				bbb = GameObject.Instantiate (obstaclePrefab) as GameObject;
				bbb.transform.position = aaa.transform.GetChild (i).position;
				bbb.transform.rotation = Quaternion.Euler(90, 0, 0);
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if ((car.transform.position - transform.position).x > 10 && !created) {
			
			tmp = GameObject.Instantiate (gameObject) as GameObject;
			tmp.transform.position = bornPoint.position;
			tmp.GetComponent<Road> ().car = car;



			created = true;
//			print (prefCount);
			prefCount++;
		}
		if ((car.transform.position - bornPoint.position).x > 10) {
			Destroy (gameObject);
//			Destroy (asd);
		}
	}

	void OnDestroy(){
//		for(int i = 0; i < Zombies.Count; i++){
//			Destroy (Zombies [i]);
//		}
	}
}
