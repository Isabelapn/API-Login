using AutoMapper;
using Login.Data.Entities;
using Login.Data.Repositories;
using Login.Domain.Models.Request;
using Login.Domain.Models.Response;
using Signa.Library.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Login.Business
{
    public class LoginBL
    {
        private readonly IMapper _mapper;
        private readonly LoginRepository _loginRepository;

        public LoginBL(IMapper mapper, LoginRepository loginRepository)
        {
            _mapper = mapper;
            _loginRepository = loginRepository;
        }

        public int InsertDados(LoginRequest loginRequest)
        {
            VerificaSeEmailExiste(loginRequest.Email);

            var loginEntity = _mapper.Map<LoginEntity>(loginRequest);
            var idLogin = _loginRepository.Insert(loginEntity);

            return idLogin;
        }

        private void VerificaSeEmailExiste(string email) //função que não vai retornar nenhum tipo
        {
            var idvar = _loginRepository.GetIdByEmail(email); //variável que está recebendo tudo o que 

            if (idvar != 0)
            {
                throw new SignaRegraNegocioException("Email já cadastrado.");
            }
        }

        public int UpdateDados (LoginUpdateRequest loginUpdateRequest)
        {
            var idUsers = _loginRepository.GetDadosById(loginUpdateRequest.IdUsers);

            if(idUsers == null)
            {
                throw new SignaRegraNegocioException("Nenhum cadastro encontrado");
            }
            var loginEntity = _mapper.Map<LoginEntity>(loginUpdateRequest);

            var updateResponse = _loginRepository.UpdateDados(loginEntity);

            return updateResponse;
        }
        public LoginResponse GetById (int id)
        {
            var loginEntity = _loginRepository.GetDadosById(id);
            var loginResponse = _mapper.Map<LoginResponse>(loginEntity);
            return loginResponse;
        }

        public IEnumerable<LoginResponse> GetAllDados()
        {
            var loginEntities = _loginRepository.GetAllDados();
            var loginResponse = loginEntities.Select(x => _mapper.Map<LoginResponse>(x));
            return loginResponse;
        }

        public int Delete(int id)
        {
            var idUsers = _loginRepository.GetDadosById(id);
            if(idUsers != null)
            {
                return _loginRepository.Delete(id);
            }
            else
            {
                throw new SignaRegraNegocioException("Nenhum cadastro encontrado.");
            }
        }

    }
}
