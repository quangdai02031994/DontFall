using UnityEngine;
using System.Collections;

public class CubePrefabsControll : MonoBehaviour {

    public Material cube_Material;
    public float _rayDistance;
    public bool _isHorizontal;

    private Material current_material;
    private Ray ray;
    private RaycastHit hit;

    private Vector3 fingerStart;
    private Vector3 fingerEnd;


    void Awake()
    {
        current_material = transform.GetComponentInChildren<MeshRenderer>().material;
    }

	void Update ()
    {
        #region Điều khiển chuyển động của các cube

        if (GameController.Instance._isGamePlaying && GameController.Instance._control_Horizontal && _isHorizontal && Time.timeScale == 1)
        {
            
            if (Input.touchCount > 0)
            {
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        fingerStart = touch.position;
                        fingerEnd = touch.position;
                    }
                    else if(touch.phase==TouchPhase.Moved)
                    {
                        fingerEnd = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        if (Mathf.Abs(fingerStart.x - fingerEnd.x) < Mathf.Abs(fingerStart.y - fingerEnd.y))
                        {
                                //Upward Swipe
                                if ((fingerEnd.y - fingerStart.y) > 0)
                                {
                                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 1);
                                }
                                //Downward Swipe
                                else
                                {
                                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 1);
                                }
                        }

                        fingerStart = Vector3.zero;
                        fingerEnd = Vector3.zero;
                    }
                }
            }

            ray = new Ray(transform.localPosition - Vector3.left, Vector3.left * _rayDistance);
            Debug.DrawRay(ray.origin, ray.direction * _rayDistance, Color.red);

            if (Physics.Raycast(ray, out hit, _rayDistance))
            {

                if (hit.collider.tag == Tags.Horizontal)
                {
                    SoundController.Inst.PlayMoveDone();
                    transform.GetComponentInChildren<MeshRenderer>().material = cube_Material;
                    transform.gameObject.tag = Tags.Horizontal;
                    transform.gameObject.layer = 8;
                    this.gameObject.GetComponent<CubePrefabsControll>().enabled = false;
                }
            }
        }
        else if (GameController.Instance._isGamePlaying && GameController.Instance._control_Horizontal == false && _isHorizontal == false && Time.timeScale == 1)
        {
            
            if (Input.touchCount > 0)
            {
               
                foreach (Touch touch in Input.touches)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        fingerStart = touch.position;
                        fingerEnd = touch.position;
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        fingerEnd = touch.position;
                        if (Mathf.Abs(fingerStart.x - fingerEnd.x) < Mathf.Abs(fingerStart.y - fingerEnd.y))
                        {

                            //Upward Swipe
                            if ((fingerEnd.y - fingerStart.y) > 0)
                            {
                                transform.localPosition = new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z);
                            }
                            //Downward Swipe
                            else if ((fingerEnd.y - fingerStart.y) < 0)
                            {
                                transform.localPosition = new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z);
                            }
                        }
                        fingerStart = Vector3.zero;
                        fingerEnd = Vector3.zero;
                    }
                }
            }

            ray = new Ray(transform.localPosition - Vector3.forward, Vector3.forward * 2);
            Debug.DrawRay(ray.origin, ray.direction * 2, Color.red);

            if (Physics.Raycast(ray, out hit, _rayDistance))
            {

                if (hit.collider.tag == Tags.Vertical)
                {
                    SoundController.Inst.PlayMoveDone();
                    transform.GetComponentInChildren<MeshRenderer>().material = cube_Material;
                    transform.gameObject.tag = Tags.Vertical;
                    transform.gameObject.layer = 8;
                    this.gameObject.GetComponent<CubePrefabsControll>().enabled = false;
                }
            }
        }

        #endregion
	}


    public void Reset()
    {
        transform.gameObject.layer = 0;
        //transform.gameObject.tag = "Untagged";
        transform.gameObject.tag = Tags.Untagged;
        transform.GetComponentInChildren<MeshRenderer>().material = current_material;
    }
}
