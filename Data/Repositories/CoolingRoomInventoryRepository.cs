﻿using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class CoolingRoomInventoryRepository(DataContext context) : BaseRepository<CoolingRoomInventoryEntity>(context), ICoolingRoomInventoryRepository
{
}
