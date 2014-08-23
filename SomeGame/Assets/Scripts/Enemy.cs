using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float moveSpeed = 1f;
    public float shootSpeed = 0.4f;
    public float followDistance = 30f;
    public float laserSpeed = 25f;
    
    bool canShoot = true;
    bool stateChanging = false;
    int enemyState = 0;
    float maxStateDelay = 4f;
    int circle = 1;
    SpriteRenderer sr;

    public int health = 1;

    public Color currentColor = Color.grey;

    public GameObject[] Projectiles;

    public GameObject player;
    public GameObject core;
    public GameObject target;

    IEnumerator stateDelay(int nextState, float delay){
        if ((target.transform.position - this.transform.position).magnitude > 50f)
            nextState = 0;
        yield return new WaitForSeconds(delay + 1f);
        enemyState = nextState;
        stateChanging = false;
        circle = (int)Mathf.Sign(Random.Range(-1f,1f));
    }

    IEnumerator delayShooting() {
        yield return new WaitForSeconds(shootSpeed);
        canShoot = true;
    }

	// Use this for initialization
	void Start () {
        sr = this.GetComponent<SpriteRenderer>();
        //play some cool animaiton on start
	    // randomize starting stats
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("PlayerBullet")) {
            this.health -= 1;
            //add to score, draw points on screen
        }
    }

    void FixedUpdate() {
        sr.color = currentColor;
    }

	// Update is called once per frame
	void Update () {
        if (this.health <=0)
            Destroy(this.gameObject);

        if (this.gameObject.layer == player.gameObject.layer)
            target = player;
        else
            target = core;

        Vector3 diff = target.transform.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        //shooting
        if (canShoot) {
            GameObject laser = (GameObject)Instantiate(Projectiles[0], (this.transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * rot_z) * 1.5f, Mathf.Sin(Mathf.Deg2Rad * rot_z) * 1.5f))), Quaternion.AngleAxis(rot_z, Vector3.forward));
            laser.gameObject.layer = this.gameObject.layer;
            laser.rigidbody2D.velocity = laser.transform.right * laserSpeed;
            canShoot = false;
            StartCoroutine(delayShooting());
        }

        //randomize ai movement
        switch (enemyState) {
            case 0: // chase player
                if (target != player) {
                    enemyState = 3;
                    break;
                }
                //target.transform.position
                rigidbody2D.AddForce((target.transform.position - this.transform.position) * moveSpeed);

                //do stuff

                if (!stateChanging) {
                    stateChanging = true;
                    StartCoroutine(stateDelay((int)Mathf.Round(Random.Range(0, 4)), Random.Range(1f, maxStateDelay)));
                }
                break;
            case 1: // circle player
                if (target != player) {
                    enemyState = 3;
                    break;
                }
                rigidbody2D.AddForce(this.transform.right * circle * 15f * moveSpeed);
                if (!stateChanging) {
                    stateChanging = true;
                    StartCoroutine(stateDelay((int)Mathf.Round(Random.Range(0, 4)), Random.Range(1f, maxStateDelay)));
                }
                break;
            case 2: // stay away from player
                if (target != player) {
                    enemyState = 3;
                    break;
                }
                // stay away from player
                if ((target.transform.position - this.transform.position).magnitude < followDistance)
                    rigidbody2D.AddForce((target.transform.position - this.transform.position) * moveSpeed * -1f);

                if (!stateChanging) {
                    stateChanging = true;
                    StartCoroutine(stateDelay((int)Mathf.Round(Random.Range(0, 4)), Random.Range(1f, maxStateDelay)));
                }
                break;
            case 3: // just attack core
                // check if player is on this layer
                if (this.gameObject.layer == player.gameObject.layer)
                    target = player;
                else
                    target = core;

                // shoot at core
                if (!stateChanging && target != player) {
                    stateChanging = true;
                    StartCoroutine(stateDelay((int)Mathf.Round(Random.Range(0, 4)), Random.Range(1f, maxStateDelay)));
                }
                break;
            case 4: // move around a bit
                if (target != player) {
                    enemyState = 3;
                    break;
                }
                print("random");
                // some sort of sporatic movement
                rigidbody2D.AddForce((new Vector3(Random.value, Random.value, 0f)) * moveSpeed);

                if (!stateChanging) {
                    stateChanging = true;
                    StartCoroutine(stateDelay(0, 0.5f));
                }
                break;

            default:
                if (!stateChanging) {
                    stateChanging = true;
                    StartCoroutine(stateDelay(0, 0.5f));
                }
                break;
        }
	}


}
