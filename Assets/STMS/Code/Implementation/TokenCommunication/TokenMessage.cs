using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.Id.Interfaces;

namespace STMS.Tokens.Communication.Implementation
{
    public class TokenMessage : TokenCommunication, ITokenMessage
    {
        public ITokenId Sender { get; private set; }

        public TokenMessage(TokenMessage _toCopy) : base(_toCopy)
        {
            Sender = _toCopy.Sender;
        }

        public TokenMessage(ITokenId _receipient, ETokenCommands _command, bool _statusResponse, ITokenId _sender) : base(_receipient, _command, _statusResponse)
        {
            Sender = _sender;
        }

        public TokenMessage(TokenMessage _base, ITokenId? _receipient = null, ETokenCommands? _command = null, bool? _statusResponse = null, ITokenId? _sender = null) : base(_base, _receipient, _command, _statusResponse)
        {
            Sender = _sender != null ? _sender : _base.Sender;
        }
    }
}