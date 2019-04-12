using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawLineTest : MonoBehaviour
{
    public Text lookText;
    public Transform startPoint;
    public BulletCharacter round;
    public Transform parent;
    void Start()
    {
        //StartCoroutine( DrawLineLook());
        StartCoroutine(DrawWoLunPath());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DrawLineSanDanPath()
    {
        float angle = 30;
        float distance = 3.0f;
        Quaternion tempQua = Quaternion.AngleAxis(3, Vector3.forward);
        Vector3 dir = Vector3.up * -1;
        lookText.text = "首先朝黑色方向发射子弹";
        Debug.DrawLine(transform.position, transform.position+ dir*distance,Color.black,2.0f);
        yield return new WaitForSeconds(1.5f);
        lookText.text = "然后旋转发射的方向，到达下一个发射方向进行发射";
        yield return new WaitForSeconds(1.5f);
        Debug.DrawLine(transform.position, transform.position + dir * distance, Color.green, 30f);
        Vector3 targetDir = tempQua * dir;
        for (int i=0;i<10;i++)
        {
            dir = tempQua * dir;
            Debug.DrawLine(transform.position, transform.position + dir * distance, Color.black, 0.3f);
            yield return new WaitForSeconds(0.2f);

        }
        lookText.text = "方向旋转完成，重新旋转发射方向，到达下一个发射方向！";
        Debug.DrawLine(transform.position, transform.position + dir * distance, Color.black, 2f);
        yield return new WaitForSeconds(2.0f);
        Debug.DrawLine(transform.position, transform.position + dir * distance, Color.green, 30f);
        Quaternion tempQuaTwo = Quaternion.AngleAxis(-3, Vector3.forward);
        for (int i=0;i<20;i++)
        {
            dir = tempQuaTwo * dir;
            Debug.DrawLine(transform.position, transform.position + dir * distance, Color.black, 0.3f);
            yield return new WaitForSeconds(0.2f);
        }
        lookText.text = "到达目标发射子弹，这样散弹发射就算完成！";
        Debug.DrawLine(transform.position, transform.position + dir * distance, Color.green, 30f);
        yield return new WaitForSeconds(02.4f);
        yield return null;
    }

    IEnumerator DrawLineRoundPath()
    {
        lookText.text = "由于圆形弹幕的性质，我们设置一个圆圈发射36个子弹，且每个子弹之间的夹角为10度";
        yield return new WaitForSeconds(1.5f);
        float angle = 10;
        float distance = 3.0f;
        Quaternion tempQua = Quaternion.AngleAxis(angle, Vector3.forward);
        Vector3 dir = Vector3.up * -1;
        lookText.text = "首先朝黑色方向发射子弹";
        Debug.DrawLine(transform.position, transform.position + dir * distance, Color.black, 2.0f);
        yield return new WaitForSeconds(1.8f);
        lookText.text = "然后绕Z轴旋转10度，到达下一个发射方向，并重复执行！";
        Debug.DrawLine(transform.position, transform.position + dir * distance, Color.red, 30f);
        for (int i=0;i<36;i++)
        {
            dir = tempQua * dir;
            Debug.DrawLine(transform.position, transform.position + dir * distance, Color.black, 0.3f);
            yield return new WaitForSeconds(0.2f);
            Debug.DrawLine(transform.position, transform.position + dir * distance, Color.red, 30f);

        }
        lookText.text = "完成后，刚好回到初始方向！";
        yield return new WaitForSeconds(1.8f);
        yield return null;
    }

    IEnumerator DrawLineRoundsPath()
    {
        float distance = 1.0f;
        lookText.text = "首先朝8个方向发射一个圆形弹幕";
        yield return new WaitForSeconds(1.5f);
        Vector3 dir = Vector3.up * -1;
        Quaternion tempQua = Quaternion.AngleAxis(45, Vector3.forward);
        for (int i=0;i<8;i++)
        {
            dir = tempQua * dir;
            Debug.DrawLine(transform.position, transform.position + dir * distance, Color.black, 0.3f);
            yield return new WaitForSeconds(0.2f);
            Debug.DrawLine(transform.position, transform.position + dir * distance, Color.red, 30f);
        }
        List<BulletCharacter> bullets = new List<BulletCharacter>();
        for (int i = 0; i < 8; i++)
        {
            dir = tempQua * dir;
            BulletCharacter bullet = Instantiate(round,transform.position,Quaternion.identity);
            bullet.transform.SetParent(parent);
            bullet.dir = dir;
            bullet.speed = 2.0f;
            bullets.Add(bullet);
        }
        yield return new WaitForSeconds(1.3f);
        lookText.text = "到达目标点，开始发射多播多方向圆形弹幕！";
        for (int i=0;i< bullets.Count;i++)
        {
            bullets[i].speed = 0;
            StartCoroutine(DrawRoundPath(bullets[i].transform.position));
        }
        yield return new WaitForSeconds(7.2f);
        lookText.text = "自此弹幕完成";
        yield return null;
    }


    public IEnumerator DrawWoLunPath()
    {
        yield return new WaitForSeconds(1.2f);
        float distance = 0.6f;
        float addVaule = 0.2f;
        Vector3 dir = Vector3.up * -1;
        Quaternion tempQua = Quaternion.AngleAxis(20, Vector3.forward);
        lookText.text = "首先发射一个生成半径不断增长圆形弹幕";
        List<BulletCharacter> bullets = new List<BulletCharacter>();
        for (int i=0;i<18;i++)
        {
            Debug.DrawLine(transform.position, transform.position + dir * distance, Color.black, 0.3f);
            BulletCharacter bullet = Instantiate(round, transform.position + dir * distance, Quaternion.identity);
            bullet.transform.SetParent(parent);
            bullet.speed = 0;
            bullets.Add(bullet);
            yield return new WaitForSeconds(0.2f);
            Debug.DrawLine(transform.position, transform.position + dir * distance, Color.red, 30f);
            dir = tempQua * dir;
            distance += addVaule;
        }
        lookText.text = "然后在每一个点的位置发射圆形弹幕";
        for (int i = 0;i< bullets.Count;i++)
        {
            StartCoroutine(DrawRoundPath(bullets[i].transform.position));
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(7.2f);
        lookText.text = "涡轮型弹幕完成！";

    }

    public IEnumerator DrawRoundPath(Vector3 point)
    {
        float distance = 1f;
        Quaternion tempQua = Quaternion.AngleAxis(10, Vector3.forward);
        Vector3 dir = Vector3.up * -1;
        for (int i = 0; i < 36; i++)
        {
            dir = tempQua * dir;
            Debug.DrawLine(point, point + dir * distance, Color.black, 0.3f);
            yield return new WaitForSeconds(0.2f);
            Debug.DrawLine(point, point + dir * distance, Color.red, 30f);

        }
    }


    public IEnumerator DrawLineLook()
    {
        Vector3 dir = Vector3.up;
        float distance = 10f;
        Quaternion tempQua = Quaternion.AngleAxis(10, Vector3.forward);
        for (int i=0;i<36;i++)
        {
            Vector3 endPos = startPoint.position + dir * distance;
            Debug.DrawLine(startPoint.position, endPos,Color.red,30f);
            dir = tempQua * dir;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
