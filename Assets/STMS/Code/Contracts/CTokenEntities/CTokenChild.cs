
using Codice.CM.Common;
using STMS.Tokens.Communication;
using STMS.Tokens.Communication.Implementation;
using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.Id.Contracts;
using STMS.Tokens.Id.Interfaces;
using STMS.Tokens.TokenEntities.Communication;
using STMS.Tokens.TokenEntities.Communication.Factory;
using STMS.Tokens.TokenEntities.Interfaces;
using System;

namespace STMS.Tokens.TokenEntities.Contracts
{
    // argueably blurring the CAB-boundries here, but its in the name of safety.
    public abstract class CTokenChild : ITokenChild
    {

        protected ChildMessage parentListener {get; private set;}
        protected bool hasToken {get; private set;}

        public ITokenId ThisId {get; private set;}



        public int CompareTo(ITokenId _other)
        {
            throw new NotImplementedException();
        }

        public CTokenChild(ITokenId _id, ChildMessage _parentMessanger)
        {
            parentListener = _parentMessanger;
            ThisId = _id;
            hasToken = false;
            parentListener += _ParentCallResponse;
        }

        protected abstract ITokenMessage _ParentCallResponse(ITokenCommunication _message);

        //TODO Work in tokencommunication
        protected virtual ITokenMessage _OnDelete(ITokenCommunication _message)
        {
            parentListener -= _ParentCallResponse;
            return ITokenCommunicationsFactory<object>.GetITokenCommunication(ETokenCommands.DeleteHolder, true, _sender: ThisId) as ITokenMessage;
        }

        protected void _SetToken(bool _value)
        {
            hasToken = _value;
        }
    }   
}