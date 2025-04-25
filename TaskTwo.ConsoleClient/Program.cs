using Microsoft.EntityFrameworkCore;
using TaskTwo_Waybill;
using TaskTwo_Waybill.Model;

using MsSqlContext context = new MsSqlContext();

List<string> supplierTest = new List<string>
{
    "ООО ТехноСнаб",
    "ЗАО ПромРесурс",
    "ИП Иванов А.А.",
    "ООО АльфаТрейд",
    "ООО ГлобалПоставка"
}; // данные тестовые
List<string> productTest = new List<string>
{
    "Бумага офисная А4",
    "Картридж для принтера",
    "Клавиатура USB",
    "Монитор 24 дюйма",
    "Мышь оптическая"
};

ImportTestData(context, supplierTest, productTest); // импорт данных


GetProduct(context); // вывод
Console.WriteLine();

GetSupplier(context);

Console.WriteLine();
GetInvoice(context);



#region импорт

void ImportTestData(MsSqlContext context, List<string> supplierTest, List<string> productTest)
{
    // почистим  все  в  бд
    context.InvoiceProducts.RemoveRange(context.InvoiceProducts); 
    context.Invoices.RemoveRange(context.Invoices);
    context.ProductPriceHistories.RemoveRange(context.ProductPriceHistories);
    context.Products.RemoveRange(context.Products);
    context.Suppliers.RemoveRange(context.Suppliers);
    context.SaveChanges();

    foreach (var supplier in supplierTest) // добавим компаниии 
    {
        AddSupplier(supplier);
    }


    foreach (var product in productTest) // продукты 
    {
        AddProduct(product);
    }

    foreach (var product in productTest) // цены
    {
        var p = context.Products.Single(x => x.Name == product);
        Random random = new Random();
        AddProductPriceHistory(p, DateTime.Now, random.Next(1, 100)); // случайные цены
    }

    for (int i = 0; i < 3; i++)   // три  накладные как в тз
    {
        var from = context.Suppliers.Single(x => x.Name == supplierTest[i]);
        var to = context.Suppliers.Single(x => x.Name == supplierTest[i + 1]);
        Invoice(from, to, DateTime.Now);
    }

    for (int i = 0; i < 3; i++) // товары в накладных
    {
        Invoice invoice = context.Invoices.Skip(i).First();

        Random random = new Random();
        for (int p = 0; p < random.Next(3, 5); p++) // случайно  от 3 до  5ы
        {
            int productRandom = random.Next(productTest.Count);
            var product = context.ProductPriceHistories.Where(x => x.Product.Name == productTest[productRandom]).ToArray().Last();
            InvoiceProducts(invoice, product, random.Next(1,5));
        }
    }
}

void AddSupplier(string name)
{
    context.Suppliers.Add(new Supplier { Name = name });
    context.SaveChanges();
}

void AddProduct(string name)
{
    context.Products.Add(new Product { Name = name, Unit = "шт" });
    context.SaveChanges();
}

void AddProductPriceHistory(Product product, DateTime dateTime, decimal price)
{
    context.ProductPriceHistories.Add(new ProductPriceHistory { Product = product, DateTime = dateTime, Price = price });
    context.SaveChanges();
}

void Invoice(Supplier from, Supplier to, DateTime date)
{
    context.Invoices.Add(new Invoice { DateTime = date, From = from, To = to, FromId = from.Id, ToId = to.Id });
    context.SaveChanges();
}

void InvoiceProducts(Invoice invoice, ProductPriceHistory product, int count)
{
    context.InvoiceProducts.Add(new InvoiceProducts { Invoice = invoice, Product = product, Count = count });
    context.SaveChanges();
}

#endregion

#region Получить из бд

static void GetProduct(MsSqlContext context)
{
    var actualPrices = context.ProductPriceHistories.Include(p => p.Product)
        .GroupBy(x => x.Id)
        .Select(g => g.OrderByDescending(x => x.DateTime).FirstOrDefault())
        .ToList();

    Console.WriteLine("продуктов в базе данных:");

    int x = 0;
    foreach (var item in actualPrices)
    {
        x++;
        Console.WriteLine($"{x}. {item.Product.Name}, цена: {item.Price}, дата назначения: {item.DateTime.ToString("d")} ");
    }
}


void GetSupplier(MsSqlContext context)
{
    Console.WriteLine("Компаний в базе данных:");

    int x = 0;
    foreach (var item in context.Suppliers)
    {
        x++;
        Console.WriteLine($"{x}. {item.Name} ");
    }
}

void GetInvoice(MsSqlContext context)
{
    Console.WriteLine("Накладные:");

    int x = 0;

    foreach (var item in context.Invoices.Include(x=>x.From))
    {
        x++;
        Console.WriteLine($"{x}. №{item.Id}\nот: {item.From.Name} - кому:{item.To.Name}, дата: {item.DateTime.ToString("d")} ");

        int y = 0;
        foreach (var p in context.InvoiceProducts.Where(x => x.Invoice == item)
            .Include(x => x.Product.Product))
        {
            y++;
            Console.WriteLine("Товары в накладной");
            Console.WriteLine($"{y}. {p.Product.Product.Name}, цена: {p.Product.Price}, кол-во: {p.Count} {p.Product.Product.Unit}");
        }

        Console.WriteLine();
    }
}

#endregion