using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Node {

    public List<Node> adjecent = new List<Node>();
    public Node previous = null;
    public string label = "";
    public Transform position;

    public void Clear(){
        previous = null;
    }
}
