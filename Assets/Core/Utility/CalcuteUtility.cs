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

    public static class CalcuteUtility
    {
        /// <summary>
        /// 生成一组[三角锥]单元信息
        /// 上面下角为原点
        /// </summary>
        /// <param name="x_Size"></param>
        /// <param name="y_Size"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        internal static WFData TrigonumSpaceGrid_Unit(float edge, float height)
        {
            var wfData = new WFData();
            var edgeHeight = edge * 0.5f * Mathf.Tan(Mathf.Deg2Rad * 60);
            var centr1 = edge * 0.5f / Mathf.Cos(Mathf.Deg2Rad * 30);
            var node1 = new WFNode(new Vector3(0, 0, 0), NodePosType.taperedBottom);
            var node2 = new WFNode(new Vector3(edge * 0.5f, 0, edgeHeight), NodePosType.taperedBottom);
            var node3 = new WFNode(new Vector3(-edge * 0.5f, 0, edgeHeight), NodePosType.taperedBottom);
            var node4 = new WFNode(new Vector3(0, -height, centr1), NodePosType.taperedTop);

            wfData.wfNodes.Add(node1);
            wfData.wfNodes.Add(node2);
            wfData.wfNodes.Add(node3);
            wfData.wfNodes.Add(node4);

            wfData.wfBars.Add(new WFBar(node1.m_id, node2.m_id, BarPosType.upBar));//1-2
            wfData.wfBars.Add(new WFBar(node1.m_id, node3.m_id, BarPosType.upBar));//1-3
            wfData.wfBars.Add(new WFBar(node1.m_id, node4.m_id, BarPosType.centerBar));//1-4
            wfData.wfBars.Add(new WFBar(node2.m_id, node3.m_id, BarPosType.upBar));//2-3
            wfData.wfBars.Add(new WFBar(node2.m_id, node4.m_id, BarPosType.centerBar));//2-4
            wfData.wfBars.Add(new WFBar(node3.m_id, node4.m_id, BarPosType.centerBar));//3-4
            return wfData;
        }

        /// <summary>
        /// 生成一组[桁架型]单元信息
        /// 左下前角为原点
        /// </summary>
        /// <param name="clamp"></param>
        /// <returns></returns>
        internal static WFData TrussTypeSpaceGrid_Unit(float x_Size, float y_Size, float height)
        {
            var wfData = new WFData();
            var node1 = new WFNode(new Vector3(0, 0, 0));
            var node2 = new WFNode(new Vector3(x_Size, 0, 0));
            var node3 = new WFNode(new Vector3(x_Size, height, 0));
            var node4 = new WFNode(new Vector3(0, height, 0));
            var node5 = new WFNode(new Vector3(0, 0, y_Size));
            var node6 = new WFNode(new Vector3(x_Size, 0, y_Size));
            var node7 = new WFNode(new Vector3(x_Size, height, y_Size));
            var node8 = new WFNode(new Vector3(0, height, y_Size));

            wfData.wfNodes.Add(node1);
            wfData.wfNodes.Add(node2);
            wfData.wfNodes.Add(node3);
            wfData.wfNodes.Add(node4);

            wfData.wfNodes.Add(node5);
            wfData.wfNodes.Add(node6);
            wfData.wfNodes.Add(node7);
            wfData.wfNodes.Add(node8);


            wfData.wfBars.Add(new WFBar(node1.m_id, node2.m_id,BarPosType.downBar));//1-2
            wfData.wfBars.Add(new WFBar(node1.m_id, node4.m_id,BarPosType.centerBar));//1-4
            wfData.wfBars.Add(new WFBar(node1.m_id, node5.m_id,BarPosType.downBar));//1-5
            wfData.wfBars.Add(new WFBar(node1.m_id, node8.m_id,BarPosType.centerBar));//1-8
            wfData.wfBars.Add(new WFBar(node2.m_id, node3.m_id,BarPosType.centerBar));//2-3
            wfData.wfBars.Add(new WFBar(node2.m_id, node4.m_id,BarPosType.centerBar));//2-4
            wfData.wfBars.Add(new WFBar(node2.m_id, node6.m_id,BarPosType.downBar));//2-6
            wfData.wfBars.Add(new WFBar(node2.m_id, node7.m_id,BarPosType.centerBar));//2-7
            wfData.wfBars.Add(new WFBar(node3.m_id, node4.m_id,BarPosType.upBar));//3-4
            wfData.wfBars.Add(new WFBar(node3.m_id, node7.m_id,BarPosType.upBar));//3-7
            wfData.wfBars.Add(new WFBar(node4.m_id, node8.m_id,BarPosType.upBar));//4-8
            wfData.wfBars.Add(new WFBar(node5.m_id, node6.m_id,BarPosType.downBar));//5-6
            wfData.wfBars.Add(new WFBar(node5.m_id, node8.m_id,BarPosType.centerBar));//5-8
            wfData.wfBars.Add(new WFBar(node6.m_id, node7.m_id,BarPosType.centerBar));//6-7
            wfData.wfBars.Add(new WFBar(node6.m_id, node8.m_id,BarPosType.centerBar));//6-8
            wfData.wfBars.Add(new WFBar(node7.m_id, node8.m_id,BarPosType.upBar));//7-8
            return wfData;
        }


        /// <summary>
        /// 生成一组[桁架型(三向交叉)]单元信息
        /// 左下前角为原点
        /// </summary>
        /// <param name="clamp"></param>
        /// <returns></returns>
        internal static WFData TrussTypeThreeDirectionSpaceGrid_Unit(float x_Size, float y_Size, float height)
        {
            var wfData = new WFData();
            var node1 = new WFNode(new Vector3(0, 0, 0));
            var node2 = new WFNode(new Vector3(x_Size, 0, 0));
            var node3 = new WFNode(new Vector3(x_Size, height, 0));
            var node4 = new WFNode(new Vector3(0, height, 0));
            var node5 = new WFNode(new Vector3(0, 0, y_Size));
            var node6 = new WFNode(new Vector3(x_Size, 0, y_Size));
            var node7 = new WFNode(new Vector3(x_Size, height, y_Size));
            var node8 = new WFNode(new Vector3(0, height, y_Size));

            wfData.wfNodes.Add(node1);
            wfData.wfNodes.Add(node2);
            wfData.wfNodes.Add(node3);
            wfData.wfNodes.Add(node4);

            wfData.wfNodes.Add(node5);
            wfData.wfNodes.Add(node6);
            wfData.wfNodes.Add(node7);
            wfData.wfNodes.Add(node8);


            wfData.wfBars.Add(new WFBar(node1.m_id, node2.m_id,BarPosType.downBar));//1-2
            wfData.wfBars.Add(new WFBar(node1.m_id, node4.m_id,BarPosType.centerBar));//1-4
            wfData.wfBars.Add(new WFBar(node1.m_id, node5.m_id,BarPosType.downBar));//1-5
            wfData.wfBars.Add(new WFBar(node1.m_id, node6.m_id,BarPosType.downBar));//1-6
            wfData.wfBars.Add(new WFBar(node1.m_id, node8.m_id,BarPosType.centerBar));//1-8
            wfData.wfBars.Add(new WFBar(node2.m_id, node3.m_id,BarPosType.centerBar));//2-3
            wfData.wfBars.Add(new WFBar(node2.m_id, node4.m_id,BarPosType.centerBar));//2-4
            wfData.wfBars.Add(new WFBar(node2.m_id, node6.m_id,BarPosType.downBar));//2-6
            wfData.wfBars.Add(new WFBar(node2.m_id, node7.m_id,BarPosType.centerBar));//2-7
            wfData.wfBars.Add(new WFBar(node3.m_id, node4.m_id,BarPosType.upBar));//3-4
            wfData.wfBars.Add(new WFBar(node3.m_id, node7.m_id,BarPosType.upBar));//3-7
            wfData.wfBars.Add(new WFBar(node4.m_id, node6.m_id,BarPosType.centerBar));//4-6
            wfData.wfBars.Add(new WFBar(node4.m_id, node7.m_id,BarPosType.upBar));//4-7
            wfData.wfBars.Add(new WFBar(node4.m_id, node8.m_id,BarPosType.upBar));//4-8
            wfData.wfBars.Add(new WFBar(node5.m_id, node6.m_id,BarPosType.downBar));//5-6
            wfData.wfBars.Add(new WFBar(node5.m_id, node8.m_id,BarPosType.centerBar));//5-8
            wfData.wfBars.Add(new WFBar(node6.m_id, node7.m_id,BarPosType.centerBar));//6-7
            wfData.wfBars.Add(new WFBar(node6.m_id, node8.m_id,BarPosType.centerBar));//6-8
            wfData.wfBars.Add(new WFBar(node7.m_id, node8.m_id,BarPosType.upBar));//7-8
            return wfData;
        }


        /// <summary>
        /// 生成一组[桁架型(菱形)]单元信息
        /// 左下前角为原点
        /// </summary>
        /// <param name="clamp"></param>
        /// <returns></returns>
        internal static WFData TrussTypeDiamondSpaceGrid_Unit(float x_Size, float y_Size, float height)
        {
            var wfData = new WFData();
            var node1 = new WFNode(new Vector3(0, 0, 0));//1
            var node2 = new WFNode(new Vector3(x_Size * 0.5f, 0, -y_Size * 0.5f));//2
            var node3 = new WFNode(new Vector3(x_Size * 0.5f, height, -y_Size * 0.5f));//3
            var node4 = new WFNode(new Vector3(0, height, 0));//4
            var node5 = new WFNode(new Vector3(x_Size * 0.5f, 0, y_Size * 0.5f));//5
            var node6 = new WFNode(new Vector3(x_Size, 0, 0));//6
            var node7 = new WFNode(new Vector3(x_Size, height, 0));//7
            var node8 = new WFNode(new Vector3(x_Size * 0.5f, height, y_Size * 0.5f));//8

            wfData.wfNodes.Add(node1);
            wfData.wfNodes.Add(node2);
            wfData.wfNodes.Add(node3);
            wfData.wfNodes.Add(node4);

            wfData.wfNodes.Add(node5);
            wfData.wfNodes.Add(node6);
            wfData.wfNodes.Add(node7);
            wfData.wfNodes.Add(node8);


            //wfData.wfBars.Add(new WFBar(node1.m_id, node2.m_id));//1-2
            //wfData.wfBars.Add(new WFBar(node1.m_id, node4.m_id));//1-4
            //wfData.wfBars.Add(new WFBar(node1.m_id, node5.m_id));//1-5
            //wfData.wfBars.Add(new WFBar(node1.m_id, node8.m_id));//1-8
            //wfData.wfBars.Add(new WFBar(node2.m_id, node3.m_id));//2-3
            //wfData.wfBars.Add(new WFBar(node2.m_id, node4.m_id));//2-4
            //wfData.wfBars.Add(new WFBar(node2.m_id, node6.m_id));//2-6
            //wfData.wfBars.Add(new WFBar(node2.m_id, node7.m_id));//2-7
            //wfData.wfBars.Add(new WFBar(node3.m_id, node4.m_id));//3-4
            //wfData.wfBars.Add(new WFBar(node3.m_id, node7.m_id));//3-7
            //wfData.wfBars.Add(new WFBar(node4.m_id, node8.m_id));//4-8
            //wfData.wfBars.Add(new WFBar(node5.m_id, node6.m_id));//5-6
            //wfData.wfBars.Add(new WFBar(node5.m_id, node8.m_id));//5-8
            //wfData.wfBars.Add(new WFBar(node6.m_id, node7.m_id));//6-7
            //wfData.wfBars.Add(new WFBar(node6.m_id, node8.m_id));//6-8
            //wfData.wfBars.Add(new WFBar(node7.m_id, node8.m_id));//7-8

            wfData.wfBars.Add(new WFBar(node1.m_id, node2.m_id, BarPosType.downBar));//1-2
            wfData.wfBars.Add(new WFBar(node1.m_id, node4.m_id, BarPosType.centerBar));//1-4
            wfData.wfBars.Add(new WFBar(node1.m_id, node5.m_id, BarPosType.downBar));//1-5
            wfData.wfBars.Add(new WFBar(node1.m_id, node8.m_id, BarPosType.centerBar));//1-8
            wfData.wfBars.Add(new WFBar(node2.m_id, node3.m_id, BarPosType.centerBar));//2-3
            wfData.wfBars.Add(new WFBar(node2.m_id, node4.m_id, BarPosType.centerBar));//2-4
            wfData.wfBars.Add(new WFBar(node2.m_id, node6.m_id, BarPosType.downBar));//2-6
            wfData.wfBars.Add(new WFBar(node2.m_id, node7.m_id, BarPosType.centerBar));//2-7
            wfData.wfBars.Add(new WFBar(node3.m_id, node4.m_id, BarPosType.upBar));//3-4
            wfData.wfBars.Add(new WFBar(node3.m_id, node7.m_id, BarPosType.upBar));//3-7
            wfData.wfBars.Add(new WFBar(node4.m_id, node8.m_id, BarPosType.upBar));//4-8
            wfData.wfBars.Add(new WFBar(node5.m_id, node6.m_id, BarPosType.downBar));//5-6
            wfData.wfBars.Add(new WFBar(node5.m_id, node8.m_id, BarPosType.centerBar));//5-8
            wfData.wfBars.Add(new WFBar(node6.m_id, node7.m_id, BarPosType.centerBar));//6-7
            wfData.wfBars.Add(new WFBar(node6.m_id, node8.m_id, BarPosType.centerBar));//6-8
            wfData.wfBars.Add(new WFBar(node7.m_id, node8.m_id, BarPosType.upBar));//7-8
            return wfData;
        }

        /// <summary>
        /// 生成一组[四角锥]单元信息
        /// </summary>
        /// <param name="x_Size"></param>
        /// <param name="y_Size"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        internal static WFData QuadrangularSpaceGrid_Unit(float x_Size, float y_Size, float height)
        {
            var wfData = new WFData();
            var node1 = new WFNode(new Vector3(0, 0, 0), NodePosType.taperedBottom);
            var node2 = new WFNode(new Vector3(x_Size, 0, 0), NodePosType.taperedBottom);
            var node3 = new WFNode(new Vector3(x_Size, 0, y_Size), NodePosType.taperedBottom);
            var node4 = new WFNode(new Vector3(0, 0, y_Size), NodePosType.taperedBottom);
            var node5 = new WFNode(new Vector3(x_Size * 0.5f, -height, y_Size * 0.5f), NodePosType.taperedTop);

            wfData.wfNodes.Add(node1);
            wfData.wfNodes.Add(node2);
            wfData.wfNodes.Add(node3);
            wfData.wfNodes.Add(node4);
            wfData.wfNodes.Add(node5);

            wfData.wfBars.Add(new WFBar(node1.m_id, node2.m_id, BarPosType.upBar));//1-2
            wfData.wfBars.Add(new WFBar(node1.m_id, node4.m_id, BarPosType.upBar));//1-4
            wfData.wfBars.Add(new WFBar(node1.m_id, node5.m_id, BarPosType.centerBar));//1-5
            wfData.wfBars.Add(new WFBar(node2.m_id, node3.m_id, BarPosType.upBar));//2-3
            wfData.wfBars.Add(new WFBar(node2.m_id, node5.m_id, BarPosType.centerBar));//2-5
            wfData.wfBars.Add(new WFBar(node3.m_id, node4.m_id, BarPosType.upBar));//3-4
            wfData.wfBars.Add(new WFBar(node3.m_id, node5.m_id, BarPosType.centerBar));//3-5
            wfData.wfBars.Add(new WFBar(node4.m_id, node5.m_id, BarPosType.centerBar));//4-5
            return wfData;
        }

        /// <summary>
        /// 生成一组[四角锥(菱形)]单元信息
        /// </summary>
        /// <param name="x_Size"></param>
        /// <param name="y_Size"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        internal static WFData QuadDiamondSpaceGrid_Unit(float x_Size, float y_Size, float height)
        {
            var wfData = new WFData();
            var node1 = new WFNode(new Vector3(0, 0, 0), NodePosType.taperedBottom);
            var node2 = new WFNode(new Vector3(x_Size * 0.5f, 0, -y_Size * 0.5f), NodePosType.taperedBottom);
            var node3 = new WFNode(new Vector3(x_Size, 0, 0), NodePosType.taperedBottom);
            var node4 = new WFNode(new Vector3(x_Size * 0.5f, 0, y_Size * 0.5f), NodePosType.taperedBottom);
            var node5 = new WFNode(new Vector3(x_Size * 0.5f, -height, 0), NodePosType.taperedTop);

            wfData.wfNodes.Add(node1);
            wfData.wfNodes.Add(node2);
            wfData.wfNodes.Add(node3);
            wfData.wfNodes.Add(node4);
            wfData.wfNodes.Add(node5);

            wfData.wfBars.Add(new WFBar(node1.m_id, node2.m_id, BarPosType.upBar));//1-2
            wfData.wfBars.Add(new WFBar(node1.m_id, node4.m_id, BarPosType.upBar));//1-4
            wfData.wfBars.Add(new WFBar(node1.m_id, node5.m_id, BarPosType.centerBar));//1-5
            wfData.wfBars.Add(new WFBar(node2.m_id, node3.m_id, BarPosType.upBar));//2-3
            wfData.wfBars.Add(new WFBar(node2.m_id, node5.m_id, BarPosType.centerBar));//2-5
            wfData.wfBars.Add(new WFBar(node3.m_id, node4.m_id, BarPosType.upBar));//3-4
            wfData.wfBars.Add(new WFBar(node3.m_id, node5.m_id, BarPosType.centerBar));//3-5
            wfData.wfBars.Add(new WFBar(node4.m_id, node5.m_id, BarPosType.centerBar));//4-5
            return wfData;
        }
        
        /// <summary>
        /// (边界连接)
        /// </summary>
        /// <param name="bundNodes"></param>
        /// <param name="barType"></param>
        /// <returns></returns>
        internal static WFData ConnectNeerBy(List<WFNode> bundNodes, float distence, string barType, BoundConnectType connectType = BoundConnectType.XOrYAxis)
        {
            var data = new WFData();
            for (int i = 0; i < bundNodes.Count; i++)
            {
                var node = bundNodes[i];
                data.wfNodes.Add(node);
                if (i < bundNodes.Count - 1)
                {
                    for (int j = i + 1; j < bundNodes.Count; j++)
                    {
                        var otherNode = bundNodes[j];
                        if (Vector3.Distance(node.position, otherNode.position) < distence + 0.1f)
                        {
                            bool match = false;

                            switch (connectType)
                            {
                                case BoundConnectType.XAxisOnly:
                                    match = Mathf.Abs(node.position.x - otherNode.position.x) < 0.1f;
                                    break;
                                case BoundConnectType.YAxisOnly:
                                    match = Mathf.Abs(node.position.z - otherNode.position.z) < 0.1f;
                                    break;
                                case BoundConnectType.XOrYAxis:
                                    match = Mathf.Abs(node.position.x - otherNode.position.x) < 0.1f
                                        || Mathf.Abs(node.position.z - otherNode.position.z) < 0.1f;
                                    break;
                                case BoundConnectType.NoXAxis:
                                    match = Mathf.Abs(node.position.x - otherNode.position.x) > 0.1f;
                                    break;
                                case BoundConnectType.NoYAxis:
                                    match = Mathf.Abs(node.position.z - otherNode.position.z) > 0.1f;
                                    break;
                                case BoundConnectType.NoXAndYAxis:
                                    match = Mathf.Abs(node.position.x - otherNode.position.x) > 0.1f
                                        && Mathf.Abs(node.position.z - otherNode.position.z) > 0.1f;
                                    break;
                                case BoundConnectType.NoRule:
                                    match = true;
                                    break;
                                default:
                                    break;
                            }
                            if (match)
                            {
                                data.wfBars.Add(new WFBar(node.m_id, otherNode.m_id, barType));
                            }
                        }
                    }
                }
            }
            return data;
        }

        /// <summary>
        /// 连接相邻点
        /// </summary>
        /// <param name="topNodes"></param>
        /// <returns></returns>
        internal static WFData ConnectNeerBy(WFNode[,] topNodes, string barType)
        {
            WFData data = new WFData();
            var xCount = topNodes.GetLength(0);
            var yCount = topNodes.GetLength(1);

            for (int i = 0; i < xCount; i++)
            {
                for (int j = 0; j < yCount; j++)
                {
                    var node = topNodes[i, j];

                    if (node == null) continue;

                    data.wfNodes.Add(node);
                    if (i > 0)//左
                    {
                        var node_l = topNodes[i - 1, j];
                        if (node_l != null)
                        {
                            data.wfBars.Add(new WFBar(node.m_id, node_l.m_id, barType));
                        }
                    }
                    if (i < xCount - 1)//右
                    {
                        var node_r = topNodes[i + 1, j];
                        if (node_r != null)
                        {
                            data.wfBars.Add(new WFBar(node.m_id, node_r.m_id, barType));
                        }
                    }
                    if (j > 0)//下
                    {
                        var node_d = topNodes[i, j - 1];
                        if (node_d != null)
                        {
                            data.wfBars.Add(new WFBar(node.m_id, node_d.m_id, barType));
                        }
                    }
                    if (j < yCount - 1)//上
                    {
                        var node_u = topNodes[i, j + 1];
                        if (node_u != null)
                        {
                            data.wfBars.Add(new WFBar(node.m_id, node_u.m_id, barType));
                        }
                    }
                    //Debug.Log(i + "" + j + topNodes[i, j].m_id);
                }
            }
            return data;
        }

        /// <summary>
        /// 记录四边型边上的点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="iMax"></param>
        /// <param name="jMax"></param>
        /// <param name="startPos"></param>
        /// <param name="x_Size"></param>
        /// <param name="y_Size"></param>
        /// <param name="positions"></param>
        internal static void RecordQuadBound(int i, int j, int iMax, int jMax, Vector3 startPos, float x_Size, float y_Size, List<WFFul> positions, FulcrumType type , float height = 0)
        {
            var pos = startPos + i * x_Size * Vector3.right + j * y_Size * Vector3.forward + Vector3.down * height;
            if (i == 0)
            {
                positions.Add(new WFFul(pos,type));
            }
            if (j == 0)
            {
                var downRight = pos + x_Size * Vector3.right;
                positions.Add(new WFFul(downRight, type));
            }

            if (i == iMax - 1)
            {
                var rightUp = pos + x_Size * Vector3.right + y_Size * Vector3.forward;
                positions.Add(new WFFul(rightUp, type));
            }

            if (j == jMax - 1)
            {
                var leftUp = pos + y_Size * Vector3.forward;
                positions.Add(new WFFul(leftUp, type));
            }
        }

        /// <summary>
        /// 记录四边型中间的一系列点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="iMax"></param>
        /// <param name="jMax"></param>
        /// <param name="startPos"></param>
        /// <param name="x_Size"></param>
        /// <param name="y_Size"></param>
        /// <param name="positions"></param>
        /// <param name="height"></param>
        internal static void RecordQuadPoint(int i, int j, int iMax, int jMax, Vector3 startPos, float x_Size, float y_Size, List<WFFul> positions, FulcrumType type, float height = 0)
        {
            if (iMax < 3 || jMax < 3) return;

            var pos = startPos + i * x_Size * Vector3.right + j * y_Size * Vector3.forward + Vector3.down * height;
            var left = 1;
            var right = iMax - 1;
            var up = jMax - 1;
            var down = 1;
            var yCenter = (j == jMax / 2 && (j - 1) % 2 == 0 && (jMax - j) % 2 == 0);
            var xCenter = (i == iMax / 2 && (i - 1) % 2 == 0 && (iMax - j) % 2 == 0);
            var yMatch = (yCenter ||((j - 1) % 2 == 0 && j <= jMax / 2) || ((jMax - j - 1) % 2 == 0) && j >= jMax / 2) && j >= down && j <= up;      //(j == jMax /2 && (j - 1) % 2 == 0 && (jMax - j - 1) % 2 == 0) ||
            var xMatch = (xCenter || ((i - 1) % 2 == 0 && i <= iMax / 2) || ((iMax - i - 1) % 2 == 0) && i >= iMax / 2) && i >= left && i <= right;    //(i == iMax /2 && (i - 1) % 2 == 0 && (iMax - i - 1) % 2 == 0) ||

            if (i == left && yMatch)
            {
                 positions.Add(new WFFul(pos,type));
            }

            if (i == right - 1 && yMatch)
            {
                var rightDown = pos + x_Size * Vector3.right;
                positions.Add(new WFFul(rightDown, type));
            }

            if (j == down && xMatch)
            {
                 positions.Add(new WFFul(pos, type));
            }

            if (j == up - 1 && xMatch)
            {
                var leftUp = pos + y_Size * Vector3.forward;
                 positions.Add(new WFFul(leftUp, type));
            }
        }

        /// <summary>
        /// 记录四边型中间一系列上四角锥的顶点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="iMax"></param>
        /// <param name="jMax"></param>
        /// <param name="startPos"></param>
        /// <param name="x_Size"></param>
        /// <param name="y_Size"></param>
        /// <param name="height"></param>
        /// <param name="positions"></param>
        internal static void RecordQuadAngularPoint(int i, int j, int iMax, int jMax, Vector3 startPos, float x_Size, float y_Size, float height, List<WFFul> positions, FulcrumType type)
        {
            if (iMax < 4 || jMax < 4) return;

            var pos = startPos + (i + 0.5f) * x_Size * Vector3.right + (j + 0.5f) * y_Size * Vector3.forward + Vector3.down * height;
            var left = 1;
            var right = iMax - 1;
            var up = jMax - 1;
            var down = 1;
            var yCenter = (j == jMax / 2 && (j - 1) % 2 == 0 && (jMax - j) % 2 == 0);
            var xCenter = (i == iMax / 2 && (i - 1) % 2 == 0 && (iMax - j) % 2 == 0);
            var yMatch = (yCenter || ((j - 1) % 2 == 0 && j < jMax / 2) || ((jMax - j) % 2 == 0) && j > jMax / 2) && j >= down && j <= up - 1;      //(j == jMax /2 && (j - 1) % 2 == 0 && (jMax - j - 1) % 2 == 0) ||
            var xMatch = (xCenter || ((i - 1) % 2 == 0 && i < iMax / 2) || ((iMax - i) % 2 == 0) && i > iMax / 2) && i >= left && i <= right - 1;    //(i == iMax /2 && (i - 1) % 2 == 0 && (iMax - i - 1) % 2 == 0) ||

            if ((i == left && yMatch) || (i == right - 1 && yMatch) || (j == down && xMatch) || (j == up - 1 && xMatch))
            {
                 positions.Add(new WFFul(pos,type));
            }
        }
        /// <summary>
        /// 记录斜放四边型边上的点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="iMax"></param>
        /// <param name="jMax"></param>
        /// <param name="startPos"></param>
        /// <param name="x_Size"></param>
        /// <param name="y_Size"></param>
        /// <param name="positions"></param>
        internal static void RecordQuadXieBound(int i, int j, int iMax, int jMax, Vector3 startPos, float x_Size, float y_Size, List<WFFul> positions, FulcrumType type, float height = 0)
        {
            var position = startPos + i * x_Size * Vector3.right + j * y_Size * Vector3.forward + Vector3.down * height;

            if (i == 0)//左
            {
                positions.Add(new WFFul( position,type));
            }
            if (j == 0)//下
            {
                var downPos = position + x_Size * Vector3.right * 0.5f - y_Size * Vector3.forward * 0.5f;
                positions.Add(new WFFul(downPos, type));
            }
            if (i == iMax - 1)//右
            {
                var rightPos = position + x_Size * Vector3.right;
                positions.Add(new WFFul(rightPos, type));
            }
            if (j == jMax - 1)//上
            {
                var upPos = position + x_Size * Vector3.right * 0.5f + y_Size * Vector3.forward * 0.5f;
                positions.Add(new WFFul(upPos, type));
            }
        }
        /// <summary>
        /// 记录四边型边上四角锥的顶点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="iMax"></param>
        /// <param name="jMax"></param>
        /// <param name="startPos"></param>
        /// <param name="x_Size"></param>
        /// <param name="y_Size"></param>
        /// <param name="height"></param>
        /// <param name="positions"></param>
        internal static void RecordQuadrAngular(int i, int j, int iMax, int jMax, Vector3 startPos, float x_Size, float y_Size, float height, List<WFFul> positions, FulcrumType type)
        {
            var pos = startPos + (i + 0.5f) * x_Size * Vector3.right + (j + 0.5f) * y_Size * Vector3.forward + Vector3.down * height;
            if (i == 0 || j == 0 || i == iMax - 1 || j == jMax - 1)
            {
                positions.Add(new WFFul(pos,type));
            }
        }
        /// <summary>
        /// 记录四边形四角锥的顶点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="iMax"></param>
        /// <param name="jMax"></param>
        /// <param name="startPos"></param>
        /// <param name="x_Size"></param>
        /// <param name="y_Size"></param>
        /// <param name="height"></param>
        /// <param name="positions"></param>
        internal static void RecordQuadrXieAngular(int i, int j, int iMax, int jMax, Vector3 startPos, float x_Size, float y_Size, float height, List<WFFul> positions, FulcrumType type)
        {
            var position = startPos + (i + 0.5f) * x_Size * Vector3.right + j * y_Size * Vector3.forward + Vector3.down * height;
            if (i == 0 || j == 0 || i == iMax - 1 || j == jMax - 1)
            {
                positions.Add(new WireFrame.WFFul(position,type));
            }
        }
        /// <summary>
        /// 记录六边形边上的点
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="num"></param>
        /// <param name="startPos"></param>
        /// <param name="unitSize"></param>
        /// <param name="unitHeight"></param>
        /// <param name="positions"></param>
        internal static void RecordSixBound(int i, int j, int num, Vector3 startPos, float unitSize, float unitHeight, List<WFFul> positions, FulcrumType type, float height = 0)
        {
            var pos = startPos +
                       (j * unitSize - 0.5f * unitSize * (num - Mathf.Abs(i + 1))) * Vector3.right +
                       unitHeight * (i + num) * Vector3.forward + Vector3.down * height;

            if (i == -num)
            {
                positions.Add(new WireFrame.WFFul(pos,type));
            }
            if (j == 0)
            {
                var leftPos = pos - unitSize * 0.5f * Vector3.right + unitHeight * Vector3.forward;
                positions.Add(new WireFrame.WFFul(leftPos, type));
            }
            if (j == 2 * num - Mathf.Abs(i + 1) - 1)
            {
                var rightPos = pos + unitSize * 0.5f * Vector3.right + unitHeight * Vector3.forward;
                positions.Add(new WireFrame.WFFul(rightPos, type));
            }
            if (i == num - 1)
            {
                var upPos = pos - unitSize * 0.5f * Vector3.right + unitHeight * Vector3.forward;
                positions.Add(new WireFrame.WFFul(upPos, type));
            }
        }
        /// <summary>
        /// 记录六边形边上三角形的顶点坐标
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="num"></param>
        /// <param name="startPos"></param>
        /// <param name="unitSize"></param>
        /// <param name="unitHeight"></param>
        /// <param name="positions"></param>
        /// <param name="height"></param>
        internal static void RecordSixBoundAngular(int i, int j, int num, Vector3 startPos, float unitSize, float unitHeight, float height, List<WFFul> positions, FulcrumType type)
        {
            var pos = startPos + (j * unitSize - 0.5f * unitSize * (num - Mathf.Abs(i + 1))) * Vector3.right + unitHeight * (i + num) * Vector3.forward + (unitSize * 0.5f / Mathf.Cos(Mathf.Deg2Rad * 30)) * Vector3.forward + Vector3.down * height;
            if (i == -num || j == 0 || j == 2 * num - Mathf.Abs(i + 1) - 1 || i == num - 1)
            {
                positions.Add(new WireFrame.WFFul(pos, type));
            }
        }

        /// <summary>
        /// 判断两点是否在 一起
        /// </summary>
        /// <param name="sourePos"></param>
        /// <param name="targetPos"></param>
        /// <returns></returns>
        internal static bool IsSimulatePos(Vector3 sourePos, Vector3 targetPos)
        {
            if (Vector3.Distance(sourePos, targetPos) < 0.1f)
            {
                return true;
            }
            return false;
        }

       
    }
}