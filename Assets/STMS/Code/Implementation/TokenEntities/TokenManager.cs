using System;
using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.Id.Implentation;
using STMS.Tokens.TokenEntities.Communication.Factory;
using STMS.Tokens.TokenEntities.CommunicationHandlers;
using STMS.Tokens.TokenEntities.Interfaces;
using STMS.Tokens.TokenEntities.Utilites;

namespace STMS.Tokens.TokenEntities.Implementation
{

    public class TokenManager : ITokenManger
    {
        private CommuncationsPackage _communication;
        private TokenChildSet _children;
        private TokenCommunicationCaseHandler _caseHandler;

        public static TokenManager tokenManager;

        public TokenManager()
        {
            if(tokenManager == null)
            {
                tokenManager = this;
            }
            else return;
            _children = new TokenChildSet(TokenId.ROOT);
            _caseHandler = new TokenCommunicationCaseHandler(_SendMessage, _SendMessage, _SendMessage, _SendMessage, _SendMessage, _OnDeleteRequest, _GroupEmptyIsInvalid, _OnDeleteRequest, _ManageGroup, _UnmanageGroup, _GroupCarrierInvalid);
        }

        public ITokenRequest<ITokenChild> Requests(ITokenCommunication _request)
        {
            _communication = new CommuncationsPackage(_request);
            _caseHandler.HandleCommunication(_communication.Incomming.Command);
            return ITokenCommunicationsFactory<ITokenChild>.CopyRequest(_communication.Outgoing);
        }

        private void _SendMessage()
        {
            _communication.Outgoing = _children.SendMessageDown(_communication.Incomming);
            if (_communication.Outgoing == null)
                throw new InvalidOperationException("requested token does not exists");
        }

        private void _OnDeleteRequest()
        {
            _communication.Outgoing = _children.SendMessageDown(_communication.Incomming);
            if (_communication.Outgoing.Command == Tokens.Communication.ETokenCommands.GroupEmpty)
            {
                _children.DeleteChild(_communication.Outgoing.Sender);
            }
        }

        private void _ManageGroup()
        {
            ITokenCarrier<ITokenChild> _response = _children.SendMessageDown(ITokenCommunicationsFactory<TokenId>.GetManageTarget(_communication.Incomming)) as ITokenCarrier<ITokenChild>;

            _children.SwapInstance(_response);

            _communication.Outgoing = _children.SendMessageDown(ITokenCommunicationsFactory<ITokenChild>.MergeAsCarrierAndTargetGroup(_communication.Incomming, _response));
        }

        private void _UnmanageGroup()
        {
            _communication.Outgoing = _children.SendMessageDown(_communication.Incomming);

            ITokenCarrier<ITokenChild> _response = _communication.Outgoing as ITokenCarrier<ITokenChild>;

            _children.SwapInstance(_response);
        }

        private void _GroupEmptyIsInvalid()
        {
            throw new ArgumentException("Group empty is not a valid downstream-facing message");
        }

        private void _GroupCarrierInvalid()
        {
            throw new ArgumentException("Group carrier is not a valid command to tokenmanager");
        }
    }
}