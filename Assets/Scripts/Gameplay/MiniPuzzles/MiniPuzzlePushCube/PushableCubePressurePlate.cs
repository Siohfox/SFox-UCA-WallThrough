using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableCubePressurePlate : MonoBehaviour
{
    [SerializeField] private MiniPuzzlePushPuzzle miniPuzzlePushPuzzle;
    [SerializeField] private Material unPushedMaterial, pushedMaterial;
    private new Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "PushableCube")
        {
            if (miniPuzzlePushPuzzle)
            {
                Debug.Log("Cube");
                renderer.material = pushedMaterial;
                miniPuzzlePushPuzzle.AddPushedPlate();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "PushableCube")
        {
            if (miniPuzzlePushPuzzle)
            {
                renderer.material = unPushedMaterial;
                miniPuzzlePushPuzzle.RemovedPushedPlate();
            }
        }
    }
}
