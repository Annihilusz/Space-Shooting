using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bgscrolling : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -2 * Time.deltaTime);

        if(transform.position.y < -6.7)
        {
            transform.position = new Vector3(transform.position.x,6.7f);
        }
    }
}
