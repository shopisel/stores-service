using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StoresService.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedPhysicalStoreBranches : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                INSERT INTO stores (id, name, brand, address, city, latitude, longitude)
                VALUES
                    ('store-1', 'Continente Online', 'Continente', NULL, NULL, NULL, NULL),
                    ('store-2', 'Pingo Doce Online', 'Pingo Doce', NULL, NULL, NULL, NULL),
                    ('store-continente-colombo', 'Continente Colombo', 'Continente', 'Centro Colombo, Av. Lusíada', 'Lisboa', 38.7562, -9.1898),
                    ('store-continente-vascogama', 'Continente Vasco da Gama', 'Continente', 'Centro Vasco da Gama', 'Lisboa', 38.7685, -9.0961),
                    ('store-continente-matosinhos', 'Continente Matosinhos', 'Continente', 'Rua Brito Capelo', 'Matosinhos', 41.1802, -8.6894),
                    ('store-pingodoce-expo', 'Pingo Doce Expo', 'Pingo Doce', 'Av. Dom João II', 'Lisboa', 38.7697, -9.0997),
                    ('store-pingodoce-alvalade', 'Pingo Doce Alvalade', 'Pingo Doce', 'Av. da Igreja', 'Lisboa', 38.7536, -9.1453),
                    ('store-pingodoce-boavista', 'Pingo Doce Boavista', 'Pingo Doce', 'Praça de Mouzinho de Albuquerque', 'Porto', 41.1579, -8.6291)
                ON CONFLICT (id) DO UPDATE
                SET name = EXCLUDED.name,
                    brand = EXCLUDED.brand,
                    address = EXCLUDED.address,
                    city = EXCLUDED.city,
                    latitude = EXCLUDED.latitude,
                    longitude = EXCLUDED.longitude;
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                DELETE FROM stores
                WHERE id IN (
                    'store-continente-colombo',
                    'store-continente-vascogama',
                    'store-continente-matosinhos',
                    'store-pingodoce-expo',
                    'store-pingodoce-alvalade',
                    'store-pingodoce-boavista'
                );

                UPDATE stores
                SET name = CASE id
                    WHEN 'store-1' THEN 'Continente'
                    WHEN 'store-2' THEN 'Pingo Doce'
                    ELSE name
                END,
                    brand = NULL,
                    address = NULL,
                    city = NULL,
                    latitude = NULL,
                    longitude = NULL
                WHERE id IN ('store-1', 'store-2');
                """);
        }
    }
}
