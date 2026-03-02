using STMS.Tokens.Communication.Interfaces;
using STMS.Tokens.Id.Interfaces;

namespace STMS.Tokens.Communication.Implementation
{
    public class TokenRequest<T> : TokenCommunication, ITokenRequest<T> where T :class
    {
        public T Payload { get; private set; }

        public TokenRequest(ITokenRequest<T> _toCopy) : base(_toCopy)
        {
            Payload = _toCopy.Payload;
        }

        public TokenRequest(ITokenId _receipient, ETokenCommands _command, bool _statusResponse, T _payload) : base(_receipient, _command, _statusResponse)
        {
            Payload = _payload;
        }

        public TokenRequest(TokenRequest<T> _base, ITokenId? _receipient = null, ETokenCommands? _command = null, bool? _statusResponse = null, T? _payload = null) : base(_base, _receipient, _command, _statusResponse)
        {
            Payload = _payload != null ? _payload : _base.Payload;
        }
        public TokenRequest(ITokenCommunication _base, ITokenId? _receipient = null, ETokenCommands? _command = null, bool? _statusResponse = null, T? _payload = null) : base(_base, _receipient, _command, _statusResponse)
        {
            Payload = _payload;
        }


    }
}