using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sang : MonoBehaviour
{

    float timer;
    float duree = 1.0f;//sec

    Vector3 vitesse;


    // Start is called before the first frame update
    void Start()
    {
        timer = 0;

        

        float alpha = Random.value * 360;
        alpha *= Mathf.PI / 180.0f;
        vitesse = new Vector3(Mathf.Cos(alpha), Mathf.Sin(alpha), 0).normalized * 0.2f;

        transform.localRotation = Quaternion.Euler(0, 0, Random.value * 360);
        transform.localScale *= Random.Range(0.6f, 1.4f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > duree) {
            Destroy(this.gameObject);
        }

        vitesse += new Vector3(0, -0.1f, 0);
        transform.position += vitesse;
        


    }
}
