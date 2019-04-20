﻿using Microsoft.AspNet.SignalR;
using SignalRTest.Hubs;
using SignalRTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Signal.EF;
using static SignalRTest.Hubs.ViewHub;
using Newtonsoft.Json;
using Signal.Model;
using Signal.Interface;
using Signal.Common;

namespace SignalRTest.Controllers
{
    public class VisitController : ApiController, VisitsBusinessInterface
    {
        [HttpGet, Route("Values/VisitAdd")]
        public string VisitAdd(CusVisitModel model)
        {

            try
            {
                CusVisitModel pagedata = new CusVisitModel();
                DBModel db = new DBModel();
                Random rd = new Random();
                foreach (var i in model.visitList)
                {
                    List<cus_visit> list = db.cus_visit.Where(m => m.VISIT_MAC == i.cusMac).ToList();
                    if (list.Count() > 0)
                    {
                        var _m = list.First();
                        pagedata.visitList.Add(new VisitModelList()
                        {
                            cusAge = _m.CUS_AGE_RANGE,
                            cusMac = _m.VISIT_MAC,
                            cusMobtle = _m.CUS_MOBILE,
                            cusName = _m.CUS_NAME,
                            cusSex = _m.CUS_SEX,
                            cusVisitTime = Convert.ToString(_m.VISIT_TIME),
                            floor = rd.Next(0, 3).ToString(),
                            xAxis = i.xAxis,
                            yAxis = rd.Next(0, 800).ToString()
                        });
                    }
                }
                LogHelper.Default.WriteInfo(JsonConvert.SerializeObject(pagedata));
                string resultJson = JsonConvert.SerializeObject(pagedata);
                SignalrServerToClient.BroadcastMessage(resultJson);

                //DBModel db = new DBModel();
                //foreach (var m in model.visitList)
                //{
                //    db.cus_visit.Add(new cus_visit()
                //    {
                //        CUS_AGE_RANGE = m.cusAge,
                //        VISIT_MAC = m.cusMac,
                //        CUS_MOBILE = m.cusMobtle,
                //        CUS_NAME = m.cusName,
                //        CUS_SEX = m.cusSex,
                //        VISIT_TIME = Convert.ToDateTime(m.cusVisitTime),
                //        CUS_INFO_STATUS = "1",
                //        Guid = System.Guid.NewGuid().ToString("N")
                //    });
                //}
                //db.SaveChanges();
                return "true";
            }
            catch (Exception ex)
            {
                return "false";
            }
        }
    }
}
