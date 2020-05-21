using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = System.Random;


public class QnA_json : MonoBehaviour
{
    public Text Question;
    public Text[] answers;
    string path;
    int qCounter = 0;
    int rightAnswerCount = 0;
    int wrongAnswersCount = 0;
    public Button[] b;
    int time_1 = 10;
    bool flag = false;
    ArrayList check = new ArrayList();
    int count = 0;
    int rand_gen;

    public Text score;

    public Text rightAnswers;
    public Text wrongAnswers;
    int score_value = 0;

    public Text finalScore;

    // bool press = false;
    Coroutine co;
    public GameObject Wrong_Panel;
    public GameObject Correct_Panel;
    public GameObject Welcome_Panel;
    public GameObject gameOverPanel;


    Course c1;
    int n;

    // Start is called before the first frame update
    void Start()
    {
        c1 = MakeJson();
        //path = Application.dataPath + "/QnA.json";
        //string json = File.ReadAllText(path);
        //Course c1 = JsonUtility.FromJson<Course>(json);
        n = int.Parse(c1.No_Of_Ques);


        //StartQuestionRoutine(c1, n, time_1);

        co = StartCoroutine(QuestionRoutin(c1, n, time_1));

        Invoke("disableWelcome", 3.0f);

    }

    public Course MakeJson()
    {
        path = Application.dataPath + "/QnA.json";
        string json = File.ReadAllText(path);
        Course c1 = JsonUtility.FromJson<Course>(json);
        return c1;
    }

    public void StartQuestionRoutine(Course c, int n, int t)
    {
        co = StartCoroutine(QuestionRoutin(c, n, t));
    }

    string amil;
    int r = 0;
    ArrayList checkq = new ArrayList();
    int countq = 0;

    //for changing questions
    IEnumerator QuestionRoutin(Course c, int n, int ti)
    {
        while (qCounter < 6)
        {

            print(qCounter);
            r = UnityEngine.Random.Range(0, 6);
            //Question.text = c.Ques_Data[r].Quesion;
            //RandomButton(c, r);
            //amil = c.Ques_Data[r].Answer;

            if (checkq.Count == 0)
            {

                checkq.Add(r);
                Question.text = c.Ques_Data[r].Quesion;
                RandomButton(c, r);
                amil = c.Ques_Data[r].Answer;
                qCounter++;


            }
            else if (checkq.Contains(r) == false)
            {

                checkq.Add(r);
                Question.text = c.Ques_Data[r].Quesion;
                RandomButton(c, r);
                amil = c.Ques_Data[r].Answer;

                qCounter++;

            }
            else
            {

               continue;
            }
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
                 //qCounter++;

            }

            //if (f == true)
            //{
            //    qCounter++;
            //    f = false;

            //    qCounter++;
            //    yield return new WaitForSeconds(1.0f);


            //}
            //else
            //{

            //    yield return new WaitForSeconds(10);
            //    qCounter++;
            //}
            //qCounter++;
        }

        finishRoutine();
           

    }


    public void RandomButton(Course c, int qCounter)
    {
        while (count <= b.Length - 1)
        {
            rand_gen = UnityEngine.Random.Range(0, 4);

            if (count == 0)
            {
                check.Add(rand_gen);
                b[count].GetComponentInChildren<Text>().text = c.Ques_Data[qCounter].options[rand_gen];
                count++;
            }

            else if (check.Contains(rand_gen) == false)
            {
                check.Add(rand_gen);
                b[count].GetComponentInChildren<Text>().text = c.Ques_Data[qCounter].options[rand_gen];
                count++;
            }
            else
            {
                Debug.Log("masla ha");
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
                rightAnswerCount++;
               StopCoroutine(co);
                print("stoped");
               StartQuestionRoutine(c1, n, 10);
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

                StopCoroutine(co);
                print("stoped");
                StartQuestionRoutine(c1, n, 10);
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


    public void ChangeButton()
    {
        for (int i = 0; i < b.Length; i++)
        {
            b[i].name = "option" + i;
        }
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