using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tabletop.Models
{
    public partial class TabletopContext : DbContext
    {
        public TabletopContext()
        {
        }

        public TabletopContext(DbContextOptions<TabletopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<ChatRecord> ChatRecord { get; set; }
        public virtual DbSet<Friendlist> Friendlist { get; set; }
        public virtual DbSet<Game> Game { get; set; }
        public virtual DbSet<GameRecord> GameRecord { get; set; }
        public virtual DbSet<GameType> GameType { get; set; }
        public virtual DbSet<Lobby> Lobby { get; set; }
        public virtual DbSet<MoveRecord> MoveRecord { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=Tabletop;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasColumnName("displayName")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasColumnName("userName")
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ChatRecord>(entity =>
            {
                entity.ToTable("Chat Record");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ChatInput)
                    .HasColumnName("chat_Input")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.ChatRecord)
                    .HasForeignKey(d => d.Accountid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKChat Recor531716");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.ChatRecord)
                    .HasForeignKey(d => d.Gameid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKChat Recor7101");
            });

            modelBuilder.Entity<Friendlist>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.FriendId).HasColumnName("friend_id");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Friendlist)
                    .HasForeignKey(d => d.Accountid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKFriendlist786353");
            });

            modelBuilder.Entity<Game>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GameTypeId).HasColumnName("gameTypeId");

                entity.Property(e => e.Players).HasColumnName("players");

                entity.Property(e => e.TurnOrder).HasColumnName("turn_order");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.Accountid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGame489276");

                entity.HasOne(d => d.GameType)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.GameTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGame375217");

                entity.HasOne(d => d.Lobby)
                    .WithMany(p => p.Game)
                    .HasForeignKey(d => d.Lobbyid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGame992017");
            });

            modelBuilder.Entity<GameRecord>(entity =>
            {
                entity.ToTable("Game Record");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GameCount).HasColumnName("game_count");

                entity.Property(e => e.LossCount).HasColumnName("loss_count");

                entity.Property(e => e.WinCount).HasColumnName("win_count");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.GameRecord)
                    .HasForeignKey(d => d.Accountid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKGame Recor225840");
            });

            modelBuilder.Entity<GameType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.GameName)
                    .HasColumnName("game_Name")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Lobby>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Admin).HasColumnName("admin");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.User).HasColumnName("user");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Lobby)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FKLobby868231");
            });

            modelBuilder.Entity<MoveRecord>(entity =>
            {
                entity.ToTable("Move Record");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RecordMove)
                    .HasColumnName("record_move")
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.MoveRecord)
                    .HasForeignKey(d => d.Accountid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMove Recor9786");

                entity.HasOne(d => d.Game)
                    .WithMany(p => p.MoveRecord)
                    .HasForeignKey(d => d.Gameid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FKMove Recor422987");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
