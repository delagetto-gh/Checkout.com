using System;
using System.Linq;
using System.Collections.Generic;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace Checkout.P2.BasketManagementClient.Tests
{
    public class IntegrationTestOrderer : ITestCaseOrderer
    {
        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
        {
            var orderedTestList = testCases.OrderBy(t => t.TestMethod.Method.GetCustomAttributes(typeof(TestOrderAttribute).AssemblyQualifiedName)
                                                                            .First()
                                                                            .GetNamedArgument<uint>("Order")).ToList();

            return orderedTestList;
        }
    }

    public class TestOrderAttribute : Attribute
    {
        public TestOrderAttribute(uint priorityOrder)
        {
            this.Order = priorityOrder;
        }

        public uint Order { get; private set; }
    }
}