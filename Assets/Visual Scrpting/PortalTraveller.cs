using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTraveller : MonoBehaviour
{
    // ���������봫���ŵ����λ��ƫ��
    public Vector3 previousOffsetFromPortal { get; set; }

    // ���ڴ�����һ�����
    public virtual void Teleport(Transform fromPortal, Transform toPortal, Vector3 pos, Quaternion rot)
    {
        transform.position = pos; // ���ô��ͺ��λ��
        transform.rotation = rot; // ���ô��ͺ����ת
    }

    // ��������봫���ŷ�Χʱ����
    public virtual void EnterPortalThreshold()
    {
        // �����������߼������紥��ĳЩ��Ч������
    }

    // �������˳������ŷ�Χʱ����
    public virtual void ExitPortalThreshold()
    {
        // �����������߼����������ĳЩ��Ч������
    }
}
