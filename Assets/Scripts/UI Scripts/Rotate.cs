using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour {

    public Vector3 axis;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(axis * Time.deltaTime);
    }

}
