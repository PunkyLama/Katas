namespace CQRSAndMediatRDemo.Models
{
    public interface IStudentRepository
    {
        public Task<List<StudentDetails>> GetStudentListAsync();
        public Task<StudentDetails> GetStudentByIdAsync(int Id);
        public Task<StudentDetails> AddStudentAsync (StudentDetails student);
        public Task<int> UpdateStudentAsync (StudentDetails student);
        public Task<int> DeleteStudentAsync (int Id);
    }
}
