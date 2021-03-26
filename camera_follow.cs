using UnityEngine;
using System.Collections;

public class camera_follow : MonoBehaviour
{
    public GameObject Target;
private Vector3 Offset;


// Start is called before the first frame update
void Start()
{

    Offset = transform.position - Target.transform.position;

}

// Update is called once per frame
void Update()
{

    transform.position = Target.transform.position + Offset;


}
}
