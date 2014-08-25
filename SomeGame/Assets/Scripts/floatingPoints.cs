using UnityEngine;
using System.Collections;

public class floatingPoints : MonoBehaviour {

    public GUIText myGUItext;
    float alpha = 1f;
    public float scroll = 0.15f;
    public float duration = 1.5f;

	// Use this for initialization
	void Start () {
        scroll = Random.Range(0f, 0.2f);
        this.transform.position = Camera.main.camera.WorldToViewportPoint(this.transform.position);
        myGUItext = GetComponent<GUIText>();
	    myGUItext.material.color = new Color(1f,1f,1f,0.5f);
        alpha = 1f;
	}
	
	// Update is called once per frame
	void Update () {
        if (alpha > 0) {
            transform.Translate(Vector3.up * Time.deltaTime * scroll) ;
            //transform.position.Set(transform.position.x, transform.position.y + scroll * Time.deltaTime, transform.position.z);
            alpha -= Time.deltaTime / duration;
            myGUItext.material.color = new Color(1f, 1f, 1f, alpha);
        } else {
            Destroy(transform.gameObject);
        }
	}
}
