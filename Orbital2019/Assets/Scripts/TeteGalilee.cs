﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeteGalilee : MonoBehaviour
{

    [SerializeField] List<Sprite> sprites;
    float timer_idle;
    float duree_idle = 1.0f;//seconde;

    SpriteRenderer sprite_renderer;

    Vector3 pos_initiale;

    bool est_tej;
    Vector3 vitesse;

    // Start is called before the first frame update
    void Start()
    {
        pos_initiale = transform.position;

        timer_idle = Random.value * 10;

        est_tej = false;

        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        timer_idle += Time.deltaTime;

        sprite_renderer.sprite = sprites[(int)(timer_idle / duree_idle) % sprites.Count];

        if (est_tej) {
            vitesse += new Vector3(0, -0.01f, 0);
            transform.position += vitesse;
        }
    }

    public void Reinitialize() {
        est_tej = false;
        transform.position = pos_initiale;
        
    }

    public void tej() {
        vitesse = new Vector3(0.3f, 0.2f, 0);
        est_tej = true;
    }
}