using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionBackgroundScroll : MonoBehaviour
{
    RawImage backgroundImage;
    public Vector2 scrollDirection;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        backgroundImage = GetComponent<RawImage>();
    }

    // Update is called once per frame
    void Update()
    {
        float newX = backgroundImage.uvRect.position.x + scrollDirection.normalized.x * speed * Time.deltaTime;
        float newY = backgroundImage.uvRect.position.y + scrollDirection.normalized.y * speed * Time.deltaTime;
        backgroundImage.uvRect = new Rect(newX, newY, 1, 1);
    }
}
