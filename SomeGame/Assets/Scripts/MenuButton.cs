using UnityEngine;
using System.Collections;

public class MenuButton : MonoBehaviour {
    public enum ButtonType { Play = 1, Help, Credits }
    public ButtonType thisButton;
    public AudioClip select;

    float pushDist = 2f;
    float startTime;
    Vector3 start;
    bool mouseOver = false;
    bool display = false;
    bool sounded = false;
    BoxCollider2D b;
    Vector2 bs;
    void OnMouseOver() {
        mouseOver = true;
        if (!sounded) {
            audio.PlayOneShot(select);
            sounded = true;
        }
    }

    void OnMouseExit() {
        mouseOver = false;
        display = false;
        sounded = false;
    }

    void OnMouseUp() {
        if (mouseOver) {
            switch (thisButton) {
                case ButtonType.Play:
                    Application.LoadLevel("Game");
                    break;
                case ButtonType.Help:
                    //fly in help
                    break;
                case ButtonType.Credits:
                    // roll credits
                    break;
            }
        }
    }
	// Use this for initialization
	void Start () {
        b = this.collider2D as BoxCollider2D;
        bs = b.size;
        start = this.transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (mouseOver) {
            b.size = bs + new Vector2(pushDist * 10f * 2, 0f);
            
            this.transform.position = start - new Vector3(pushDist, 0f, 0f);
        } else {
            b.size = bs;
            this.transform.position = start;
        }
	}
}
