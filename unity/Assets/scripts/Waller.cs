using UnityEngine;
using System.Collections;

public class Waller : MonoBehaviour {
    public static string TAG = "Wall";
    public static string TAG_QUESTION = "Wall Question";
    public bool isQuestion = false;
    private GameObject minimapObject;
    private LayerMask _playerLayer;

    // Use this for initialization
    void Start () {
        gameObject.layer = LayerMask.NameToLayer("Default");
        transform.tag = isQuestion ? TAG_QUESTION : TAG;
        if (isQuestion) {
            _playerLayer = LayerMask.NameToLayer("Player");
            minimapObject = transform.FindChild("minimap").gameObject;
            if (minimapObject == null) {
                Debug.Log("ERROR: minimapObject can not be found in Question Prefab or its name IS NOT minimap");
            }else {
                minimapObject.layer = gameObject.layer;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void updateLayers(LayerMask layerMask){
        gameObject.layer = layerMask;
        if (minimapObject != null) {
            minimapObject.layer = layerMask.value == 0 ? layerMask : _playerLayer;
        }
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
