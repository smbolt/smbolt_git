using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.DocGen.DocSpec
{
    public class DocumentElementSet : Dictionary<string, DocumentElement>
    {
        public Doc Doc { get; set; }
        public Query Query { get; set; }

        public DocumentElementSet() { }

        public DocumentElementSet(Doc doc, string query)
        {
            this.Doc = doc;
            this.Query = new Query(query);
            
            this.LoadAllElementsOfType(this.Query);
        }


        public void LoadAllElementsOfType(Query query)
        {
            List<DocumentElement> list = GetDocumentElements(this.Doc, null, query);

            foreach (DocumentElement de in list)
            {
                if (this.ContainsKey(de.Name))
                    throw new Exception("A duplicate name exists in the collection of DocumentElement objects in this document.  All names for document elements in this document must be unique.");

                this.Add(de.Name, de);
            }
        }

        public List<DocumentElement> GetDocumentElements(DocumentElement e, List<DocumentElement> list, Query q)
        {
            if (list == null)
                list = new List<DocumentElement>();

            if (this.IncludeThisType(e, q))
                list.Add(e);

            if (e.Properties != null)
            {
                foreach (DocumentElement pe in e.Properties.ChildElements)
                {
                    if (this.IncludeThisType(pe, q))
                        list.Add(pe);
                }
            }

            foreach (DocumentElement de in e.ChildElements)
            {
                GetDocumentElements(de, list, q);
            }

            return list;
        }

        public bool IncludeThisType(DocumentElement e, Query q)
        {
            Type t = e.GetType();

            if (q.TypeSpecs.Count == 0)
                return true;

            foreach (TypeSpec ts in q.TypeSpecs)
            {
                if (t == ts.Type)
                    return true;
            }

            return false;
        }
    }
}
