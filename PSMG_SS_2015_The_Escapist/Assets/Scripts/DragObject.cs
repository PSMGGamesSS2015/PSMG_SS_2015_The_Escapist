using System;
using System.Collections;
using UnityEngine;

public class DragObject : MonoBehaviour
{
    public LayerMask layerMask;
    public float maxDragDistance = 2f;

    public float k_Spring = 50.0f;
    public float k_Damper = 5.0f;
    public float k_Drag = 10.0f;
    public float k_AngularDrag = 5.0f;
    public float k_Distance = 0.2f;
    public bool k_AttachToCenterOfMass = false;

    private SpringJoint m_SpringJoint;
    private GameObject player;
    private Camera firstPersonCam;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        firstPersonCam = player.GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        // Make sure the user pressed the mouse down
        if (!Input.GetMouseButtonDown(0)) { return; }

        // We need to actually hit an object
        RaycastHit hit = new RaycastHit();
        Ray ray = firstPersonCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(ray.origin, ray.direction * maxDragDistance, Color.green);

        if (!Physics.Raycast(ray, out hit, maxDragDistance, layerMask)) { return; }

        // GET RENDERER OF OBJECT HIT
        Renderer hitRenderer = hit.collider.GetComponent<Renderer>();

        // MAKE SURE WE HIT NO DOOR
        if (hitRenderer.tag == "Door") { return; }

        // We need to hit a rigidbody that is not kinematic
        if (!hit.rigidbody || hit.rigidbody.isKinematic) { return; }


        if (!m_SpringJoint)
        {
            var go = new GameObject("Rigidbody dragger");
            Rigidbody body = go.AddComponent<Rigidbody>();
            m_SpringJoint = go.AddComponent<SpringJoint>();
            body.isKinematic = true;
        }

        m_SpringJoint.transform.position = hit.point;
        m_SpringJoint.anchor = Vector3.zero;

        m_SpringJoint.spring = k_Spring;
        m_SpringJoint.damper = k_Damper;
        m_SpringJoint.maxDistance = k_Distance;
        m_SpringJoint.connectedBody = hit.rigidbody;

        StartCoroutine("Drag", hit.distance);
    }


    private IEnumerator Drag(float distance)
    {
        var oldDrag = m_SpringJoint.connectedBody.drag;
        var oldAngularDrag = m_SpringJoint.connectedBody.angularDrag;
        m_SpringJoint.connectedBody.drag = k_Drag;
        m_SpringJoint.connectedBody.angularDrag = k_AngularDrag;

        while (Input.GetMouseButton(0))
        {
            var ray = firstPersonCam.ViewportPointToRay(new Vector3(0.5f, 0.9f, 0));
            m_SpringJoint.transform.position = ray.GetPoint(distance);
            yield return null;
        }
        if (m_SpringJoint.connectedBody)
        {
            m_SpringJoint.connectedBody.drag = oldDrag;
            m_SpringJoint.connectedBody.angularDrag = oldAngularDrag;
            m_SpringJoint.connectedBody = null;
        }
    }


    private Camera FindCamera()
    {
        if (GetComponent<Camera>())
        {
            return GetComponent<Camera>();
        }

        return Camera.main;
    }
}

