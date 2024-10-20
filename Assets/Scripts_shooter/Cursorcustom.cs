
using UnityEngine;
using Cursor = UnityEngine.Cursor;

public class Cursorcustom : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10; 
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
