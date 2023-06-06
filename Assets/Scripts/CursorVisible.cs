using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorVisible : MonoBehaviour
{
    [SerializeField] bool isVisible;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = isVisible;
    }

}
