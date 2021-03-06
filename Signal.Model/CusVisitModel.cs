﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal.Model
{
    public class CusVisitModel
    {
        public CusVisitModel()
        {
            visitList = new List<VisitModelList>();
            visitHotspotLists = new List<VisitHotspotList>();
        }
        public string type { get; set; }
        public List<VisitModelList> visitList { get; set; }

        public List<VisitHotspotList> visitHotspotLists { get; set; }


    }

    /// <summary>
    /// 热力图
    /// </summary>
    public class VisitHotspotList
    {

        public int xAxis { get; set; }

        public int yAxis { get; set; }

        public int Count { get; set; }

        public string[] Macs { get; set; }
    }

    /// <summary>
    /// 点状图
    /// </summary>
    public class VisitModelList
    {
        /// <summary>
        /// 客户mac地址
        /// </summary>
        public string cusMac { get; set; }

        /// <summary>
        /// 展厅X轴
        /// </summary>
        public decimal xAxis { get; set; }

        /// <summary>
        /// 展厅Y轴
        /// </summary>
        public decimal yAxis { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string cusName { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string cusAge { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string cusMobtle { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string cusSex { get; set; }

        /// <summary>
        /// 进店时间
        /// </summary>
        public string cusVisitTime { get; set; }

        /// <summary>
        /// 楼层
        /// </summary>
        public string floor { get; set; }

    }
}
