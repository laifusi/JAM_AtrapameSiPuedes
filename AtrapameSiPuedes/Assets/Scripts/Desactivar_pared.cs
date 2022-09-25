using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Desactivar_pared : MonoBehaviour
{
    // Start is called before the first frame update

    float timer = 10f;
    float actual_time;
    void Start()
    {
        actual_time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        actual_time += Time.deltaTime;
        if (actual_time >= timer)
            Destroy(this.gameObject);
    }
}
