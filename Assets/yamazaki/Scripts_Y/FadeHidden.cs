using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeHidden : MonoBehaviour
{
    public float startDistance = 2;
    public float hiddenDisanta = 1;
    public float offset_y = -1;
    public float offset_z = -1;

    
    //説明： 透過オブジェクトはRenderringModeをFadeに変更し
    //本スクリプトをアタッチ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = Camera.main.transform.position;
        pos.y += offset_y;
        pos.z += offset_z;
        var d = Vector3.Distance(pos, transform.position);

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
