using STMS.Tokens.Id.Implentation;
using STMS.Tokens.Id.Interfaces;
using STMS.Tokens.TokenEntities.Communication;
using STMS.Tokens.TokenEntities.Implementation;
using STMS.Tokens.TokenEntities.Interfaces;

namespace STMS.Tokens.TokenEntities.Factory
{
    public static class TokenFactory
    {
        public static ITokenChild CreateChild(ITokenId _ownerId, ITokenId _targetId, ref ChildMessage _message)
        {
            if(_ownerId == TokenId.ROOT || _targetId.Name == TokenId.NEXUS)
            {
                return new TokenGroup(_targetId.TargetGroup(),ref _message);
            }
            else
            {
                return new TokenHolder(_targetId, ref _message);
            }
        }
    }

}