using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JobLogger.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethodFile()
        {
            ContentLog con = new ContentLog(true, false, false);
            con.ContText = "Prueba Mensaje";
            con.IdContent = 1;
            JobLogger job = new JobLogger();
            bool resultado = job.GenerateLog(con, true, false, false);
        }

        [TestMethod]
        public void TestMethodCMD()
        {
            ContentLog con = new ContentLog(true, false, false);
            con.ContText = "Prueba Mensaje";
            con.IdContent = 1;
            JobLogger job = new JobLogger();
            bool resultado = job.GenerateLog(con, false, true, false);
        }

        [TestMethod]
        public void TestMethodBD()
        {
            ContentLog con = new ContentLog(true, false, false);
            con.ContText = "Prueba Mensaje";
            con.IdContent = 1;
            JobLogger job = new JobLogger();
            bool resultado = job.GenerateLog(con, false,false , true);
        }
    }
}
