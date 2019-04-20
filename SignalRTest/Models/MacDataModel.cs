using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRTest.Models
{
    public class MacDataModel
    {
        /// <summary>
        /// 设备ID,默认为设备的MAC地址。
        /// </summary>
        public string device_id { get; set; }

        /// <summary>
        /// 上传时间 2018-03-14#10:47:38
        /// </summary>
        public string device_time { get; set; }

        /// <summary>
        /// 设备的key,主要目的，是检验数据的合法性， 开发时可以不处理此字段
        /// </summary>
        public string device_key { get; set; }

        /// <summary>
        /// 位置mac 24:24:01:0D:4A:FA|new_mesh,24:24:02:0C:8F:3D|new_mesh,24:24:02:0A:52:D1|new_mesh
        /// </summary>
        public string postion_mac { get; set; }

        /// <summary>
        /// MAC地址列表，每一个MAC由\r\n分隔，每一行为逗号分隔，
        /// 第一列表示手机MAC,第二列表示路由mac,第三列表示路由名称（如果为空表示没有名称），
        /// 第四列表示采集设备与手机的距离，第5列表示是否已经连接路由
        /// </summary>
        public string message { get; set; }
    }
}