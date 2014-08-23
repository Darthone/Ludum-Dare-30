using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public enum PowerUpType { Shield = 1, Upgrade, Bomb }
    public PowerUpType thisType;
    public GameObject floatingText;
    public int points = 1000;
    public AudioClip sound;

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
            GameController.control.score += (int)(points * GameController.control.multiplyer);
            GameObject text = (GameObject)Instantiate(floatingText, this.transform.position, Quaternion.identity);
            switch (thisType) {
                case PowerUpType.Shield:
                    text.guiText.text = "SHIELD";
                    pc.canBeHurt = false;
                    
                    break;
                case PowerUpType.Upgrade:
                    text.guiText.text = "LASERS +";
                    pc.laserCount++;
                    break;

                case PowerUpType.Bomb:
                    text.guiText.text = "BOMBS +";
                    pc.bombs++;
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
