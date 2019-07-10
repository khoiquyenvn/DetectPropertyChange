using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;

namespace ConsoleApp1
{
    static class Extentions
    {
        public static List<Variance> DetailedCompare<T>(T val1, T val2)
        {
            List<Variance> variances = new List<Variance>();
            CompareField(val1, val2, variances);
            ComparePros(val1, val2, variances);

            return variances;
        }

        private static void CompareField<T>(T val1, T val2, List<Variance> variances)
        {
            FieldInfo[] fi = val1.GetType().GetFields();
            foreach (FieldInfo f in fi)
            {
                Variance v = new Variance();
                v.Prop = f.Name;
                v.valA = f.GetValue(val1);
                v.valB = f.GetValue(val2);
                if (!v.valA.Equals(v.valB))
                    variances.Add(v);
            }
        }

        private static void ComparePros<T>(T val1, T val2, List<Variance> variances)
        {
            PropertyInfo[] pi = val1.GetType().GetProperties();
            foreach (PropertyInfo p in pi)
            {
                Variance v = new Variance();
                v.Prop = p.Name;
                

                if (p.PropertyType.Namespace == "System.Collections.Generic")
                {
                    dynamic lst1 = p.GetValue(val1);
                    dynamic lst2 = p.GetValue(val2);

                    if (lst1 != null && lst2 != null)
                    {
                        var notExistInList2 = ((IEnumerable<dynamic>)lst1).Except((IEnumerable<dynamic>)lst2).ToList();
                        var notExistInList1 = ((IEnumerable<dynamic>)lst2).Except((IEnumerable<dynamic>)lst1).ToList();

                        if (notExistInList1.Any() || notExistInList2.Any())
                        {
                            v.valA = lst1;
                            v.valB = lst2;
                            v.DeleteItems = notExistInList2;
                            v.ModifyItems = notExistInList1;
                            variances.Add(v);
                        }
                    }
                }
                else
                {
                    v.valA = p.GetValue(val1);
                    v.valB = p.GetValue(val2);
                    if (!v.valA.Equals(v.valB))
                        variances.Add(v);
                }
            }
        }
    }
    class Variance
    {
        public string Prop { get; set; }
        public dynamic valA { get; set; }
        public dynamic valB { get; set; }
        public dynamic DeleteItems { get; set; }
        public dynamic ModifyItems { get; set; }
    }
}
