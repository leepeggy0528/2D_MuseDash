using UnityEngine;

public class APIStatic : MonoBehaviour
{
    private void Start()
    {
        //屬性-得到
        print("Random=" + Random.value);

        //屬性-設定
        Cursor.visible = false;

        //方法
        int r = Random.Range(50, 100);
        print("隨機攻擊" + r);
    }
}
