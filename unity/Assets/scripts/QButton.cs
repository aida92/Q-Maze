using UnityEngine;
using System.Collections;

public class QButton : MonoBehaviour {

    public int index = -1;
    public GameObject onClickListener;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void buttonTapped() {
        if (onClickListener != null) {
            onClickListener.SendMessage("answerSelected", index);
        }
    }
}
