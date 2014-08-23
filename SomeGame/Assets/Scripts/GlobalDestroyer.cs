using UnityEngine;
using System.Collections;

public class GlobalDestroyer : MonoBehaviour {

    void OnTriggerExit2D(Collider2D other) {
        if (!other.CompareTag("Player"))
            Destroy(other.gameObject);
    }
}
