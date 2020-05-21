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
    public Button[] b;

    string path;
    int qCounter = 0;
    float time_1 = 10.0f;

    ArrayList check = new ArrayList();
    int count = 0;
    int rand_gen;

    // bool press = false;

    public GameObject Wrong_Panel;
    public GameObject Correct_Panel;
    public GameObject Welcome_Panel;


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


        StartQuestionRoutine(c1, n, time_1);

        Invoke("disableWelcome", 3.0f);

    }

    public Course MakeJson()
    {
        path = Application.dataPath + "/QnA.json";
        string json = File.ReadAllText(path);
        Course c1 = JsonUtility.FromJson<Course>(json);
        return c1;
    }

    public void StartQuestionRoutine(Course c, int n, float t)
    {
        StartCoroutine(QuestionRoutin(c, n, t));
    }

    string amil;

    //for changing questions
    IEnumerator QuestionRoutin(Course c, int n, float t)
    {
        while(qCounter < n)
        {

            Question.text = c.Ques_Data[qCounter].Quesion;
            RandomButton(c, qCounter);

            amil = c.Ques_Data[qCounter].Answer;

            qCounter++;
            yield return new WaitForSeconds(t);
        }
    }


    public void RandomButton(Course c, int qCounter)
    {
        while (count <= b.Length -1)
        {
            rand_gen = UnityEngine.Random.Range(0, 4);
            
            if (count == 0)
            {
                check.Add(rand_gen);
                b[count].GetComponentInChildren<Text>().text = c.Ques_Data[qCounter].options[rand_gen];
                count++;
            }

            else if(check.Contains(rand_gen) == false)
            {
                check.Add(rand_gen);
                b[count].GetComponentInChildren<Text>().text = c.Ques_Data[qCounter].options[rand_gen];
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
        if (button_txt.text == amil)
        {
            Animator anim = Correct_Panel.GetComponentInChildren<Animator>();
            if (anim != null)
            {
                Correct_Panel.SetActive(true);
                bool isRightAnsPressed = anim.GetBool("isRightAnsPressed");
                anim.SetBool("isRightAnsPressed", !isRightAnsPressed);
                Invoke("disableCorrectPanel", 1.0f);
                StartQuestionRoutine(c1, n, 1.0f);
            }
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
            }
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