using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Interfaces
{
    public interface IMemberRepository
    {
        void AddMember(Member member);
        IReadOnlyList<Member> GetMembers(string filter);
        void UpdateMember(Member member, Member newMember);
        void DeleteMember(Member member);
    }
}
