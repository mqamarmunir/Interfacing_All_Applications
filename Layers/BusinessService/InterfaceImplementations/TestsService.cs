using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using AutoMapper;
using BusinessEntities;
using System.Transactions;

namespace BusinessService.InterfaceImplementations
{
    public class TestsService : ITestsService
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public TestsService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public BusinessEntities.TestsEntity GetTestById(int TestID)
        {
            var test = _unitOfWork._TestRepository.GetByID(TestID);
            if (test != null)
            {
                //Mapper.Configuration.CreateMapper<mi_ttests, TestsEntity>();
                //Mapper.Map<
                var testModel = Mapper.Map<mi_ttests, TestsEntity>(test);
                return testModel;
            }
            return null;
        }

        public IEnumerable<BusinessEntities.TestsEntity> GetAllTests()
        {

            var tests = _unitOfWork._TestRepository.GetAll().ToList();
            if (tests.Any())
            {
                dynamic testModel=null;
                try
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<mi_ttests,TestsEntity>());
                    var mapper = config.CreateMapper();
                    testModel = mapper.Map<List<TestsEntity>>(tests).ToList() ;
                }
                catch(Exception ee)
                {
                    throw;
                }
                    return testModel;
            }
            return null;
        }

        public long CreateTest(BusinessEntities.TestsEntity TestEntity)
        {
            using (var scope = new TransactionScope())
            {
                var test = Mapper.Map<TestsEntity, mi_ttests>(TestEntity);
                _unitOfWork._TestRepository.Insert(test);
                _unitOfWork.Save();
                scope.Complete();
                return test.Machine_testid;               
            }
        }

        public bool UpdateTest(int TestID, BusinessEntities.TestsEntity productEntity)
        {
            throw new NotImplementedException();
        }

        public bool DeleteTest(int TestID)
        {
            throw new NotImplementedException();
        }
    }
}
