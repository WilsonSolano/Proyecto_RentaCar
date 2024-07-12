using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SistemaEntidades
{
    public partial class CUSERSALIENDESKTOPPROYECTOSPROYECTO_RENTACARSISTEMAENTIDADESRENTACARMDFContext : DbContext
    {
        public CUSERSALIENDESKTOPPROYECTOSPROYECTO_RENTACARSISTEMAENTIDADESRENTACARMDFContext()
        {
        }

        public CUSERSALIENDESKTOPPROYECTOSPROYECTO_RENTACARSISTEMAENTIDADESRENTACARMDFContext(DbContextOptions<CUSERSALIENDESKTOPPROYECTOSPROYECTO_RENTACARSISTEMAENTIDADESRENTACARMDFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Configuracion> Configuracions { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Factura> Facturas { get; set; } = null!;
        public virtual DbSet<Menu> Menus { get; set; } = null!;
        public virtual DbSet<Multa> Multas { get; set; } = null!;
        public virtual DbSet<Renta> Rentas { get; set; } = null!;
        public virtual DbSet<Rol> Rols { get; set; } = null!;
        public virtual DbSet<RolMenu> RolMenus { get; set; } = null!;
        public virtual DbSet<Vehiculo> Vehiculos { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Clientes__677F38F5C64ADC61");

                entity.HasIndex(e => e.Email, "UQ__Clientes__AB6E6164BB467012")
                    .IsUnique();

                entity.HasIndex(e => e.Dui, "UQ__Clientes__C03671B91A947078")
                    .IsUnique();

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Direccion)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("direccion");

                entity.Property(e => e.Dui)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DUI");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("telefono");

                entity.Property(e => e.TipoLicencia)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tipo_licencia");
            });

            modelBuilder.Entity<Configuracion>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Configuracion");

                entity.Property(e => e.Propiedad)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("propiedad");

                entity.Property(e => e.Recurso)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("recurso");

                entity.Property(e => e.Valor)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("valor");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PK__tmp_ms_x__88B51394B1AADF35");

                entity.HasIndex(e => e.Usuario, "UQ__tmp_ms_x__9AFF8FC6BBF14E4C")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__tmp_ms_x__AB6E6164FFFA4357")
                    .IsUnique();

                entity.HasIndex(e => e.Dui, "UQ__tmp_ms_x__C03671B9900B818D")
                    .IsUnique();

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.Property(e => e.Apellido)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("apellido");

                entity.Property(e => e.Contrasena)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("contrasena");

                entity.Property(e => e.Dui)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("DUI");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.EsActivo)
                    .IsRequired()
                    .HasColumnName("es_activo")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.IdPuesto).HasColumnName("id_puesto");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nombre");

                entity.Property(e => e.SueldoBase)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("sueldo_base");

                entity.Property(e => e.UrlImagen)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("url_imagen")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Usuario)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("usuario");

                entity.HasOne(d => d.IdPuestoNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdPuesto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Empleados__id_pu__0504B816");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdFactura)
                    .HasName("PK__Facturas__6C08ED5386856619");

                entity.Property(e => e.IdFactura).HasColumnName("id_factura");

                entity.Property(e => e.FechaEmision)
                    .HasColumnType("date")
                    .HasColumnName("fecha_emision");

                entity.Property(e => e.IdRenta).HasColumnName("id_renta");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("monto");

                entity.HasOne(d => d.IdRentaNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdRenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Facturas__id_ren__2C538F61");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.HasKey(e => e.IdMenu)
                    .HasName("PK__Menu__C26AF48302668A3E");

                entity.ToTable("Menu");

                entity.Property(e => e.IdMenu).HasColumnName("idMenu");

                entity.Property(e => e.Controlador)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("controlador");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Icono)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("icono");

                entity.Property(e => e.IdMenuPadre).HasColumnName("idMenuPadre");

                entity.Property(e => e.PaginaAccion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("paginaAccion");

                entity.HasOne(d => d.IdMenuPadreNavigation)
                    .WithMany(p => p.InverseIdMenuPadreNavigation)
                    .HasForeignKey(d => d.IdMenuPadre)
                    .HasConstraintName("FK__Menu__idMenuPadr__4DB4832C");
            });

            modelBuilder.Entity<Multa>(entity =>
            {
                entity.HasKey(e => e.IdMulta)
                    .HasName("PK__Multas__295650BB734D8588");

                entity.Property(e => e.IdMulta).HasColumnName("id_multa");

                entity.Property(e => e.Categoria)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("categoria");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.IdRenta).HasColumnName("id_renta");

                entity.Property(e => e.Monto)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("monto");

                entity.HasOne(d => d.IdRentaNavigation)
                    .WithMany(p => p.Multa)
                    .HasForeignKey(d => d.IdRenta)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Multas__id_renta__2F2FFC0C");
            });

            modelBuilder.Entity<Renta>(entity =>
            {
                entity.HasKey(e => e.IdRenta)
                    .HasName("PK__Rentas__43A508DD9D0D709A");

                entity.Property(e => e.IdRenta).HasColumnName("id_renta");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("date")
                    .HasColumnName("fecha_fin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("date")
                    .HasColumnName("fecha_inicio");

                entity.Property(e => e.IdCliente).HasColumnName("id_cliente");

                entity.Property(e => e.IdEmpleado).HasColumnName("id_empleado");

                entity.Property(e => e.IdVehiculo).HasColumnName("id_vehiculo");

                entity.Property(e => e.Total)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Renta)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rentas__id_clien__278EDA44");

                entity.HasOne(d => d.IdEmpleadoNavigation)
                    .WithMany(p => p.Renta)
                    .HasForeignKey(d => d.IdEmpleado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rentas__id_emple__77AABCF8");

                entity.HasOne(d => d.IdVehiculoNavigation)
                    .WithMany(p => p.Renta)
                    .HasForeignKey(d => d.IdVehiculo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Rentas__id_vehic__2882FE7D");
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.HasKey(e => e.IdRol)
                    .HasName("PK__Rol__3C872F7675F0BDC6");

                entity.ToTable("Rol");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<RolMenu>(entity =>
            {
                entity.HasKey(e => e.IdRolMenu)
                    .HasName("PK__Rol_menu__CD2045D83E7FB910");

                entity.ToTable("Rol_menu");

                entity.Property(e => e.IdRolMenu).HasColumnName("idRolMenu");

                entity.Property(e => e.EsActivo).HasColumnName("esActivo");

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaRegistro")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdMenu).HasColumnName("idMenu");

                entity.Property(e => e.IdRol).HasColumnName("idRol");

                entity.HasOne(d => d.IdMenuNavigation)
                    .WithMany(p => p.RolMenus)
                    .HasForeignKey(d => d.IdMenu)
                    .HasConstraintName("FK__Rol_menu__idMenu__5B0E7E4A");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.RolMenus)
                    .HasForeignKey(d => d.IdRol)
                    .HasConstraintName("FK__Rol_menu__idRol__5A1A5A11");
            });

            modelBuilder.Entity<Vehiculo>(entity =>
            {
                entity.HasKey(e => e.IdVehiculo)
                    .HasName("PK__Vehiculo__F5DC0F396E0E2AFB");

                entity.Property(e => e.IdVehiculo).HasColumnName("id_vehiculo");

                entity.Property(e => e.Anio).HasColumnName("anio");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(500)
                    .IsUnicode(false)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Disponible)
                    .HasColumnName("disponible")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Marca)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("marca");

                entity.Property(e => e.Modelo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("modelo");

                entity.Property(e => e.Placas)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("placas");

                entity.Property(e => e.PrecioRenta)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("precio_renta");

                entity.Property(e => e.UrlImagen)
                    .HasMaxLength(400)
                    .IsUnicode(false)
                    .HasColumnName("urlImagen");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
