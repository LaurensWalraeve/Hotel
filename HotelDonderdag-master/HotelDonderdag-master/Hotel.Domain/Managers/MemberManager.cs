using Hotel.Domain.Exceptions;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Domain.Managers
{
    public class MemberManager
    {
        private IMemberRepository _memberRepository;

        public MemberManager(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
        }

        public IReadOnlyList<Member> GetMembers(string filter)
        {
            try
            {
                return _memberRepository.GetMembers(filter);
            }
            catch (Exception ex)
            {
                throw new MemberManagerException("GetMembers");
            }
        }

        public void AddMember(Member member)
        {
            try
            {
                _memberRepository.AddMember(member);
            }
            catch (Exception ex)
            {
                throw new MemberManagerException("AddMember");
            }
        }

        public void DeleteMember(Member member)
        {
            try
            {
                _memberRepository.DeleteMember(member);
            }
            catch (Exception ex)
            {
                throw new MemberManagerException("DeleteMembers");
            }
        }

        public void UpdateMember(Member member, Member newMember)
        {
            try
            {
                _memberRepository.UpdateMember(member, newMember);
            }
            catch (Exception)
            {

                throw new MemberManagerException("UpdateMembers");
            }
        }
    }
}
