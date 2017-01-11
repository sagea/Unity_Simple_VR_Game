using UnityEngine;
using System.Collections;
public class TextPoint : MonoBehaviour {
    public int value = 0;
    public float timeout = 5;
    public TextMesh textMesh;
    public float animationRate = 1f;
    public float movementSpeed = 1f;
    public float startingvalue = 500;
    Renderer renderer;
    // Use this for initialization
    void Start () {
        textMesh = GetComponent<TextMesh> ();
        renderer = GetComponent<Renderer> ();
        if (textMesh == null) {
            textMesh = gameObject.AddComponent<TextMesh> ();
        }
        textMesh.fontSize = 100;
        textMesh.anchor = TextAnchor.MiddleCenter;
        textMesh.alignment = TextAlignment.Center;
        textMesh.text = value.ToString ();
        textMesh.color = new Color (39, 174, 96);
        //Destroy (gameObject, timeout);
    }

    //// Update is called once per frame
    void Update () {
        float rate = animationRate * Time.deltaTime;
        Color color = renderer.material.color;
        color.a = Mathf.Max(color.a - rate, 0);
        renderer.material.color = color;
        transform.position += Vector3.up * (rate * movementSpeed);
        if (color.a == 0) {
            Destroy (gameObject);
        }
    }

    public static GameObject AddTextAtPoint (int value, Vector3 pos) {
        return AddTextAtPoint (value, pos, Vector3.one);
    }
    public static GameObject AddTextAtPoint (int value, Vector3 pos, Vector3 scale) {
        GameObject text = new GameObject ();
        TextPoint tp = text.AddComponent<TextPoint> ();
        text.AddComponent<MeshRenderer> ();
        text.AddComponent<TextMesh> ();
        text.transform.position = pos;
        text.transform.localScale = scale;
        tp.value = value;
        return text;
    }
}
