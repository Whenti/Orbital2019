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

    enum State { Game, Intro, Intro2, BoxIntro, Lose, Reload};
    
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
    GameObject title;
    [SerializeField]
    GameObject names;
    [SerializeField]
    GameObject intro_object;
    [SerializeField]
    Bourreau bourreau;


    [SerializeField]
    Lame lame;

    [SerializeField]
    TeteGalilee tete_galilee;

    [SerializeField]
    public Canvas canvas;

    List<Question> question_list;

    List<Question_template> all_possible_questions;

    KeyCode[,] keyCodePairs;

    //TIMES
    [SerializeField]
    int TIME_QUESTIONS;
    int TIME_LOSE;
    [SerializeField]
    int timer;

    [SerializeField]
    Audio_Manager audio_manager;

    // Start is called before the first frame update
    void Start()
    {
        //init times
        TIME_QUESTIONS = 400;// 200;
        TIME_LOSE = 100;

        timer = TIME_QUESTIONS;
        question_list = new List<Question>() { };

        //init timer
        timer = 0;

        audio_manager.musique_clavecin.Play();

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
            new Question_template("Serai-je riche un jour ?", "Peut-être", "Peut-être pas", 1),
            new Question_template("Que ressent-on sous la couperet?", "L'ennuie", "La peur", 2),
            new Question_template("Comment pourrais-je, si cela est possible, améliorer ma recette de quiche à l'abricot ?", "Impossible", "Du sel", 2),
            new Question_template("Ô grand sage, puis-je vous poser une question indélicate ?", "Oui", "Non", 2),
            new Question_template("En quel position se trouve le pénultième coureur du course ?", "Avant dernier", "Second", 1),
            new Question_template("L'humain va-t-il un jour s'affranchir du travail ou restera-t-il à jamais esclave de sa condition ?", "Aucunement", "Moui", 2),
            new Question_template("Mon cousin Ernie est souffrant, quelle est la cause de son mal ?", "Tumeur", "Hernie", 1),
	    new Question_template("Quel n'est pas le contraire du mot \"non\"  ?", "Non", "Oui", 1),
	    new Question_template("J'ai tort ou j'ai tord ?", "Tord","Tort", 2),
	    new Question_template("? srevne 'l à eril suov-zevaS", "iuO", "noN",1),
            new Question_template("Pain au chocolat ou chocolatine ?", "Charlotte aux fraises", "Baba au rhum", 2),
	    new Question_template("Je souffre de diplopie !", "Ddee qquuooii??", "De quoi",1),
            new Question_template("Un coca ?", "Mouais, bof", "Allez, chaud !", 2),
	    new Question_template("Un hôtel ? ", "Trivago", "Oui, merci !", 1),
	    new Question_template("Comment appelle-t-on un éléphant mort ?", "Décédéléphant", "Eléphantôme", 2),
	    new Question_template("Qui est le père du cousin de la belle-mère de la coiffeuse de mon frère ?", "Henry IV", "Jeanne d'Arc",1),
	    new Question_template("... .- .-.. ..- -     -.-. .-     ...- .-     ··--··", "--- ..- .. ", "-. --- -.", 2),
	    new Question_template("C'est quoi le plus ?", "10.000.000", "10.000 lions",1),
	    new Question_template("Vite ! Réponds au bol !", "AAAAAAH", "Au bol", 2),
	    new Question_template("l","o","l",1),
	    new Question_template("Est-ce une question piège ?", "euh...", "hmm...", 2),
	    new Question_template("Un léopard, mais où ?", "Savane", "Jungle", 1),
	    new Question_template("12 citrons + 1 tronc = ?", "73 troncs", "1 citronnier", 2),
	    new Question_template("Liberté, égalité, ... ?", "Fraternité", "Maternité", 1),
	    new Question_template("Tu stresses ?", "Oskour", "Même pas", 2),
	    new Question_template("Que dit Oui-Oui quand il n'est pas d'accord ?", "Non-Non", "Non", 1),
	    new Question_template("Omae wa mou shindeiru", "Yamete", "NANI ?", 2),
	    new Question_template("En quelle année Galilée a-t-il été guillotiné ?", "Non", "1625", 1),
	    new Question_template("Ping !", "Ping ?", "Pong !", 2),
	    new Question_template("Tu connais la blague du con qui dit non ?", "Certes", "Non", 1),
	    new Question_template("O galileo galileo, galileo figaro ?", "Spaghetti ?", "MAGNIFICO", 2),
	    new Question_template("Est-ce qu'un chauve sourit ?", "Non", "Sûrement", 1),
	    new Question_template("XBox ou PlayStation ?", "Atari2600", "Wii U", 2),
	    new Question_template("Nespresso ?", "What else ?", "Toi-même", 1),
	    new Question_template("Dis \"paramédical\" !", "paramédical", "cal", 2),
	    new Question_template("Où va le monde ?", "Vers la droite", "C'est compliqué", 1),
	    new Question_template("XBox ou PlayStation ?", "Atari2600", "Wii U", 2),
	    new Question_template("Tenez, c'est cadeau !", "Bonne réponse", "Mauvaise réponse", 1),
	    new Question_template("Alors, c'est l'histoire d'un mec qui rentre dans un café et...", "On s'en fout", "Plouf", 2),
	    new Question_template("Athos, portos et ?", "Aramis", "Pastis", 1),
	    new Question_template("8 heures et quart, c'est l'heure de quoi ?", "Dormir", "Ricard", 2)
			
			/*
            new Question_template("répondez faux !", "vrai", "faux", 2),
            new Question_template("répondez vrai !", "vrai", "faux", 1),
            new Question_template("répondez 1 !", "1", "un", 1),
            new Question_template("répondez sisi la famille !", "lasagna", "sisi", 2)*/
        };

        keyCodePairs = new KeyCode[,] { { KeyCode.LeftArrow, KeyCode.RightArrow }};

        Intro();
    }

    public void Lose()
    {
        Debug.Log("you have lost :(");
        bourreau.setActive(true);
        timer = TIME_LOSE;
        game_state = State.Lose;

        audio_manager.musique_stressante.Stop();

        for(int i=0;i<question_list.Count;++i)
        {
            Destroy(question_list[i].gameObject);
        }
        question_list = new List<Question>(){ };
        Canvas.ForceUpdateCanvases();

        audio_manager.boo.Play();
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
                if (q.getAnswer() != 0) {
                    //if answer is good
                    if (q.getAnswer() == 1) {
                        lame.Right();
                        audio_manager.success.Play();
                    }
                    //if answer is bad
                    else if (q.getAnswer() == -1){
                        lame.Wrong();
                        audio_manager.failure.Play();
                    }

                    Destroy(q.gameObject);
                    question_list.RemoveAt(i);
                    --i;
                    Canvas.ForceUpdateCanvases();
                }
            }

            lame.UpdateHeight();
        }
        

        //LOSE
        else if (game_state == State.Lose)
        {
            //[100, 95] lame fall
            if(timer >= 95 && timer<= 100)
                lame.Fall((timer-95)/(float)5);

            if (timer == 95) {
                tete_galilee.tej();
                audio_manager.coupe.Play();
            }

            if (timer == 0)
            {
                game_state = State.Reload;
                timer = 200;
            }
        }

        //INTRO1
        else if (game_state == State.Intro)
        {
            float sy = Screen.height/100f;
            int a = 0;
            int b = 0;

            //FONDU
            a = 300;
            b = 200;
            if (timer <= a && timer >= b)
            {
                float lambda_fondu = (timer - b) / (float)(a-b);
                title.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1 - lambda_fondu);
            }
            //SIGNATURES
            //from -7 to -3.5
            a = 200;
            b = 100;
            if (timer <= a && timer>=b)
            {
                float lambda_names = (timer - b) / (float)(a-b);
                float y_names = lambda_names * (-7) + (1 - lambda_names) * (-3.5f);
                names.transform.localPosition = new Vector3(0, y_names, 0);
            }

            if(timer == 0)
            {
                game_state = State.BoxIntro;
                timer = 50;
            }
        }
        //BOXINTRO
        else if(game_state == State.BoxIntro)
        {
            if(timer == 0 && question_list.Count==0)
            {
                audio_manager.bouip.Play();
                Question q = Instantiate<Question>(question_prefab, new Vector3(0, -60, 0), Quaternion.identity);
                q.transform.SetParent(questions_pointer.transform, false);
                q.Initialiaze("Voulez-vous jouer ?", "Oui", "Non", 1, KeyCode.LeftArrow, KeyCode.RightArrow);
                question_list.Add(q);
            }

            if(question_list.Count==1)
            {
                Question q = question_list[0];
                if (q.getAnswer() != 0)
                {
                    if (q.getAnswer() == 1)
                    {
                        audio_manager.fondreClavecin();
                        audio_manager.success.Play();
                        game_state = State.Intro2;
                        timer = 200;
                    }
                    else if (q.getAnswer() == -1)
                    {
                        timer = 50;
                        audio_manager.failure.Play();
                    }
                    Destroy(q.gameObject);
                    question_list.RemoveAt(0);
                    Canvas.ForceUpdateCanvases();
                }
            }
        }

        //INTRO2
        else if (game_state == State.Intro2)
        {
            //[200-]
            float sy = Screen.height / 100f;
            int a = 200;
            int b = 0;
            float lambda = timer / (float)(a - b);

            if (timer <= a && timer >= b)
            {
                float intro_h = (1 - lambda) * (2 * sy) + lambda * (0);
                intro_object.transform.position = new Vector3(0, intro_h, 0);
                float background_h = (1 - lambda) * 0 + lambda * (-sy);
                background.transform.position = new Vector3(0, background_h, 0);
                float foreground_h = (1 - lambda) * 0 + lambda * 2 * (-sy);
                foreground.transform.position = new Vector3(0, foreground_h, 0);
            }

            if (timer == 0)
            {
                game_state = State.Game;
                audio_manager.musique_stressante.Play();
            }
        }

        //RELOAD
        else if (game_state == State.Reload)
        {
            float sy = Screen.height / 100f;
            int a = 200;
            int b = 0;

            float lambda = (timer-b) / (float)(a - b);
            lambda = 1 - lambda;

            float intro_h = (1 - lambda) * (2 * sy) + lambda * (0);
            intro_object.transform.position = new Vector3(0, intro_h, 0);
            float background_h = (1 - lambda) * 0 + lambda * (-sy);
            background.transform.position = new Vector3(0, background_h, 0);
            float foreground_h = (1 - lambda) * 0 + lambda * 2 * (-sy);
            foreground.transform.position = new Vector3(0, foreground_h, 0);

            if(timer==150)
            {
                audio_manager.musique_clavecin.Play();
            }

            if (timer == 0)
            {
                //reset all
                lame.Init();
                bourreau.setActive(false);
                game_state = State.BoxIntro;

                tete_galilee.Reinitialize();
                audio_manager.boo.Stop();
            }
        }

    }

    public void Intro()
    {
        float sy = Screen.height / 100f;
        float background_h = (-sy);
        background.transform.position = new Vector3(0, background_h, 0);
        float foreground_h =  2 * (-sy);
        foreground.transform.position = new Vector3(0, foreground_h, 0);

        //init game state
        game_state = State.Intro;
        timer = 300;
    }

    void NewQuestion()
    {
        if (question_prefab == null)
        {
            Debug.Log("question prefab not loaded");
        }

        //choose random question
        int index_question = Random.Range(0, all_possible_questions.Count);
        Question_template qt = all_possible_questions[index_question];
        //choose random key combinations
        int index_codepair = Random.Range(0, keyCodePairs.GetLength(0));
        KeyCode k1 = keyCodePairs[index_codepair,0];
        KeyCode k2 = keyCodePairs[index_codepair, 1];

        //choose random position
        float nm = 2;// * 1.7075f;
        float x_ = question_prefab.GetComponent<RectTransform>().sizeDelta.x/nm;
        float y_ = question_prefab.GetComponent<RectTransform>().sizeDelta.y/nm;

        float bx = 800;
        float by = 400;

        float x = Random.Range(-bx*0.5f / nm +x_, bx/nm - x_);
        float y = Random.Range(-by/ nm + y_, by/nm - y_);

        //instantiate question
        Question q = Instantiate<Question>(question_prefab, new Vector3(x, y, 0), Quaternion.identity);
        q.transform.SetParent(questions_pointer.transform, false);
        //initialize question
        q.Initialiaze(qt.text, qt.answer1, qt.answer2, qt.right, k1, k2);

        //add to existing questions
        question_list.Add(q);

        audio_manager.bouip.Play();

        //TIME_QUESTIONS = (int)(TIME_QUESTIONS*0.8f);

        Debug.Log("new question !");
    }
}
