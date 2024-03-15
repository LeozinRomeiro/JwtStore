using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Core.Contexts.SharedContext.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create
{
    public class Handler
    {
        private readonly IRepository _repository;
        private readonly IService _service;

        public Handler(IRepository repository, IService service)
        {
            _repository = repository;
            _service = service;
        }

        public async Task<Response> Handle(Request request, CancellationToken cancellation)
        {
            #region 1. Validar
            try
            {
                var response = Specification.Ensure(request);
                if (!response.IsValid)
                    return new Response("Requisição invalida", 400, response.Notifications);
            }
            catch 
            {
                return new Response("Requisição invalida", 400);
            }
            #endregion
        }
    }
}
