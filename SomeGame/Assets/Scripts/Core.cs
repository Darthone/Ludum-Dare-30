using UnityEngine;
using System.Collections;

public class Core : MonoBehaviour {
    public int coreHealth = 200;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (coreHealth <= 0) {
            GameController.control.GameOver();
        }
	}
    void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("EnemyBullet")) {
            coreHealth--;
            Destroy(collision.gameObject);
        }
    }
}
