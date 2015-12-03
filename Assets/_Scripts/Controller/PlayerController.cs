using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PlayerController : MonoBehaviour {


    public GameObject _badyDuck;
    public GameObject _cow;
    public GameObject _doge;
    public GameObject _dragon;
    public GameObject _ed;
    public GameObject _instantmartian;
    public GameObject _koopa;
    public GameObject _patrick;
    public GameObject _piggyBank;
    public GameObject _spearGuy;
    public GameObject _spongeBob;


    private float _speedMove = 5;
    private float _rayDistance = 1;

    private Transform player;


	void Start () 
    {
        //string _ball = PlayerPrefs.GetString(Config.Player);
        //switch (_ball)
        //{
        //    case PlayerNames.Cow:
        //        GeneratorObject(_cow);
        //        break;
        //    case PlayerNames.BabyDuck:
        //        GeneratorObject(_doge);
        //        break;
        //    case PlayerNames.Doge:
        //        GeneratorObject(_badyDuck);
        //        break;
        //    case PlayerNames.Dragon:
        //        GeneratorObject(_dragon);
        //        break;
        //    default:
        //        GeneratorObject(_badyDuck);
        //        break;
        //}

        GeneratorObject(_ed);
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
                player.DORotate(Vector3.zero, 0.3f).From();
                GameController.Instance._control_Horizontal = true;
            }
            else if (hit.collider.tag == Tags.Vertical)
            {
                transform.DOMoveX(hit.collider.transform.position.x, 0.1f).From();
                player.DORotate(new Vector3(0, 90, 0), 0.3f).From();
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
        obj.transform.localPosition = new Vector3(0, 0.5f, 0);
        if (obj.GetComponent<AutoRotation>() != null)
        {
            obj.GetComponent<AutoRotation>().enabled = false;
        }
        player = transform.GetChild(0).transform;
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
