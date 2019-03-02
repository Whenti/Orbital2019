using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lame : MonoBehaviour
{
    
    private float hauteur;
    private float hauteur_shown;

    private float hauteur_min;
    private float hauteur_max;
    private float hauteur_lose;
    private float hauteur_delta;

    [SerializeField]
    question_handler qh;

    [SerializeField]
    float time_effect;
    [SerializeField]
    float wrong_effect;
    [SerializeField]
    float right_effect;

    // Start is called before the first frame update
    void Start()
    {
        //set limits
        hauteur_min = -1.45f;
        hauteur_max = 1.0f;
        hauteur_lose = -2.039f;
        hauteur_delta = 0.05f;

        //set beginning height
        Init();

    }

    public void Init()
    {
        hauteur = hauteur_min;
        hauteur_shown = hauteur_min;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition = new Vector3(0, hauteur_shown, 0);
    }

    public void UpdateHeight()
    {
        hauteur += time_effect;
        if (hauteur <= hauteur_min)
            hauteur = hauteur_min;
        else if (hauteur >= hauteur_max)
            hauteur = hauteur_max;

        if (Mathf.Abs(hauteur_shown - hauteur) < hauteur_delta)
            hauteur_shown = hauteur;
        else
            hauteur_shown += Mathf.Sign(hauteur - hauteur_shown) * hauteur_delta;

        if(hauteur_shown>=hauteur_max)
        {
            qh.Lose();
        }
    }

    public void Wrong()
    {
        hauteur += wrong_effect;
    }

    public void Right()
    {
        hauteur += right_effect;
    }

    public void Fall(float lambda)
    {
        if(lambda >= 0)
            hauteur_shown = (1 - lambda) * hauteur_lose + lambda * hauteur_max;
    }
}
