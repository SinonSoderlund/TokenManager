using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.Id.Interfaces;
using STMS.Tokens.TokenEntities.Communication;
using STMS.Tokens.TokenEntities.Communication.Factory;
using STMS.Tokens.TokenEntities.Contracts;
using STMS.Tokens.TokenEntities.Utilites;

namespace STMS.Tokens.TokenEntities.Implementation
{
    public class ManagedGroup : BtokenGroup
    {
        public ManagedGroup(ITokenId _id, ChildMessage _parentMessanger) : base(_id, _parentMessanger) { }
        public ManagedGroup(ITokenId _id, ChildMessage _parentMessanger, TokenChildSet _children) : base(_id, _parentMessanger, _children) { }


        protected override ITokenCarrier<CTokenChild> ConvertToManagedGroup(ITokenCommunication _message)
        {
            throw new System.InvalidOperationException("This object is already a managed group"); ;
        }

        protected override ITokenCarrier<CTokenChild> ConvertToStandardGroup(ITokenCommunication _message)
        {
            return ITokenCommunicationsFactory<CTokenChild>.SenderReceiverSwapReturnCarrier(_message, _payload: new TokenGroup(ThisId, parentListener, Children));
        }

    }
}