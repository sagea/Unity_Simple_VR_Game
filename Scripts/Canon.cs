using UnityEngine;
using System.Collections;

public class Canon : MonoBehaviour {
    public GameObject ammoPrefab;
    public float ammoLifetime = 20;
    public bool autoFire = true;
    public bool isRandom = false;
    public float randomChance = 0.1f;
    public float interval = 3;
    public float canonPower = 200;

    public AudioClip fireAudio;

    float lastFireTime;
    Transform forwardDir;
    void Start () {
        forwardDir = transform.Find ("AimPointer");
    }
    // Update is called once per frame
    void Update () {
        if (ammoPrefab && autoFire) {
            if (isRandom) {
                if (Random.value <= randomChance) {
                    FireItem (canonPower);
                }
            } else {
                if (lastFireTime == 0 || Time.time - lastFireTime >= interval) {
                    int angle = Random.Range (-45, 45);
                    transform.localRotation = Quaternion.Euler (0, 0, angle);
                    FireItem (canonPower);
                }
            }
        }
    }

    public void FireItem (float force = 400) {
        lastFireTime = Time.time;
        if (ammoPrefab) {
            Vector3 forward = forwardDir ? forwardDir.forward : transform.forward;
            GameObject ammo = (GameObject) Instantiate (ammoPrefab, transform.position, Quaternion.Euler (forward));
            ammo.GetComponent<Rigidbody> ().AddForce (forward * canonPower);
            if (fireAudio) {
                AudioSource.PlayClipAtPoint (fireAudio, transform.position);
            }
            Destroy (ammo, ammoLifetime);
        }
    }
}
