using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal class DalProduct : IProduct
{
    const string s_products = "Products"; //file name
    // מה לזרוק בחריגה???
    static DO.Product? createProductfromXElement(XElement s)
    {
        return new DO.Product()
        {
            ID = s.ToIntNullable("ID") ?? throw new FormatException("id"), //fix to: DalXmlFormatException(id)),
            Name = (string?)s.Element("Name"),
            Category = s.ToEnumNullable<DO.Category>("Category") ?? 0,
            Amount = s.ToIntNullable("Amount") ?? 0,
            Price = s.ToDoubleNullable("Price") ?? 0, // s.ToDoubleNullable("Grade")
        };
    }
    public int Add(Product item)
    {
        //List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);// get list of products from xml file to local list
        //if (listProducts.Exists(x => x?.ID == item.ID) == true)
        //    throw new DO.DalIDAlreadyExistException(item.ID, "product is already exist");
        //else
        //{
        //    listProducts.Add(item);
        //}
        //XMLTools.SaveListToXMLSerializer(listProducts, s_products);// save to file the new list
        //return item.ID;
        XElement productssRootElem = XMLTools.LoadListFromXMLElement(s_products);

        XElement? prod = (from st in productssRootElem.Elements()
                          where st.ToIntNullable("ID") == item.ID //where (int?)st.Element("ID") == doStudent.ID
                          select st).FirstOrDefault();
        if (prod != null)
            throw new Exception("id already exist"); // fix to: throw new DalMissingIdException(id);

        XElement prodElem = new XElement("Product",
                                   new XElement("ID", item.ID),
                                   new XElement("Name", item.Name),
                                   new XElement("Category", item.Category),
                                   new XElement("Amount", item.Amount),
                                   new XElement("Price", item.Price)
                                   );

        productssRootElem.Add(prodElem);

        XMLTools.SaveListToXMLElement(productssRootElem, s_products);

        return item.ID; ;
    }

    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filter = null)
    {
        XElement? productsRootElem = XMLTools.LoadListFromXMLElement(s_products);// hold the root of many products
        if (filter != null)
        {
            return from s in productsRootElem.Elements()
                   let doProd = createProductfromXElement(s)
                   where filter(doProd)
                   select (DO.Product?)doProd;
        }
        else
        {
            return from s in productsRootElem.Elements()
                   select createProductfromXElement(s);
        }

    }
    public void Delete(int id)
    {
        XElement productsRootElem = XMLTools.LoadListFromXMLElement(s_products);

        XElement? prod = (from st in productsRootElem.Elements()
                          where (int?)st.Element("ID") == id
                          select st).FirstOrDefault() ?? throw new Exception("missing id"); // fix to: throw new DalMissingIdException(id);

        prod.Remove(); //<==>   Remove stud from studentsRootElem

        XMLTools.SaveListToXMLElement(productsRootElem, s_products);
    }

    public Product GetById(int id)
    {
        //List<DO.Product?> listProducts = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);// get list of products from xml file to local list
        //Product ord = listProducts.Find(x => x?.ID == id) ?? throw new DalIDNotExistException(id, "PRODUCT ID NOT FOUND");
        //return ord;
        //XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_products);


        XElement studentsRootElem = XMLTools.LoadListFromXMLElement(s_products);
        return (from s in studentsRootElem?.Elements()
                where s.ToIntNullable("ID") == id
                select (DO.Product?)createProductfromXElement(s)).FirstOrDefault()
                ?? throw new DalIDNotExistException(id, "PRODUCT ID NOT FOUND"); //throw new Exception("missing id");
    }

    public void UpDate(Product item)
    {
        Delete(item.ID);
        Add(item);
    }
}

