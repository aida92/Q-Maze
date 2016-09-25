using UnityEngine;
using System.Collections;

public class QuestionItem : MonoBehaviour {
    public Question question;
    public bool isAnswered = false;
    public bool isEnd = false;
    private Transform innerTexture;
    private Transform player;

	// Use this for initialization
	void Start () {
        innerTexture = transform.GetChild(0);
	}
	
	void FixedUpdate () {
        Vector3 look = player.position - innerTexture.position;
        //look.z = 0;
        //look.x = 0;
        innerTexture.rotation = Quaternion.LookRotation(look);
    }

    public void setPlayer(Transform _player) {
        player = _player;
    }
}
