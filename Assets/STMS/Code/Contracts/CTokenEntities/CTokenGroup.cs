using STMS.Tokens.Id.Interfaces;
using STMS.Tokens.TokenEntities.Communication;
using STMS.Tokens.TokenEntities.Implementation;
using STMS.Tokens.TokenEntities.Utilites;

namespace STMS.Tokens.TokenEntities.Contracts
{
    public abstract class CTokenGroup : BTokenChild
    {
        protected TokenChildSet Children;

        protected CTokenGroup(ITokenId _id, ChildMessage _parentMessanger) : base(_id, _parentMessanger) { }

    }
}