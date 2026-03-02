using STMS.Tokens.Id.Interfaces;

namespace STMS.Tokens.Id.Utilty
{
    public static class ITokenIdHelper
    {
        public static TokenIdMatchStatus GetMatchStatus(this ITokenId _lhs, ITokenId _rhs)
        {
            if (_lhs.GUID == _rhs.GUID)
            {
                if(_lhs.Name == _rhs.Name)
                    return TokenIdMatchStatus.Full;
                else
                    return TokenIdMatchStatus.GroupIdOnly;
            }
            return TokenIdMatchStatus.None;
        }
    }
}