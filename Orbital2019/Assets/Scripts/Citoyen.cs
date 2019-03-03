using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Citoyen : MonoBehaviour
{

    [SerializeField] List<Sprite> sprites;
    float timer_idle;
    float duree_idle = 1.0f;//seconde;

    SpriteRenderer sprite_renderer;

    // Start is called before the first frame update
    void Start()
    {
        timer_idle = Random.value * 10;

        sprite_renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer_idle += Time.deltaTime;

        sprite_renderer.sprite = sprites[(int)( timer_idle/duree_idle)%sprites.Count];

        sprite_renderer.sortingOrder = -(int)(transform.position.y * 100)+1400;
    }
}
