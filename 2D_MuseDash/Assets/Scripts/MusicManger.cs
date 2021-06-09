using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManger : MonoBehaviour
{
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

    private void Start()
    {
        aud = GameObject.Find("MusicObject").GetComponent<AudioSource>(); //尋找並取得音源元件
        aud.clip = musicdata.music; //指定音源
        aud.Play(); //播放音樂

        Invoke("StartMusic", musicdata.timeWait); //等待後開始生成
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
}
