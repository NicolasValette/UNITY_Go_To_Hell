using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragNDropTest : MonoBehaviour
{
    [SerializeField]
    private LayerMask _mask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var m = Mouse.current;

        Vector3 vect = Vector3.zero;

       
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(m.position.ReadValue());
        Debug.Log(m.position.ReadValue());
        if (Physics.Raycast(ray, out hit, _mask))
        {
            Debug.Log("raycast");
            Vector3 ve = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            transform.position = ve;
        }
       


    }
}
