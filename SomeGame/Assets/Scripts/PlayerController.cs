using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public GameObject laserPrefab;
    public GameObject bombPrefab;
    public GameObject shieldPrefab;
    
    public bool canShoot = true;
    public bool canMove = true;
    public bool canBeHurt = true;
    public AudioClip laserSound;

    public float shootSpeed = 0.25f;
    public float maxSpeed = 1f;
    public float SPEEDCONSTANT = 20F;
    public float laserSpeed = 30f;

    bool delayedShield = false;
    
    //float maxSpeed = 
    //float damping = 

    public int laserCount = 1;
    public int bombs = 1;
    Rect cameraRect;

    IEnumerator delayShooting() {
        yield return new WaitForSeconds(shootSpeed);
        canShoot = true;
    }

    IEnumerator delayInvul(float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(this.transform.GetChild(0).gameObject);
        canBeHurt = true;
        delayedShield = false;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (canBeHurt) {
            if (collision.gameObject.CompareTag("EnemyBullet")) {
                GameController.control.lives -= 1;
                laserCount = 1;
                if (GameController.control.lives >= 0) {
                    Respawn();
                } else {
                    GameController.control.GameOver();
                    GameController.control.lives = 3;
                }

                //add to score, draw points on screen
            }
        }
    }

	// Use this for initialization
	void Start () {
        var bottomLeft = Camera.main.camera.ScreenToWorldPoint(Vector3.zero);
        var topRight = Camera.main.camera.ScreenToWorldPoint(new Vector3(
            Camera.main.camera.pixelWidth, Camera.main.camera.pixelHeight));
        cameraRect = new Rect(
            bottomLeft.x,
            bottomLeft.y,
            topRight.x - bottomLeft.x,
            topRight.y - bottomLeft.y);
    }
	
	// Update is called once per frame
	void Update () {
        //draw shield if invul TODO
        if (!canBeHurt && !delayedShield) {
            GameObject shield = (GameObject)Instantiate(shieldPrefab, this.transform.position , Quaternion.AngleAxis(0, Vector3.forward));
            shield.transform.parent = this.transform;
            delayedShield = true;
            StartCoroutine(delayInvul(3f));
        }

        //fire
        if (Input.GetButton("Fire1") && canShoot) {
            audio.PlayOneShot(laserSound);
            Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            diff.Normalize();
            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;

            GameObject laser = (GameObject)Instantiate(laserPrefab, (this.transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * rot_z) * 1.5f, Mathf.Sin(Mathf.Deg2Rad * rot_z) * 1.5f))), Quaternion.AngleAxis(rot_z, Vector3.forward));
            laser.layer = this.gameObject.layer;
            laser.rigidbody2D.velocity = laser.transform.right * laserSpeed;


            canShoot = false;
            StartCoroutine(delayShooting());

            //laser count
        }

        if (bombs > 0 && Input.GetButtonDown("Fire2")){ // right click drop bombs
            // creat bomb under player
            GameObject bomb = (GameObject)Instantiate(bombPrefab, this.transform.position, Quaternion.AngleAxis(0f, Vector3.forward));
            bombs--;
        }

        //switch between layers
        if (Input.GetButtonDown("SwitchLeft")) { // place holder
            if (this.gameObject.layer > 8) {
                this.gameObject.layer--;
                //play sound
            } else { }
                // play  sound
        } else if (Input.GetButtonDown("SwitchRight")) {
            if (this.gameObject.layer < 8 + GameController.control.level) {
                this.gameObject.layer++;
                //play sound
            } else { 
                // play sound
            }
        }
	}

    void FixedUpdate() {
        //keep on screen
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, cameraRect.xMin, cameraRect.xMax),
            Mathf.Clamp(transform.position.y, cameraRect.yMin, cameraRect.yMax),
            0f);

        // face mouse
        Vector3 diff = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position);
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
        
        //get movement input
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Move(h, v);
    }

    void Move(float hMove, float vMove) {
        // Move the character
        rigidbody2D.AddForce(new Vector2(hMove * SPEEDCONSTANT * maxSpeed, vMove * SPEEDCONSTANT * maxSpeed));
        //rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
    }

    void Respawn(){
        StartCoroutine(GameController.control.Shake(0.5f, 3f));
        this.transform.position = Vector3.zero;
        this.rigidbody2D.velocity = Vector2.zero;
        GameObject shield = (GameObject)Instantiate(shieldPrefab, this.transform.position, Quaternion.AngleAxis(0, Vector3.forward));
        shield.transform.parent = this.transform;
        canBeHurt = false;
        StartCoroutine(delayInvul(3f)); // 3 second protected
        delayedShield = true;
        // play some exploding particles TODO
    }

    

}
