using UnityEngine;
using System.Collections;
[RequireComponent(typeof(BulletEvents))]
public class CannonAmmo : MonoBehaviour {
    public AudioClip deathSound;
    public float devalueRate = 1;
    public float startingPointValue = 500;
    public float minPointValue = 100;
    public float currentPointValue;
    public bool isDead = false;
    public float deathFadeRate = 1f;

    public float explosiveForce = 500;
    public float explosionRadius = 10;

    BulletEvents bulletEvents;
    Renderer renderer;
    Rigidbody rb;
    float currentDevalueRate = 0;
    // Use this for initialization
    void Start () {
        renderer = GetComponent<Renderer> ();
        rb = GetComponent<Rigidbody> ();
        currentPointValue = startingPointValue;
        bulletEvents = GetComponent<BulletEvents> ();
        bulletEvents.OnBulletCollision += HandleBulletCollision;
    }

    void Update () {
        if (isDead == false) { 
            currentDevalueRate += devalueRate * Time.deltaTime;
            currentPointValue = Mathf.Max (minPointValue, currentPointValue - currentDevalueRate);
        } else {
            Color color = renderer.material.color;
            color.a = Mathf.Max (0, color.a - (deathFadeRate * Time.deltaTime));
            transform.localScale = Vector3.Max(Vector3.zero, transform.localScale - (Vector3.one * .5f * Time.deltaTime));
            rb.mass = rb.mass * (0.1f * Time.deltaTime);
            renderer.material.color = color;
            if (color.a == 0) {
                Destroy (gameObject);
            }
        }
    }

    void HandleBulletCollision (BulletEventArgs args) {
        Debug.Log ("#HandleBulletCollision");
        AudioSource.PlayClipAtPoint (deathSound, transform.position);
        GameObject text = TextPoint.AddTextAtPoint ((int) Mathf.Max(currentPointValue), transform.position, Vector3.one * 0.05f);
        text.transform.Rotate (new Vector3 (0, 180, 0));
        isDead = true;
        GetComponent<Collider> ().enabled = false;
        Vector3 force = explosiveForce * args.bullet.GetComponent<Rigidbody> ().velocity;
        rb.AddForceAtPosition (force, args.hit.point, ForceMode.VelocityChange);
        //rb.AddForce();
        Destroy (args.bullet);
    }
}
