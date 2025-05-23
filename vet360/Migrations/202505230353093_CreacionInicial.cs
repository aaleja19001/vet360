namespace vet360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreacionInicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cita",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HorarioId = c.Int(nullable: false),
                        Estado = c.String(),
                        UsuarioId = c.Int(nullable: false),
                        MascotaId = c.Int(nullable: false),
                        ServicioId = c.Int(nullable: false),
                        VeterinarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Mascota", t => t.MascotaId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .ForeignKey("dbo.Horario", t => t.HorarioId)
                .ForeignKey("dbo.Servicio", t => t.ServicioId)
                .Index(t => t.HorarioId)
                .Index(t => t.UsuarioId)
                .Index(t => t.MascotaId)
                .Index(t => t.ServicioId);
            
            CreateTable(
                "dbo.HistorialesMedicos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MascotaId = c.Int(nullable: false),
                        Diagnostico = c.String(nullable: false, maxLength: 2000),
                        Tratamiento = c.String(maxLength: 2000),
                        FechaRegistro = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UsuarioId = c.Int(nullable: false),
                        CitaId = c.Int(),
                        Mascota_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cita", t => t.CitaId)
                .ForeignKey("dbo.Mascota", t => t.Mascota_Id)
                .ForeignKey("dbo.Mascota", t => t.MascotaId)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.MascotaId)
                .Index(t => t.UsuarioId)
                .Index(t => t.CitaId)
                .Index(t => t.Mascota_Id);
            
            CreateTable(
                "dbo.Mascota",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Especie = c.String(),
                        Raza = c.String(),
                        Edad = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Correo = c.String(),
                        Contraseña = c.String(),
                        RolId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RolId)
                .Index(t => t.RolId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RolId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false, maxLength: 50),
                        Descripcion = c.String(maxLength: 200),
                        Activo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RolId);
            
            CreateTable(
                "dbo.Horario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioId = c.Int(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        HoraInicio = c.Time(nullable: false, precision: 7),
                        HoraFin = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Servicio",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NombreServicioVet = c.String(),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cita", "ServicioId", "dbo.Servicio");
            DropForeignKey("dbo.Cita", "HorarioId", "dbo.Horario");
            DropForeignKey("dbo.Horario", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.HistorialesMedicos", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.HistorialesMedicos", "MascotaId", "dbo.Mascota");
            DropForeignKey("dbo.Mascota", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Usuario", "RolId", "dbo.Roles");
            DropForeignKey("dbo.Cita", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.HistorialesMedicos", "Mascota_Id", "dbo.Mascota");
            DropForeignKey("dbo.Cita", "MascotaId", "dbo.Mascota");
            DropForeignKey("dbo.HistorialesMedicos", "CitaId", "dbo.Cita");
            DropIndex("dbo.Horario", new[] { "UsuarioId" });
            DropIndex("dbo.Usuario", new[] { "RolId" });
            DropIndex("dbo.Mascota", new[] { "UsuarioId" });
            DropIndex("dbo.HistorialesMedicos", new[] { "Mascota_Id" });
            DropIndex("dbo.HistorialesMedicos", new[] { "CitaId" });
            DropIndex("dbo.HistorialesMedicos", new[] { "UsuarioId" });
            DropIndex("dbo.HistorialesMedicos", new[] { "MascotaId" });
            DropIndex("dbo.Cita", new[] { "ServicioId" });
            DropIndex("dbo.Cita", new[] { "MascotaId" });
            DropIndex("dbo.Cita", new[] { "UsuarioId" });
            DropIndex("dbo.Cita", new[] { "HorarioId" });
            DropTable("dbo.Servicio");
            DropTable("dbo.Horario");
            DropTable("dbo.Roles");
            DropTable("dbo.Usuario");
            DropTable("dbo.Mascota");
            DropTable("dbo.HistorialesMedicos");
            DropTable("dbo.Cita");
        }
    }
}
