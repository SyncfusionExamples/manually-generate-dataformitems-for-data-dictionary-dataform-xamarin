using Syncfusion.XForms.DataForm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using Xamarin.Forms;

namespace DataformXamarin
{
    public class DataformBehavior : Behavior<ContentPage>
    {
        SfDataForm dataForm;
        protected override void OnAttachedTo(ContentPage bindable)
        {
            base.OnAttachedTo(bindable);

            dataForm = bindable.FindByName<SfDataForm>("dataForm");
            dataForm.DataObject = new object();
            var dictionary = new Dictionary<string, object>();
            dictionary.Add("ID", 1);
            dictionary.Add("Name", "John");
            dataForm.ItemManager = new DataFormItemManagerExt(dataForm, dictionary);
        }
    }
    public class DataFormItemManagerExt : DataFormItemManager
    {
        Dictionary<string, object> dataFormDictionary;
        public DataFormItemManagerExt(SfDataForm dataForm, Dictionary<string, object> dictionary) : base(dataForm)
        {
            dataFormDictionary = dictionary;
        }
        protected override List<DataFormItemBase> GenerateDataFormItems(PropertyInfoCollection itemProperties, List<DataFormItemBase> dataFormItems)
        {
            var items = new List<DataFormItemBase>();
            foreach (var key in dataFormDictionary.Keys)
            {
                DataFormItem dataFormItem;
                if (key == "ID")
                    dataFormItem = new DataFormNumericItem() { Name = key, Editor = "Numeric", MaximumNumberDecimalDigits = 0 };
                else if (key == "Name")
                    dataFormItem = new DataFormTextItem() { Name = key, Editor = "Text" };
                else
                    dataFormItem = new DataFormTextItem() { Name = key, Editor = "Text" };

                items.Add(dataFormItem);
            }

            return items;
        }
        public override object GetValue(DataFormItem dataFormItem)
        {
            var value = dataFormDictionary[dataFormItem.Name];
            return value;
        }

        public override void SetValue(DataFormItem dataFormItem, object value)
        {
            dataFormDictionary[dataFormItem.Name] = value;
        }
    }
}

    





