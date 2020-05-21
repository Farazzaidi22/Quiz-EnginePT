using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class Rand_question : MonoBehaviour
{
    string path;
    Course c1;
    int n;


    List<QnA> quesAns = new List<QnA>();
    ArrayList check  = new ArrayList();

    // Start is called before the first frame update
    void Start()
    {
        c1 = MakeJson();
        n = int.Parse(c1.No_Of_Ques);

        LoadQuestions(c1, n, 3);

        for(int j = 0; j < 3; j++)
        {
            print(quesAns[j].Quesion);
            print(quesAns[j].Answer);
            print(quesAns[j].options[j]);
        }
    }

    public void LoadQuestions(Course c1, int n, int no_Of_ques)
    {
        int i = 0;
        int r;
        if (no_Of_ques <= n)
        {
            while(i < no_Of_ques)
            {
                r = UnityEngine.Random.Range(0, no_Of_ques);
                if(i == 0 && quesAns.Count == 0)
                {
                    check.Add(r);
                    quesAns.Add(c1.Ques_Data[r]);
                    i++;
                }
                else if(check.Contains(r) == false)
                {
                    check.Add(r);
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

    public Course MakeJson()
    {
        path = Application.dataPath + "/QnA.json";
        string json = File.ReadAllText(path);
        Course c1 = JsonUtility.FromJson<Course>(json);
        return c1;
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
