using STMS.Tokens.Communication.Interfaces;

namespace STMS.Tokens.TokenEntities.Communication
{
    public delegate ITokenMessage? ChildMessage(ITokenCommunication _message);
}