using Signal.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signal.Interface
{
    public interface VisitsBusinessInterface
    {
        string VisitDistribution(CusVisitModel model);

        string VisitHotspot(CusVisitModel model);

        string VisitCircular(CusVisitModel model);
    }
}
