using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bourreau : MonoBehaviour
{
    enum Animation { Wait, Activate};
    Animation anim;

    [SerializeField]
    Sprite sprite1;
    [SerializeField]
    Sprite sprite2;
    [SerializeField]
    Sprite sprite_clac;

    int timer;

    // Start is called before the first frame update
    void Start()
    {
        anim = Animation.Wait;
        timer = 1;
    }

    // Update is called once per frame
    void Update()
    {
        --timer;
        if (timer < 0)
            timer = 100;

        if (anim == Animation.Wait)
        {
            if(timer>=50)
                GetComponent<SpriteRenderer>().sprite = sprite1;
            else if(timer<50)
                GetComponent<SpriteRenderer>().sprite = sprite2;
        }
        else if (anim == Animation.Activate)
        {
            GetComponent<SpriteRenderer>().sprite = sprite_clac;
        }

    }

    public void setActive(bool b)
    {
        if(b)
            anim = Animation.Activate;
        else
            anim = Animation.Wait;
    }
}
