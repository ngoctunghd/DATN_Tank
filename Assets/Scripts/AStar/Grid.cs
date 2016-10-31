using UnityEngine;
using System.Collections;

public class Grid : MonoBehaviour {

    public float width = 16f;
    public float height = 16f;

    public void OnDrawGizmos()
    {
        Vector3 pos = transform.position;

        for (float y = pos.y - 5f; y <= pos.y + 5f; y += this.height) {
            Gizmos.DrawLine(new Vector3(-5f, Mathf.Floor(y / this.height) * height, 0),
                            new Vector3(5f, Mathf.Floor(y / this.height) * height, 0));
        }
        for (float x = pos.y - 5f; x <= pos.y + 5f; x += this.height)
        {
            Gizmos.DrawLine(new Vector3(Mathf.Floor(x / this.width) * width, -5, 0),
                            new Vector3(Mathf.Floor(x / this.width) * width, 5, 0));
        }
    }

	
}
