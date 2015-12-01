using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PlayerController : MonoBehaviour {


    public GameObject sphere;
    public GameObject cube;
    public GameObject capsule;
    public GameObject cylinder;

    private float _speedMove = 5;
    private float _rayDistance = 1;



	void Start () 
    {
        string _ball = PlayerPrefs.GetString(Config.Ball);
        switch (_ball)
        {
            case BallName.Ball_Cube:
                GeneratorObject(cube);
                break;
            case BallName.Ball_Capsule:
                GeneratorObject(capsule);
                break;
            case BallName.Ball_Sphere:
                GeneratorObject(sphere);
                break;
            case BallName.Ball_Cylinder:
                GeneratorObject(cylinder);
                break;
            default:
                GeneratorObject(sphere);
                break;
        }
	}
	
	void Update () 
    {
        Ray ray = new Ray(transform.position + Vector3.up, Vector3.down * _rayDistance);
        Debug.DrawRay(ray.origin, Vector3.down * _rayDistance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _rayDistance))
        {
            if (hit.collider.tag == Tags.Horizontal)
            {
                transform.DOMoveZ(hit.collider.transform.position.z, 0.1f).From();
                GameController.Instance._control_Horizontal = true;
            }
            else if (hit.collider.tag == Tags.Vertical)
            {
                transform.DOMoveX(hit.collider.transform.position.x, 0.1f).From();
                GameController.Instance._control_Horizontal = false;
            }
        }
        if (GameController.Instance._isGamePlaying)
        {
            if (GameController.Instance._control_Horizontal)
            {
                transform.Translate(Vector3.right * Time.deltaTime * _speedMove);
            }
            else
            {
                transform.Translate(Vector3.back * Time.deltaTime * _speedMove);
            }
        }
	}

    public void GeneratorObject(GameObject o)
    {
        GameObject obj = Instantiate(o, Vector3.zero, Quaternion.identity) as GameObject;
        obj.transform.parent = transform;
        obj.transform.localPosition = new Vector3(0, 1, 0);
        if (obj.GetComponent<AutoRotation>() != null)
        {
            obj.GetComponent<AutoRotation>().enabled = false;
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.Diamond)
        {
            GameController.Instance.AddScore();
            other.gameObject.SetActive(false);
        }
    }

}
