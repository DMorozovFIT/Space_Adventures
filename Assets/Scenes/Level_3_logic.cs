using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Task_3
{
    ((Piano.Note, Piano.Note), (Piano.Note, Piano.Note))[] intervals;
    int[] ans;
    int[] rightAns;

    public Task_3()
    {
        intervals = new ((Piano.Note, Piano.Note), (Piano.Note, Piano.Note))[10];
        ans = new int[10];
        //rightAns = new int[10];
        intervals[0] = ((Piano.Note.C, Piano.Note.C2), (Piano.Note.B, Piano.Note.DSharp));
        intervals[1] = ((Piano.Note.A, Piano.Note.ASharp), (Piano.Note.CSharp, Piano.Note.DSharp));
        intervals[2] = ((Piano.Note.G, Piano.Note.B), (Piano.Note.F, Piano.Note.FSharp));
        intervals[3] = ((Piano.Note.CSharp, Piano.Note.F), (Piano.Note.CSharp, Piano.Note.E));

        intervals[4] = ((Piano.Note.C, Piano.Note.C2), (Piano.Note.F, Piano.Note.C2));

        intervals[5] = ((Piano.Note.D, Piano.Note.D), (Piano.Note.D, Piano.Note.DSharp));

        intervals[6] = ((Piano.Note.F, Piano.Note.FSharp), (Piano.Note.C, Piano.Note.B));

        intervals[7] = ((Piano.Note.D, Piano.Note.A), (Piano.Note.A, Piano.Note.ASharp));

        intervals[8] = ((Piano.Note.C, Piano.Note.C2), (Piano.Note.B, Piano.Note.DSharp));
        intervals[9] = ((Piano.Note.B, Piano.Note.C2), (Piano.Note.B, Piano.Note.DSharp));
        rightAns = new int[] { 1, 2, 1, 1, 1, 1, 2, 1, 1, 1 };
    }

    public bool SetAnswer(int i, string answer)
    {
        if (answer == "1") ans[i] = 1;
        else ans[i] = 2;
        return (ans[i] == rightAns[i]);
    }

    public ((Piano.Note, Piano.Note), (Piano.Note, Piano.Note)) GetIntervals(int i)
     {
        return intervals[i];
     }


    public int GetResult()
    {
        int res = 0;
        for(int i=0; i<10; i++)
        {
            if (ans[i] == rightAns[i]) res++;
        }
        return res;
    }

}

public class Level_3_logic : MonoBehaviour
{
    int completed_tasks;
    Task_3 task;
    Piano pianoObg;
    bool played1, played2, played3, played4, pressed;
    int timeStart;
    int task_res;

    [SerializeField]
    public AudioSource As;

    [SerializeField]
    public Image Note1;
    [SerializeField]
    public Image Note2;
    [SerializeField]
    public Image Note3;
    [SerializeField]
    public Image Note4;
    [SerializeField]
    public Image Note5;
    [SerializeField]
    public Image Note6;
    [SerializeField]
    public Image Note7;
    [SerializeField]
    public Image Note8;
    [SerializeField]
    public Image Note9;
    [SerializeField]
    public Image Note10;
    [SerializeField]
    public Image Ok;
    [SerializeField]
    public Image Fail;

    [SerializeField]
    public Button btn_1;
    [SerializeField]
    public Button btn_2;

    [SerializeField]
    public Canvas res_canvas;

    [SerializeField]
    public Image res_image;

    public void On_Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 6);
    }

    public void On_Btn_Click(string sender)
    {
        pressed = true;
        if(task.SetAnswer(completed_tasks, sender)==true)
        {
            Ok.enabled = true;
            task_res = 1;
            switch(sender)
            {
                case "1": Ok.transform.localPosition = new Vector3(btn_1.transform.localPosition.x, Ok.transform.localPosition.y); break;
                case "2": Ok.transform.localPosition = new Vector3(btn_2.transform.localPosition.x, Ok.transform.localPosition.y); break;
            }
        }
        else
        {
            Fail.enabled = true;
            Ok.enabled = true;
            task_res = 2;
            switch (sender)
            {
                case "1":
                    Ok.transform.localPosition = new Vector3(btn_2.transform.localPosition.x, Ok.transform.localPosition.y);
                    Fail.transform.localPosition = new Vector3(btn_1.transform.localPosition.x, Ok.transform.localPosition.y);
                    break;
                case "2": Ok.transform.localPosition = new Vector3(btn_1.transform.localPosition.x, Ok.transform.localPosition.y);
                    Fail.transform.localPosition = new Vector3(btn_2.transform.localPosition.x, Ok.transform.localPosition.y);
                    break;
            }
        }
        timeStart = System.Environment.TickCount;
        completed_tasks++;
        On_next();
    }

    void On_next()
    {
        played1 = false;
        played2 = false;
        played3 = false;
        played4 = false;
    }

    void Play1First(((Piano.Note, Piano.Note), (Piano.Note, Piano.Note)) interval)
    {
        pianoObg.PlayNote(As, interval.Item1.Item1);
        played1 = true;
    }

    void Play1Second(((Piano.Note, Piano.Note), (Piano.Note, Piano.Note)) interval)
    {
        pianoObg.PlayNote(As, interval.Item1.Item2);
        played2 = true;
    }

    void Play2First(((Piano.Note, Piano.Note), (Piano.Note, Piano.Note)) interval)
    {
        pianoObg.PlayNote(As, interval.Item2.Item1);
        played3 = true;
    }

    void Play2Second(((Piano.Note, Piano.Note), (Piano.Note, Piano.Note)) interval)
    {
        pianoObg.PlayNote(As, interval.Item2.Item2);
        played4 = true;
    }



    // Start is called before the first frame update
    void Start()
    {
        timeStart = System.Environment.TickCount;
        played1 = played2 = played3 = played4 = false;
        pressed = false;
        Ok.enabled = false;
        Fail.enabled = false;
        pianoObg = new Piano();
        task = new Task_3();
        res_canvas.enabled = false;
        completed_tasks = 0;
        task_res = -1;
    }

    void InterfaceControl()
    {
        btn_1.enabled = played1 && played2 && played3 && played4;
        btn_2.enabled = played1 && played2 && played3 && played4;

    }

    public void level_end()
    {
        int result = task.GetResult();
        if(result>5)
            res_image.sprite = Resources.Load<Sprite>("Ok");
        res_canvas.enabled = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (System.Environment.TickCount - timeStart > 180 && pressed)
        {
            Ok.enabled = false;
            Fail.enabled = false;
            pressed = false;
        }
        if (System.Environment.TickCount - timeStart > 60 && !played1)
        {
            Play1First(task.GetIntervals(completed_tasks));
            played1 = true;
        }
        if (System.Environment.TickCount - timeStart > 390 && !played2)
        {
            Play1Second(task.GetIntervals(completed_tasks));
            played2 = true;
        }
        if (System.Environment.TickCount - timeStart > 1890 && !played3)
        {
            Play2First(task.GetIntervals(completed_tasks));
            played3 = true;
        }
        if (System.Environment.TickCount - timeStart > 2220 && !played4)
        {
            Play1Second(task.GetIntervals(completed_tasks));
            played4 = true; ;
        }
        InterfaceControl();
        if (completed_tasks == 9)
            level_end();
        if (task_res == 1)
        {
            switch (completed_tasks)
            {
                case 1: Note1.sprite = Resources.Load<Sprite>("note_ok"); break;
                case 2: Note2.sprite = Resources.Load<Sprite>("note_ok"); break;
                case 3: Note3.sprite = Resources.Load<Sprite>("note_ok"); break;
                case 4: Note4.sprite = Resources.Load<Sprite>("note_ok"); break;
                case 5: Note5.sprite = Resources.Load<Sprite>("note_ok"); break;
                case 6: Note6.sprite = Resources.Load<Sprite>("note_ok"); break;
                case 7: Note7.sprite = Resources.Load<Sprite>("note_ok"); break;
                case 8: Note8.sprite = Resources.Load<Sprite>("note_ok"); break;
                case 9: Note9.sprite = Resources.Load<Sprite>("note_ok"); break;
                case 10: Note10.sprite = Resources.Load<Sprite>("note_ok"); break;
            }
            task_res = -1;
        }
        if (task_res == 2)
        {
            switch (completed_tasks)
            {
                case 1: Note1.sprite = Resources.Load<Sprite>("note_fail"); break;
                case 2: Note2.sprite = Resources.Load<Sprite>("note_fail"); break;
                case 3: Note3.sprite = Resources.Load<Sprite>("note_fail"); break;
                case 4: Note4.sprite = Resources.Load<Sprite>("note_fail"); break;
                case 5: Note5.sprite = Resources.Load<Sprite>("note_fail"); break;
                case 6: Note6.sprite = Resources.Load<Sprite>("note_fail"); break;
                case 7: Note7.sprite = Resources.Load<Sprite>("note_fail"); break;
                case 8: Note8.sprite = Resources.Load<Sprite>("note_fail"); break;
                case 9: Note9.sprite = Resources.Load<Sprite>("note_fail"); break;
                case 10: Note10.sprite = Resources.Load<Sprite>("note_fail"); break;
            }
            task_res = -1;
        }
        

    }
}
