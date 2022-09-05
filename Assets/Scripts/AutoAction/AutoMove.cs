using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : AutoAction
{
    [SerializeField]
    Vector2[] destinations;
    [SerializeField]
    float speed;
    [SerializeField]
    GameObject target;
    [SerializeField]
    bool InteractAfterEnd;

    public override void Action()
    {
        StartCoroutine(MoveToward(destinations[0]));
    }

    IEnumerator MoveToward(Vector3 destination)
    {
        Vector3 startPosition = target.transform.position;
        float diffX = destination.x - startPosition.x;
        float diffY = destination.y - startPosition.y;
        if (diffX == 0 || diffY == 0)//0을 0으로 나누는 현상 방지
        {
            StopCoroutine(MoveToward(destination));//빠져나감
        }
        float dirX = diffX / Mathf.Abs(diffX);
        float dirY = diffY / Mathf.Abs(diffY);

        float distanceX = Mathf.Abs(diffX);
        float distanceY = Mathf.Abs(diffY);
        float tmpDistanceX = 0;
        float tmpDistanceY = 0;
        while ( Mathf.Abs(target.transform.position.x - startPosition.x) < distanceX)
        {
            GameManager.playerControllerScript.SetHorizontalAxis(dirX);

            yield return new WaitForSeconds(Time.deltaTime);
        }
        GameManager.playerControllerScript.SetHorizontalAxis(0);
        while(Mathf.Abs(target.transform.position.y - startPosition.y) < distanceY)
        {
            GameManager.playerControllerScript.SetVerticalAxis(dirY);
            yield return new WaitForSeconds(Time.deltaTime);
        }
        GameManager.playerControllerScript.SetVerticalAxis(0);
        if (InteractAfterEnd)
        {
            InteractAndFinish();
        }
        yield return new WaitForSeconds(1.0f);
    }
    void InteractAndFinish() //이동 다 마치고 interact
    {
        GameManager.playerControllerScript.ProcessInteractEvent();
    }

}
