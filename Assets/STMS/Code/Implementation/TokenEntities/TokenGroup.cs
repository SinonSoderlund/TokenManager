using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.Id.Interfaces;
using STMS.Tokens.TokenEntities.Communication;
using STMS.Tokens.TokenEntities.Communication.Factory;
using STMS.Tokens.TokenEntities.Contracts;
using STMS.Tokens.TokenEntities.Utilites;

namespace STMS.Tokens.TokenEntities.Implementation
{
    public class TokenGroup : BtokenGroup
    {
        public TokenGroup(ITokenId _id,ref ChildMessage _parentMessanger) : base(_id,ref _parentMessanger){}
        public TokenGroup(ITokenId _id,ref ChildMessage _parentMessanger, TokenChildSet _children) : base(_id, ref _parentMessanger, _children) { }



        protected override void ConvertToManagedGroup()
        {
            Communication.Outgoing = ITokenCommunicationsFactory<CTokenChild>.SenderReceiverSwapReturnCarrier(Communication.Incomming, _payload: new ManagedGroup(ThisId,ref parentListener, Children)); 
        }

        protected override void ConvertToStandardGroup()
        {
            throw new System.InvalidOperationException("This object is already a standard group");;
        }





    }
}
