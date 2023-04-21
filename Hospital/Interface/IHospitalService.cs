namespace Hospital.Data;

public interface IHospitalService<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<bool> CreateAsync(T item);
    Task<bool> UpdateAsync(int id, T item);
    Task<bool> DeleteAsync(int id);
}