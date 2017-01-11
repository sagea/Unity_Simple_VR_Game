using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

public struct BulletEventArgs {
    public GameObject bullet;
    public RaycastHit hit;
}

public delegate void BulletEventHandler (BulletEventArgs args);
public class BulletEvents : MonoBehaviour {
    public event BulletEventHandler OnBulletCollision = delegate { };
    public void InvokeBulletCollision (BulletEventArgs args) {
        OnBulletCollision (args);
    }
}
