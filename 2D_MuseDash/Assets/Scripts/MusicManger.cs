using System.Collections;
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

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit();
    }

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
    /// </summary>
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
}

