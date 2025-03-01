﻿using Data.Entities;

namespace Data.Interfaces;

public interface IUserRepository : IBaseRepository<UsersEntity>
{
    // Kullanıcı kimlik doğrulama için şifre hash’i karşılaştırma
    // User authentication for password hash comparison
    Task<UsersEntity?> AuthenticateAsync(string email);
}
