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
    public GameObject pointup;
    [Header("生成位置:下方")]
    public GameObject pointdown;

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

        Instantiate(objup); //生成物件 (object.Instantiate(object) <- 原寫法)
    }
}
