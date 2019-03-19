using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using iRobi;

public class CarController : MonoBehaviour {
	Rigidbody carRB;
	public Transform cam;
	Vector3 startPos;
	float Y_Y_Y;
	float smooth;

	public Image speedImage;
	float speed = 25;

	public Text speedText;

	public Texture2D decal;

	public GameObject ragDollPrefab;

	public GameObject explosionParticle, obstacleParticle;

	public GameObject rightSparks, leftSparks;

	public MeshRenderer renderer;

	public AudioClip skrejet, bum, explo, obstacle;

	AudioSource audio;

	public Image healthImage;
	float hp = 100;

	public static int score = 0;

	public GameObject GameUI;
	public GameObject GameOverUI;


	public static Texture bloodTexture;

	static public bool gameOver;
	float[] red = new float[]{0.2f,0.2f,0.2f,0.2f, 0.6f, 0.6f, 0.6f};

	void Start () {
		audio = GetComponent<AudioSource> ();
		carRB = GetComponent<Rigidbody> ();
		startPos = cam.position-transform.position;
		Y_Y_Y = transform.rotation.y*transform.rotation.w;

		BrushPaint.paintedTexture.name = "Painted";
		renderer.material.mainTexture = BrushPaint.paintedTexture;
		
		gameOver = false;

		AudioListener.volume = 1;
		Road.prefCount = 0;
		score = 0;
	}

	void Update () {
		speedText.text = "Speed: " + (int)(speed*3);

		healthImage.fillAmount = Mathf.Lerp (healthImage.fillAmount, hp / 100, Time.deltaTime);
		speedImage.fillAmount = Mathf.Lerp (speedImage.fillAmount, ((speed-20)/13), Time.deltaTime);
	}
		
	void FixedUpdate(){

		if (hp <= 0) {
			gameOver = true;
			GameUI.SetActive (false);
			GameOverUI.SetActive (true);
			Color color = GameOverUI.GetComponentInChildren<Image> ().color;
			color.a += 0.005f;
			GameOverUI.GetComponentInChildren<Image> ().color = color;
			bloodTexture = renderer.material.mainTexture;
			AudioListener.volume -= 0.01f;
		}

		cam.position = startPos + transform.position;

		if (!gameOver) {
			speed += Time.fixedDeltaTime/2;
			Vector3 z = carRB.velocity;
			z = carRB.transform.right * (( Mathf.Clamp( speed,-100,75)));
			z.y = -5;
			carRB.velocity = z;


			if (Input.GetKeyDown (KeyCode.G)) {
				carRB.velocity = Vector3.zero;
			}
			smooth = Mathf.Lerp(smooth, Input.GetAxis("Horizontal"),Time.fixedDeltaTime*15);
			//		print (smooth);
			carRB.angularVelocity = Vector3.ClampMagnitude(new Vector3(0,smooth*7/(carRB.velocity.x/5),0), 1);

		}
	}

	void OnTriggerEnter(Collider col){
		if(!gameOver)
		if (col.gameObject.name.Contains("TedyBear")) {
			if (speed > 33) {
				GameObject particle = GameObject.Instantiate (explosionParticle) as GameObject;
				particle.transform.position = col.transform.position + (Vector3.right * 3.5f);
				audio.PlayOneShot (explo);
				score++;

				UVPaint.Create (gameObject, col.transform.position + (Vector3.up), Quaternion.Euler(30,270,0), decal, UVPaint.DecalColor(new Color(red[Random.Range(0, red.Length)],0,0,1)), UVPaint.DecalSize(6));
			} else {
				speed -= 0.2f;
				GameObject bear = GameObject.Instantiate (ragDollPrefab) as GameObject;
//				Rigidbody[] rb = bear.GetComponentsInChildren<Rigidbody> ();
//				for (int i = 0; i < rb.Length; i++) {
//					rb [i].WakeUp ();
//				}
				bear.transform.position = col.transform.position;
//				bear.GetComponentInChildren<Rigidbody>().AddForce (Vector3.right * 500, ForceMode.Impulse);
				audio.PlayOneShot (bum);
				UVPaint.Create (gameObject, col.transform.position + (Vector3.up), Quaternion.Euler(30,270,0), decal, UVPaint.DecalColor(new Color(red[Random.Range(0, red.Length)],0,0,0.4f)), UVPaint.DecalSize(4), UVPaint.AngleInDegrees(Random.Range(0, 360)));
			}
			Destroy (col.gameObject);

		}else if(col.gameObject.name.Contains("obstacle")){
			GameObject particle = GameObject.Instantiate (obstacleParticle) as GameObject;
			particle.transform.position = col.transform.position + (Vector3.right * 3.5f);
			Destroy (col.gameObject);
			hp -= 20;
			audio.PlayOneShot (obstacle);
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.name == "right" || col.gameObject.name == "left") {
			audio.PlayOneShot (skrejet);
		}
	}

	void OnCollisionExit(Collision col){
		if (col.gameObject.name == "right" || col.gameObject.name == "left") {
			rightSparks.SetActive (false);
			leftSparks.SetActive (false);
			audio.Stop ();
		}
	}

	void OnCollisionStay(Collision col){
		
		if ((col.gameObject.name == "right" || col.gameObject.name == "left") && speed > 20) {
			speed -= 0.5f;
		}

		if (col.gameObject.name == "right") {
			rightSparks.SetActive (true);
		} else if (col.gameObject.name == "left") {
			leftSparks.SetActive (true);
		}
	}
}
