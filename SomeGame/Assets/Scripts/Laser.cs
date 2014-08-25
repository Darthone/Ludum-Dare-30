using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {
	
	// Update is called once per frame
    void Update() {
        if (this.gameObject.layer == GameController.control.playerLayer)
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0f);
        else
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 100f);
    }

}
