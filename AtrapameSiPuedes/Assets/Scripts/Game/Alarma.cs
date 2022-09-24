using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alarma : MonoBehaviour
{

    private float timer_change = 2f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= 1)
        {
            timer = 0;
            ChangeColor();
        }

    }

    private void ChangeColor()
    {
        if(this.gameObject.GetComponent<Image>().color == Color.red)
        {
            this.gameObject.GetComponent<Image>().color = Color.yellow;
        }
        else
        {
            this.gameObject.GetComponent<Image>().color = Color.red;
        }
    }
}
