using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ConsoleApplication3
{

    /// <summary>
    /// 
    /// ***************************************************************************
    /// * ECount.Core.JsonSerializer.cs(ECount.Core.Framework.JsonSerializer) Copy
    /// ***************************************************************************
    /// 
    /// This class allows the access of JSON object with dynamic method.
    /// Variable must declares as a dynamic.
    /// </summary>
    /// <example>
    /// <code>
    /// var json = @"{'foo':'json', 'bar':100, 'nest':{ 'foobar':true } }";
    /// 
    /// dynamic _entry = ECJsonSerializer.Decode(json);
    /// 
    /// var r1 = _entry.foo; //=> "json" - dynamic(string)
    /// var r2 = _entry.bar; //=> 100 - dynamic(double)
    /// var r3 = _entry.nest.foobar; //=> true - dynamic(bool)
    /// 
    /// 
    /// // another example here...
    /// var json = "{
    ///    'Date': '21/11/2010',
    ///    'Items':[
    ///         { 'Name': 'Apple', 'Price': 12.3 },
    ///         { 'Name': 'Grape', 'Price': 3.21 }
    ///     ],
    /// }"
    /// 
    /// dynamic data = JsonSerializer.Decode(json);
    /// data.Date; //=> "21/11/2010"
    /// data.Items.Count; //=> 2
    /// data.Items[0].Name; //=> "Apple"
    /// data.Items[1].Name; //=> "Grape"
    /// data.Items[1].Price; //=> 3.21 (as a decimal)
    /// </code>
    /// </example>
    public class JsonSerializer : DynamicObject, ISerializer<object, string>, IDisposable
    {
        private IDictionary<string, object> _dictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="ECJsonSerializer"/> class.
        /// </summary>
        /// <param name="dictionary">The dictionary.</param>
        public JsonSerializer(IDictionary<string, object> dictionary)
        {
            _dictionary = dictionary;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        void IDisposable.Dispose()
        {
            _dictionary = null;
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Provides the implementation for operations that get member values. Classes derived from the <see cref="T:System.Dynamic.DynamicObject"/> class can override this method to specify dynamic behavior for operations such as getting a value for a property.
        /// </summary>
        /// <param name="binder">Provides information about the object that called the dynamic operation. The binder.Name property provides the name of the member on which the dynamic operation is performed. For example, for the Console.WriteLine(sampleObject.SampleProperty) statement, where sampleObject is an instance of the class derived from the <see cref="T:System.Dynamic.DynamicObject"/> class, binder.Name returns "SampleProperty". The binder.IgnoreCase property specifies whether the member name is case-sensitive.</param>
        /// <param name="result">The result of the get operation. For example, if the method is called for a property, you can assign the property value to <paramref name="result"/>.</param>
        /// <returns>
        /// true if the operation is successful; otherwise, false. If this method returns false, the run-time binder of the language determines the behavior. (In most cases, a run-time exception is thrown.)
        /// </returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            // return null to avoid exception. caller can check for null this way...
            result = null;

            if (_dictionary.TryGetValue(binder.Name, out result)) {
                if (result is IDictionary<string, object>) {
                    result = new JsonSerializer(result as IDictionary<string, object>);
                }
                else {
                    var arrayList = result as ArrayList;
                    if (arrayList != null && arrayList.Count > 0) {
                        List<object> arrResult = new List<object>();

                        foreach (var item in arrayList) {
                            var valueAsDic = item as IDictionary<string, object>;
                            if (valueAsDic != null) {
                                arrResult.Add(new JsonSerializer(valueAsDic));
                            }
                            else {
                                arrResult.Add(item);
                            }
                        }
                        result = arrResult;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Gets all member names from the underlying instance.
        /// </summary>
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _dictionary.Keys;
        }


        #region ISerializer

        string ISerializer<object, string>.Serialize(object source)
        {
            return Serialize(source);
        }

        object ISerializer<object, string>.Deserialize(string json, Type objectType)
        {
            return Deserialize(json, objectType);
        }

        dynamic ISerializer<object, string>.Deserialize(string json)
        {
            return Deserialize(json);
        }

        #endregion

        /// <summary>
        /// Converts an object to json string
        /// </summary>
        /// <param name="data">Object to convert json string</param>
        /// <returns>string representation of the object in JSON format</returns>
        public static string Serialize(object data)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            return serializer.Serialize(data);
        }

        /// <summary>
        /// Converts an object to json string
        /// </summary>
        /// <param name="data">Object to convert json string</param>
        /// <returns>string representation of the object in JSON format</returns>
        public static string SerializeNewtonsoft(object data)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }

        /// <summary>
        /// Converts a json string to an object of the specified type
        /// </summary>
        /// <typeparam name="T">Object type to return</typeparam>
        /// <param name="json">The json.</param>
        /// <returns>
        /// The string converted to the specified object type
        /// </returns>
        public static object Deserialize(string json, Type objectType)
        {
            if (json == null) {
                return new object();
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            return serializer.Deserialize(json, objectType);
        }


        /// <summary>
        /// Decodes the specified json string to dynamic object.
        /// </summary>
        /// <param name="json">The json string.</param>
        /// <returns></returns>
        /// <example>
        /// json -&gt; dynamic decoder adopted from Shawn Weisfeld, http://bit.ly/jPqVsQ
        /// <code>
        /// dynamic _entry = ECJsonSerializer.Decode(json);
        /// 
        /// trace(_entry.glossary.GlossDiv.title);
        /// trace(_entry.glossary.GlossDiv.GlossList.GlossEntry.ID);
        /// </code>
        /// </example>
        public static dynamic Deserialize(string json)
        {
            if (json == null) {
                return new object();
            }

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new DynamicJsonConverter() });
            serializer.MaxJsonLength = Int32.MaxValue;
            return serializer.Deserialize(json, typeof(object));
        }


        private sealed class DynamicJsonConverter : JavaScriptConverter
        {
            public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
            {
                return type == typeof(object) ? new JsonSerializer(dictionary) : null;
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override IEnumerable<Type> SupportedTypes
            {
                get { return new ReadOnlyCollection<Type>(new List<Type>(new[] { typeof(object) })); }
            }
        }


    }


    #region Serializer interface
    /// <summary>
    /// Serializer interface
    /// </summary>
    /// <typeparam name="T">Type that the object is serialized to/from</typeparam>
    public interface ISerializer<TObject, TSerialize>
    {
        /// <summary>
        /// Serializes the object
        /// </summary>
        /// <param name="Object">Object to serialize</param>
        /// <returns>The serialized object</returns>
        TSerialize Serialize(TObject source);

        /// <summary>
        /// Deserializes the data
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <param name="Data">Data to deserialize</param>
        /// <returns>The resulting object</returns>
        TObject Deserialize(TSerialize data, Type objectType);

        /// <summary>
        /// Deserializes the data
        /// </summary>
        /// <param name="ObjectType">Object type</param>
        /// <param name="Data">Data to deserialize</param>
        /// <returns>The resulting object</returns>
        dynamic Deserialize(TSerialize data);
    }
    #endregion
}
