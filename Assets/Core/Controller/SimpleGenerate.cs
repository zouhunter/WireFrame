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
namespace WireFrame
{

    /// <summary>
    /// 利用模拟的几何数据加载出点和线
    /// </summary>
    public class SimpleGenerate : WireFrameGenerater
    {
        public override bool CanDouble
        {
            get
            {
                return true;
            }
        }
        public WFData wfData;
        public override bool CanCreate(FrameRule clamp)
        {
            return true;
        }
        protected override WFData GenerateWFData(FrameRule clamp)
        {
            return GetTestData();
        }

        public static WFData GetTestData()
        {
            var data = new WFData();
            data.wfNodes = new List<WFNode>();
            var node1 = new WFNode(Vector3.zero);
            data.wfNodes.Add(node1);
            var node2 = new WFNode(Vector3.one);
            data.wfNodes.Add(node2);

            data.wfBars = new List<WFBar>();
            var bar = new WFBar(node1.m_id, node2.m_id,BarPosType.upBar);
            data.wfBars.Add(bar);
            return data;
        }

        protected override WFData GenerateWFDataUnit(FrameRule clamp)
        {
            throw new NotImplementedException();
        }
        public override List<WFFul> CalcFulcrumPos(FrameRule clamp)
        {
            return new List<WFFul>();
        }
    }
}