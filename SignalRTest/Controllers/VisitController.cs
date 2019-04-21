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
        [HttpGet, Route("Values/VisitDistribution")]
        public string VisitDistribution(CusVisitModel model)
        {
            try
            {
                CusVisitModel pagedata = new CusVisitModel() { type = "VisitDistribution" };
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


        [HttpGet, Route("Values/VisitHotspot")]
        public string VisitHotspot(CusVisitModel model)
        {

            try
            {
                CusVisitModel pagedata = new CusVisitModel();
                if (model.type != "test")
                {
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
                                cusName = _m.CUS_NAME,
                                xAxis = i.xAxis,
                                yAxis = rd.Next(0, 800).ToString()
                            });
                        }
                    }
                }
                else
                {
                    pagedata = model;
                }
                for (int i = 0, length = pagedata.visitList.Count; i < length; i++) {
                    pagedata.visitList[i].xAxis = Convert.ToString(Math.Floor(Convert.ToDecimal(pagedata.visitList[i].xAxis) / 50));
                    pagedata.visitList[i].yAxis = Convert.ToString(Math.Floor(Convert.ToDecimal(pagedata.visitList[i].yAxis) / 100));
                }
                CusVisitsHotspotModel listPage = new CusVisitsHotspotModel() { type = "VisitHotspot" };
                foreach (var i in pagedata.visitList.GroupBy(t => new { t.xAxis, t.yAxis })) {

                    VisitHotspotList refPageModel = new VisitHotspotList();
                    string names = string.Join(",",i.ToList().Select(t=>t.cusName).ToList());

                    var _model = i.FirstOrDefault();
                    refPageModel.xAxis = _model.xAxis;
                    refPageModel.yAxis = _model.yAxis;
                    refPageModel.Names = names;
                    listPage.visitHotspotLists.Add(refPageModel);
                }

                string resultJson = JsonConvert.SerializeObject(listPage); ;
                SignalrServerToClient.BroadcastMessage(resultJson);

                
                return "true";
            }
            catch (Exception ex)
            {
                return "false";
            }
        }


        [HttpGet, Route("Values/VisitCircular")]
        public string VisitCircular(CusVisitModel model)
        {

            try
            {
                CusVisitModel pagedata = new CusVisitModel() { type= "VisitCircular" };
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
                string resultJson = JsonConvert.SerializeObject(pagedata);
                SignalrServerToClient.BroadcastMessage(resultJson);
                return "true";
            }
            catch (Exception ex)
            {
                return "false";
            }
        }
    }
}
