using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuicktimeWall : MonoBehaviour
{
    int[] ColourArray = { };
    public GameObject quickTimeMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        quickTimeMenu.SetActive(true);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Pressing T");
            //replace with moving down animation
            Destroy(transform.parent.gameObject);
        }
        Debug.Log("Staying");
    }
}
