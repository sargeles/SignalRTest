using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Signal.EF;
using Signal.Interface;
using Signal.Model;
using SignalRTest.Models;
using Singla.Common;

namespace SignalRTest.Controllers
{
    public class WifiPinPointUploadController : ApiController
    {
        [HttpPost, Route("WifiPinPoint/Upload")]
        public string Upload(MacDataModel model)
        {
            LogHelper.loginfo.Info(model);
            List<string> items = model.message.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            CusVisitModel list = new CusVisitModel();

            //using能及时释放资源,例如数据库连接异常，可以即使将上下文释放
            using (var db = new DBModel())
            {
                foreach (var item in items)
                {
                    var fileds = item.Trim().Split(',');

                    if (fileds.Length == 5)
                    {
                        var data = new WIFI_MAC_DATA();
                        data.ID = Guid.NewGuid().ToString();
                        data.DEVICE_ID = model.device_id;
                        data.PHONE_MAC_ADDRESS = fileds[0];
                        data.WIFI_MAC_ADDRESS = fileds[1];
                        data.WIFI_MAC_NAME = fileds[2];
                        data.DISTANCE = fileds[3];
                        data.IS_CONNECTED = fileds[4];
                        data.UPLOAD_DATE = DateTime.Parse(model.device_time.Replace("#", " "));
                        db.wifi_mac_data.Add(data);
                        list.visitList.Add(new VisitModelList() { cusMac = data.PHONE_MAC_ADDRESS,xAxis =data.DISTANCE });
                    }
                }
                db.SaveChanges();
            }

            VisitsBusinessInterface businessInterface = new VisitController();
            businessInterface.VisitAdd(list);

            return "true";
        }
    }
}