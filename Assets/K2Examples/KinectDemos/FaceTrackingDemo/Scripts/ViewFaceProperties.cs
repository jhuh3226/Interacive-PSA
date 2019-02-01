using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFaceProperties : MonoBehaviour 
{
	[Tooltip("Index of the player, tracked by this component. 0 means the 1st player, 1 - the 2nd one, 2 - the 3rd one, etc.")]
	public int playerIndex = 0;

	[Tooltip("UI text used for information display.")]
	public UnityEngine.UI.Text infoText;

	// current face properties
	Dictionary<string, string> faceProps = new Dictionary<string, string>();

	// reference to kinect- and face-tracking managers
	private KinectManager kinectManager;
	private FacetrackingManager faceManager;


	void Update () 
	{
		// get the face-tracking manager instance
		if(faceManager == null)
		{
			kinectManager = KinectManager.Instance;
			faceManager = FacetrackingManager.Instance;
		}

		// get user-id by user-index
		long userId = kinectManager ? kinectManager.GetUserIdByIndex(playerIndex) : 0;

		if (kinectManager && kinectManager.IsInitialized() && userId != 0 &&
		   faceManager && faceManager.IsTrackingFace(userId)) 
		{
			if (faceManager.GetUserFaceProperties(userId, ref faceProps)) 
			{
				string sFaceProps = "";
				foreach (string propName in faceProps.Keys) 
				{
					string propValue = faceProps[propName];
					sFaceProps += propName + "=" + propValue + "\n";
				}

				if (infoText && sFaceProps.Length > 0) 
				{
					//Debug.Log("FaceProps: " + sFaceProps);
					infoText.text = sFaceProps;
				}
			}
		}

	}

}
