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

    enum State { Game, Intro, Lose };
    
    State game_state;

    [SerializeField]
    public Question question_prefab;
    [SerializeField]
    GameObject questions_pointer;
    [SerializeField]
    GameObject background;
    [SerializeField]
    GameObject foreground;

    [SerializeField]
    Lame lame;

    [SerializeField]
    public Canvas canvas;

    List<Question> question_list;

    List<Question_template> all_possible_questions;

    KeyCode[,] keyCodePairs;

    //TIMES
    [SerializeField]
    int TIME_QUESTIONS;
    int TIME_LOSE;
    int TIME_INTRO;
    [SerializeField]
    int timer;

    // Start is called before the first frame update
    void Start()
    {
        //init times
        TIME_QUESTIONS = 200;
        TIME_LOSE = 100;
        TIME_INTRO = 200;

        timer = TIME_QUESTIONS;
        question_list = new List<Question>() { };

        //init timer
        timer = 0;

        //question database
        all_possible_questions = new List<Question_template>()
        {
            new Question_template("Combien y a-t-il de planètes ?", "8", "9", 1),
            new Question_template("Quel est mon poids en plumes ?", "15'000", "70'000", 2),
            new Question_template("De quel côté se lève le Soleil ?", "Est", "Ouest", 1),
            new Question_template("De quelle forme est le grain de beauté sur ma fesse droite ?", "Géoïdale", "Cruciforme", 2),
            new Question_template("Combien de veaux ont un couple de chevaux ?", "0", "2", 1),
            new Question_template("Pourquoi ma femme m'a quitté ?", "La voix", "L'odeur", 2),
            new Question_template("Peut-on repondre autre chose que oui à cette question ?", "Non ?", "Oui ! Mince !", 1),
            new Question_template("Mon prépuce a enflé cette nuit, est-ce grave ?", "Un peu" , "Du tout", 1),
            new Question_template("Serais-je riche un jour ?", "Peut-être", "Peut-être pas", 1),
            new Question_template("Que ressent-on sous la couperet?", "L'ennuie", "La peur", 2),
            new Question_template("Comment pourrais-je, si cela est possible, améliorer ma recette de quiche à l'abricot ?", "Impossible", "Du sel", 2),
            new Question_template("Ô grand sage, puis-je vous poser une question indélicate ?", "Oui", "Non", 2),
            new Question_template("En quel position se trouve le pénultième coureur du course ?", "Avant dernier", "Second", 1),
            new Question_template("L'humain va-t-il un jour s'affranchir du travail ou restera-t-il à jamais esclave de sa condition ?", "Aucunement", "Moui", 2),
            new Question_template("Mon cousin Ernie est souffrant, quelle est la cause de son mal ?", "Tumeur", "Hernie", 1)
			/*
            new Question_template("répondez faux !", "vrai", "faux", 2),
            new Question_template("répondez vrai !", "vrai", "faux", 1),
            new Question_template("répondez 1 !", "1", "un", 1),
            new Question_template("répondez sisi la famille !", "lasagna", "sisi", 2)*/
        };

        keyCodePairs = new KeyCode[,] { { KeyCode.LeftArrow, KeyCode.RightArrow },
            { KeyCode.A, KeyCode.B }};

        Intro();
    }

    public void Intro()
    {
        //init game state
        game_state = State.Intro;
        timer = TIME_INTRO;
    }

    public void Lose()
    {
        Debug.Log("you have lost :(");
        timer = TIME_LOSE;
        game_state = State.Lose;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= 1;
        if(timer<=0)
            timer = 0;

        //GAME
        if (game_state == State.Game)
        {
            if (timer == 0)
            {
                timer = TIME_QUESTIONS;
                NewQuestion();
            }

            for (int i = 0; i < question_list.Count; i += 1)
            {
                Question q = question_list[i];
                if (q.getAnswer() != 0)
                {
                    //if answer is good
                    if (q.getAnswer() == 1)
                        lame.Right();
                    //if answer is bad
                    else if (q.getAnswer() == -1)
                        lame.Wrong();

                    Destroy(q.gameObject);
                    question_list.RemoveAt(i);
                    --i;
                    Canvas.ForceUpdateCanvases();
                }
            }

            lame.UpdateHeight();
        }

        //LOSE
        if (game_state == State.Lose)
        {
            //[100, 95] lame fall
            if(timer >= 95 && timer<= 100)
                lame.Fall((timer-95)/(float)5);

            if (timer == 0)
                game_state = State.Game;
        }

        //INTRO
        if (game_state == State.Intro)
        {
            float sy = Screen.height/100f;

            //[200]
            float lambda = timer / 200f;
            float background_h = (1 - lambda) * 0 + lambda * (-sy);
            background.transform.position = new Vector3(background.transform.position.x, background_h, background.transform.position.z);
            float foreground_h = (1 - lambda) * 0 + lambda * 2*(-sy);
            foreground.transform.position = new Vector3(foreground.transform.position.x, foreground_h, foreground.transform.position.z);
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
