using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour
{
    [SerializeField] Transform startTransform, endTransform;

    [SerializeField] int segmentCount = 10;
    [SerializeField] float totalLength = 10;
    [SerializeField] float totalWeight = 10;

    [SerializeField] float radius = 0.5f;

    [SerializeField] float drag = 1;
    [SerializeField] float angularDrag = 1;

    [SerializeField] bool usePhysics = false;

    Transform[] segments;
    [SerializeField] Transform segmentParent;


    private void Start()
    {
        segments = new Transform[segmentCount];
        GenerateSegments();
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < segments.Length; i++)
        {
            Gizmos.DrawWireSphere(segments[i].position, 0.1f);
        }   
    }

    private void GenerateSegments()
    {
        JoinSegment(startTransform, null, true);
        Transform prevTransform = startTransform;

        Vector3 direction = (endTransform.position - startTransform.position);

        for (int i = 0; i < segmentCount; i++)
        {
            GameObject segment = new GameObject($"segment_{i}");
            segment.transform.SetParent(segmentParent);
            segments[i] = segment.transform;

            Vector3 pos = prevTransform.position + (direction / segmentCount);
            segment.transform.position = pos;

            JoinSegment(segment.transform, prevTransform);

            prevTransform = segment.transform;
        }

        JoinSegment(endTransform, prevTransform, true, true);
    }

    private void JoinSegment(Transform current, Transform connectedTransform, bool isKinetic = false, bool isCloseConnected = false)
    {
        // Adding rigid body
        Rigidbody rigidbody = current.gameObject.AddComponent<Rigidbody>();
        rigidbody.isKinematic = isKinetic;
        rigidbody.mass = totalWeight / segmentCount;
        rigidbody.drag = drag;
        rigidbody.angularDrag = angularDrag;

        if (usePhysics)
        {
            SphereCollider sphereCollider = current.gameObject.AddComponent<SphereCollider>();
            sphereCollider.radius = radius;
        }

        if (connectedTransform != null)
        {
            ConfigurableJoint joint = current.gameObject.AddComponent<ConfigurableJoint>();

            joint.connectedBody = connectedTransform.GetComponent<Rigidbody>();

            joint.autoConfigureConnectedAnchor = false;

            if (isCloseConnected)
            {
                joint.connectedAnchor = Vector3.forward * 0.1f; //applied for end transform
            }
            else
            {
                joint.connectedAnchor = Vector3.forward * (totalLength / segmentCount); // applied for start and all segments
            }

            joint.xMotion = ConfigurableJointMotion.Locked;
            joint.yMotion = ConfigurableJointMotion.Locked;
            joint.zMotion = ConfigurableJointMotion.Locked;

            joint.angularXMotion = ConfigurableJointMotion.Free;
            joint.angularYMotion = ConfigurableJointMotion.Free;
            joint.angularZMotion = ConfigurableJointMotion.Limited; // Limited so it can rotate a bit but not much, so the rope doesn't get twisted

            SoftJointLimit softJointLimit = new();
            softJointLimit.limit = 0;
            joint.angularZLimit = softJointLimit;

            JointDrive jointDrive = new();
            jointDrive.positionDamper = 0;
            jointDrive.positionSpring = 0;
            joint.angularXDrive = jointDrive;
            joint.angularYZDrive = jointDrive;
        }
    }
}
