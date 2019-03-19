using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using iRobi;

public class BrushPaint : MonoBehaviour {
	public Texture2D [] brush;
	public Transform Brush;
	Vector3 KeepMousePos;
	public GameObject Obj;
	public Texture2D[] Palette;
	Rect posPalette;
	Rect RightPickPalette;
	Color SetColor = new Color(1,0,0,1);
	Color SetupColor = new Color(1,0,0,1);

	public static List<Dictionary<string, Texture>> textureByProperty;

	Color carColor;
	Color brushColor;

	bool RandomRotation;

	public static Texture paintedTexture;


	public Texture2D texture;

	int selected = 0;

	bool btn;

	float smooth;

	int currentI;
	
			float RR=1;
			float HH=1;
	
	Projector proj;
	int BrushInt;
	float Alpha=1;
	public GUIStyle style;
	public GUIStyle Buttons;

	public GUIStyle clearButton;

	public GUIStyle boxStyle;

	public GUIStyle bakeButton, StartButton;

	public float BrushSize=1;
	bool everyFrame = true;
	Animator anim;
	bool Erase;
	int position;

	Vector2 scrollPosition;
	Vector2 fprev = new Vector2();
	Vector2 prev = new Vector2();
	Vector2 speed = new Vector2();
	Vector2 mouse = new Vector2();
	public GUISkin skin;

	void Start () {
		if (BrushPaint.paintedTexture != null) {
			Obj.GetComponent<Renderer>().material.mainTexture = BrushPaint.paintedTexture;
			Obj.GetComponent<Renderer>().material.mainTexture.name = "Worked";
		}
		posPalette = new Rect(20,20,100,100);
		RightPickPalette = new Rect(posPalette.xMax+12,posPalette.y,20,posPalette.height);
		proj = Brush.GetComponent<Projector>();
		anim = Obj.GetComponent<Animator>();
		position = 0;
		currentI = 3;
		if (textureByProperty != null) {
			RestoreContainer RC = Obj.AddComponent<RestoreContainer> ();
			RC.Texture_by_Property = textureByProperty;
		} else
			UVPaint.Create(Obj,Brush.position,Brush.rotation,brush[BrushInt],UVPaint.DecalSize(3.3f*proj.orthographicSize),UVPaint.NormalizedAlpha(0));
	}

	void Update () {
		proj.material.mainTexture = brush[BrushInt];
		proj.material.color = SetColor;
		proj.orthographicSize = BrushSize/5;

	Vector3 target = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, GetComponent<Camera>().nearClipPlane));
		Brush.position = transform.position;
		Brush.LookAt(target);

		if(Input.GetMouseButton(0)&&everyFrame||Input.GetMouseButtonDown(0))
		{
			if(Input.GetKey(KeyCode.Space))
				UVPaint.ShaderPropertyName("_BumpMap");
			if (!Erase) {
				if (RandomRotation) {
					UVPaint.Create(Obj,Brush.position,Brush.rotation,brush[BrushInt],UVPaint.DecalSize(3.3f*proj.orthographicSize),UVPaint.DecalColor(SetColor), UVPaint.AngleInDegrees(Random.Range(0,180)));
				} else {
					UVPaint.Create(Obj,Brush.position,Brush.rotation,brush[BrushInt],UVPaint.DecalSize(3.3f*proj.orthographicSize),UVPaint.DecalColor(SetColor));
				}
			}else
				UVPaint.Restore(Obj,Brush.position,Brush.rotation,brush[BrushInt],UVPaint.DecalSize(3.3f*proj.orthographicSize),UVPaint.NormalizedAlpha(Alpha));
		}
		KeepMousePos = Input.mousePosition;
		float Y = KeepMousePos.y-Screen.height+posPalette.y*2+posPalette.height;
		
		float R=0;
			float G=0;
			float B=0;
		if(RightPickPalette.Contains(new Vector2(KeepMousePos.x,Y))&&Input.GetMouseButton(0))
		{
			
			
			if(Y>=RightPickPalette.yMax/6*5&&Y<RightPickPalette.yMax)
			{
				float pos = (Y-RightPickPalette.yMax/6*5) / (RightPickPalette.yMax-RightPickPalette.yMax/6*5);
				R = 1;
				B = 1-pos;
				G = 0;
			}
			
			if(Y>=RightPickPalette.yMax/6*4&&Y<RightPickPalette.yMax/6*5)
			{
				float pos = (Y-RightPickPalette.yMax/6*4) / (RightPickPalette.yMax/6*5-RightPickPalette.yMax/6*4);
				R = pos;
				B = 1;
				G = 0;
			}
			
			if(Y>=RightPickPalette.yMax/6*3&&Y<RightPickPalette.yMax/6*4)
			{
				float pos = (Y-RightPickPalette.yMax/6*3) / (RightPickPalette.yMax/6*4-RightPickPalette.yMax/6*3);
				R = 0;
				B = 1;
				G = 1-pos;
			}
			
			if(Y>=RightPickPalette.yMax/6*2&&Y<RightPickPalette.yMax/6*3)
			{
				float pos = (Y-RightPickPalette.yMax/6*2) / (RightPickPalette.yMax/6*3-RightPickPalette.yMax/6*2);
				R = 0;
				B = pos;
				G = 1;
			}
			
			if(Y>=RightPickPalette.yMax/6&&Y<RightPickPalette.yMax/6*2)
			{
				float pos = (Y-RightPickPalette.yMax/6) / (RightPickPalette.yMax/6*2-RightPickPalette.yMax/6);
				R = 1-pos;
				B = 0;
				G = 1;
			}
			
			if(Y>=RightPickPalette.yMin&&Y<RightPickPalette.yMax/6)
			{
				float pos = (Y-RightPickPalette.yMin) / (RightPickPalette.yMax/6-RightPickPalette.yMin);
				R = 1;
				B = 0;
				G = pos;
			}

			SetupColor = new Color(R,G,B,1);
		}
		
		if(posPalette.Contains(new Vector2(KeepMousePos.x,Y))&&Input.GetMouseButton(0))
		{
			
			RR = (posPalette.xMax-KeepMousePos.x)/(posPalette.xMax-posPalette.xMin);
			HH = (Y-posPalette.yMin)/(posPalette.yMax-posPalette.yMin);
			
		}

		if(Input.GetMouseButton(0)){
			Color color = new Color (Mathf.Lerp (SetupColor.r - (1 - HH), HH, RR), Mathf.Lerp (SetupColor.g - (1 - HH), HH, RR), Mathf.Lerp (SetupColor.b - (1 - HH), HH, RR), Alpha);
			SetColor = color;
		}
			

		mouse = new Vector2 (KeepMousePos.x, Y);

		if (Input.GetMouseButtonDown (0) && new Rect((Screen.width/2-(700*(Screen.width/1200f))/2), 15, 700*(Screen.width/1200f), 120*(Screen.height/640f)).Contains (mouse)) {
			
			fprev = mouse;
			prev = mouse;
		}

		if (new Rect((Screen.width/2-(700*(Screen.width/1200f))/2), 15, 700*(Screen.width/1200f), 120*(Screen.height/640f)).Contains (mouse) && Input.GetMouseButton (0)) {
			
			smooth = scrollPosition.x + (prev - mouse).x*10;

			prev = mouse;

		}

		speed = prev - mouse;

		if(Input.GetMouseButtonUp(0) && new Rect((Screen.width/2-(700*(Screen.width/1200f))/2), 15, 700*(Screen.width/1200f), 120*(Screen.height/640f)).Contains (mouse)){
			
			smooth = scrollPosition.x + (speed).x*5;
			prev = new Vector2();
			if ((mouse - fprev).x > -10 && (mouse - fprev).x < 10) {
				btn = true;
			} else {
				btn = false;
			}


		}

		if(Input.GetKeyDown(KeyCode.E)){
			Erase = !Erase;
		}

		if(Input.GetKeyDown(KeyCode.F)){
			everyFrame = !everyFrame;
		}
			
		if(Input.GetKeyDown(KeyCode.R)){
			RandomRotation = !RandomRotation;
		}

		scrollPosition.x = Mathf.Lerp(scrollPosition.x, smooth, Time.deltaTime*10);
	}
	
	void OnGUI(){
		GUI.color = new Color(1,1,1,1);
		GUI.Box(new Rect(0,0,180,530),"", boxStyle);

		if (GUI.Button (new Rect (15, 125, 150, 40), "", clearButton)) {
			Obj.GetComponent<Renderer> ().material.mainTexture = texture;
		}

		GUI.DrawTexture(posPalette,Palette[0],ScaleMode.ScaleAndCrop,false,1);
		GUI.DrawTexture(RightPickPalette,Palette[2],ScaleMode.ScaleAndCrop,false,1);
		GUI.color = SetupColor;
		GUI.DrawTexture(posPalette,Palette[1],ScaleMode.ScaleAndCrop,true,1);

		GUI.skin = skin;
		scrollPosition = GUI.BeginScrollView(new Rect((Screen.width/2-(700*(Screen.width/1200f))/2), 15, 700*(Screen.width/1200f), 120*(Screen.height/640f)), scrollPosition, new Rect(0, 0, (100*(Screen.width/1200f)) *brush.Length, 0),false,false);

		for(int i=0;i<brush.Length;i++){

			Rect pos = new Rect (i * (100*(Screen.width/1200f)), 20*(Screen.width/1200f), 100*(Screen.width/1200f), 100*(Screen.height/640f));
			if(BrushInt == i)
				GUI.color = SetColor;
			else
				GUI.color = Color.white;

			Color col = GUI.color;

			col.a = Mathf.Sqrt( Mathf.PingPong( (pos.center.x-scrollPosition.x) , (700*(Screen.width/1200f))/2) /500);
			Vector2 size = pos.size*(Mathf.Sqrt(col.a));
			pos.x -= size.x / 2-50;
			pos.y -= size.y / 2-50;
			pos.size = size;

			GUI.color = col;

			if (GUI.Button (pos, brush [i],style)) {
				if (btn) {
					BrushInt = i;
				}
			}
		}
		GUI.EndScrollView();

		GUI.color = new Color(1,1,1,1);
		GUI.Label(new Rect(20,230,50,20),"Opacity",style);
		Alpha = GUI.HorizontalSlider(new Rect(20,250,140,15),Alpha,0,1);

		GUI.Label(new Rect(20,270,50,20),"Brush Size",style);
		BrushSize = GUI.HorizontalSlider(new Rect(20,290,140,15),BrushSize,0.1f,5);

		GUI.Label(new Rect(20,310,50,20),"Evere Frame(F)",style);
		everyFrame = GUI.Toggle(new Rect(120,310,20,20),everyFrame,"",Buttons);
		
		GUI.Label(new Rect(20,340,50,20),"Erase(E)",style);
		Erase = GUI.Toggle(new Rect(85,340,20,20),Erase,"",Buttons);

		GUI.Label(new Rect(20,370,50,20),"Random Rotation(R)",style);
		RandomRotation = GUI.Toggle(new Rect(150,370,20,20),RandomRotation,"",Buttons);

		if (GUI.Button (new Rect (15, 400, 150, 40), "", bakeButton)) {
			UVPaint.SaveStateForRestore (Obj);
		}

		if (GUI.Button (new Rect (15, 450, 150, 40), "", StartButton)) {
			paintedTexture = Obj.GetComponent<Renderer>().material.mainTexture;
			UVPaint.SaveStateForRestore (Obj);
			RestoreContainer RC = Obj.GetComponent<RestoreContainer> ();
			if (RC != null)
				textureByProperty = RC.Texture_by_Property;
			Application.LoadLevel ("Game");
		}

	}
}
