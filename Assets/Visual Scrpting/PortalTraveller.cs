using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTraveller : MonoBehaviour
{
    // 保存物体与传送门的相对位置偏移
    public Vector3 previousOffsetFromPortal { get; set; }

    // 用于传送玩家或物体
    public virtual void Teleport(Transform fromPortal, Transform toPortal, Vector3 pos, Quaternion rot)
    {
        transform.position = pos; // 设置传送后的位置
        transform.rotation = rot; // 设置传送后的旋转
    }

    // 当物体进入传送门范围时调用
    public virtual void EnterPortalThreshold()
    {
        // 这里可以添加逻辑，例如触发某些特效或声音
    }

    // 当物体退出传送门范围时调用
    public virtual void ExitPortalThreshold()
    {
        // 这里可以添加逻辑，例如结束某些特效或声音
    }
}
