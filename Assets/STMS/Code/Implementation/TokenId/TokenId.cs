using System;
using STMS.Tokens.Id.Interfaces;

namespace STMS.Tokens.Id.Implentation
{
    public class TokenId : ITokenId
    {
        protected string FullId;
        public string GUID { get; private set; }

        public string Name { get; private set; }
        public TokenId(string _guid, string _name)
        {
            GUID = _guid;
            Name = _name;
            FullId = $"{GUID}-{Name}";
        }

        public int CompareTo(ITokenId other)
        {
            if(other is TokenId _cid)
                return FullId.CompareTo(_cid.FullId);
            else throw new ArgumentException();
        }
    }
}