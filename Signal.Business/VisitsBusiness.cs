using Newtonsoft.Json;
using Signal.EF;
using Signal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal.Business
{
    public class VisitsBusiness
    {
        public string SetPageData(CusVisitModel model)
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
                return "true";
            }
            catch (Exception ex)
            {
                return "false";
            }
        }
    }
}
