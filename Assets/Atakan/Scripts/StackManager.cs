using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : Singleton<StackManager>
{
    [SerializeField] private float distanceBetwwenObjects;
    [SerializeField] private float forwardDistance;
    [SerializeField] public Transform parent;

    public List<Pencils> pencils = new List<Pencils>();

    void Start()
    {
        distanceBetwwenObjects = 0.5f;
        forwardDistance = 0.7f;
        if (transform.childCount > 0)
        {
            pencils.Add(transform.GetChild(0).GetComponent<Pencils>());
            sortPencils();
        }
    }

    public void popUp(int index)
    {
        pencils.RemoveAt(index);
        for (int i = 0; i < pencils.Count; i++)
        {
            pencils[i].listIndex = i;

        }
    }

    public void delayedSortList()
    {
            Invoke("sortPencils", 0.3f);
    }

    public void PickUp(GameObject pickedObject, bool needTag = false, string tag=null)
    {
        if (needTag)
        {
            pickedObject.tag = tag;
        }
        pickedObject.transform.parent = parent;
        pickedObject.GetComponent<StackTrigger>().inPlayer = true;
        pencils.Add(pickedObject.GetComponent<Pencils>());
        sortPencils();
        pickedObject.GetComponent<Pencils>().enabled = true;
    }
    float _tempLeftRightDistance;
    float _tempForwardDistance;
        
    public void sortPencils()
    {
        for (int i = 0; i < pencils.Count; i++)
        {
            pencils[i].listIndex = i;
            getDistances(i);
            Vector3 _newPos = new Vector3(_tempLeftRightDistance, 0.5f, _tempForwardDistance);
            // pencils[i].transform.localPosition = _newPos;
            if (pencils[i])
            {
                pencils[i].destiny = _newPos;
                pencils[i].firstLocalPos = pencils[i].transform.localPosition;
                pencils[i].moving = true;
            }
            //LeanTween.moveLocal(pencils[i].gameObject, _newPos, 0.2f);
        }
    }

    private void getDistances(int index)
    {
        index++;
        if (index % 11 == 1) // middle
        {
            _tempLeftRightDistance = 0;
            _tempForwardDistance = forwardDistance * (int)((index - 1) / 11);
        }
        else if ((int)(((index - 1) + (1 * (index / 11))) / 6) % 2 == 0) // right
        {
            _tempLeftRightDistance = distanceBetwwenObjects * (((index - 1) + (1 * (index / 11))) % 6);
        }
        else // left
        {
            _tempLeftRightDistance = -distanceBetwwenObjects * ((index + (1 * (index / 12))) % 6);
        }
    }
}
