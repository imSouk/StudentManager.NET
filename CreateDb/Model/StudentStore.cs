using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using CreateDb.Model;


namespace CreateDb.Model
{
    public class StudentStore : Student
    {

        public readonly SchoolContext _context;
        public StudentStore(SchoolContext context)
        {
            _context = context;
        }
        public async Task<IdentityResult> CreateAsync(Student user)
        {
            _context.Students.Add(user);
             await _context.SaveChangesAsync();
            return IdentityResult.Success;  
        }

        public async Task<IdentityResult> DeleteAsync(Student user)
        {
            _context.Students.Remove(user); 
            await _context.SaveChangesAsync();
            return IdentityResult.Success;
        }

        public void Dispose() => throw new NotImplementedException();

        public async Task<Student?> FindByIdAsync(int userId)
        {
            return await _context.Students.FindAsync(userId);
        }

        public async Task<Student?> FindByNameAsync(string UserName)
        {
            return await _context.Students.FindAsync(UserName);
        }
        public Task<int> GetUserIdAsync(Student user)
        {
            return Task.FromResult(user.Id);
        }

        public Task<string?> GetUserNameAsync(Student user)
        {
            return Task.FromResult(user.Name);
        }
        public Task SetUserNameAsync(Student user, string? userName)
        {
            user.Name = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(Student user)
        {
            _context.Students.Update(user);
            await _context.SaveChangesAsync();
            return IdentityResult.Success;  
        }
    }
}
