using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    //question text object
    [SerializeField]
    Text text;

    //the a's objects
    [SerializeField]
    Text a1;
    [SerializeField]
    Text a2;

    //the k's objects
    [SerializeField]
    Text k1_text;
    [SerializeField]
    Text k2_text;

    //the different keycodes
    KeyCode k1 = KeyCode.None;
    KeyCode k2 = KeyCode.None;

    [SerializeField] Image flecheGauche;
    [SerializeField] Image flecheDroite;
    //represent the right answer to the question : 1 means 1st answser is correct, 2 means 2nd answser is correct
    int good_answer = 0;
    //variable assessing weither or not the user was right : -1 he is wrong, 0 no answer yet, 1 he is right
    int answer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    public int getAnswer() => answer;

    // Update is called once per frame
    void Update()
    {
        bool well_defined = (k1 != KeyCode.None && k2 != KeyCode.None);
        if(well_defined)
        {
            if(Input.GetKeyUp(k1))
            {
                Debug.Log("oui");
                answer = (1 == good_answer) ? 1 : -1;
                flecheGauche.color = Color.white;
            }
            else if(Input.GetKeyUp(k2))
            {
                Debug.Log("non");
                answer = (2 == good_answer) ? 1 : -1;
                flecheDroite.color = Color.white;
            }
        }
    }

    public void Initialiaze(string text_, string a1_, string a2_, int good_answer_, KeyCode k1_, KeyCode k2_)
    {
        text.text = text_;
        a1.text = a1_;
        a2.text = a2_;
        k1 = k1_;
        k2 = k2_;
        k1_text.text = k1_.ToString();
        k2_text.text = k2_.ToString();
        good_answer = good_answer_;
    }
}
