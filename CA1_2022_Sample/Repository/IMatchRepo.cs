using CA1_2022_Sample.Models;

namespace CA1_2022_Sample_With_Repo.Repository
{
    public interface IMatchRepo
    {
        List<Match> GetFixtures();
        Match GetFixture(int Id);

        void CreateFixture(Match m);
        void EditFixture(Match m);


    }
}
