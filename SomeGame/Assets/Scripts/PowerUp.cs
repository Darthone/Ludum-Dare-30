using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public enum PowerUpType { Shield = 1, Upgrade, Bomb }
    public PowerUpType thisType;

	// Use this for initialization
	void Start () {
	    // choose a type TODO
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")){
            PlayerController pc = collision.GetComponent<PlayerController>();
            switch (thisType) {
                case PowerUpType.Shield:
                    pc.canBeHurt = false;
                    break;
                case PowerUpType.Upgrade:
                    pc.laserCount++;
                    break;

                case PowerUpType.Bomb:
                    pc.bombs++;
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
