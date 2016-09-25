using UnityEngine;
using System.Collections;

public class NetHelper {

    public static IEnumerator get(string url, MonoBehaviour receiver) {
        NetResponse listener = receiver as NetResponse;
        if( listener == null ) {
            Debug.Log("ERROR: Receiver must implement NetResponse interface. Receiver: " + receiver );
            yield return null;
        }
        WWW www = new WWW(url);
        yield return www;
        listener.onResponse(www.text);
    }
}
