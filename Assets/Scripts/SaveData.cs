using UnityEngine;
using System.Collections;
using System;


[System.Serializable]
public class SaveData {
   
    public string scenario;
    public int view;
    public bool auto, text, color, music;
    public float autoDelay, textSpeed, vol;

    public SaveData()
    {
        scenario = "ScenarioScript1";
        view = 0;
    }

    public SaveData(string s, int v)
    {
        scenario = s;
        view = v;
    }

    public SaveData(string s, int v, bool a, bool t, bool c, bool m, float ad, float ts, float vo)
    {
        scenario = s;
        view = v;
        auto = a;
        text= t;
        color = c;
        music = m;
        autoDelay = ad;
        textSpeed = ts;
        vol = vo;
    }

}
