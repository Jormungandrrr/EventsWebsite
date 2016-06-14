<<<<<<< HEAD
﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Metal_Archives.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;


namespace EventsWebsite.Database
{
    [TestClass()]
    public class DatabaseTests
    {
        [TestMethod()]
        public void CheckConnection()
        {
            bool connection = false;
            string Connectionstring = @"Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = fhictora01.fhict.local)(PORT = 1521)))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = fhictora))); User ID = dbi331842; PASSWORD =CZSKUvxUUs;";
            try
            {
                using (OracleConnection conn = new OracleConnection(Connectionstring))
                {
                    conn.Open();
                    connection = true;
                }
            }
            catch {}
            Assert.IsTrue(connection);
        }

        [TestMethod()]
        public void ReadStringWithConditionTest()
        {
            Assert.Fail();
        }
    }
=======
﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Types;
using Oracle.ManagedDataAccess.Client;


namespace EventsWebsite.Database
{
    [TestClass()]
    public class DatabaseTests
    {
        [TestMethod()]
        public void CheckConnection()
        {
            bool connection = false;
            string Connectionstring = @"Data Source = (DESCRIPTION = (ADDRESS_LIST = (ADDRESS = (PROTOCOL = TCP)(HOST = fhictora01.fhict.local)(PORT = 1521)))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = fhictora))); User ID = dbi331842; PASSWORD =CZSKUvxUUs;";
            try
            {
                using (OracleConnection conn = new OracleConnection(Connectionstring))
                {
                    conn.Open();
                    connection = true;
                }
            }
            catch {}
            Assert.IsTrue(connection);
        }

        [TestMethod()]
        public void ReadStringWithConditionTest()
        {
            Assert.Fail();
        }
    }
>>>>>>> origin/master
}