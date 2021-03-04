using System.Data.SqlClient;
using System.Data;

namespace AutoLevelHomework
{
    class Program
    {
        static void Main(string[] args)
        {
            var shopDb = new DataSet("ShopDb");

            var orders = new DataTable("Orders");
            var customers = new DataTable("Customers");
            var employees = new DataTable("Employees");
            var orderDetails = new DataTable("OrderDetails");
            var products = new DataTable("Products");

            var connectionString = "Server=WW\\MSSQLSERVER2017; Database=ShopDb; Trusted_Connection=True";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var selectOrders = new SqlCommand("select * from Orders", connection);
                var ordersReader = selectOrders.ExecuteReader();
                orders.Load(ordersReader);
                selectOrders.Dispose();

                var selectCustomers = new SqlCommand("select * from Customers", connection);
                var customersReader = selectCustomers.ExecuteReader();
                customers.Load(customersReader);
                selectCustomers.Dispose();

                var selectEmployees = new SqlCommand("select * from Employees", connection);
                var employeesReader = selectEmployees.ExecuteReader();
                employees.Load(employeesReader);
                selectEmployees.Dispose();

                var selectOrderDetails = new SqlCommand("select * from OrderDetails", connection);
                var orderDetailsReader = selectOrderDetails.ExecuteReader();
                orderDetails.Load(orderDetailsReader);
                selectOrderDetails.Dispose();

                var selectProducts = new SqlCommand("select * from Products", connection);
                var productsReader = selectProducts.ExecuteReader();
                products.Load(productsReader);
                selectProducts.Dispose();

                ordersReader.Close();
                customersReader.Close();
                employeesReader.Close();
                orderDetailsReader.Close();
                productsReader.Close();
            }

            orders.PrimaryKey = new DataColumn[] { orders.Columns["Id"] };
            customers.PrimaryKey = new DataColumn[] { customers.Columns["Id"] };
            employees.PrimaryKey = new DataColumn[] { employees.Columns["Id"] };
            orderDetails.PrimaryKey = new DataColumn[] { orderDetails.Columns["Id"] };
            products.PrimaryKey = new DataColumn[] { products.Columns["Id"] };

            shopDb.Tables.AddRange(new DataTable[] { orders, customers, employees, orderDetails, products });

            var fkOrdersOrderDetails = new ForeignKeyConstraint(orderDetails.Columns["Id"], orders.Columns["OrderDetailsId"]);
            var fkOrdersCostumers = new ForeignKeyConstraint(customers.Columns["Id"], orders.Columns["CustomerId"]);
            var fkOrdersEmployees = new ForeignKeyConstraint(employees.Columns["Id"], orders.Columns["EmplyeeId"]);
            orders.Constraints.Add(fkOrdersOrderDetails);
            orders.Constraints.Add(fkOrdersCostumers);
            orders.Constraints.Add(fkOrdersEmployees);

            var fkOrderDetailsProducts = new ForeignKeyConstraint(orderDetails.Columns["ProductId"], products.Columns["Id"]);
            products.Constraints.Add(fkOrderDetailsProducts);
        }
    }
}
