using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeHidden : MonoBehaviour
{
    public float startDistance = 2;
    public float hiddenDisanta = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var d = Vector3.Distance(Camera.main.transform.position, transform.position);

        var color = this.GetComponent<Renderer>().material.color;
        if (d <= hiddenDisanta)
            color.a = 0.0f;
        else if (d <= startDistance)
            color.a = (d - hiddenDisanta) / (startDistance - hiddenDisanta);
        else
            color.a = 1.0f;
        this.GetComponent<Renderer>().material.color = color;

    }
}
