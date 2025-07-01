using UnityEngine;
using System.Collections;

public class Action {

    public string sprite, startPoint, endPoint;
    public View.SpecialAnimation motion;

    public Action(string sp, string x1, string x2, View.SpecialAnimation mo)
    {
        sprite = sp;
        startPoint = x1;
        endPoint = x2;
        motion = mo;
    }


}
