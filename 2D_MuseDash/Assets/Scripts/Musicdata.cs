using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="peggy/MusicData", fileName ="MusicgameBase")]
public class Musicdata : ScriptableObject
{
    [Header("音樂")]
    public AudioClip music;

    [Header("音樂開始前的等待時間"),Range(0,10)]
    public float timeWait=2f;
    [Header("節點間隔時間"), Range(0, 5)]
    public float interval = 2f;
    [Header("節點移動速度"), Range(0, 1000)]
    public float speed = 300;

    [Header("音樂節點")]
    public PointType[] points;
}

///<summary>
///音樂節點的類型:無節點、上方節點、下方節點、上下節點
/// </summary>
public enum PointType
{
    none,up,down,both
}

