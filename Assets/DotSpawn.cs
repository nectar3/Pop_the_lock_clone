using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotSpawn : MonoBehaviour
{

    //TODO: �װų� �������� ��鸮�� ���

    public GameObject DotPref;
    public GameObject Bar;

    public GameObject dotParent;

    private Bar Bar_script;

    private GameObject m_curDot;

    public float GetCurDotAngle()
    {
        return m_curDot.transform.rotation.eulerAngles.z;
    }

    private void Start()
    {
        Bar_script = Bar.GetComponent<Bar>();
    }

    public void Null_CurDot()
    {
        if (m_curDot)
            Destroy(m_curDot);
        m_curDot = null;
    }


    // �׻� Bar�ʿ��� ������ �������� ȣ���ؾ���
    public void MakeDot()
    {
        float angle = 0;

        if (m_curDot == null) // ù ���� (�ݽð�� ����)
        {
            angle = Random.Range(60, 160);
        }
        else
        {
            var diff = Random.Range(30, 150);
            var pre = GetCurDotAngle();
            angle = (Bar_script.clockwise) ? pre - diff : pre + diff;

            Null_CurDot();
        }

        var dot = Instantiate(DotPref, Vector2.zero, Quaternion.Euler(0, 0, angle));
        dot.transform.SetParent(dotParent.transform);
        m_curDot = dot;
    }

}
