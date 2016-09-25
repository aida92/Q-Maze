using UnityEngine;
using System.Collections;

public class User : MonoBehaviour {
    private string _id;
    private string _email;
    private string _username;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /** 
     * string data should be formatted as:
     * id,email
     * */
    public void setUser(string data){
        var splitted = data.Split(',');
        _id = splitted[0];
        _email = splitted[1];
    }
}
