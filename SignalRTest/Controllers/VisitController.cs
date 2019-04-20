using Microsoft.AspNet.SignalR;
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

namespace SignalRTest.Controllers
{
    public class VisitController : ApiController, VisitsBusinessInterface
    {
        [HttpGet, Route("Values/VisitAdd")]
        public string VisitAdd(CusVisitModel model)
        {

            try
            {
                DBModel db = new DBModel();
                foreach (var i in model.visitList)
                {
                    cus_visit visit = new cus_visit()
                    {
                        VISIT_MAC = i.cusMac,
                        CUS_NAME = i.cusName,
                        CUS_MOBILE = i.cusMobtle,
                        CUS_AGE_RANGE = i.cusAge,
                        CUS_SEX = i.cusSex,
                        VISIT_TIME = Convert.ToDateTime(i.cusVisitTime),
                        CUS_INFO_STATUS = "1",
                        Guid = System.Guid.NewGuid().ToString("N")
                    };

                    db.cus_visit.Add(visit);
                }
                db.SaveChanges();

                string resultJson = JsonConvert.SerializeObject(model);
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
