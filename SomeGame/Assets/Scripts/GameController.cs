using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public static GameController control = null;
    public GameObject player;
    public GameObject core;
    public int playerLayer = 8;
    public Sprite[] guiImages;
    bool paused = false;
    public bool canPause = true;

    float fadeSpeed = 3f;          // Speed that the screen fades to and from black.
    private bool sceneStarting = true;      // Whether or not the scene is still fading in.
    public bool sceneEnding = false;

    public long score = 0;
    public int lives = 3;
    public int level = 4;

    public float minSpawnTime = 1f;
    public float maxSpawnTime = 3f;
    public float powerupChance = 0.15f;

	// Use this for initialization
    void Awake() {
        if (control == null) {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this) {
            Destroy(gameObject);
            return;
        }

        //fill screen with gui texture
        guiTexture.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Menu")) {
            if (canPause) {
                paused = togglePause();
                canPause = false;
                Invoke("resetPause", 0.05f);
            }
        }

        if (Input.GetButtonUp("Menu")) {
            canPause = true;
        }
	}

    void FixedUpdate() {
        playerLayer = player.layer;

        if (sceneStarting)
            StartScene();
        if (sceneEnding) {
            EndScene();
        }
    }

    void OnGUI() {
        if (paused) {
            GUILayout.Label("Game is paused!");
            if (GUILayout.Button("Unpause"))
                paused = togglePause();
        } else {
            // lives - top left
            
            //score - top right
            
            // layer - bottom left

            // core health - top middle

            // alerts
        }
    }

    bool togglePause() {
        if (Time.timeScale == 0f) {
            Time.timeScale = 1f;
            return (false);
        }
        else {
            Time.timeScale = 0f;
            return (true);
        }
    }

    //Fading Functions
    void OnLevelWasLoaded() {
        sceneStarting = true;
    }

    void FadeToClear() {
        // Lerp the colour of the texture between itself and transparent.
        guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
    }


    void FadeToBlack() {
        // Lerp the colour of the texture between itself and black.
        guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
    }


    void StartScene() {
        // Fade the texture to clear.
        FadeToClear();
        // If the texture is almost clear...
        if (guiTexture.color.a <= 0.02f) {
            // ... set the colour to clear and disable the GUITexture.
            guiTexture.color = Color.clear;
            guiTexture.enabled = false;

            // The scene is no longer starting.
            sceneStarting = false;
        }
    }


    public void EndScene() {
        // Make sure the texture is enabled.
        guiTexture.enabled = true;

        // Start fading towards black.
        FadeToBlack();
        if (guiTexture.color.a >= 0.95f) {
            // ... set the colour to clear and disable the GUITexture.
            guiTexture.color = Color.black;
            sceneEnding = false;
        }
    }

    public void GameOver() {
        Application.LoadLevel(Application.loadedLevel);
        Destroy(this.gameObject);
        // do some stuff
        // go to main menu
    }

}
