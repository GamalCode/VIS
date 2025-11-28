using System.Data;

namespace DataAccess.Database
{
    public class DatabaseInitializer
    {
        private readonly DatabaseConnection _dbConnection;

        public DatabaseInitializer(DatabaseConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public void Initialize()
        {
            using (var connection = _dbConnection.CreateConnection())
            {
                connection.Open();
                CreateTables(connection);
            }
        }

        private void CreateTables(IDbConnection connection)
        {
            CreateSupplierTable(connection);
            CreateStorageTable(connection);
            CreateCompanyTable(connection);
            CreateProductTable(connection);
            CreateStockTable(connection);
            CreateRequestTable(connection);
            CreateRequestStockTable(connection);
            CreateDefectiveProductTable(connection);
        }

        private void CreateSupplierTable(IDbConnection connection)
        {
            string sql = @"
            CREATE TABLE IF NOT EXISTS Supplier (
                Supplier_ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Phone TEXT,
                Email TEXT,
                Storage_ID INTEGER NOT NULL,
                FOREIGN KEY (Storage_ID) REFERENCES Storage(Storage_ID)
            );
        
            CREATE INDEX IF NOT EXISTS idx_supplier_storage ON Supplier(Storage_ID);";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        private void CreateStorageTable(IDbConnection connection)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS Storage (
                    Storage_ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Storage_Location TEXT NOT NULL,
                    Storage_Capacity INTEGER,
                    Last_Updated TEXT
                );";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        private void CreateCompanyTable(IDbConnection connection)
        {
            string sql = @"
            CREATE TABLE IF NOT EXISTS Company (
                Company_ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Company_Name TEXT NOT NULL,
                Contact_Email TEXT,
                Contact_Phone TEXT,
                Storage_ID INTEGER NOT NULL,
                FOREIGN KEY (Storage_ID) REFERENCES Storage(Storage_ID)
            );
        
            CREATE INDEX IF NOT EXISTS idx_company_storage ON Company(Storage_ID);";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        private void CreateProductTable(IDbConnection connection)
        {
            string sql = @"
            CREATE TABLE IF NOT EXISTS Product (
                Product_ID INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Type TEXT,
                CarModel TEXT,
                Supplier_ID INTEGER NOT NULL,
                Price REAL,
                Storage_ID INTEGER NOT NULL,
                FOREIGN KEY (Supplier_ID) REFERENCES Supplier(Supplier_ID),
                FOREIGN KEY (Storage_ID) REFERENCES Storage(Storage_ID)
            );
        
            CREATE INDEX IF NOT EXISTS idx_product_supplier ON Product(Supplier_ID);
            CREATE INDEX IF NOT EXISTS idx_product_storage ON Product(Storage_ID);";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        private void CreateStockTable(IDbConnection connection)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS Stock (
                    Stock_ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Product_ID INTEGER NOT NULL,
                    Storage_ID INTEGER NOT NULL,
                    Quantity INTEGER,
                    Location_In_Storage TEXT,
                    FOREIGN KEY (Product_ID) REFERENCES Product(Product_ID),
                    FOREIGN KEY (Storage_ID) REFERENCES Storage(Storage_ID)
                );
                
                CREATE INDEX IF NOT EXISTS idx_stock_product ON Stock(Product_ID);
                CREATE INDEX IF NOT EXISTS idx_stock_storage ON Stock(Storage_ID);";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        private void CreateRequestTable(IDbConnection connection)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS Request (
                    Request_ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Company_ID INTEGER NOT NULL,
                    Product_ID INTEGER NOT NULL,
                    Request_Quantity INTEGER,
                    Request_Date TEXT,
                    Status TEXT,
                    FOREIGN KEY (Company_ID) REFERENCES Company(Company_ID),
                    FOREIGN KEY (Product_ID) REFERENCES Product(Product_ID)
                );
                
                CREATE INDEX IF NOT EXISTS idx_request_company ON Request(Company_ID);
                CREATE INDEX IF NOT EXISTS idx_request_status ON Request(Status);";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        private void CreateRequestStockTable(IDbConnection connection)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS RequestStock (
                    Request_Stock_ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Request_ID INTEGER,
                    Stock_ID INTEGER,
                    Allocated_Quantity INTEGER,
                    FOREIGN KEY (Request_ID) REFERENCES Request(Request_ID),
                    FOREIGN KEY (Stock_ID) REFERENCES Stock(Stock_ID)
                );
                
                CREATE INDEX IF NOT EXISTS idx_requeststock_request ON RequestStock(Request_ID);
                CREATE INDEX IF NOT EXISTS idx_requeststock_stock ON RequestStock(Stock_ID);";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }

        private void CreateDefectiveProductTable(IDbConnection connection)
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS DefectiveProduct (
                    Defective_ID INTEGER PRIMARY KEY AUTOINCREMENT,
                    Product_ID INTEGER NOT NULL,
                    Storage_ID INTEGER NOT NULL,
                    Quantity INTEGER,
                    Report_Date TEXT,
                    Reason TEXT,
                    FOREIGN KEY (Product_ID) REFERENCES Product(Product_ID),
                    FOREIGN KEY (Storage_ID) REFERENCES Storage(Storage_ID)
                );
                
                CREATE INDEX IF NOT EXISTS idx_defective_storage ON DefectiveProduct(Storage_ID);
                CREATE INDEX IF NOT EXISTS idx_defective_product ON DefectiveProduct(Product_ID);";

            using (var command = connection.CreateCommand())
            {
                command.CommandText = sql;
                command.ExecuteNonQuery();
            }
        }
    }
}