using Microsoft.VisualStudio.TestTools.UnitTesting;
using Neo.SmartContract.Testing;
using Neo.SmartContract.Testing.TestingStandards;
using System.Numerics;

namespace Neo.Compiler.CSharp.UnitTests
{
    [TestClass]
    public class UnitTest_Invoke : TestBase<Contract_InvokeCsNef>
    {
        public UnitTest_Invoke() : base(Contract_InvokeCsNef.Nef, Contract_InvokeCsNef.Manifest) { }

        [TestMethod]
        public void Test_Return_Integer()
        {
            Assert.AreEqual(new BigInteger(42), Contract.ReturnInteger());
        }

        [TestMethod]
        public void Test_Return_String()
        {
            Assert.AreEqual("hello world", Contract.ReturnString());
        }

        [TestMethod]
        public void Test_Main()
        {
            Assert.AreEqual(new BigInteger(22), Contract.TestMain());
        }
    }
}
