using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.TokenEntities.Contracts;

namespace STMS.Tokens.TokenEntities.Interfaces
{
    public interface ITokenManger
    {
        static ITokenManger TokenManger;
        abstract ITokenRequest<CTokenChild> Requests(ITokenCommunication _request);
    }
}