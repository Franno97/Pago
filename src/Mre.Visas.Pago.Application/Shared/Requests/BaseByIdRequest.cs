namespace Mre.Visas.Pago.Application.Requests
{
    public abstract class BaseByIdRequest
    {
        #region Constructors

        protected BaseByIdRequest()
        {
        }

        protected BaseByIdRequest(string id)
        {
            Id = id;
        }

        #endregion Constructors

        #region Properties

        public string Id { get; set; }

        #endregion Properties
    }
}