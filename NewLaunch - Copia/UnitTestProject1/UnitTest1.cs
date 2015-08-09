using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Effort;
using NewLaunch.Models;
using System.Data.EntityClient;
using System.Data.Common;


namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
           // EntityConnection connection =
                 //   Effort.EntityConnectionFactory.CreateTransient("name=dbEntities");


            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["dbEntities"].ConnectionString;
            // dbEntities context;
            // CreateTransient throws exception
            //EntityConnection conn = Effort.EntityConnectionFactory.CreateTransient(connString);

            //context = new System.Data.CoPathDataContext(conn);


            var context = Effort.EntityConnectionFactory.CreatePersistent("name=dbEntities");


     

        }
    }
}
