using System;
using Xunit;
namespace ApiWorkbench.Test
{
    public class UnitTest0 : IDisposable
    {
        public UnitTest0()
        {
            // Do "global" initialization here; Only called once.
        }

        public void Dispose()
        {
            // Do "global" teardown here; Only called once.
        }
    }

    public class DummyTests : IClassFixture<UnitTest0>
    {
        public DummyTests(UnitTest0 data)
        {
        }
    }
}

