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

    [SerializeField] int sides = 4;

    [SerializeField] float drag = 1;
    [SerializeField] float angularDrag = 1;

    [SerializeField] bool usePhysics = false;

    Transform[] segments;
    [SerializeField] Transform segmentParent;

    List<GameObject> segmentSpheres = new List<GameObject>(); // List to store spheres

    private int prevSegmentCount;
    private float prevTotalLength;
    private float prevDrag;
    private float prevTotalWeight;
    private float prevAngularDrag;
    private float prevRadius;

    List<Vector3> tempVerticies = new();

    private void Update()
    {
        UpdateSegmentData();
        GenerateVertices();
    }

    private void UpdateSegmentData()
    {
        if (prevSegmentCount != segmentCount)
        {
            RemoveSegments();
            segments = new Transform[segmentCount];
            GenerateSegments();
            SetupCollisionAvoidance();
            prevSegmentCount = segmentCount;
        }

        if (totalLength != prevTotalLength || prevDrag != drag || prevTotalWeight != totalWeight || prevAngularDrag != angularDrag)
        {
            UpdateWire();
        }

        prevTotalLength = totalLength;
        prevDrag = drag;
        prevTotalWeight = totalWeight;
        prevAngularDrag = angularDrag;

        if (prevRadius != radius && usePhysics)
        {
            UpdateRadius();
        }
        prevRadius = radius;
    }

    private void GenerateVertices()
    {
        tempVerticies.Clear();
        for (int i = 0; i < segments.Length; i++)
        {
            GenerateCircleVertices(segments[i], i);
        }
    }

    private void GenerateCircleVertices(Transform segmentTransform, int segmentIndex)
    {
        float angleDiff = 360 / sides;

        Quaternion diffRotation = Quaternion.FromToRotation(Vector3.forward, segmentTransform.forward);

        for (int sideIndex = 0; sideIndex < sides; sideIndex++)
        {
            float angleInRad = sideIndex * angleDiff * Mathf.Deg2Rad;
            float x = -1 * radius * Mathf.Cos(angleInRad);
            float y = radius * Mathf.Sin(angleInRad);

            Vector3 pointOffset = new(x, y, 0);

            Vector3 pointRotated = diffRotation * pointOffset;

            Vector3 pointRotatedAtCenterOfTransform = segmentTransform.position + pointRotated;

            tempVerticies.Add(pointRotatedAtCenterOfTransform);
        }
    }

    private void UpdateRadius()
    {
        for (int i = 0; i < segments.Length; i++)
        {
            SetRadiusOnSegment(segments[i], radius);
        }
    }

    private void SetRadiusOnSegment(Transform transform, float radius)
    {
        SphereCollider sphereCollider = transform.GetComponent<SphereCollider>();
        sphereCollider.radius = radius;
    }

    private void UpdateWire()
    {
        for (int i = 0; i < segments.Length; i++)
        {
            if (i != 0) // if it's not the first, only update the length
            {
                UpdateLengthOnSegment(segments[i], totalLength / segmentCount);
            }
            UpdateWeightOnSegment(segments[i], totalWeight, drag, angularDrag); // for all segments update weight
        }
    }

    private void UpdateWeightOnSegment(Transform transform, float totalWeight, float drag, float angularDrag)
    {
        Rigidbody rigidbody = transform.GetComponent<Rigidbody>();
        rigidbody.mass = totalWeight / segmentCount;
        rigidbody.drag = drag;
        rigidbody.angularDrag = angularDrag;
    }

    private void UpdateLengthOnSegment(Transform transform, float v)
    {
        ConfigurableJoint joint = transform.GetComponent<ConfigurableJoint>();
        if (joint != null)
        {
            joint.connectedAnchor = Vector3.forward * totalLength / segmentCount;
        }
    }

    private void RemoveSegments()
    {
        if (segments != null)
        {
            for (int i = 0; i < segments.Length; i++)
            {
                if (segments[i] != null)
                {
                    Destroy(segments[i].gameObject);
                }
            }
        }

        // Remove sphere objects if any
        foreach (var sphere in segmentSpheres)
        {
            Destroy(sphere);
        }
        segmentSpheres.Clear(); // Clear the sphere list
    }

    void OnDrawGizmos()
    {
        if (segments != null)
        {
            for (int i = 0; i < segments.Length; i++)
            {
                Gizmos.DrawWireSphere(segments[i].position, radius);
            }

            for (int i = 0; i < tempVerticies.Count; i++)
            {
                Gizmos.DrawSphere(tempVerticies[i], 0.1f);
            }
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

            // Create a sphere for the segment
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = segment.transform.position;
            sphere.transform.localScale = new Vector3(radius * 2, radius * 2, radius * 2); // Scale the sphere to match the segment radius
            sphere.transform.SetParent(segment.transform); // Set the sphere as a child of the segment
            segmentSpheres.Add(sphere); // Store the sphere in the list

            JoinSegment(segment.transform, prevTransform);

            prevTransform = segment.transform;
        }

        JoinSegment(endTransform, prevTransform, true, true);
    }

    private void JoinSegment(Transform current, Transform connectedTransform, bool isKinetic = false, bool isCloseConnected = false)
    {
        // Adding rigid body
        if (current.GetComponent<Rigidbody>() == null)
        {
            Rigidbody rigidbody = current.gameObject.AddComponent<Rigidbody>();
            rigidbody.isKinematic = isKinetic;
            rigidbody.mass = totalWeight / segmentCount;
            rigidbody.drag = drag;
            rigidbody.angularDrag = angularDrag;

            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }

        if (usePhysics)
        {
            SphereCollider sphereCollider = current.gameObject.AddComponent<SphereCollider>();
            sphereCollider.radius = radius;
        }

        if (connectedTransform != null)
        {
            ConfigurableJoint joint = current.gameObject.GetComponent<ConfigurableJoint>();
            if (joint == null)
            {
                joint = current.gameObject.AddComponent<ConfigurableJoint>();
            }

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

            joint.angularXDrive = new JointDrive { positionSpring = 10, positionDamper = 1 };
            joint.angularYZDrive = new JointDrive { positionSpring = 10, positionDamper = 1 };
            joint.angularZMotion = ConfigurableJointMotion.Limited; // Limited so it can rotate a bit but not much, so the rope doesn't get twisted

            SoftJointLimit softJointLimit = new();
            softJointLimit.limit = 0;
            joint.angularZLimit = softJointLimit;

            JointDrive jointDrive = new();
            jointDrive.positionDamper = 0;
            jointDrive.positionSpring = 0;
            joint.angularXDrive = jointDrive;
            joint.angularYZDrive = jointDrive;

            SpringJoint springJoint = current.gameObject.AddComponent<SpringJoint>();
            springJoint.connectedBody = connectedTransform.GetComponent<Rigidbody>();
            springJoint.spring = 100;
            springJoint.damper = 5;
            springJoint.maxDistance = 0.1f;
        }
    }

    private void SetupCollisionAvoidance()
    {
        // Apply the custom "Segment" layer to all segments
        int segmentLayer = LayerMask.NameToLayer("Segment");

        for (int i = 0; i < segments.Length; i++)
        {
            // Set the layer for each segment
            segments[i].gameObject.layer = segmentLayer;

            // Get the SphereCollider attached to the segment
            SphereCollider collider = segments[i].GetComponent<SphereCollider>();

            // Ignore collisions between consecutive segments
            if (i - 1 >= 0)
            {
                Physics.IgnoreCollision(collider, segments[i - 1].GetComponent<SphereCollider>(), true);
            }
            if (i + 1 < segments.Length)
            {
                Physics.IgnoreCollision(collider, segments[i + 1].GetComponent<SphereCollider>(), true);
            }
        }

        // Ignore collisions between any two segments (this is done globally for all segments)
        Physics.IgnoreLayerCollision(segmentLayer, segmentLayer, true);
    }
}
