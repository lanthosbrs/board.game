using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using board.game.GameDb.Models;

namespace board.game.GameDb.Context
{
    public class GameDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Game> Games => Set<Game>();

    }
}
