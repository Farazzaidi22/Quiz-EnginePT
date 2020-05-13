using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class QnA_json : MonoBehaviour
{
    public Text Question;
    public Text[] answers;
    string path;

    // Start is called before the first frame update
    void Start()
    {
        //QnA q1 = new QnA(); 

        //string json = JsonUtility.ToJson(q1);
        //Debug.Log(json);
        //File.WriteAllText(Application.dataPath + "/QnA.json", json);

        path = Application.dataPath + "/QnA.json";
        string json = File.ReadAllText(path);
        QnA q1 = JsonUtility.FromJson<QnA>(json);
        Question.text = q1.Quesion;

        //int t = Random.Range(0, 3);

        //answers[t].text = q1.Answer;

        //int[] check = new int[3];

        //for (int i = 0; i < check.Length ; i++)
        //{
        //    //if(i  ==  t)
        //    //{
        //    //    Debug.Log("i: "+i);
        //    //    Debug.Log("t: "+t);
        //    //    answers[i + 1].text = q1.options[i];
        //    //}
        //    //else
        //    //{
        //    //    answers[i].text = q1.options[i];
        //    //    Debug.Log("amil");
        //    //}

        //    int t = Random.Range(0, 3);
        //    answers[t].text = q1.Answer;

        //    if (check[i] == t)
        //    {

        //    }
        //}

        //Debug.Log(q1.Quesion);
        //Debug.Log(q1.Answer);
        //Debug.Log(q1.options[0]);


        int[] check = new int[3];
        int i = 0;
        int count = 0;

        while(i <= 3)
        {
            int t = Random.Range(0, 4);
            Debug.Log(t);

            if(i == 0)
            {
                answers[t].text = q1.Answer;
                check[i] = t;
                Debug.Log("first: " + check[i]);
                i++;
                //count++;
            }
            else
            {
                if(!IntArrayLinearSearch(check, t))
                {
                    answers[t].text = q1.options[i -1];
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


    private class QnA
    {
        public string Quesion;
        public string Answer;
        public string[] options;
    }
}
