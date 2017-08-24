using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessService
{
    public interface ITestsService
    {
        TestsEntity GetTestById(int TestID);
        IEnumerable<TestsEntity> GetAllTests();
        long CreateTest(TestsEntity TestEntity);
        bool UpdateTest(int TestID, TestsEntity productEntity);
        bool DeleteTest(int TestID);
    }
}
