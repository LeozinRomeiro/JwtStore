using JwtStore.Core.AccountContext.ValueObjects;
using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;

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

            #region 2. Gerar os objetos
            Email email;
            Password password;
            User user;
            try
            {
                email = new Email(request.Email);
                password = new Password(request.Password);
                user = new User(request.Name, email, password);
            }
            catch (Exception e)
            {
                return new Response(e.Message, 400);
            }
            #endregion

            #region 3. Verifica se o user existe no banco
            try
            {
                var exists = await _repository.AnyAsync(request.Email, cancellation);
                if (exists)
                    return new Response("Este Email já está em uso", 400);
            }
            catch
            {
                return new Response("Falha ao verificar email cadastrado", 500);
            }
            #endregion

            #region 4. Persiste dados
            try
            {
                await _repository.SaveAsync(user, cancellation);
            }
            catch
            {
                return new Response("Falha ao persistir dados",500);
            }
            #endregion

            #region 5. Envia Email de ativação
            try
            {
                await _service.SendVerificationEmailAsync(user, cancellation);
            }
            catch
            {
                throw;
            }
            #endregion

            return new Response("Criado com sucesso", new ResponseData(user.Id, user.Name, user.Email));
        }
    }
}
