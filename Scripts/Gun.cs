using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
    public GameObject bulletPrefab;
    public float bulletForce = 50;
    public bool automaticOn = true;
    public float autoFireRate = 0.5f;
    public AudioClip fireAudio;

    SteamVR_TrackedObject trackedObject;
    SteamVR_Controller.Device ctrl;
    Transform bulletDirectionTransform;
    float lastFireTime = 0;

    // Use this for initialization
    void Start () {
        trackedObject = GetComponent<SteamVR_TrackedObject> ();
        bulletDirectionTransform = transform.Find ("AimPointer");
    }

    // Update is called once per frame
    void Update () {
        ctrl = SteamVR_Controller.Input ((int) trackedObject.index);
        if (ctrl.GetTouchDown (SteamVR_Controller.ButtonMask.Touchpad)) {
            ToggleAutoFire ();
        }
        if (bulletPrefab) {
            if (automaticOn) {
                if (ctrl.GetTouch (SteamVR_Controller.ButtonMask.Trigger) && Time.time - lastFireTime >= autoFireRate) {
                    FireBullet ();
                }
            } else {
                if (ctrl.GetTouchDown (SteamVR_Controller.ButtonMask.Trigger)) {
                    FireBullet ();
                }
            }
        }
    }

    void ToggleAutoFire () {
        automaticOn = !automaticOn;
    }

    void FireBullet () {
        lastFireTime = Time.time;
        if (bulletPrefab) {
            Vector3 forward = bulletDirectionTransform ? bulletDirectionTransform.forward : transform.forward;
            GameObject obj = (GameObject) Instantiate (bulletPrefab, transform.position, Quaternion.Euler(forward));
            obj.GetComponent<Rigidbody> ().velocity = forward * bulletForce;
            if (fireAudio) {
                AudioSource.PlayClipAtPoint (fireAudio, transform.position);
            }
            Destroy (obj, 5);
        }
    }

    void OnFooEvent () {
        Debug.Log ("#ON FOO EVENT");
    }
}
