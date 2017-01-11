using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour {

    Vector3 lastPos;
    Vector3 lastForward;

    void Start () {
        lastPos = transform.position;
        lastForward = transform.forward;
    }
    void Update () {
        float distance = Mathf.Abs (Vector3.Distance(transform.position, lastPos));
        RaycastHit hit;
        //Debug.Log (LayerMask.NameToLayer ("Ignore Raycast"));
        if (Physics.Linecast(lastPos, transform.position, out hit)) {
            BulletEvents be = hit.transform.GetComponent<BulletEvents> ();
            if (be) {
                BulletEventArgs args;
                args.bullet = gameObject;
                args.hit = hit;
                be.InvokeBulletCollision (args);
            }
        }

        lastForward = transform.forward;
        lastPos = transform.position;
    }
}
