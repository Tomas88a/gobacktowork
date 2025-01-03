using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Portal linkedPortal;
    Camera playerCam;
    Camera portalCam;
    public GameObject door;
    public GameObject screen;
    Quaternion lastRotation;

    // 传送功能
    [SerializeField]
    public List<PortalTraveller> trackedTravellers;
    Vector3 screenPos;

    Quaternion DoorRotation
    {
        set
        {
            if (linkedPortal == null) return; // 如果没有 linkedPortal，则不运行
            if (value == lastRotation) return;
            lastRotation = value;
            linkedPortal.door.transform.localRotation = value;
        }
    }

    private void Awake()
    {
        playerCam = Camera.main;
        portalCam = GetComponentInChildren<Camera>();
        trackedTravellers = new List<PortalTraveller>();
        screenPos = screen.transform.position;
    }

    private void Update()
    {
        if (linkedPortal == null) return; // 如果没有 linkedPortal，则不运行
        DoorRotation = door.transform.localRotation;
        Render();
        ProtectScreenFormClipping();
    }

    private void FixedUpdate()
    {
        if (linkedPortal == null) return; // 如果没有 linkedPortal，则不运行

        // 判断是否需要传送玩家
        for (int i = 0; i < trackedTravellers.Count; i++)
        {
            PortalTraveller traveller = trackedTravellers[i];
            Transform travellerT = traveller.transform;

            Vector3 offsetFromPortal = travellerT.position - transform.position;

            int portalSide = System.Math.Sign(Vector3.Dot(offsetFromPortal, transform.forward));
            int portalSideOld = System.Math.Sign(Vector3.Dot(traveller.previousOffsetFromPortal, transform.forward));

            if (portalSide != portalSideOld)
            {
                var m = linkedPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * travellerT.localToWorldMatrix;
                traveller.Teleport(transform, linkedPortal.transform, m.GetColumn(3), m.rotation);
                trackedTravellers.RemoveAt(i);
                i--;
            }
            else
            {
                traveller.previousOffsetFromPortal = offsetFromPortal;
            }
        }
    }

    private void Render()
    {
        if (linkedPortal == null) return; // 如果没有 linkedPortal，则不运行
        var m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix * playerCam.transform.localToWorldMatrix;
        portalCam.transform.SetPositionAndRotation(m.GetColumn(3), m.rotation);
    }

    void ProtectScreenFormClipping()
    {
        if (linkedPortal == null) return; // 如果没有 linkedPortal，则不运行

        Vector3 dirToPortal = playerCam.transform.position - screenPos;
        float dirLength = dirToPortal.magnitude;
        float angleDot = Vector3.Dot(screen.transform.forward, dirToPortal);
        float side = System.Math.Sign(angleDot);

        if (dirLength < 0.5f)
        {
            float width = 0.5f;
            screen.transform.localScale = new Vector3(screen.transform.localScale.x, screen.transform.localScale.y, width);
            screen.transform.localPosition = new Vector3(0, 0, width * side / 2);
        }
        else
        {
            screen.transform.localScale = new Vector3(0, 0, 0);
        }
    }

    void OnTravellerEnterPortal(PortalTraveller traveller)
    {
        if (linkedPortal == null) return; // 如果没有 linkedPortal，则不运行

        if (!trackedTravellers.Contains(traveller))
        {
            traveller.EnterPortalThreshold();
            traveller.previousOffsetFromPortal = traveller.transform.position - transform.position;
            trackedTravellers.Add(traveller);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var traveller = other.GetComponent<PortalTraveller>();
        if (traveller)
        {
            OnTravellerEnterPortal(traveller);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var traveller = other.GetComponent<PortalTraveller>();
        if (traveller && trackedTravellers.Contains(traveller))
        {
            traveller.ExitPortalThreshold();
            trackedTravellers.Remove(traveller);
        }
    }
}
