using System;
using STMS.Tokens.Id.Interfaces;

namespace STMS.Tokens.Id.Implentation
{
    public class TokenId : ITokenId
    {
        private static readonly string NEXUS_ADRESS = "-1";
        public string GUID { get; private set; }
        public string Name { get; private set; }

        public static TokenId ROOT => new TokenId(NEXUS_ADRESS, NEXUS_ADRESS);

        protected string FullId;

        public TokenId(string _guid, string _name)
        {
            GUID = _guid;
            Name = _name;
            FullId = $"{GUID}-{Name}";
        }

        public int CompareTo(ITokenId other)
        {
            if (other is TokenId _cid)
                return FullId.CompareTo(_cid.FullId);
            else throw new ArgumentException();
        }

        /// <summary>
        /// Creates a new tokenId with a target of [target, Nexus] (nexus being the root)
        /// </summary>
        /// <returns></returns>
        public static TokenId TargetNexus(string _target)
        {
            return new TokenId(_target, NEXUS_ADRESS);
        }
        /// <summary>
        /// Creates a new tokenId with a target of [Name, Nexus] (nexus being the root)
        /// </summary>
        /// <returns></returns>
        public ITokenId TargetAsNexus()
        {
            return new TokenId(Name, NEXUS_ADRESS);
        }
        /// <summary>
        /// Creates a new tokenId with a target of [GUID, Nexus] (nexus being the root)
        /// </summary>
        /// <returns></returns>
        public ITokenId TargetGroup()
        {
            return new TokenId(GUID, NEXUS_ADRESS);
        }
    }
}