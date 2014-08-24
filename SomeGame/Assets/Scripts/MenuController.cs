using UnityEngine;
using System.Collections;

public class MenuController : MonoBehaviour {


	
	GUIText myGUIText;
	GUIStyle myGUIStyle;




	void Awake() {
	//	if (control == null) {
	//		DontDestroyOnLoad(gameObject);
	//		control = this;
	//	}
	//	else if (control != this) {
	//		Destroy(gameObject);
	//		return;
	//	}
		
		//fill screen with gui texture
		guiTexture.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
		myGUIText = this.GetComponent<GUIText>();
		myGUIText.pixelOffset = new Vector2(Screen.width - 30f, Screen.height - 15f);
		myGUIStyle = new GUIStyle();
		myGUIStyle.fontStyle = myGUIText.fontStyle;
		myGUIStyle.fontSize = myGUIText.fontSize;
		myGUIStyle.font = myGUIText.font;
		myGUIStyle.normal.textColor = Color.white;
	//	pc = player.GetComponent<PlayerController>();
	//	underAttack = new bool[2];
	//	for (int i = 0; i < underAttack.Length; i++) {
	//		underAttack[i] = false;
	//	}
	}





	void OnGUI() {

			// lives - top left
			//GUI.DrawTexture( new Rect(30f,15f,110f,22f), guiLives[lives]);
			
			//score - top right
			myGUIText.text = "SCORE";
			
			// layer - bottom left
		//	float boxHeight = 26f;
		//	float boxDelim = 3f;
		//	float boxWidth = 14f;
		//	for(int i = 0;  i <= level; i ++){
				//if level alerted draw different Texture
		//		if (playerLayer - 8 == i) {
		//			GUI.DrawTexture(new Rect(30f + i * (boxWidth + boxDelim), Screen.height - 30f - boxHeight, boxWidth, boxHeight), guiLevel[0]);
		//		} else {
		//			GUI.DrawTexture(new Rect(30f + i * (boxWidth + boxDelim), Screen.height - 30f - boxHeight, boxWidth, boxHeight), guiLevel[1]);
		//		}
		//	}
			
			//bombs
		//	GUI.Label(new Rect(Screen.width - 54f -  33f, Screen.height - 30f - boxHeight + 1f, boxWidth + 10f, boxHeight), "x" + pc.bombs.ToString(), myGUIStyle);
		//	GUI.DrawTexture(new Rect(Screen.width - 54f, Screen.height - 30f - boxHeight, boxWidth + 10f, boxHeight), bombTex);
		}




}
