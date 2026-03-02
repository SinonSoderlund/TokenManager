using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.Id.Interfaces;

namespace STMS.Tokens.Communication.Implementation
{
    public class TokenCarrier<T> : TokenRequest<T>, ITokenCarrier<T> where T: class
    {
        public ITokenId Sender { get; private set; }

        public TokenCarrier(TokenCarrier<T> _toCopy, ITokenId _sender) : base(_toCopy)
        {
            Sender = _sender;
        }

        public TokenCarrier(ITokenId _receipient, ETokenCommands _command, bool _statusResponse, T _payload,  ITokenId _sender) : base(_receipient, _command, _statusResponse, _payload)
        {
            Sender = _sender;
        }

        public TokenCarrier(TokenCarrier<T> _base, ITokenId _receipient = null, ETokenCommands? _command = null, bool? _statusResponse = null, T? _payload = null,  ITokenId? _sender = null) : base(_base, _receipient, _command, _statusResponse, _payload)
        {
            Sender = _sender != null ? _sender : _base.Sender;
        }
        public TokenCarrier(TokenRequest<T> _base, ITokenId? _sender = null) : base(_base)
        {
            Sender = _sender;
        }

    }
}