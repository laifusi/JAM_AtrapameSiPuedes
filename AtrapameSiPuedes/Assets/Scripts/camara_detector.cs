using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camara_detector : MonoBehaviour
{

    public float limit_pos;

    public float limit_neg;

    public float vel_cam = 5;

    public float timer_limit = 20;
    public float timer = 0;

    public float dir = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var rotationVector = this.transform.rotation.eulerAngles;
        rotationVector.y += dir * vel_cam * Time.deltaTime;
        this.transform.rotation = Quaternion.Euler(rotationVector);
        timer += Time.deltaTime;
        if(timer >= timer_limit)
        {
            timer = 0;
            dir = dir * -1;
        }
    }
}
