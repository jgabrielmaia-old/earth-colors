namespace EarthColors.Domain.Interfaces;

public interface IRepository<T>
{
    public void Create(T country);

    public T? GetById(Guid id);

    public IEnumerable<T> GetAll();

    public void Update(T country);

    public void Delete(Guid id);
}