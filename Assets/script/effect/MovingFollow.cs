using UnityEngine;
using System.Collections;

public class MovingFollow : MonoBehaviour {
	public GameObject player;
	public bool alongX;
	public bool alongY;
	public float speed=0.6f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 p = player.transform.position;
		Vector2 op = transform.position;
		Vector2 np=op;
		float x = p.x;
		float y = p.y;
		if (alongX) {
//			x = Mathf.Lerp (op.x, x, speed);
			x = Mathf.MoveTowards (op.x, x, speed);
			np.x = x;

		}
		if (alongY) {
//			y = Mathf.Lerp (op.y, y, speed);
			y = Mathf.MoveTowards (op.y, y, speed);
			np.y =y;

		}
		transform.position = np;
	}
}
