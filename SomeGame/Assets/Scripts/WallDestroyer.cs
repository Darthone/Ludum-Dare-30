using UnityEngine;
using System.Collections;

public class WallDestroyer : MonoBehaviour {

	// Use this for initialization
    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag("PlayerBullet") || collision.collider.CompareTag("EnemyBullet")) {
            Destroy(collision.gameObject);
        }
    }
}
