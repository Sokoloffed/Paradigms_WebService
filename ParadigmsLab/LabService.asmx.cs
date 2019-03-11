using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ParadigmsLab;

namespace ParadigmsLab
{
    /// <summary>
    /// Сводное описание для LabService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Чтобы разрешить вызывать веб-службу из скрипта с помощью ASP.NET AJAX, раскомментируйте следующую строку. 
    // [System.Web.Script.Services.ScriptService]
    public class LabService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Привет всем!";
        }

        [WebMethod]
        public int Sosite_primitivi(int suka)
        {
            return suka*1488;

        }

        [WebMethod]
        public int[] Lab1_22(List<int> input)
        {
            Lab22 lab22 = new Lab22(input);
            lab22.createDictionary();
            return lab22.solve().ToArray<int>();
        }
    }
}
