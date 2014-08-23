using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    float timer = 1.5f;
    float explodeTimer = 0.5f;
    
	// Use this for initialization
	void Start () {
	    Invoke("Explode", timer);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void Explode(){
        Invoke("DestroyThis", timer);
    }

    void DestroyThis() {
        Destroy(this.gameObject);
    }

}
