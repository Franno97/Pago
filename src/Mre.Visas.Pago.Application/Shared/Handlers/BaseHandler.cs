using AutoMapper;
using Mre.Visas.Pago.Application.Shared.Interfaces;

namespace Mre.Visas.Pago.Application.Shared.Handlers
{
    public abstract class BaseHandler
    {
        #region Constructors

        protected BaseHandler()
        {
        }

        protected BaseHandler(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        protected BaseHandler(IMapper mapper)
        {
            Mapper = mapper;
        }

        protected BaseHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            Mapper = mapper;
            UnitOfWork = unitOfWork;
        }

        #endregion Constructors

        #region Properties

        protected IMapper Mapper;

        protected IUnitOfWork UnitOfWork;

        #endregion Properties
    }
}