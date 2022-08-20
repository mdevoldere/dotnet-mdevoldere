using MDevoldere.Domain;
using MDevoldere.Domain.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDevoldere.Tests.Domain
{
    [TestClass]
    public  class Stock_Test
    {
        [TestMethod]
        public void Stock_Test1()
        {
            int max = 123;

            ModelStock<BaseUser> users = new(new BaseUser() { Username = "Mike"}, max);

            users.Push(42);

            Assert.AreEqual(42, users.Quantity);

            users.Push(13.37); // 84

            Assert.AreEqual(55.37, users.Quantity);

            double remaining = users.Push(100); // 123 (max)

            Assert.AreEqual(max, users.Quantity);
        }
    }
}
