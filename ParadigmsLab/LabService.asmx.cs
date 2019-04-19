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
        public int[] Lab1_22(List<int> input)
        {
            Lab22 lab22 = new Lab22(input);
            return lab22.findThreefoldElements();
        }

        [WebMethod]
        public int[] Lab1_51(List<int> input)
        {
            Lab51 lab51 = new Lab51(input);
            return lab51.findBelowZero();
        }
        [WebMethod]
        public int[] Lab2_4(List<int> input)
        {
            Lab2_4 lab2_4 = new Lab2_4(input);
            return lab2_4.findLowPeaks();
        }

        [WebMethod]
        public void Lab3_7(string w)
        {
            Lab3_7 lab = new Lab3_7(w);


        }
    }
}
