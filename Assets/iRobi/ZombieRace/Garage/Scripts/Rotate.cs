using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif


public class Rotate : MonoBehaviour
{
	public float speed = 1;
	public Transform camera ;

	Vector3 previous;

    float differenceX;
    float differenceZ;

    float xEuler;
    float yEuler;

	float smoothY;
	float smoothZ;

    Vector3 lastPos;
	Vector3 smooth;
	int i;


    void Start()
    {
		smooth = camera.position;
		yEuler = transform.eulerAngles.y;
		xEuler = transform.eulerAngles.x;

		i = 0;
	}

    
    void FixedUpdate()
    {

		if(Input.GetAxis ("Mouse ScrollWheel")<0 && i > 0)
		{
			
			camera.Translate(transform.right/4,Space.World);
			i--;

		}
		else if(Input.GetAxis ("Mouse ScrollWheel")>0 && i < 8)
		{
			camera.Translate(-transform.right/4,Space.World);
			i++;
		}

        if (Input.GetMouseButtonDown(1))//нажатие
        {
            previous = Input.mousePosition;
            yEuler += differenceX; //направление передвижения и запись
            xEuler -= differenceZ;
            

        }
		if (Input.GetMouseButton (1)) {//удержание
			differenceX = (Input.mousePosition.x - previous.x) / 5;
			differenceZ = (Input.mousePosition.y - previous.y) / 5;
            
		}

		smoothY = Mathf.Lerp(smoothY, yEuler + differenceX, Time.fixedDeltaTime*speed);
		smoothZ = Mathf.Lerp(smoothZ, xEuler - differenceZ, Time.fixedDeltaTime*speed);
        

		transform.rotation = Quaternion.Euler(0, smoothY, Mathf.Clamp(smoothZ, -10, 40));


	
    }
    
}