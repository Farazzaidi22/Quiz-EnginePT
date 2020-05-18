using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = System.Random;


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

   // bool press = false;

    public GameObject Wrong_Panel;
    public GameObject Correct_Panel;
    public GameObject Welcome_Panel;


    public System.Random a = new System.Random(); // replace from new Random(DateTime.Now.Ticks.GetHashCode());
                                                 // Since similar code is done in default constructor internally
    public List<int> randomList = new List<int>();
    int MyNumber = 0;


    // Start is called before the first frame update
    void Start()
    {

        path = Application.dataPath + "/QnA.json";
        string json = File.ReadAllText(path);
        Course c1 = JsonUtility.FromJson<Course>(json);
        int n = int.Parse(c1.No_Of_Ques);


        StartQuestionRoutine(c1, n);

        Invoke("disableWelcome", 3.0f);
        //ChangeButton();

    }

    public Course MakeJson()
    {
        path = Application.dataPath + "/QnA.json";
        string json = File.ReadAllText(path);
        Course c1 = JsonUtility.FromJson<Course>(json);
        return c1;
    }

    public void StartQuestionRoutine(Course c, int n)
    {
        StartCoroutine(QuestionRoutin(c, n));
    }

    string amil;

    //for changing questions
    IEnumerator QuestionRoutin(Course c, int n)
    {
        while(qCounter < n)
        {
            //if(press)
            //{
            //    yield return new WaitForSeconds(0.1f);
            //}
            //else
            //{
                yield return new WaitForSeconds(3.0f);
            //}

            Question.text = c.Ques_Data[qCounter].Quesion;

            //for (int i = 0; i < b.Length ; i++)
            //{
            //    b[i].GetComponentInChildren<Text>().text = c.Ques_Data[qCounter].options[i];
            //}

            print(qCounter);
            RandomButton(c, qCounter);

            //b[3].GetComponentInChildren<Text>().text = c.Ques_Data[qCounter].Answer;
            amil = c.Ques_Data[qCounter].Answer;

            qCounter++;
        }
    }

    //int[] check = new int[3];
    ArrayList check = new ArrayList();
    int count = 0;
    int t;

    public void RandomButton(Course c, int qCounter)
    {
        print("rand" + qCounter);
        while (count <= b.Length -1)
        {
            t = UnityEngine.Random.Range(0, 4);
            //print("t:" + t);
            if (count == 0)
            {
                //print(count);
                check.Add(t);
                b[count].GetComponentInChildren<Text>().text = c.Ques_Data[qCounter].options[t];
                print(c.Ques_Data[qCounter].options[t]);
                count++;
               // print(count);
            }

            else if(check.Contains(t) == false)
            {
                //print("else if wala: " + count);
                check.Add(t);
                b[count].GetComponentInChildren<Text>().text = c.Ques_Data[qCounter].options[t];
                print(c.Ques_Data[qCounter].options[t]);
                count++;
            }
            else
            {
                Debug.Log("masla ha");
            }
        }
        print(count);
        print(b.Length);
        if (count == b.Length)
        {
            count = 0;
            print("amil");
            print(check);
            check.Clear();
        }


        //for (int i = 0; i < b.Length; i++)
        //{
        //    b[i].GetComponentInChildren<Text>().text = c.Ques_Data[qCounter].options[i];
        //}
    }

    public void CheckAnswer(Text button_txt)
    {
       // press = true;
        if (button_txt.text == amil)
        {
            print("Chal gaya bc");

            Animator anim = Correct_Panel.GetComponentInChildren<Animator>();
            if (anim != null)
            {
                Correct_Panel.SetActive(true);
                bool isRightAnsPressed = anim.GetBool("isRightAnsPressed");
                anim.SetBool("isRightAnsPressed", !isRightAnsPressed);
                Invoke("disableCorrectPanel", 1.0f);
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

    //not ready to use functions
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

    public bool IntArrayLinearSearch(int[] data, int item)
    {
        int N = data.Length;
        for (int i = 0; i < N; i++)
            if (data[i] == item)
                return true;
        return false;
    }

    private void NewNumber()
    {
        MyNumber = a.Next(0, 4);
        if (!randomList.Contains(MyNumber))
            randomList.Add(MyNumber);
        print("randome list: " + MyNumber);
    }


    public void ChangeButton()
    {
        for (int i = 0; i < b.Length; i++)
        {
            b[i].name = "option" + i;
        }
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