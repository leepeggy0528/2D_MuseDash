﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicManger : MonoBehaviour
{
    #region 欄位
    [Header("生成物件:上方")]
    public GameObject objup;
    [Header("生成物件:下方")]
    public GameObject objdown;
    [Header("生成位置:上方")]
    public Transform pointup;
    [Header("生成位置:下方")]
    public Transform pointdown;

    [Header("此關卡的音樂資料")]
    public Musicdata musicdata;

    [Header("判定位置:上方")]
    public Transform pointCheckup;
    [Header("判定位置:下方")]
    public Transform pointCheckdown;

    [Header("判定距離-perfect,good,miss")]
    public float rangePerfect = 0.5f;
    public float rangeGood = 0.8f;
    public float rangeMiss = 1.1f;

    [Header("上方節點檢查名稱")]
    public string nameup = "藍鳥";

    public AreaType areaType;

    ///<summary>
    ///Music-Music object
    /// </summary>
    private AudioSource aud;

    ///<summary>
    ///介面:血條
    /// </summary>
    private Image imgHp;
    ///<summary>
    ///介面:血量
    /// </summary>
    private Text textHp;

    ///<summary>
    ///player information
    /// </summary>
    private player player;

    private float maxHp;

    ///<summary>
    ///player information
    /// </summary>
    private CanvasGroup groupFinal;

    #endregion

    #region 事件
    /// <summary>
    /// starup
    /// </summary>
    private ParticleSystem psUp;

    /// <summary>
    /// stardown
    /// </summary>
    private ParticleSystem psDown;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(0, 0, 1, 0.8f);
        Gizmos.DrawSphere(pointCheckup.position, rangePerfect);
        Gizmos.color = new Color(0, 1, 0, 0.3f);
        Gizmos.DrawSphere(pointCheckup.position, rangeGood);
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(pointCheckup.position, rangeMiss);
    }


    private void Start()
    {
        aud = GameObject.Find("MusicObject").GetComponent<AudioSource>(); //尋找並取得音源元件
        aud.clip = musicdata.music; //指定音源
        aud.Play(); //播放音樂

        imgHp = GameObject.Find("血條").GetComponent<Image>();
        textHp = GameObject.Find("血量").GetComponent<Text>();

        player = GameObject.Find("role").GetComponent<player>();
        maxHp = player.blood;
        textHp.text = player.blood + "/" + maxHp;

        groupFinal = GameObject.Find("end page").GetComponent<CanvasGroup>();

        Invoke("StartMusic", musicdata.timeWait); //等待後開始生成

        textScore = GameObject.Find("分數").GetComponent<Text>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit();
    }

    private void Update()
    {
        Checkpoint();
        Clickcheck();
    }
    #endregion

    #region 方法
    ///<summary>
    ///開始音樂節點生成
    /// </summary>
    private void StartMusic()
    {
        print("開始生成");

        //呼叫協同程序
        StartCoroutine(SpawnPoint());

        
    }

    ///<summary>
    ///間隔生成節點
    /// </summary> cf
    private IEnumerator SpawnPoint()
    {
        for (int i = 0; i < musicdata.points.Length; i++)
        {
            switch (musicdata.points[i])
            {
                case PointType.none:
                    break;
                case PointType.up:
                    GameObject oUp = Instantiate(objup, pointup.position, Quaternion.identity);
                    oUp.AddComponent<Musicpoint>().speed = musicdata.speed;
                    break;
                case PointType.down:
                    GameObject oDown = Instantiate(objdown, pointdown.position, Quaternion.identity);
                    oDown.AddComponent<Musicpoint>().speed = musicdata.speed;
                    break;
                case PointType.both:
                    GameObject oBUp = Instantiate(objup, pointup.position, Quaternion.identity);
                    GameObject oBDown = Instantiate(objdown, pointdown.position, Quaternion.identity);
                    oBUp.AddComponent<Musicpoint>().speed = musicdata.speed;
                    oBDown.AddComponent<Musicpoint>().speed = musicdata.speed;
                    break;
            }

            //Instantiate(objup); //生成物件 (object.Instantiate(object) <- 原寫法)

            yield return new WaitForSeconds(musicdata.interval); //等待秒數
        }
    }

    private void hit()
    {
        player.blood -= 20;
        textHp.text = player.blood + "/" + maxHp; //更新血量文字
        imgHp.fillAmount = player.blood / maxHp; //更新血條

        if (player.blood <= 0) StartCoroutine(Gameover());
    }

    private IEnumerator Gameover()
    {
        player.dead();

        for (int i = 0; i < 40; i++)
        {
            groupFinal.alpha += 0.02f; // 0.01 透明度
            yield return new WaitForSeconds(0.05f); //wait 2 seconds
        }

        groupFinal.interactable = true;
        groupFinal.blocksRaycasts = true;
    }

    /// <summary>
    /// 用來保存進入檢查區域的的音樂節點-用來刪除用
    /// </summary>
    private GameObject objPoint;

    private void Checkpoint()
    {
        Collider2D hitMiss = Physics2D.OverlapCircle(pointCheckup.position, rangeMiss);
        Collider2D hitGood = Physics2D.OverlapCircle(pointCheckup.position, rangeGood);
        Collider2D hitPerfect = Physics2D.OverlapCircle(pointCheckup.position, rangePerfect);

        if(hitPerfect && hitPerfect.name.Contains(nameup))
        {
            areaType = AreaType.perfect;
            objPoint = hitPerfect.gameObject;
        }
        else if (hitGood && hitGood.name.Contains(nameup))
        {
            areaType = AreaType.good;
            objPoint = hitGood.gameObject;
        }
        else if (hitMiss && hitMiss.name.Contains(nameup))
        {
            areaType = AreaType.miss;
            objPoint = hitMiss.gameObject;
        }
        else
        {
            areaType = AreaType.none;
            objPoint = null;
        }
    }

    private void Clickcheck()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            switch (areaType)
            {
                case AreaType.none:
                    break;
                case AreaType.perfect:
                    AddScore(30);
                    Destroy(objPoint);
                    psUp.Play();
                    break;
                case AreaType.good:
                    AddScore(15);
                    Destroy(objPoint);
                    psUp.Play();
                    break;
                case AreaType.miss:
                    Destroy(objPoint);
                    break;
            }
        }
    }

    /// <summary>
    /// 文字介面:分數
    /// </summary>
    private Text textScore;

    /// <summary>
    /// score
    /// </summary>
    private int score;

    /// <summary>
    /// 添加分數
    /// </summary>
    /// <param name="add">要添加的分數</param>
    private void AddScore(int add)
    {
        score += add;
        textScore.text = "Score:" + score;
    }

    [Header("打擊文字")]
    public GameObject objClickText;

    /// <summary>
    /// show perfect
    /// </summary>
    /// <param name="showText">show text which I want to show.</param>
    /// <returns></returns>
    private IEnumerator ShowText(string showText)
    {
        GameObject temp = Instantiate(objClickText);
        Text tempText = temp.GetComponent<Text>();
        tempText.text = showText;
        yield return new WaitForSeconds(0.1f);
    }
    #endregion

    public enum AreaType
    {
        none,perfect,good,miss
    }
}

