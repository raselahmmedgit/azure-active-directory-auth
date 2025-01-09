using lab.azure_active_directory_auth.Models;

namespace lab.azure_active_directory_auth.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        public Member Member { get; set; }
        public List<Member> Members { get; set; }

        public MemberRepository()
        {
            Member = new Member();
            Members = new List<Member>();
        }

        public Member GetMemberAsync(int id)
        {
            return Members.FirstOrDefault(x => x.MemberId == id);
        }

        public IEnumerable<Member> GetMembersAsync()
        {
            return Members.ToList();
        }

        public bool InsertOrUpdateMemberAsync(Member model)
        {
            if (model.MemberId == 0)
            {
                Members.Add(model);
            }
            else
            {
                var indexMember = Members.FindIndex(r => r.MemberId == model.MemberId);
                if (indexMember != -1)
                {
                    Members[indexMember] = model;
                }
            }
            return true;
        }

        public bool InsertMemberAsync(Member model)
        {
            try
            {
                if (model.MemberId == 0)
                {
                    Members.Add(model);
                }
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateMemberAsync(Member model)
        {
            if (model.MemberId > 0)
            {
                var indexMember = Members.FindIndex(r => r.MemberId == model.MemberId);
                if (indexMember != -1)
                {
                    Members[indexMember] = model;
                }
            }
            return true;
        }

        public bool DeleteMemberAsync(Member model)
        {
            if (model.MemberId > 0)
            {
                Members.Remove(model);
            }
            return true;
        }

        public bool InsertMemberAsync(List<Member> modelList)
        {
            try
            {
                // Create an instance and save the entity to the database

                Members.AddRange(modelList);

                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

    public interface IMemberRepository
    {
        Member GetMemberAsync(int id);
        IEnumerable<Member> GetMembersAsync();
        bool InsertOrUpdateMemberAsync(Member model);
        bool InsertMemberAsync(Member model);
        bool UpdateMemberAsync(Member model);
        bool DeleteMemberAsync(Member model);
        bool InsertMemberAsync(List<Member> modelList);
    }
}
