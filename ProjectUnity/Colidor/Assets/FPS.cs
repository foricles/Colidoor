using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class FPS : MonoBehaviour  {

	private CharacterController character;
	private Transform mcamera;

	private Vector3 move = Vector3.zero;
	private float rx = 0;
	private float ry = 0; 
	// Use this for initialization
	void Start () {
		character = GetComponent<CharacterController> ();
		mcamera = Camera.main.transform;
	}
	static int i = 0;
	// Update is called once per frame
	void Update () 
	{
		move.x = CrossPlatformInputManager.GetAxis(Joystic.horizontalAxisName) * 5 * Time.deltaTime;
		move.z = CrossPlatformInputManager.GetAxis(Joystic.verticalAxisName) * 5 * Time.deltaTime;

		move.y = -9;
		move = character.transform.TransformDirection (move);
		character.Move (move);

		rx += CrossPlatformInputManager.GetAxis (TouchPanel.verticalAxisName) * Time.deltaTime;
		ry += CrossPlatformInputManager.GetAxis (TouchPanel.horizontalAxisName) * Time.deltaTime;

		mcamera.transform.localRotation = Quaternion.Slerp (mcamera.transform.localRotation, Quaternion.Euler (-rx, 0, 0), 0.3f);
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.Euler (0, ry, 0), 0.3f);
	}
}
