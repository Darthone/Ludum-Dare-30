using UnityEngine;
using System.Collections;

public class MenuButton_Mac : MonoBehaviour {
    public GameObject Logo;
    public enum ButtonType { Play = 1, Help, Credits, Quit }
    public ButtonType thisButton;
    public AudioClip select;
    public Texture message;
    public bool toogleSound = false;
    
    Material mat = null;

    Rect messageRect;
    Vector3 logoDest = new Vector3(100f, 1.734491f);
    Vector3 logoStart;
    Vector2 destination;
    Vector2 flyoffDest = new Vector2(150f, 500f);
    float textureWidth;
    float textureHeight;
    float pushDist = 2f;
    Vector3 start;
    bool mouseOver = false;
    bool display = false;
    bool sounded = false;
    bool flyout;
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
        flyout = true;
        mouseOver = false;
        display = false;
        sounded = false;
    }

    void OnMouseUp() {
        if (mouseOver && !display) {
            switch (thisButton) {
                case ButtonType.Play:
                    audio.PlayOneShot(select);
                    Application.LoadLevel("Game_Mac");
                    break;
                case ButtonType.Help:
                    //fly in
                    audio.PlayOneShot(select);
                    flyout = false;
                    display = true;
                    textureWidth = 600f;
                    textureHeight = 600f;
					flyoffDest = new Vector2(150f, 1250f);
                    messageRect = new Rect(0f, -600f, textureWidth, textureHeight);
                    destination = new Vector2(100f, 0f);
                    //fly in help
                    break;
                case ButtonType.Credits:
                    audio.PlayOneShot(select);
                    display = true;
                    flyout = false;
                    display = true;
                    textureWidth = 600f;
                    textureHeight = 600f;
                    flyoffDest = new Vector2(150f, 1250f);
                    messageRect = new Rect(100f, 1500f, textureWidth, textureHeight);
                    destination = new Vector2(100f, 0f);
                    break;
				case ButtonType.Quit:
					audio.PlayOneShot(select);
					Application.Quit();
				break;
            }
        }
    }

    void Update() {
        if (toogleSound && Input.GetButtonUp("Mute")) {
            if (PlayerPrefs.HasKey("Muted")) {
                switch (PlayerPrefs.GetInt("Muted")){
                    case 0:
                        AudioListener.pause = true;
                        PlayerPrefs.SetInt("Muted", 1);
                        break;
                    case 1:
                        AudioListener.pause = false;
                        PlayerPrefs.SetInt("Muted", 0);
                        break;
                }
                PlayerPrefs.Save();
            } else {
                AudioListener.pause = true;
                PlayerPrefs.SetInt("Muted", 1);
                PlayerPrefs.Save();
            }
        }
        switch (PlayerPrefs.GetInt("Muted")) {
            case 1:
                AudioListener.pause = true;
                break;
            case 0:
                AudioListener.pause = false;
                break;
        }
    }

	// Use this for initialization
	void Start () {
        b = this.collider2D as BoxCollider2D;
        bs = b.size;
        start = this.transform.position;
        switch (PlayerPrefs.GetInt("Muted")) {
            case 1:
                AudioListener.pause = true;
                break;
            case 0:
                AudioListener.pause = false;
                break;
        }
        logoStart = Logo.transform.position;
	}

    void OnGUI() {
        if (display || flyout && message !=null) {
            Graphics.DrawTexture(messageRect, message, mat);
        }
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (display) {
            if (messageRect.position != destination) {
                messageRect = new Rect(messageRect.position.x + (destination.x - messageRect.position.x) * 0.1f,  messageRect.position.y + (destination.y - messageRect.position.y) * 0.1f, textureWidth, textureHeight);
            }
            Logo.transform.position = Vector3.Lerp(Logo.transform.position, logoDest, Time.deltaTime);
        }
        if (flyout) {
            if (messageRect.position != flyoffDest) {
                messageRect = new Rect(messageRect.position.x + (flyoffDest.x - messageRect.position.x) * 0.1f, messageRect.position.y + (flyoffDest.y - messageRect.position.y) * 0.1f, textureWidth, textureHeight);
            } else {
                flyout = false;
            }
            Logo.transform.position = Vector3.Lerp(Logo.transform.position, logoStart, Time.deltaTime );
        }

        if (mouseOver) {
            b.size = bs + new Vector2(pushDist * 10f * 2, 0f);
            this.transform.position = start - new Vector3(pushDist, 0f, 0f);
        } else {
            b.size = bs;
            this.transform.position = start;
        }
	}
}
