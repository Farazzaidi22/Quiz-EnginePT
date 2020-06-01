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
    int Tn; //total no of questions
    int Nc; //no of catagories

    List<QnA> quesAns = new List<QnA>();
    ArrayList checkr = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        c1 = MakeJson();
     
        Tn = int.Parse(c1.Total_no_Of_Ques);
        Nc = int.Parse(c1.no_of_categories);

        //Catagory choosen_cat = SearchForCatagory(c1, Nc, "Chemistry");
        //if(choosen_cat != null)
        //{
        //    LoadQuestions(choosen_cat, Tn, 2);
        //}

        //LoadQuestions(c1, Tn, 3);

        //for (int j = 0; j < 2; j++)
        //{
        //    print(quesAns[j].Question);
        //    print(quesAns[j].Answer);
        //    print(quesAns[j].options[j]);
        //}

        //StartQuestionRoutine(c1, n, time_1);
        Invoke("disableWelcome", 3.0f);
        Basic("Chemistry");

        //StartCoroutine(QuestionRoutin(quesAns, time_1));

    }

    public Course MakeJson()
    {
        path = Application.dataPath + "/QnA.json";
        string json = File.ReadAllText(path);
        //print("here it is: " + myJSON);
        Course c = JsonUtility.FromJson<Course>(json);
        return c;
    }

    public void Basic(string cat)
    {
        Catagory choosen_cat = SearchForCatagory(c1, Nc, cat);
        if (choosen_cat != null)
        {
            LoadQuestions(choosen_cat, Tn, 2);
            StartCoroutine(QuestionRoutin(quesAns, time_1));
        }
    }

    public Catagory SearchForCatagory(Course c, int no_of_categories, string cat)
    {
        Catagory cat_obj;

        for(int i = 0; i < no_of_categories; i++)
        {
            if(c.categories[i].category_name == cat)
            {
                cat_obj = c.categories[i];
                return cat_obj;
            }
        }
        return null;
    }

    //new one
    public void LoadQuestions(Catagory choosen_cat, int Tn, int no_Of_ques_to_be_loaded)
    {
        int i = 0;
        int r;
        int No_of_questons_in_cat = int.Parse(choosen_cat.No_Of_Ques); ;

        if (no_Of_ques_to_be_loaded < Tn && no_Of_ques_to_be_loaded <= No_of_questons_in_cat)
        {
            while (i < no_Of_ques_to_be_loaded)
            {
                r = UnityEngine.Random.Range(0, no_Of_ques_to_be_loaded);
                if (i == 0 && quesAns.Count == 0)
                {
                    checkr.Add(r);
                    quesAns.Add(choosen_cat.Ques_Data[r]);
                    i++;
                }
                else if (checkr.Contains(r) == false)
                {
                    checkr.Add(r);
                    quesAns.Add(choosen_cat.Ques_Data[r]);
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

    int ii = 0;

    //public void LoadQuestions(Course c1, int n, int no_Of_ques)
    //{
    //    int i = 0;
    //    int r;
    //    if (no_Of_ques <= n)
    //    {
    //        while (i < no_Of_ques)
    //        {
    //            r = UnityEngine.Random.Range(0, no_Of_ques);
    //            if (i == 0 && quesAns.Count == 0)
    //            {
    //                checkr.Add(r);
    //                quesAns.Add(c1.Ques_Data[r]);
    //                i++;
    //            }
    //            else if (checkr.Contains(r) == false)
    //            {
    //                checkr.Add(r);
    //                quesAns.Add(c1.Ques_Data[r]);
    //                i++;
    //            }
    //            else
    //            {
    //                print("same random no is regenerated again");
    //            }
    //        }
    //    }
    //    else
    //    {
    //        print("No of required questions is greater than no of questions present");
    //    }
    //}

    //public void StartQuestionRoutine(Course c, int n, float t)
    //{
    //StartCoroutine(QuestionRoutin(c, n, t));
    //}

    string amil;

    //for changing questions
    IEnumerator QuestionRoutin(List<QnA> qA, float t)
    {
        while (qCounter < qA.Count)
        {

            Question.text = qA[qCounter].Question;
            RandomButton(qA, qCounter);

            amil = qA[qCounter].Answer;
            qCounter++;

            yield return new WaitForSeconds(t);

            //Question.text = c.Ques_Data[qCounter].Quesion;
            //RandomButton(c, qCounter);

            //amil = c.Ques_Data[qCounter].Answer;

            //qCounter++;
            //yield return new WaitForSeconds(t);
        }
        ii++;

        if(qCounter >= quesAns.Count && ii <= Nc)
        {
            qCounter = 0;
            quesAns.Clear();
            checkr.Clear();
            Debug.Log("coming");
            Basic("Astronomy");
            ii++;
        }
        else
        {
            Debug.Log("finished");
        }
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
        if (button_txt.text == amil)
        {
            Animator anim = Correct_Panel.GetComponentInChildren<Animator>();
            if (anim != null)
            {
                Correct_Panel.SetActive(true);
                bool isRightAnsPressed = anim.GetBool("isRightAnsPressed");
                anim.SetBool("isRightAnsPressed", !isRightAnsPressed);
                Invoke("disableCorrectPanel", 1.0f);
                //StartQuestionRoutine(c1, n, 1.0f);
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
        public string Question;
        public string Answer;
        public string[] options;
    }

    [Serializable]
    public class Catagory
    {
        public string category_name;
        public string No_Of_Ques;
        public QnA[] Ques_Data;
    }

    [Serializable]
    public class Course
    {
        public string CourseName;
        public string Total_no_Of_Ques;
        public string no_of_categories;
        public Catagory[] categories;
    }
}

