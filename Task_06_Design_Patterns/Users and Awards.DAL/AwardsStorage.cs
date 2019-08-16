using System.Collections.Generic;
using System.Linq;
using Users_and_Awards.Entities;

namespace Users_and_Awards.DAL
{
    public class AwardsStorage : IAwardRepository
    {
        public AwardsStorage()
        {
            Awards = new List<Award>();
        }

        public AwardsStorage(IEnumerable<Award> awards)
        {
            Awards = awards.ToList();
        }

        private List<Award> Awards { get; set; }

        public bool AddAward(Award award)
        {
            if (!Awards.Exists(a => a.Id == award.Id
                || a.Title == award.Title))
            {
                Awards.Add(award);
                return true;
            }

            return false;
        }

        public ICollection<Award> GetAllAwards() => Awards;

        public Award GetAward(string title) =>
            Awards.FirstOrDefault(a => a.Title == title);

        public bool RemoveAward(string title)
        {
            Award award = GetAward(title);

            if (award == null)
                return false;

            return Awards.Remove(award);
        }
    }
}
