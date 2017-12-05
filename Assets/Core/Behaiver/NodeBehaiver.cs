﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.Sprites;
using UnityEngine.Scripting;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;
using UnityEngine.Assertions.Must;
using UnityEngine.Assertions.Comparers;
using System.Collections;
using System.Collections.Generic;
using System;
/// <summary>
/// 节点
/// </summary>
public class NodeBehaiver : MonoBehaviour {
    public NodeInfo nodeInfo;
    [SerializeField]
    private GameObject renderObj;
    internal void Hide()
    {
        renderObj.SetActive(false);
    }

    internal void UnHide()
    {
        renderObj.SetActive(true);
    }
}
