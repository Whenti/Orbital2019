using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lame : MonoBehaviour
{
    
    private float hauteur;
    private float hauteur_min;
    private float hauteur_max;
    private float hauteur_lose;

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
        hauteur_min = -1.228f;
        hauteur_max = -1.56f;
        hauteur_lose = -2.039f;

        //set beginning height
        hauteur = hauteur_min;

    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.localPosition = new Vector3(0, hauteur, 0);

    }

    public void UpdateHeight()
    {
        hauteur += time_effect;
        if (hauteur <= hauteur_min)
        {
            hauteur = hauteur_min;
        }
        if (hauteur >= hauteur_max)
        {
            hauteur = hauteur_max;
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
            hauteur = (1 - lambda) * hauteur_lose + lambda * hauteur_max;
    }
}
