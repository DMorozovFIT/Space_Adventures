using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Nav
{
    public class Nav_Logic : MonoBehaviour
    {
        float[,] platets_position_array = { { 1, 2, 3, 4, 5, 6 }, { 1, 2, 3, 4, 5, 6 } };//�������, �� ������� ����� ������������
        string[] planets_name_arr = { "�����", "���������", "��������", "������� ���������", "������� ������" };

        [SerializeField] int pos = 3;

        [SerializeField] public List<int> enabled_levels;

        [SerializeField] TextMeshProUGUI planet_name;

        [SerializeField] Button next_btn;

        [SerializeField] Button prev_btn;

        [SerializeField] public GameObject BG;

        Vector3[] planets_pos_arr;

        float startPosX, startPosY;

        bool next_flag = false, prev_flag = false;



        public void Next()
        {
            if (!IsInEnd())
            {
                pos++;
            }

            next_flag = true;
            Update();
        }

        public void Prev()
        {
            if (!IsInStart())
            {
                pos--;
            }
            prev_flag = true;
            Update();
        }
        
        public void toMainMeny()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }

        public void LoadEatrh()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void LoadDiatonic()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
        }


        public void LoadOstinato()
        {
            
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 5);
            
        }    

        private bool IsInEnd()
        {
            return (pos == 3- 1);
        }

        private bool IsInStart()
        {
            return (pos == 0);
        }

        // Start is called before the first frame update

        void Start()
        {
            startPosX = BG.transform.position.x;
            startPosY = BG.transform.position.y;
            planet_name.text = planets_name_arr[pos];
            planets_pos_arr = new Vector3[6];
            planets_pos_arr[0] = new Vector3(BG.transform.position.x, BG.transform.position.y); //new Vector3(BG.transform.position.x-100, BG.transform.position.y) };
            planets_pos_arr[1] = new Vector3(203, BG.transform.position.y);
            planets_pos_arr[2] = new Vector3(BG.transform.position.x, 248);
            planets_pos_arr[3] = new Vector3(203, 248);
        }

        // Update is called once per frame
        void Update()
        {
            planet_name.text = planets_name_arr[pos];
            next_btn.gameObject.SetActive(!IsInEnd() && !next_flag);
            prev_btn.gameObject.SetActive(!IsInStart() && !prev_flag);
            

            switch (pos)
            {
                case 0:
                    //������� �� 2 �� 1 �������
                    if (prev_flag && BG.transform.position.x < planets_pos_arr[pos].x - 1)
                        BG.transform.Translate(Vector3.right * Time.deltaTime * 750);
                    else { prev_flag = false; }
                    break;

                case 1:
                    //������� � 1 �� 2 �������
                    if (next_flag && (BG.transform.position.x > planets_pos_arr[pos].x))
                        BG.transform.Translate(Vector3.left * Time.deltaTime * 750);
                    else
                    { next_flag = false; }

                    if (prev_flag && BG.transform.position.x > planets_pos_arr[pos].x)
                    {
                        BG.transform.Translate(Vector3.left * Time.deltaTime * 750);
                        if (BG.transform.position.y > planets_pos_arr[pos].y)
                            BG.transform.Translate(Vector3.down * Time.deltaTime * 740);
                    }
                    else
                    { prev_flag = false; }

                    break;
                case 2:
                    //������� �� 2 �� 3 �������
                    if (next_flag && BG.transform.position.x < planets_pos_arr[pos].x - 1)
                    {
                        BG.transform.Translate(Vector3.right * Time.deltaTime * 750);
                        if (BG.transform.position.y <= planets_pos_arr[pos].y)
                            BG.transform.Translate(Vector3.up * Time.deltaTime * 750);
                    }
                    else { next_flag = false; }

                    //������� � 3 �� 4 �������
                    if (prev_flag && (BG.transform.position.x < planets_pos_arr[pos].x))
                        BG.transform.Translate(Vector3.right * Time.deltaTime * 750);
                    else
                    { prev_flag = false; }
                    break;

                case 3:
                    //������� � 3 �� 4 �������
                    if (next_flag && (BG.transform.position.x > planets_pos_arr[pos].x))
                        BG.transform.Translate(Vector3.left * Time.deltaTime * 750);
                    else
                    { next_flag = false; }
                    break;

            }
        }

    }
}

