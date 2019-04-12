using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCharacter : MonoBehaviour
{
    public Vector3 dir;
    public float speed;
    public bool isMove;
    bool isBallModePlay;
    void Start()
    {
        isMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
        {
            BulletMove();
        }

    }

    public void BulletMove()
    {
        transform.position += dir * speed * Time.deltaTime;
    }

    /// <summary>
    /// 动态变换移动方向的移动模式
    /// </summary>
    /// <param name="endTime">移动结束时间</param>
    /// <param name="dirChangeTime"><方向转变时间</param>
    /// <param name="angle">方向改变的角度</param>
    /// <returns></returns>
    public IEnumerator DirChangeMoveMode(float endTime, float dirChangeTime, float angle)
    {
        float time = 0;
        bool isRotate = true;
        isBallModePlay = true;
        while (isBallModePlay)
        {
            time += Time.deltaTime;
            transform.position += speed * dir * Time.deltaTime;
            if (time >= dirChangeTime && isRotate)
            {
                isRotate = false;
                StartCoroutine(BulletRotate(angle));
            }

            yield return null;
        }
    }

    /// <summary>
    /// 弹幕动态改变移动方向
    /// </summary>
    IEnumerator BulletRotate(float angle)
    {
        while (isBallModePlay)
        {
            Quaternion tempQuat = Quaternion.AngleAxis(angle, Vector3.forward);
            dir = tempQuat * dir;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
