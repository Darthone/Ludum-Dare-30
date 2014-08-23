﻿using UnityEngine;
using System.Collections;

public class Core : MonoBehaviour {
    public int coreHealth = 400;
    public Texture2D healthbar;
    int coreMaxHealth = 400;
    float barDisplay = 0f;
    Vector2 pos = Vector2.zero;
    Vector2 size = new Vector2(100f, 5f);

	// Use this for initialization
	void Start () {
        coreMaxHealth = coreHealth;
	}
	
	// Update is called once per frame
	void Update () {
        if (coreHealth <= 0) {
            GameController.control.GameOver();
        }
        barDisplay = (float)coreHealth / (float)coreMaxHealth;
        size = new Vector2(barDisplay * 100f, size.y);
	}

    void OnGUI() {
        DrawQuad(new Rect(Screen.width / 2 - size.x / 2, Screen.height/2 + 60, size.x, size.y), Color.Lerp(Color.red, Color.green, barDisplay));
    }


    void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("EnemyBullet")) {
            coreHealth--;
            Destroy(collision.gameObject);
        }
    }

    void DrawQuad(Rect position, Color color) {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }

}
