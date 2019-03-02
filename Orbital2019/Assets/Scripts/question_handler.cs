using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Question_template
{
    public string text;
    public string answer1;
    public string answer2;
    public int right;

    public Question_template(string text_, string answer1_, string answer2_, int right_)
    {
        text = text_;
        right = right_;
        answer1 = answer1_;
        answer2 = answer2_;
    }
}

public class question_handler : MonoBehaviour
{

    [SerializeField]
    public Question question_prefab;

    [SerializeField]
    GameObject questions_pointer;

    [SerializeField]
    public Canvas canvas;

    List<Question> question_list;

    List<Question_template> all_possible_questions;

    KeyCode[,] keyCodePairs;

    [SerializeField]
    int TIME;

    [SerializeField]
    int timer;

    // Start is called before the first frame update
    void Start()
    {
        TIME = 10;
        timer = TIME;
        question_list = new List<Question>() { };

        //question database
        all_possible_questions = new List<Question_template>()
        {
            new Question_template("répondez faux !", "vrai", "faux", 2),
            new Question_template("répondez vrai !", "vrai", "faux", 1),
            new Question_template("répondez 1 !", "1", "un", 1),
            new Question_template("répondez sisi la famille !", "lasagna", "sisi", 2)
        };

        keyCodePairs = new KeyCode[,] { { KeyCode.LeftArrow, KeyCode.RightArrow },
            { KeyCode.A, KeyCode.B }};
    }

    // Update is called once per frame
    void Update()
    {
        timer -= 1;
        if(timer == 0)
        {
            timer = TIME;
            NewQuestion();
        }

        for(int i=0; i<question_list.Count;i+=1)
        {
            Question q = question_list[i];
            if(q.getAnswer()!=0)
            {
                Destroy(q.gameObject);
                question_list.RemoveAt(i);
                --i;
                Canvas.ForceUpdateCanvases();
            }
        }
    }

    void NewQuestion()
    {
        if (question_prefab == null)
        {
            Debug.Log("question prefab not loaded");
        }

        //choose random question
        int index_question = Random.Range(0, all_possible_questions.Count - 1);
        Question_template qt = all_possible_questions[index_question];
        //choose random key combinations
        int index_codepair = Random.Range(0, keyCodePairs.GetLength(0) - 1);
        KeyCode k1 = keyCodePairs[index_codepair,0];
        KeyCode k2 = keyCodePairs[index_codepair, 1];

        //choose random position
        float nm = 2 * 1.7075f;
        float x_ = question_prefab.GetComponent<RectTransform>().sizeDelta.x/nm;
        float y_ = question_prefab.GetComponent<RectTransform>().sizeDelta.y/nm;
        float x = Random.Range(-Screen.width / nm +x_, Screen.width/nm - x_);
        float y = Random.Range(-Screen.height/ nm + y_, Screen.height/nm - y_);

        //instantiate question
        Question q = Instantiate<Question>(question_prefab, new Vector3(x, y, 0), Quaternion.identity);
        q.transform.SetParent(questions_pointer.transform, false);
        //initialize question
        q.Initialiaze(qt.text, qt.answer1, qt.answer2, qt.right, k1, k2);

        //add to existing questions
        question_list.Add(q);

        Debug.Log("new question !");
    }
}
