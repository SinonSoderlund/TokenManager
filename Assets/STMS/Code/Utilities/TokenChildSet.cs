using System.Collections.Generic;
using STMS.Tokens.Communication;
using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.Id.Interfaces;
using STMS.Tokens.TokenEntities.Communication;
using STMS.Tokens.TokenEntities.Communication.Factory;
using STMS.Tokens.TokenEntities.Contracts;
using STMS.Tokens.TokenEntities.Factory;
using STMS.Tokens.TokenEntities.Interfaces;

namespace STMS.Tokens.TokenEntities.Utilites
{

    public class TokenChildSet
    {
        private SortedList<ITokenId,ITokenChild> _childList { get; set;}
        private ChildMessage _callChildren { get; set; }
        private ITokenId _ownerId;

        public TokenChildSet(ITokenId _id)
        {
            _ownerId =_id;
            _childList = new SortedList<ITokenId, ITokenChild>();
        }


        public ITokenMessage SendMessageDown(ITokenCommunication _message)
        {
            ITokenMessage _response = _callChildren?.Invoke(_message);

            if(_response is null)
            {
                _response = _CreateChildAndRecall(_message);
            }
            return _response;
        }

        public ITokenMessage AddChild(ITokenCarrier<ITokenChild> _message)
        {
            _childList.Add(_message.Payload.ThisId, _message.Payload);
            return ITokenCommunicationsFactory<ITokenMessage>.SenderReceiverSwapReturnCarrier(_message);
        }

        public ITokenMessage DeleteChild(ITokenId _target)
        {
            _childList.Remove(_target);
            if(_childList.Count == 0)
                return _CallbackEmpty(true);
            else return _CallbackEmpty(false);
        }

        private ITokenMessage _CreateChildAndRecall(ITokenCommunication _message)
        {
            ITokenChild _newChild = TokenFactory.CreateChild();
            _childList.Add(_newChild.ThisId, _newChild);
            return SendMessageDown(_message);
        }

        private ITokenMessage _CallbackEmpty(bool _status)
        {
            return ITokenCommunicationsFactory<object>.GetITokenCommunication(ETokenCommands.GroupEmpty,_status) as ITokenMessage;
        }
    }
}