using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;


public class temp : MonoBehaviour
{
    
    //public Text[] answers;
    public Text Question;
    public Text Timer;
    public Text rightAnswers;
    public Text wrongAnswers;
    public Text score;
    public Text finalScore;
    public Button[] b;

    Course c1;
    int n;          //total no of questions present in the json file
    string path;

    List<QnA> quesAns = new List<QnA>(); //for savng Qdata from json

    Coroutine co;

    public GameObject Welcome_Panel;
    public GameObject Wrong_Panel;
    public GameObject Correct_Panel;
    public GameObject gameOverPanel;

    int score_value = 0;
    string amil; //for comparing button text with current question's answer


    //make them local some how
    int qCounter = 0;
    int rightAnswerCount = 0;
    int wrongAnswersCount = 0;
    
    int time_1 = 10;
    bool flag = false;
    ArrayList check = new ArrayList();
    int count = 0;
    int rand_gen;

    //timer
    public float currentTime = 0;
    public float startingTime = 10;
    int flag1 = 0;

    // Start is called before the first frame update
    void Start()
    {

        c1 = MakeJson();
        n = int.Parse(c1.No_Of_Ques);

        co = StartCoroutine(QuestionRoutin(quesAns, time_1));

        Invoke("disableWelcome", 3.0f);
        currentTime = startingTime;
    }
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        Timer.text = currentTime.ToString("0");
        if (currentTime <= 0)
        {
            //ClickSound();
            currentTime = 0;
            Timer.text = currentTime.ToString("0");
            //Timer.color = Color.red;
        }
    }

    public Course MakeJson()
    {
        path = Application.dataPath + "/QnA.json";
        string json = File.ReadAllText(path);
        Course c1 = JsonUtility.FromJson<Course>(json);
        return c1;
    }

    public void LoadQuestions(Course c1, int n, int no_Of_ques)
    {
        int i = 0;
        int r;
        ArrayList check_random_ques = new ArrayList();

        if (no_Of_ques <= n)
        {
            while (i < no_Of_ques)
            {
                r = UnityEngine.Random.Range(0, no_Of_ques);
                if (i == 0 && quesAns.Count == 0)
                {
                    check_random_ques.Add(r);
                    quesAns.Add(c1.Ques_Data[r]);
                    i++;
                }
                else if (check_random_ques.Contains(r) == false)
                {
                    check_random_ques.Add(r);
                    quesAns.Add(c1.Ques_Data[r]);
                    i++;
                }
                else
                {
                    print("same random no is regenerated again");
                }
            }
        }
        else
        {
            print("No of required questions is greater than no of questions present");
        }
    }


    int r = 0;
    ArrayList checkq = new ArrayList();
    int countq = 0;

    //for changing questions
    IEnumerator QuestionRoutin(List<QnA> qA, float ti)
    {
        while (qCounter < qA.Count)
        {

            Question.text = qA[qCounter].Quesion;
            RandomButton(qA, qCounter);

            amil = qA[qCounter].Answer;

            if (ti < 10)
            {
                //flag = false;
                ti = 10;
                yield return new WaitForSeconds(ti);

                //qCounter++;
                //StartQuestionRoutine(c1, n, 10);
            }
            else
            {
                //qCounter++;
                yield return new WaitForSeconds(ti);
                currentTime = startingTime;

                //qCounter++;

            }
        }
        finishRoutine();
    }

    public void RandomButton(List<QnA> qA, int qCounter)
    {
        while (count <= b.Length - 1)
        {
            rand_gen = UnityEngine.Random.Range(0, 4);

            if (count == 0)
            {
                check.Add(rand_gen);
                //b[count].GetComponentInChildren<Text>().text = c.Ques_Data[qCounter].options[rand_gen];
                b[count].GetComponentInChildren<Text>().text = qA[qCounter].options[rand_gen];
                count++;
            }

            else if (check.Contains(rand_gen) == false)
            {
                check.Add(rand_gen);
                //b[count].GetComponentInChildren<Text>().text = c.Ques_Data[qCounter].options[rand_gen];
                b[count].GetComponentInChildren<Text>().text = qA[qCounter].options[rand_gen];
                count++;
            }
            else
            {
                //Debug.Log("masla ha");
            }
        }

        if (count == b.Length)
        {
            count = 0;
            check.Clear();
        }

      //for hardcoded buttons
        //for (int i = 0; i < b.Length; i++)
        //{
        //    b[i].GetComponentInChildren<Text>().text = c.Ques_Data[qCounter].options[i];
        //}
    }

    public void CheckAnswer(Text button_txt)
    {
        //time_1 = 10.0f;
        if (button_txt.text == amil)
        {
            flag = true;

            Animator anim = Correct_Panel.GetComponentInChildren<Animator>();
            if (anim != null)
            {

                Correct_Panel.SetActive(true);
                bool isRightAnsPressed = anim.GetBool("isRightAnsPressed");
                anim.SetBool("isRightAnsPressed", !isRightAnsPressed);
                Invoke("disableCorrectPanel", 1.0f);
                score_value = score_value + 10;
                score.text = "Score: " + score_value;

                //  StartQuestionRoutine(c1, n, 1);
                currentTime = startingTime;
                rightAnswerCount++;
                StopCoroutine(co);
                print("stoped");
               StartCoroutine(QuestionRoutin(quesAns, time_1));
                //qCounter++;
                // flag = true;

            }

            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Animator anim = Wrong_Panel.GetComponentInChildren<Animator>();
            if (anim != null)
            {
                Wrong_Panel.SetActive(true);
                bool isPressed = anim.GetBool("isPressed");
                anim.SetBool("isPressed", !isPressed);
                Invoke("disableWrongPanel", 1.0f);
                score_value = score_value - 5;
                score.text = "Score: " + score_value;
                wrongAnswersCount++;
                currentTime = startingTime;
                StopCoroutine(co);
                print("stoped");
                StartCoroutine(QuestionRoutin(quesAns, time_1));
            }
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void disableWrongPanel()
    {
        Wrong_Panel.SetActive(false);
    }

    public void disableCorrectPanel()
    {
        Correct_Panel.SetActive(false);
    }

    public void disableWelcome()
    {
        Welcome_Panel.SetActive(false);
    }

    public void finishRoutine()
    {
        gameOverPanel.SetActive(true);
        print("game Ended");
        finalScore.text = score_value.ToString();
        rightAnswers.text = rightAnswerCount.ToString();
        wrongAnswers.text = wrongAnswersCount.ToString();
        Application.Quit();
    }

    public void replay()
    {
        SceneManager.LoadScene("Demo_UI");
    }

    [Serializable]
    public class QnA
    {
        public string Quesion;
        public string Answer;
        public string[] options;
    }

    [Serializable]
    public class Course
    {
        public string CourseName;
        public string No_Of_Ques;
        public QnA[] Ques_Data;
    }
}