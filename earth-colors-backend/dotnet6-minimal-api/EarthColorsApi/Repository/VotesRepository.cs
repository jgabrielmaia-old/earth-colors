using EarthColorsApi.Models;

public class VotesRepository 
{
    private readonly Dictionary<Guid, Vote> _votes = new();

    public Vote GetById(Guid id) => _votes[id];

    public List<Vote> GetAll() => _votes.Values.ToList();

    public void Delete(Guid id) => _votes.Remove(id);

    public void Create(Vote vote)
    {
        if(vote is null)
        {
            return;
        }

        _votes[vote.Id] = vote;
    }
    public void Update(Vote Vote)
    {
        var existingVote = GetById(Vote.Id);
        if(existingVote is null)
        {
            return;
        }

        _votes[Vote.Id] = Vote;
    }
}