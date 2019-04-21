using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Signal.Common;
using Signal.EF;
using Signal.Interface;
using Signal.Model;
using SignalRTest.Controllers;
using SignalRTest.Models;
using Newtonsoft.Json;

namespace Signal.Controllers
{
    public class WifiPinPointUploadController : ApiController
    {
        [HttpPost, Route("WifiPinPoint/Upload")]
        public string Upload(MacDataModel model)
        {
            string errMsg = string.Empty;
            string resultStr = string.Empty;



            //计算坐标图
            var ret1 = CalculateVisitDistribution(out errMsg, model);
            resultStr += string.Format("计算坐标图结果【{0}】：{1} \r\n", (ret1 ? true : false), string.IsNullOrEmpty(errMsg) ? "成功" : errMsg);
            //计算热力图
            var ret2 = CalculateVisitHotspot(out errMsg);
            resultStr += string.Format("计算热力图结果【{0}】：{1} \r\n", (ret2 ? true : false), string.IsNullOrEmpty(errMsg) ? "成功" : errMsg);
            //计算扇形图
            var ret3 = CalculateVisitDistribution(out errMsg, model);
            resultStr += string.Format("计算扇形图结果【{0}】：{1} \r\n", (ret3 ? true : false), string.IsNullOrEmpty(errMsg) ? "成功" : errMsg);

            return string.Format("计算坐标图结果：");
        }

        private bool CalculateVisitDistribution(out string errMsg, MacDataModel model)
        {
            errMsg = "";
            LogHelper.Default.WriteInfo(JsonConvert.SerializeObject(model) + "\r\n");
            List<string> items = model.message.Split(new string[] { "\r\n" }, StringSplitOptions.None).ToList();
            CusVisitModel list = new CusVisitModel();
            try
            {
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
                            list.visitList.Add(new VisitModelList() { cusMac = data.PHONE_MAC_ADDRESS, xAxis = Convert.ToDecimal(data.DISTANCE) });
                        }
                    }
                    db.SaveChanges();
                }

                LogHelper.Default.WriteInfo(JsonConvert.SerializeObject(list));
                VisitsBusinessInterface businessInterface = new VisitController();
                businessInterface.VisitDistribution(list);

                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                LogHelper.Default.WriteError("计算坐标图坐标时出错" + ex.Message, ex);
                return false;
            }
        }

        private bool CalculateVisitHotspot(out string errMsg)
        {
            errMsg = "";
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
            int positionCount = 0, hoursCount = 0;

            CusVisitModel list = new CusVisitModel();
            try
            {
                //using能及时释放资源,例如数据库连接异常，可以即使将上下文释放
                using (var db = new DBModel())
                {
                    //今天所有的纪录
                    var dataSource = db.wifi_mac_data.Where(d => d.UPLOAD_DATE < today.AddHours(-8) && d.UPLOAD_DATE > today.AddDays(-1).AddHours(-8)).ToList();
                    var positions = dataSource.GroupBy(g => g.DEVICE_ID).OrderBy(g => g.Key).ToList();
                    var hours = DateTime.Now.Hour + 1;

                    //分组
                    for (var p = 0; p < positions.Count(); p++)
                    {
                        //分时
                        for (var h = 0; h < hours; h++) 
                        {
                            //计算热度
                            var value = CalculateHotValue(positions[p].Key, today.AddHours(h), today.AddHours(h+1), dataSource);
                            list.visitList.Add(new VisitModelList()
                            {
                                xAxis = h.ToString(),
                                yAxis = p.ToString(),
                                cusName = value.ToString()
                            });
                        }
                    }


                    db.SaveChanges();
                }

                LogHelper.Default.WriteInfo(JsonConvert.SerializeObject(list));
                VisitsBusinessInterface businessInterface = new VisitController();
                businessInterface.VisitHotspot(list);

                return true;
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
                LogHelper.Default.WriteError("计算热力图坐标时出错" + ex.Message, ex);
                return false;
            }
        }

        private int CalculateHotValue(string deviceName, DateTime startTime, DateTime endTime, List<WIFI_MAC_DATA> dataSource)
        {
            var source = dataSource.Where(d => d.DEVICE_ID == deviceName && d.UPLOAD_DATE >= startTime.AddHours(-8) && d.UPLOAD_DATE <= endTime.AddHours(-8)).ToList();
            int value = 0;

            foreach (var d in source)
            {
                int distance = int.Parse(d.DISTANCE);
                value += distance > 0 && distance < 10 ? 10 - distance : 0;
            }

            return value;
        }
    }
}