using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

    public enum PowerUpType { Shield = 1, Upgrade, Bomb, Speed}
    public PowerUpType thisType;
    public GameObject floatingText;
    public int points = 1000;
    public AudioClip sound;

	// Use this for initialization
	void Start () {
	    // choose a type TODO
        thisType = (PowerUpType)Mathf.Round(Random.Range(1, 5));
	}

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player")){
           // play audio
			AudioSource.PlayClipAtPoint(sound, transform.position);

            PlayerController pc = collision.GetComponent<PlayerController>();
            int addScore = (int)(points * GameController.control.multiplyer);
            GameController.control.score += addScore;
            GameObject text = (GameObject)Instantiate(floatingText, this.transform.position, Quaternion.identity);
            GameObject textPoints = (GameObject)Instantiate(floatingText, this.transform.position, Quaternion.identity);
            textPoints.guiText.text = "+" + addScore.ToString();
            switch (thisType) {
                case PowerUpType.Shield:
                    text.guiText.text = "SHIELD";
                    pc.canBeHurt = false;
                    break;

                case PowerUpType.Upgrade:
                    text.guiText.text = "LASERS +";
                    pc.laserCount++;
                    pc.shootSpeed -= 0.02f;
                    pc.laserSpeed++;
                    break;

                case PowerUpType.Bomb:
                    text.guiText.text = "BOMBS +";
                    pc.bombs++;
                    break;

                case PowerUpType.Speed:
                    text.guiText.text = "SPEED +";
                    pc.maxSpeed += 0.15f;
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
