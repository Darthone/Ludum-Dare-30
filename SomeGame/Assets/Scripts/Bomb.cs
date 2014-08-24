using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

    float timer = 1.5f;
    float explodeTimer = 0.7f;
    float shockwaveSpeed = 0.35f;
    bool exploding = false;
    public AudioClip boom;
    
	// Use this for initialization
	void Start () {
        this.gameObject.layer = 0;
	    Invoke("Explode", timer);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (exploding) {
            this.transform.localScale += new Vector3(shockwaveSpeed, shockwaveSpeed, 0f); //.Set(this.transform.localScale.x + shockwaveSpeed, this.transform.localScale.y + shockwaveSpeed, 1f);
        }
	}

    void Explode(){
        exploding = true;
        AudioSource.PlayClipAtPoint(boom, this.transform.position);
        Invoke("DestroyThis", explodeTimer);
    }

    void DestroyThis() {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if (!collision.CompareTag("Player") && !collision.CompareTag("PowerUp") && !collision.CompareTag("PowerUp") &&
            !collision.CompareTag("DND")) {
                try {
                    collision.GetComponent<Enemy>().health -= 100;
                } catch { }
        }
    }
}
