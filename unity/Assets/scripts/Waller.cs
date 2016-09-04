using UnityEngine;
using System.Collections;

public class Waller : MonoBehaviour {
    public static string TAG = "Wall";
    
    // Use this for initialization
    void Start () {
        gameObject.layer = LayerMask.NameToLayer("Default");
        transform.tag = TAG;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    //void OnCollisionEnter(Collision collision) {
    //    Debug.Log("on collision enter");

    //    if (collision.collider.tag == "torch") {
    //        isTorched = true;
    //        gameObject.layer = layerWall;
    //    }
    //}

    //void OnCollisionExit(Collision collisionInfo) {
    //    Debug.Log("on collision exit");

    //    if (collisionInfo.collider.tag == "torch") {
    //        isTorched = false;
    //        gameObject.layer = layerDefault;
    //    }
    //}

}
