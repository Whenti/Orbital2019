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
    //represent the right answer to the question : 1 means 1st answser is correct, 2 means 2nd answser is correct
    int good_answer = 0;
    //variable assessing weither or not the user was right : -1 he is wrong, 0 no answer yet, 1 he is right
    int answer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    public int getAnswer() => good_answer;

    // Update is called once per frame
    void Update()
    {
    }

    public void Initialiaze(string text_, string a1_, string a2_, int good_answer_)
    {
        text.text = text_;
        a1.text = a1_;
        a2.text = a2_;
        good_answer = good_answer_;
    }
}
