using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;


//public class QnA_json : MonoBehaviour
//{
//    public Text Question;
//    public Text[] answers;
//    string path;
//    public Button[] b;
//    Text a;
//    //public GameObject aObject;


//    private void Awake()
//    {

//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        /*a.text = b.GetComponentInChildren<Text>().text*/;
//        //QnA q1 = new QnA(); 

//        //string json = JsonUtility.ToJson(q1);
//        //Debug.Log(json);
//        //File.WriteAllText(Application.dataPath + "/QnA.json", json);

//        path = Application.dataPath + "/QnA.json";
//        string json = File.ReadAllText(path);
//        QnA q1 = JsonUtility.FromJson<QnA>(json);
//        Question.text = q1.Quesion;
//        string te = q1.Answer;
//        print(te);
//        CheckAnswer(te);
//        //int t = Random.Range(0, 3);

//        //answers[t].text = q1.Answer;

//        //int[] check = new int[3];

//        //for (int i = 0; i < check.Length ; i++)
//        //{
//        //    //if(i  ==  t)
//        //    //{
//        //    //    Debug.Log("i: "+i);
//        //    //    Debug.Log("t: "+t);
//        //    //    answers[i + 1].text = q1.options[i];
//        //    //}
//        //    //else
//        //    //{
//        //    //    answers[i].text = q1.options[i];
//        //    //    Debug.Log("amil");
//        //    //}

//        //    int t = Random.Range(0, 3);
//        //    answers[t].text = q1.Answer;

//        //    if (check[i] == t)
//        //    {

//        //    }
//        //}

//        //Debug.Log(q1.Quesion);
//        //Debug.Log(q1.Answer);
//        //Debug.Log(q1.options[0]);


//        int[] check = new int[3];
//        int i = 0;
//        int count = 0;

//        while (i <= 3)
//        {
//            int t = Random.Range(0, 4);
//            //Debug.Log(t);

//            if (i == 0)
//            {
//                answers[t].text = q1.Answer;
//                check[i] = t;
//               // Debug.Log("first: " + check[i]);
//                i++;
//                //count++;
//            }
//            else
//            {
//                if (!IntArrayLinearSearch(check, t))
//                {
//                    answers[t].text = q1.options[i - 1];
//                    check[i] = t;
//                    //Debug.Log("so on: " + check[i]);
//                    i++;
//                    //count++;
//                }
//                else
//                {
//                    //Debug.Log("Amil");
//                }
//            }


//            //Debug.Log("Amil1");
//            ////i++;
//            //Debug.Log("i++: " + i);
//        }
//    }

//    public static bool IntArrayLinearSearch(int[] data, int item)
//    {
//        int N = data.Length;
//        for (int i = 0; i < N; i++)
//            if (data[i] == item)
//                return true;
//        return false;
//    }

//    public void CheckAnswer(string q1)
//    {
//        print(q1);
//        for(int i =0; i< b.Length; i++)
//        {
//            if (q1 == b[i].GetComponentInChildren<Text>().text)
//            {
//                Debug.Log("Correct Answer");
//            }
//            else
//            {
//                Debug.Log("Wrong Answer");
//                break;
//            }
//        }
//        print("bhir agaye loop se");
//    }


//    public class QnA
//    {
//        public string Quesion;
//        public string Answer;
//        public string[] options;
//    }

//    //private class Course
//    //{
//    //    public string CourseName;
//    //    public string No_Of_Ques;
//    //    public QnA[] Ques_Data;
//    //}
//}


public class QnA_json : MonoBehaviour
{
    public Text Question;
    public Text[] answers;
    string path;
    int qCounter = 0;
    public Button[] b;
    public GameObject Wrong_Panel;

    // Start is called before the first frame update
    void Start()
    {
        
        //QnA q1 = new QnA(); 

        //string json = JsonUtility.ToJson(q1);
        //Debug.Log(json);
        //File.WriteAllText(Application.dataPath + "/QnA.json", json);


        //path = Application.dataPath + "/QnA.json";
        //string json = File.ReadAllText(path);
        //QnA q1 = JsonUtility.FromJson<QnA>(json);
        //Question.text = q1.Quesion;


        path = Application.dataPath + "/QnA.json";
        string json = File.ReadAllText(path);
        Course c1 = JsonUtility.FromJson<Course>(json);
        int n = int.Parse(c1.No_Of_Ques);

        //if (int.TryParse(c, out n))
        //{
        //    print("amil4");
        //    energyAmt.text = amt;
        //}

        //int qCounter = 0;


        StartQuestionRoutine(c1, n);

        //SetRandom(c1.Ques_Data[0]);

        //Debug.Log(c1.CourseName);
        //Debug.Log(c1.No_Of_Ques);
        //Debug.Log(c1.Ques_Data[1].options[0]);
        //var list = (QnA)c1.Ques_Data[0];
        //Debug.Log(list);

    }

    //private void Update()
    //{
    //    Course c1 = MakeJson();
    //    StartCoroutine(QuestionRoutin(c1));
    //}

    //public Course MakeJson()
    //{
    //    path = Application.dataPath + "/QnA.json";
    //    string json = File.ReadAllText(path);
    //    Course c1 = JsonUtility.FromJson<Course>(json);
    //    return c1;
    //}

    //public void UpdateQuestion()
    //{
    //    Course c = MakeJson();
    //    print("1" + qCounter);
    //    Question.text = c.Ques_Data[qCounter].Quesion;
    //    qCounter++;
    //    print(qCounter);
    //}

    public void SetRandom(Course c)
    {
        int[] check = new int[3];
        int i = 0;
        //int count = 0;

        while (i <= 3)
        {
            int t = UnityEngine.Random.Range(0, 4);
            Debug.Log(t);

            if (i == 0)
            {
                //answers[t].text = q1.Answer;
                answers[t].text = c.Ques_Data[i].Answer;
                check[i] = t;
                Debug.Log("first: " + check[i]);
                i++;
                //count++;
            }
            else
            {
                if (!IntArrayLinearSearch(check, t))
                {
                    answers[t].text = c.Ques_Data[i].options[i];
                    //answers[t].text = q1.options[i - 1];
                    check[i] = t;
                    Debug.Log("so on: " + check[i]);
                    i++;
                    //count++;
                }
                else
                {
                    Debug.Log("Amil");
                }
            }


            Debug.Log("Amil1");
            //i++;
            Debug.Log("i++: " + i);
        }
    }

    public static bool IntArrayLinearSearch(int[] data, int item)
    {
        int N = data.Length;
        for (int i = 0; i < N; i++)
            if (data[i] == item)
                return true;
        return false;
    }

    public void StartQuestionRoutine(Course c, int n)
    {
        StartCoroutine(QuestionRoutin(c, n));
        //StartCoroutine(ButtonUpdate(c, qCounter, n));
    }

    //for changing questions
    IEnumerator QuestionRoutin(Course c, int n)
    {
        //print(qCounter);
        //print("amil");
        //qCounter++;
        //print(qCounter);
        while(qCounter < n)
        {
            print("n: " + n);
            yield return new WaitForSeconds(3.0f);
            Question.text = c.Ques_Data[qCounter].Quesion;

            //for(int i =0; i< b.Length; i++)
            //{
            //    b[i].GetComponentInChildren<Text>().text = c.Ques_Data[i].options[i];
            //}

            //SetRandom(c);
            print("Before" + qCounter);
            qCounter++;
            //b[qCounter].GetComponentInChildren<Text>().text = "";
            //ButtonUpdate(c, qCounter);
            print("After" + qCounter);
        }
    }

    IEnumerator ButtonUpdate(Course c ,int qCounter, int n)
    {
        while(qCounter < n)
        {
            b[qCounter].GetComponentInChildren<Text>().text = c.Ques_Data[qCounter].Answer;
            for (int i = 1; i < 4; i++)
            {
                b[i].GetComponentInChildren<Text>().text = c.Ques_Data[qCounter].options[i - 1];
            }

            yield return new WaitForSeconds(3.0f);
        }
    }

    public void CheckAnswer(string q1)
    {
        print(q1);
        for (int i = 0; i < b.Length; i++)
        {
            if (q1 == b[i].GetComponentInChildren<Text>().text)
            {
                Debug.Log("Correct Answer");
            }
            else
            {
                Debug.Log("Wrong Answer");
                Animator anim = Wrong_Panel.GetComponentInChildren<Animator>();
                if(anim != null)
                {
                    Wrong_Panel.SetActive(true);
                    int c = 0;
                    bool isPressed = anim.GetBool("isPressed");
                    anim.SetBool("isPressed", !isPressed);
                }
                else
                {
                    Wrong_Panel.SetActive(false);
                }
                break;
            }
        }
        print("bhir agaye loop se");
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