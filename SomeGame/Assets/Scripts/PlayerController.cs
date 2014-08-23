using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject laserPrefab;
    public GameObject bombPrefab;

    public bool canShoot = true;
    public bool canMove = true;

    public float shootSpeed = 0.25f;
    public float maxSpeed = 1f;
    public float SPEEDCONSTANT = 20F;
    public float laserSpeed = 20f;
    //float maxSpeed = 
    //float damping = 

    int laserCount = 1;

    IEnumerator delayShooting() {
        yield return new WaitForSeconds(shootSpeed);
        canShoot = true;
    }

	// Use this for initialization
	void Start () {
        //print(this.gameObject.layer);
        //this.gameObject.layer = 9;
        //print(this.gameObject.layer);
    }
	
	// Update is called once per frame
	void Update () {
        //face mouse
        
        //fire
        if (Input.GetButton("Fire1") && canShoot) {
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            GameObject laser = (GameObject)Instantiate(laserPrefab, (this.transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * rot_z) * 1.5f, Mathf.Sin(Mathf.Deg2Rad * rot_z) * 1.5f))), Quaternion.AngleAxis(rot_z, Vector3.forward));

            laser.gameObject.layer = this.gameObject.layer;
            laser.rigidbody2D.velocity = laser.transform.right * laserSpeed;
            canShoot = false;
            StartCoroutine(delayShooting());
            //laser count
        }

        //switch between layers

	}

    void FixedUpdate() {
        Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h, v);
    }

    void Move(float hMove, float vMove) {
        // Move the character
        rigidbody2D.AddForce(new Vector2(hMove * SPEEDCONSTANT * maxSpeed, vMove * SPEEDCONSTANT * maxSpeed));
        //rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
    }

}
