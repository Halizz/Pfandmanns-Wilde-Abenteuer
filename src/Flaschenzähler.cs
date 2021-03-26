using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flaschenzähler : MonoBehaviour
{
    public int Flasche;
    public GameObject TextObject;
    Text textComponent;

    void Start()
    {
        textComponent = TextObject.GetComponent<Text>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Flasche")
        {
            Flasche = Flasche + 1;
            textComponent.text = Flasche.ToString();
            Destroy(collision.gameObject);
        }
    }
}
