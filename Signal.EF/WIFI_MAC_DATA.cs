using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal.EF
{
    [Table("wifi_mac_data")]
    public class WIFI_MAC_DATA
    {
        [Key]
        [StringLength(100)]
        public string ID { get; set; }

        /// <summary>
        /// 探针ID
        /// </summary>
        [StringLength(100)]
        public string DEVICE_ID { get; set; }

        /// <summary>
        /// 路由设备名称
        /// </summary>
        [StringLength(100)]
        public string WIFI_MAC_NAME { get; set; }

        /// <summary>
        /// 路由设备MAC
        /// </summary>
        [StringLength(100)]
        public string WIFI_MAC_ADDRESS { get; set; }

        /// <summary>
        /// 手机MAC
        /// </summary>
        [StringLength(100)]
        public string PHONE_MAC_ADDRESS { get; set; }

        /// <summary>
        /// 是否连接
        /// </summary>
        [StringLength(100)]
        public string IS_CONNECTED { get; set; }

        /// <summary>
        /// 距离
        /// </summary>
        [StringLength(100)]
        public string DISTANCE { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        [Column(TypeName = "date")]
        public DateTime? UPLOAD_DATE { get; set; }
    }
}
