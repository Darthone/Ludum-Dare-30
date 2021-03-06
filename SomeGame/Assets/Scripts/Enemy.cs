﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public float moveSpeed = 1f;
    public float shootSpeed = 0.5f;
    public float followDistance = 30f;
    public float laserSpeed = 30f;
    int points = 250;
    
    bool canShoot = true;
    bool stateChanging = false;
    int enemyState = 0;
    float minStateDelay = 1f;
    float maxStateDelay = 4f;
    int circle = 1;
    public GameObject floatingText;
    public GameObject explosionPrefab;
    SpriteRenderer sr;
    public Sprite[] enemyImages;
    public AudioClip laserSound;
	public AudioClip enemyDieSound;

    public int health = 1;

    public Color currentColor = Color.grey;

    public GameObject[] Projectiles;

    public GameObject player;
    public GameObject core;
    public GameObject target;

    int lastLayer;

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
        player = GameController.control.player;
        core  = GameController.control.core;
        sr = this.GetComponent<SpriteRenderer>();
        if (enemyImages.Length > 0) {
            sr.sprite = enemyImages[(int)Mathf.Round(Random.Range(0, enemyImages.Length - 1))];
        }

	    // randomize starting stats
        moveSpeed = Random.Range(-1f,1f) * 0.2f + .6f;
        shootSpeed = Random.Range(-1f, 1f) * 0.3f + 0.8f;
        followDistance = Random.Range(-1f, 1f) * 15f + 40f;
        laserSpeed = Random.Range(-1f, 1f) * 5f + 35f;

        lastLayer = player.layer;
	}

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("PlayerBullet")) {
            this.health -= 1;          
            Destroy(collision.gameObject);
            //add to score, draw points on screen
        }
    }

	// Update is called once per frame
	void Update () {
        if (this.health <= 0) {
            int addScore = (int)(points * GameController.control.multiplyer);
            GameController.control.score += addScore;
            GameObject textPoints = (GameObject)Instantiate(floatingText, this.transform.position, Quaternion.identity);
            textPoints.guiText.text = "+" + addScore.ToString();
            GameObject explosion = (GameObject)Instantiate(explosionPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
			AudioSource.PlayClipAtPoint(enemyDieSound, transform.position);
        }

        if (this.gameObject.layer == player.layer) {
            target = player;
            //if (player.layer != lastLayer) {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0f);
            //}
        } else {
            target = core;
            //if (player.layer != lastLayer) {
                this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 100f);
            //}
        }

        lastLayer = player.layer;


        Vector3 diff = target.transform.position - transform.position;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);

        //shooting
        if (canShoot) {
            audio.PlayOneShot(laserSound);
            GameObject laser = (GameObject)Instantiate(Projectiles[0], (this.transform.position + (new Vector3(Mathf.Cos(Mathf.Deg2Rad * rot_z) * 1.5f, Mathf.Sin(Mathf.Deg2Rad * rot_z) * 1.5f))), Quaternion.AngleAxis(rot_z, Vector3.forward));
            laser.layer = this.gameObject.layer;
            laser.rigidbody2D.velocity = laser.transform.right * laserSpeed;
            canShoot = false;
            StartCoroutine(delayShooting());
        }

        //randomize ai movement
        switch (enemyState) {
            case 0: // chase player
                //target.transform.position
                rigidbody2D.AddForce((target.transform.position - this.transform.position) * moveSpeed);

                if (!stateChanging) {
                    stateChanging = true;
                    StartCoroutine(stateDelay((int)Mathf.Round(Random.Range(0, 5)), Random.Range(minStateDelay, maxStateDelay)));
                }
                break;
            case 1: // circle player
                rigidbody2D.AddForce(this.transform.right * circle * 15f * moveSpeed);
                if (!stateChanging) {
                    stateChanging = true;
                    StartCoroutine(stateDelay((int)Mathf.Round(Random.Range(0, 5)), Random.Range(minStateDelay, maxStateDelay)));
                }
                break;
            case 2: // stay away from player
                if ((target.transform.position - this.transform.position).magnitude < followDistance)
                    rigidbody2D.AddForce((target.transform.position - this.transform.position) * moveSpeed * -1f);

                if (!stateChanging) {
                    stateChanging = true;
                    StartCoroutine(stateDelay((int)Mathf.Round(Random.Range(0, 5)), Random.Range(minStateDelay, maxStateDelay)));
                }
                break;
            case 3: // just attack core
                // check if player is on this layer
                if (this.gameObject.layer == player.gameObject.layer) {
                    target = player;
                    if (!stateChanging) {
                        stateChanging = true;
                        StartCoroutine(stateDelay((int)Mathf.Round(Random.Range(0, 5)), Random.Range(minStateDelay, maxStateDelay)));
                    }
                } else
                    target = core;

                // shoot at core
                if (!stateChanging && target != player) {
                    stateChanging = true;
                    StartCoroutine(stateDelay((int)Mathf.Round(Random.Range(0, 5)), Random.Range(minStateDelay, maxStateDelay)));
                }
                break;
            case 4: // move around a bit

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
