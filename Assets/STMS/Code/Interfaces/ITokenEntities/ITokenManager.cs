using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.TokenEntities.Contracts;

namespace STMS.Tokens.TokenEntities.Interfaces
{
    public interface ITokenManger
    {
        abstract ITokenRequest<ITokenChild> Requests(ITokenCommunication _request);
    }
}