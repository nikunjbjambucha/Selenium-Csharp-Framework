using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFrameworkTest.Data
{
    public class ObjectMother
    {
        public static TestDataRowTitle TestData(TestContext testContext)
        {
            var Testdatarow = new TestDataRowTitle
            {
                BrowserType = Convert.ToString(testContext.DataRow.GetValue("BrowserType")),
                UserName = Convert.ToString(testContext.DataRow.GetValue("username")),
                Password = Convert.ToString(testContext.DataRow.GetValue("password")),


            };
            return Testdatarow;
        }
    }

    public static class DataRowExtensions
    {
        public static object GetValue(this DataRow row, string column)
        {
            return row.Table.Columns.Contains(column) ? row[column] : null;
        }
    }
}