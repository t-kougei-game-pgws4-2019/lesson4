using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoBehaviourScript1 : MonoBehaviour
{
    [SerializeField]
    CustomRenderTexture _texture;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Material m = _texture.material;
        m.SetFloat("_BulletX", -1.0f);
        m.SetFloat("_BulletZ", -1.0f);

        if (Input.GetMouseButton(0))
        {
            Ray ray = new Ray();
            RaycastHit hit = new RaycastHit();
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {

                Vector3 p = new Vector3(hit.point.x * 0.2f, 0f, hit.point.z * 0.2f);
                float u = p.x * 0.5f + 0.5f;
                float v = p.z * 0.5f + 0.5f;

                u -= 0.5f;
                v -= 0.5f;
                m.SetFloat("_BulletX", u);
                m.SetFloat("_BulletZ", v);

                Debug.Log(u);
                Debug.Log(v);

            }
        }
    }
}
