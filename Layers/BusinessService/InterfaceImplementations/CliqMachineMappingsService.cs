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
    public class CliqMachineMappingsService : ICliqMachineMappings
    {
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public CliqMachineMappingsService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public CliqMachineEntity GetById(int ID)
        {
            var test = _unitOfWork.CliqMachineMappings.GetByID(ID);
            if (test != null)
            {
                //Mapper.Configuration.CreateMapper<mi_ttests, TestsEntity>();
                //Mapper.Map<
                var config = new MapperConfiguration(cfg => cfg.CreateMap<mi_ttests, TestsEntity>());
                var mapper = config.CreateMapper();
                var testModel = mapper.Map<CliqMachineEntity>(test);
                //var testModel = Mapper.Map<cliqmachinemapping, CliqMachineEntity>(test);
                return testModel;
            }
            return null;
        }

        public IEnumerable<CliqMachineEntity> GetAll()
        {
            var tests = _unitOfWork._TestRepository.GetAll().ToList();
            if (tests.Any())
            {
                dynamic testModel = null;
                try
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<cliqmachinemapping, CliqMachineEntity>());
                    var mapper = config.CreateMapper();
                    testModel = mapper.Map<List<CliqMachineEntity>>(tests).ToList();
                }
                catch (Exception ee)
                {
                    throw;
                }
                return testModel;
            }
            return null;
        }

        public long Create(CliqMachineEntity Entity)
        {
            using (var scope = new TransactionScope())
            {
                var test = Mapper.Map<CliqMachineEntity, cliqmachinemapping>(Entity);
                _unitOfWork.CliqMachineMappings.Insert(test);
                _unitOfWork.Save();
                scope.Complete();
                return test.id;
            }
        }

        public bool Update(int ID, CliqMachineEntity Entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int ID)
        {
            throw new NotImplementedException();
        }

        Func<cliqmachinemapping, bool> GetMappedSelector(Func<CliqMachineEntity, bool> selector)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CliqMachineEntity, cliqmachinemapping>();
            });

            IMapper mapper = config.CreateMapper();
            try
            {
                //var source = new CliqMachineEntity();
                Func<cliqmachinemapping, bool> dest = mapper.Map<Func<CliqMachineEntity, bool>, Func<cliqmachinemapping, bool>>(selector);

                //  Func<cliqmachinemapping, bool> mappedSelector = cat => selector(dest(cat));
                return dest;
            }
            catch (Exception ee)
            {
                return null;
            }
        }
        public IEnumerable<CliqMachineEntity> GetMany(Func<CliqMachineEntity, bool> whereSource)
        {
            var whereDest = GetMappedSelector(whereSource);
            var tests = _unitOfWork.CliqMachineMappings.GetMany(whereDest);
            if (tests.Any())
            {
                dynamic testModel = null;
                try
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<cliqmachinemapping, CliqMachineEntity>());
                    var mapper = config.CreateMapper();
                    testModel = mapper.Map<List<CliqMachineEntity>>(tests).ToList();
                }
                catch (Exception ee)
                {
                    throw;
                }
                return testModel;
            }
            return null;  
        }
    }
}
