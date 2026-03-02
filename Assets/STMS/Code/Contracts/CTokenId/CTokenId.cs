
using System;
using STMS.Tokens.Id.Interfaces;

namespace STMS.Tokens.Id.Contracts
{

    public abstract class DepricatedCTokenId : ITokenId
    {
        protected string FullId;
        public string GUID { get; private set; }

        public string Name { get; private set; }
        //Not a C-Type violation, this acts as if declared properties were declared as Init
        public DepricatedCTokenId(string _guid, string _name)
        {
            GUID = _guid;
            Name = _name;
            FullId = $"{GUID}-{Name}";
        }

        public static bool operator ==(DepricatedCTokenId _left, DepricatedCTokenId _right)
        {
            throw new NotImplementedException("If you're seeing this it means you didnt overload CTokenId == operator");
        }
        public static bool operator !=(DepricatedCTokenId _left, DepricatedCTokenId _right)
        {
            throw new NotImplementedException("If you're seeing this it means you didnt overload CTokenId != operator");
        }
        public override bool Equals(object obj)
        {
            throw new NotImplementedException("If you're seeing this it means you didnt overload CTokenId Equals function");
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public abstract int CompareTo(ITokenId other);

    }
}